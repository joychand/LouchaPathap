Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Globalization

Partial Public Class frmUserlogin
    Inherits System.Web.UI.Page
    Dim connectPrj As New SqlConnection(ConfigurationManager.ConnectionStrings("connectionString").ToString)
    Protected Shared Ses_Cookie As String
    Protected Shared uid As String
    Protected Shared lastvisit As String

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.UtcNow)
        Response.Expires = -1
        'Response.Write((md5Fun.md5Encr(md5Fun.md5Encr("sdcporompat"))))
        Try
            connectPrj.Open()
            connectPrj.Close()
        Catch ex As Exception
            Response.Redirect("~/users/conerror.htm")
        End Try

        If Not Page.IsPostBack Then

            If Not Request.Cookies("TM") Is Nothing Then
                If isAuthenticated() And Not IsDBNull(Session("uid")) Then
                    If Session("userrole").ToString = "SCY_USER" Or Session("userrole") = "SDC_USER" Then
                        Response.Redirect("~/users/_SCYREV/frmLogTrl.aspx")
                    ElseIf Session("userrole").ToString = "CSC_USER" Then
                        Response.Redirect("~/users/_CSC/frmLogTrl_csc.aspx")
                    End If
                End If
            End If
            ''seed value
            Session.Add("rndNo", 0)
            Session("rndNo") = md5Fun.md5Encr(System.Guid.NewGuid().ToString())
            lSeed.Value = Session("rndNo")

            btnLogIn.Attributes.Add("onclick", "javascript:pwdinhash();")

        End If
        Me.frmUserLogin.Attributes.Add("autocomplete", "off")
    End Sub

    Protected Sub btnLogIn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogIn.Click
        Try

       
            Page.Validate()
            If Page.IsValid Then
                If txtCap.Text = "" Then
                    lberror.Text = "Please Enter the Captcha Characters"
                    Exit Sub
                End If
                ccjoin.ValidateCaptcha(txtCap.Text)
                If (Not ccjoin.UserValidated) Then
                    lberror.Text = "Log In denied! Captcha letters input were wrong."
                    Exit Sub
                End If
                If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
                Dim cmd As New SqlCommand("select userid,password,role,gsalt,work_area,office,cir_code from  tbl_users where rtrim(userid) = @uname ", connectPrj)
                With cmd
                    .Parameters.Add(New SqlParameter("@uname", txtUserID.Text.Trim))
                End With
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then

                    ''seed value
                    lSeed.Value = Session("rndNo")

                    Dim dbpwd As String = dt.Rows(0).Item("password")

                    Call updateNtry()
                    '' check request hash password with profile hash password

                    If md5Fun.md5Encr(dbpwd & lSeed.Value) <> txtpwd.Value Then


                        lberror.Text = "Invalid user id or password."
                        Call updateLastFailVisit()
                        Call LogAdTrails("LogIn", "Fail", "")



                        If fTry() = True Then ''also updateNtry 
                            ''locked out after 3 failed login attemps
                            lberror.Text = "You have been locked for the day because of 3 FAILED login attempts!"
                            txtUserID.Enabled = False
                            txtpwd.Disabled = True
                            btnLogIn.Enabled = False
                            Exit Sub
                        End If

                    Else

                        Call unblock()
                        
                        Call LogAdTrails("LogIn", "Success", "")
                        Session("uid") = dt.Rows(0).Item("userid")
                        Session("userrole") = dt.Rows(0).Item("role")
                        Session("workarea") = dt.Rows(0).Item("work_area")
                        Session("office") = dt.Rows(0).Item("office")
                        Session("cir_code") = dt.Rows(0).Item("cir_code")
                        ''seed value clear
                        Session("rndNo") = ""
                        txtpwd.Value = ""
                        lSeed.Value = ""
                        cmd.Dispose()
                        connectPrj.Close()
                        Dim uname As String = txtUserID.Text.Trim

                        If Session("userrole").ToString = "SCY_USER" Or Session("userrole") = "SDC_USER" Then
                            Session.Abandon()
                            Dim Manager As SessionIDManager = New SessionIDManager
                            Dim NewID As String = Manager.CreateSessionID(Context)
                            Manager.SaveSessionID(Context, NewID, False, True)

                            enter_user_session(NewID, uname)

                            Dim auth_cookie As HttpCookie = New HttpCookie("TM")
                            auth_cookie.Values("uid") = uname
                            auth_cookie.Values("sid") = NewID
                            'auth_cookie.Values("lv") = DateTime.Now.ToString()
                            'auth_cookie.Values("token") = "sdfdfd"
                            Response.Cookies.Set(CookieSecurityProvider2.Encrypt(auth_cookie))
                            Response.Redirect("~/users/_SCYREV/frmLogTrl.aspx")
                            'Response.Redirect("~/users/_SCYREV/SRpage.aspx")
                        End If
                        If Session("userrole").ToString = "CSC_USER" Then
                            Session.Abandon()
                            Dim Manager As SessionIDManager = New SessionIDManager
                            Dim NewID As String = Manager.CreateSessionID(Context)
                            Manager.SaveSessionID(Context, NewID, False, True)
                            enter_user_session(NewID, uname)
                            Dim auth_cookie As HttpCookie = New HttpCookie("TM")
                            auth_cookie.Values("uid") = uname
                            auth_cookie.Values("sid") = NewID
                            'auth_cookie.Values("lv") = DateTime.Now.ToString()
                            Response.Cookies.Set(CookieSecurityProvider2.Encrypt(auth_cookie))
                            Response.Redirect("~/users/_CSC/frmLogTrl_csc.aspx")
                            'Response.Redirect("~/users/_CSC/Jama123.aspx")
                        End If

                    End If
                Else
                    lberror.Text = "Invalid user id or password."
                    Call LogAdTrails("LogIn", "Fail", "")
                End If
                cmd.Dispose()
                connectPrj.Close()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function fTry() As Boolean
        fTry = False
        Dim ldt, cdt As Date
        cdt = Format(Now, "dd/MM/yyyy")

        If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
        Dim cmd As New SqlCommand("select ntry,blocked_date from   blocked_IpUser where  userid= @vuserid", connectPrj)
        cmd.Parameters.Add(New SqlParameter("@vuserid", txtUserID.Text))

        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        If dt.Rows.Count > 0 Then

            ldt = CDate(dt.Rows(0).Item("blocked_date"))

            If DateDiff(DateInterval.Day, ldt, cdt) > 0 Then
                'if date is expired by 1 day unblock it
                Call unblock()
                fTry = False
            Else

                If Val(dt.Rows(0).Item("ntry")) > 2 Then
                    btnLogIn.Enabled = False
                    txtUserID.Enabled = False
                    txtpwd.Disabled = True
                    lberror.Text = "You have been locked out for the day because of 3 FAILED login attempts!"
                    fTry = True
                End If
            End If

        End If
        connectPrj.Close()


    End Function

    Private Sub updateNtry()
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim cmd As New SqlCommand("select count(*) from   blocked_IpUser where  userid= @vuserid", connectPrj)
            cmd.Parameters.Add(New SqlParameter("@vuserid", txtUserID.Text))
            If cmd.ExecuteScalar > 0 Then
                Dim upcmd As New SqlCommand("update blocked_ipuser set ntry=ntry+1, blocked='YES' where  userid= @vuserid7", connectPrj)
                upcmd.Parameters.Add(New SqlParameter("@vuserid7", txtUserID.Text))
                upcmd.ExecuteNonQuery()
            Else
                Dim inscmd As New SqlCommand("insert into blocked_ipuser(ip,userid,blocked,blocked_date,ntry) values(@p1,@p2,@p3,@p4,@p5)", connectPrj)
                With inscmd
                    .Parameters.Add(New SqlParameter("@p1", GetIPAddress()))
                    .Parameters.Add(New SqlParameter("@p2", txtUserID.Text))
                    .Parameters.Add(New SqlParameter("@p3", "NO"))
                    .Parameters.Add(New SqlParameter("@p4", Format(Now, "dd/MM/yyyy")))
                    .Parameters.Add(New SqlParameter("@p5", "1"))
                End With


                inscmd.ExecuteNonQuery()
            End If
            connectPrj.Close()
        Catch ex As Exception
            lberror.Text = "page error!"
            Response.Redirect("~/users/conerror.htm")
        End Try
    End Sub
    Private Sub updateLastFailVisit()
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim cmd As New SqlCommand("select count(*) from   lastOnlineAct where  userId= @vuserid", connectPrj)
            cmd.Parameters.Add(New SqlParameter("@vuserid", txtUserID.Text))
            If cmd.ExecuteScalar > 0 Then
                Dim upcmd As New SqlCommand("update lastOnlineAct set lastFailVisit=@p1,ClientIPFailVisit=@p2 where  userid= @vuserid77", connectPrj)
                With upcmd
                    .Parameters.Add(New SqlParameter("@vuserid77", txtUserID.Text))
                    .Parameters.Add(New SqlParameter("@p1", Format(Now, "dd-MMM-yyyy HH:mm tt")))
                    .Parameters.Add(New SqlParameter("@p2", GetIPAddress()))
                End With

                upcmd.ExecuteNonQuery()
            Else
                Dim inscmd As New SqlCommand("insert into lastOnlineAct(userid,lastFailVisit,ClientIPFailVisit) values(@vuserid77,@p1,@p2)", connectPrj)
                With inscmd
                    .Parameters.Add(New SqlParameter("@vuserid77", txtUserID.Text))
                    .Parameters.Add(New SqlParameter("@p1", Format(Now, "dd-MMM-yyyy HH:mm tt")))
                    .Parameters.Add(New SqlParameter("@p2", GetIPAddress()))
                End With
                inscmd.ExecuteNonQuery()
            End If
            connectPrj.Close()
        Catch ex As Exception
            lberror.Text = "page error!"
            Response.Redirect("~/users/conerror.htm")
        End Try
    End Sub

    Private Sub LogAdTrails(ByVal Ltype As String, ByVal Lstatus As String, ByVal PWhat As String)
       
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim inscmd As New SqlCommand("insert into AdTrails(userid,dateNtime,IPaddress,LoginType,LoginStatus,ProcessWhat,logID)" & _
            " values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", connectPrj)

            With inscmd
                .Parameters.Add(New SqlParameter("@p1", txtUserID.Text))
                .Parameters.Add(New SqlParameter("@p2", Format(Now, "dd-MMM-yyyy HH:mm:ss tt")))
                .Parameters.Add(New SqlParameter("@p3", GetIPAddress()))
                .Parameters.Add(New SqlParameter("@p4", Ltype))
                .Parameters.Add(New SqlParameter("@p5", Lstatus))
                .Parameters.Add(New SqlParameter("@p6", PWhat))
                .Parameters.Add(New SqlParameter("@p7", md5Fun.md5Encr(System.Guid.NewGuid().ToString())))

            End With
            inscmd.ExecuteNonQuery()
            connectPrj.Close()

        Catch ex As Exception

        End Try
        

    End Sub
    Private Sub unblock()
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim delcmd As New SqlCommand("delete from blocked_IpUser where  userid= @vuserid", connectPrj)
            delcmd.Parameters.Add(New SqlParameter("@vuserid", txtUserID.Text))
            delcmd.ExecuteNonQuery()

            connectPrj.Close()
        Catch ex As Exception
            lberror.Text = "page error!"
            Response.Redirect("~/users/conerror.htm")

        End Try

    End Sub
  

    Protected Sub btnPP_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPP.Click
        Response.Redirect("~/intHome.aspx")
    End Sub

    Private Sub enter_user_session(ByVal newid As String, ByVal userid As String)
        'Try
        '    If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
        '    Dim updatecmd As New SqlCommand("insert into tbl_usr_session (userid,session_id,ip) values (@userid,@session_ID,@ip)", connectPrj)

        '    updatecmd.Parameters.Add(New SqlParameter("@userid", userid))
        '    updatecmd.Parameters.Add(New SqlParameter("@session_ID", newid))
        '    updatecmd.Parameters.Add(New SqlParameter("@ip", GetIPAddress()))
        '    updatecmd.ExecuteNonQuery()

        '    connectPrj.Close()
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            'Dim inscmd As New SqlCommand("insert into tbl_usr_session(userid,session_id,ip,user_agent,first_visit) values (@userid,@session_id,@ip,@user_agent,@first_visit)", connectPrj)
            Dim inscmd As New SqlCommand("insert into tbl_usr_session(userid,session_id,user_agent,first_visit) values (@userid,@session_id,@user_agent,getdate())", connectPrj)
            With inscmd
                .Parameters.Add(New SqlParameter("@userid", userid))
                .Parameters.Add(New SqlParameter("@session_id", newid))
                '.Parameters.Add(New SqlParameter("@ip", GetIPAddress()))
                .Parameters.Add(New SqlParameter("@user_agent", Request.UserAgent.ToString))
                '.Parameters.Add(New SqlParameter("@first_visit", DateTime.Now))

            End With
            inscmd.ExecuteNonQuery()
            connectPrj.Close()

        Catch ex As Exception
            lberror.Text = "page error!"
            Response.Redirect("~/users/conerror.htm")

        End Try
    End Sub
    Private Function isAuthenticated() As Boolean
        Try

            If Not Request.Cookies("TM") Is Nothing Then
                Dim authcookie As HttpCookie = CookieSecurityProvider2.Decrypt(Request.Cookies("TM"))
                uid = authcookie.Values("uid")
                Ses_Cookie = authcookie.Values("sid")
                'lastvisit = authcookie.Values("lv")
                If Not Session("token") Is Nothing Then

                    'Dim token As String = md5Fun.md5Encr(authcookie.Values("token") & Request.UserAgent.ToString & GetIPAddress())
                    Dim token As String = md5Fun.md5Encr(authcookie.Values("token") & Request.UserAgent.ToString & uid)
                    If Session("token") = token Then
                        If isInSession() Then
                            token = gen_rnd_token()
                            Session("token") = md5Fun.md5Encr(token & Request.UserAgent.ToString & Session("uid"))
                            Session("last_visit") = DateTime.Parse(DateTime.Now, CultureInfo.CreateSpecificCulture("en-NZ"))
                            update_authcookie(authcookie, token)
                            Return True
                        End If
                        Return False
                    End If
                Else  ' check whether the authenticated session exist for this request at the database
                    Try
                        If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
                        'Dim checkcmd As New SqlCommand("select * from (select * from tbl_usr_session where userid=@userid and session_id=@session_ID and  ip = @ip and user_agent= @user_agent) as a where DateDiff(minute,a.first_visit,GetDate())<=10", connectPrj)
                        Dim checkcmd As New SqlCommand("select * from (select * from tbl_usr_session where userid=@userid and session_id=@session_ID and user_agent= @user_agent) as a where DateDiff(minute,a.first_visit,GetDate())<=10", connectPrj)
                        checkcmd.Parameters.Add(New SqlParameter("@userid", uid))
                        checkcmd.Parameters.Add(New SqlParameter("@session_ID", Ses_Cookie))
                        'checkcmd.Parameters.Add(New SqlParameter("@ip", GetIPAddress()))
                        checkcmd.Parameters.Add(New SqlParameter("@user_agent", Request.UserAgent.ToString))

                        Dim da As New SqlDataAdapter(checkcmd)
                        Dim dt As New DataTable
                        connectPrj.Close()
                        da.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            MsgBox(dt.Rows(0).Item("first_visit"))
                            Session("first_visit") = DateTime.Parse(dt.Rows(0).Item("first_visit"), CultureInfo.CreateSpecificCulture("en-NZ"))
                            Session("last_visit") = DateTime.Parse(DateTime.Now, CultureInfo.CreateSpecificCulture("en-NZ"))
                            update_user_session(Ses_Cookie, uid)
                            'If Session("lastvisit") Is Nothing Then
                            '    Session("lastVisit") = DateTime.Parse(lastvisit, CultureInfo.CreateSpecificCulture("en-NZ"))
                            'End If
                            If isInSession() Then

                                Dim token As String = gen_rnd_token()
                                'Session("token") = md5Fun.md5Encr(token & Request.UserAgent.ToString & GetIPAddress())
                                Session("token") = md5Fun.md5Encr(token & Request.UserAgent.ToString & dt.Rows(0).Item("userid"))
                                update_authcookie(authcookie, token)
                                Return True
                            Else
                                Return False
                            End If
                        Else
                            Return False
                        End If

                    Catch ex As Exception
                        'lberror.Text = "page error!"
                        Response.Redirect("~/users/conerror.htm")

                    End Try
                End If

            End If
        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function GetIPAddress() As String
        Dim context As System.Web.HttpContext = System.Web.HttpContext.Current()

        Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")

        If String.IsNullOrEmpty(sIPAddress) Then
            Return context.Request.ServerVariables("REMOTE_ADDR")
        Else
            Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
            Return ipArray(0)
        End If

    End Function

    Private Sub update_authcookie(ByVal authocookie As HttpCookie, ByVal token As String)
        Dim auth_cookie As HttpCookie = New HttpCookie("TM")
        auth_cookie.Values("uid") = uid
        auth_cookie.Values("sid") = Ses_Cookie
        'auth_cookie.Values("lv") = lastvisit
        auth_cookie.Values("token") = token
        Response.Cookies.Set(CookieSecurityProvider2.Encrypt(auth_cookie))
    End Sub

    Private Function isInSession() As Boolean

        'lastvisit_date = DateTime.Parse(lastvisit, CultureInfo.CreateSpecificCulture("en-NZ"))
        If DateTime.Compare(DateTime.Parse(DateTime.Now, CultureInfo.CreateSpecificCulture("en-NZ")), Session("first_visit").AddMinutes(20)) < 0 Then
            If DateTime.Compare(DateTime.Parse(DateTime.Now, CultureInfo.CreateSpecificCulture("en-NZ")), Session("last_visit").AddMinutes(10)) < 0 Then
                If Ses_Cookie.Equals(Session.SessionID) And Session.SessionID.ToString.Equals(Request.Cookies("ASP.NET_SessionID").Value) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If


    End Function

    Private Function gen_rnd_token() As String
        Dim token As String = System.Guid.NewGuid().ToString()
        Return token
    End Function
    Private Sub update_user_session(ByVal Ses_Cookie As String, ByVal userid As String)
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            'Dim delcmd As New SqlCommand("delete from tbl_usr_session where (userid=@userid and session_id=@sessionID and ip=@ip and user_agent=@user_agent) or (DateDiff(minute,first_visit,GetDate())>=10)", connectPrj)
            Dim delcmd As New SqlCommand("delete from tbl_usr_session where (userid=@userid and session_id=@sessionID and user_agent=@user_agent) or (DateDiff(minute,first_visit,GetDate())>=18)", connectPrj)

            delcmd.Parameters.Add(New SqlParameter("@userid", userid))
            delcmd.Parameters.Add(New SqlParameter("@sessionID", Ses_Cookie))
            'delcmd.Parameters.Add(New SqlParameter("@ip", GetIPAddress()))
            delcmd.Parameters.Add(New SqlParameter("@user_agent", Request.UserAgent.ToString))
            delcmd.ExecuteNonQuery()

            connectPrj.Close()
        Catch ex As Exception

            Response.Redirect("~/users/conerror.htm")

        End Try

    End Sub
End Class

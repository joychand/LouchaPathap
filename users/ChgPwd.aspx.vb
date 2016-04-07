
Imports System.Data.SqlClient
Imports System.Globalization
Partial Public Class ChgPwd
    Inherits System.Web.UI.Page
    Dim connectPrj As New SqlConnection(ConfigurationManager.ConnectionStrings("connectionString").ToString)
    Dim upconnectPrj As New SqlConnection(ConfigurationManager.ConnectionStrings("connectionString").ToString)
    Protected Shared Ses_Cookie As String
    Protected Shared uid As String
    Protected Shared lastvisit As String


    Private Sub ChgPwd_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If isAuthenticated() Then
                ViewStateUserKey = Session.SessionID
            Else
                Response.Redirect("~/users/errorsession.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("~/users/errorsession.aspx")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.UtcNow)
        Response.Expires = -1
        Try
            Dim ctl As Control
            Dim ctlPath As String
            If Session("userrole") = "SCY_USER" Or Session("userrole") = "SDC_USER" Then
                ctlPath = "~/users/_SCYREV/CtlUserSR.ascx"
                ctl = LoadControl(ctlPath)
                ctl.ID = "UC1"
                PlaceHolder1.Controls.Add(ctl)
            ElseIf Session("userrole") = "CSC_USER" Then
                ctlPath = "~/users/_CSC/CtlUserCSC.ascx"
                ctl = LoadControl(ctlPath)
                ctl.ID = "UC1"
                PlaceHolder1.Controls.Add(ctl)
            End If



        Catch ex As Exception

        End Try
        If Not Page.IsPostBack Then

            Session.Add("rndNo", 0)
            Session("rndNo") = md5Fun.md5Encr(System.Guid.NewGuid().ToString())
            clseed.Value = Session("rndNo")

            Button1.Attributes.Add("onclick", "javascript:return pwdinhash2();")
        End If
        
        
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        'Try
        '    If upconnectPrj.State = ConnectionState.Closed Then upconnectPrj.Open()
        '    Dim cmd3 As New SqlCommand("update tbl_users set password = @newpwd", upconnectPrj) ''where rtrim(userid)=@uname and role = @role
        '    With cmd3
        '        .Parameters.Add(New SqlParameter("@newpwd", txtnewpwd.Text.Substring(0, 32)))
        '        '.Parameters.Add(New SqlParameter("@uname", Session("uid")))
        '        '.Parameters.Add(New SqlParameter("@role", Session("userrole")))
        '    End With
        '    cmd3.ExecuteNonQuery()
        '    cmd3.Dispose()
        '    upconnectPrj.Close()
        '    Session("rndNo") = ""
        '    clseed.Value = ""
        '    Response.Redirect("~/users/chgpwd_success.aspx")
        'Catch ex As Exception

        'End Try
        'Page.Validate()
        'If Page.IsValid Then
        'ccjoin.ValidateCaptcha(txtCap.Text)
        'If (Not ccjoin.UserValidated) Then
        '    lblerror.Text = "Error! Captcha input are wrong."
        '    Exit Sub
        'End If

        If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
        Dim cmd As New SqlCommand("select userid,password from  tbl_users where rtrim(userid) = @uname and role = @role ", connectPrj)
        With cmd
            .Parameters.Add(New SqlParameter("@uname", Session("uid")))
            .Parameters.Add(New SqlParameter("@role", Session("userrole")))
        End With
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)
        If dt.Rows.Count > 0 Then

            ''seed value
            clseed.Value = Session("rndNo")
            'MsgBox(clseed.Value)
            Dim dbpwd As String = dt.Rows(0).Item("password")
            cmd.Dispose()
            da.Dispose()

            'validate request old password with profile hash password
            If md5Fun.md5Encr(dbpwd & clseed.Value) <> oldpwd.Text Then
                lblerror.Text = "Old Password not valid"
                Exit Sub
            ElseIf txtnewpwd.Text <> txtconfmpwd.Text Then
                lblerror.Text = "New passwords mismatch"
                Exit Sub
                If md5Fun.md5Encr(md5Fun.md5Encr(txtnewpwd.Text.Substring(0, 32) + clseed.Value)) <> txtnewpwd.Text.Substring(33, 64) And md5Fun.md5Encr(md5Fun.md5Encr(txtconfmpwd.Text.Substring(0, 32) + clseed.Value)) <> txtconfmpwd.Text.Substring(33, 64) Then
                    lblerror.Text = "New passwords not recieved correctly in server"
                    Exit Sub
                End If
            Else

                If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
                Dim cmd2 As New SqlCommand("select userid,password from  tbl_users where rtrim(userid) = @uname and role = @role and password = @pwd", connectPrj)
                With cmd2
                    .Parameters.Add(New SqlParameter("@uname", Session("uid")))
                    .Parameters.Add(New SqlParameter("@role", Session("userrole")))
                    .Parameters.Add(New SqlParameter("@pwd", txtnewpwd.Text.Substring(0, 32)))
                End With
                Dim reader As SqlDataReader = cmd2.ExecuteReader()
                If reader.HasRows Then
                    lblerror.Text = "Password already exists, Plz try again"
                    reader.Dispose()
                    reader.Close()
                    cmd2.Dispose()
                    connectPrj.Close()
                Else
                    Try
                        If upconnectPrj.State = ConnectionState.Closed Then upconnectPrj.Open()
                        Dim cmd3 As New SqlCommand("update tbl_users set password = @newpwd  where rtrim(userid)=@uname and role = @role ", upconnectPrj)
                        With cmd3
                            .Parameters.Add(New SqlParameter("@newpwd", txtnewpwd.Text.Substring(0, 32)))
                            .Parameters.Add(New SqlParameter("@uname", Session("uid")))
                            .Parameters.Add(New SqlParameter("@role", Session("userrole")))
                        End With
                        cmd3.ExecuteNonQuery()
                        cmd3.Dispose()
                        upconnectPrj.Close()
                        Session("rndNo") = ""
                        clseed.Value = ""
                        Response.Redirect("~/users/chgpwd_success.aspx")
                    Catch ex As Exception

                    End Try

                End If
                'Exit Sub
            End If
        Else
            Response.Redirect("~/users/errorsession.aspx")
        End If
        'End If
        
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
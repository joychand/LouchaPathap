Imports System.Data
Imports System.Data.SqlClient
Imports System.String
Imports System.Globalization

Partial Public Class frmLogTrl
    Inherits System.Web.UI.Page
    Dim connectPrj As New SqlConnection(ConfigurationManager.ConnectionStrings("connectionString").ToString)
    Protected Shared Ses_Cookie As String
    Protected Shared uid As String
    Protected Shared lastvisit As String


    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
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

        Me.frmLogTrl.Attributes.Add("autocomplete", "off")
        Try
            If Not Page.IsPostBack Then
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.Cache.SetNoStore()
                Response.Cache.SetExpires(DateTime.UtcNow)
                Response.Expires = -1

                If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
                Dim cmd As New SqlCommand("select userid,role,office,cir_code from  tbl_users where rtrim(userid) = @uname and (role=@scyrole or role=@sdcrole) ", connectPrj)
                With cmd
                    .Parameters.Add(New SqlParameter("@uname", uid))
                    .Parameters.Add(New SqlParameter("@scyrole", "SCY_USER"))
                    .Parameters.Add(New SqlParameter("@sdcrole", "SDC_USER"))
                End With
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    Session("uid") = dt.Rows(0).Item("userid")
                    Session("userrole") = dt.Rows(0).Item("role")
                    'Session("workarea") = dt.Rows(0).Item("work_area")
                    Session("office") = dt.Rows(0).Item("office")
                    Session("cir_code") = dt.Rows(0).Item("cir_code")
                    cmd.Dispose()
                    connectPrj.Close()

                Else
                    Response.Redirect("~/users/errorsession.aspx")
                End If


                Call DisplayLastAct()
                Call updateLastVisit()


                AdGrid.Visible = True
                DisplayAdLog()
            End If
        Catch ex As Exception
            Response.Redirect("~/users/errorsession.aspx")
        End Try
        
        
    End Sub

    Private Sub updateLastVisit()

        If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
        Dim cmd As New SqlCommand("select count(*) from   lastOnlineAct where  userId= @vuserid", connectPrj)
        cmd.Parameters.Add(New SqlParameter("@vuserid", Session("uid")))
        If cmd.ExecuteScalar > 0 Then
            Dim upcmd As New SqlCommand("update lastOnlineAct set lastVisit='" & Format(Now, "dd-MMM-yyyy HH:mm tt") & "', ClientIPVisit='" & GetIPAddress() & "' where  userid= @vuserid77", connectPrj)
            upcmd.Parameters.Add(New SqlParameter("@vuserid77", Session("uid")))
            upcmd.ExecuteNonQuery()
        Else
            Dim inscmd As New SqlCommand("insert into lastOnlineAct(userid,lastVisit,clientIPVisit) values('" & Session("uid") & "','" & Format(Now, "dd-MMM-yyyy HH:mm tt") & "','" & GetIPAddress() & "')", connectPrj)
            inscmd.ExecuteNonQuery()
        End If
        connectPrj.Close()

    End Sub

    Private Sub DisplayAdLog()

        If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
        Dim cmd As New SqlCommand("select * from  adtrails", connectPrj)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            AdGrid.DataSource = Nothing
            AdGrid.DataSource = dt.DefaultView
            AdGrid.DataBind()
        Else
            AdGrid.DataSource = Nothing
        End If

        connectPrj.Close()

    End Sub
    Private Sub DisplayLastAct()

        If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
        Dim cmd As New SqlCommand("select * from   lastOnlineAct where  userId= @vuserid", connectPrj)
        cmd.Parameters.Add(New SqlParameter("@vuserid", Session("uid")))
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        If dt.Rows.Count > 0 Then
            lbLastVisit.Text = IIf(IsDBNull(dt.Rows(0).Item("lastVisit")), Format(Now, "dd-MMM-yyyy HH:mm tt"), dt.Rows(0).Item("lastVisit"))
            lbClientIPVisit.Text = IIf(IsDBNull(dt.Rows(0).Item("clientIPVisit")), "NA", dt.Rows(0).Item("clientIPVisit"))

            lbLastFailVisit.Text = IIf(IsDBNull(dt.Rows(0).Item("lastFailVisit")), "NA", dt.Rows(0).Item("lastFailVisit"))
            lbClientIPFailVisit.Text = IIf(IsDBNull(dt.Rows(0).Item("clientIPFailVisit")), "NA", dt.Rows(0).Item("clientIPFailVisit"))

        Else
            lbLastVisit.Text = "NA"
            lbLastFailVisit.Text = "NA"
        End If

        connectPrj.Close()

    End Sub

    
    Protected Sub btnClosePanel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClosePanel.Click
        Panel1.Visible = False

        AdGrid.Visible = False
        btnClosePanel.Visible = False

    End Sub



    Private Sub AdGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles AdGrid.PageIndexChanging
        AdGrid.PageIndex = e.NewPageIndex
        DisplayAdLog()
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
                        Dim checkcmd As New SqlCommand("select * from (select * from tbl_usr_session where userid=@userid and session_id=@session_ID and user_agent= @user_agent) as a where DateDiff(minute,a.first_visit,GetDate())<=5", connectPrj)
                        checkcmd.Parameters.Add(New SqlParameter("@userid", uid))
                        checkcmd.Parameters.Add(New SqlParameter("@session_ID", Ses_Cookie))
                        'checkcmd.Parameters.Add(New SqlParameter("@ip", GetIPAddress()))
                        checkcmd.Parameters.Add(New SqlParameter("@user_agent", Request.UserAgent.ToString))

                        Dim da As New SqlDataAdapter(checkcmd)
                        Dim dt As New DataTable
                        da.Fill(dt)
                        connectPrj.Close()
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
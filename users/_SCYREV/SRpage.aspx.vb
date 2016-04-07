
Imports Bytescout.BarCodeReader
Imports System.Data.SqlClient
Imports System.String
Imports System.Globalization
Imports System.Configuration


Partial Public Class SRpage
    Inherits System.Web.UI.Page
    Dim connectPrj As New SqlConnection(ConfigurationManager.ConnectionStrings("connectionString").ToString)
    Dim distcode As String
    Protected circode As String
    Protected Shared Ses_Cookie As String
    Protected Shared uid As String
    Protected Shared lastvisit As String
    Protected Shared lastvisit_date As DateTime

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'MsgBox(Session.SessionID)
        'MsgBox(Session("session_status").ToString)
        'If Session("session_status").ToString.Equals("new") Then
        '    MsgBox("hahaha you fried")
        'End If
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
        Me.SRpage.Attributes.Add("autocomplete", "off")
        'MsgBox(Session.SessionID.ToString)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.UtcNow)
        Response.Expires = -1
        Try
            'If Not Ses_Cookie <> Session.SessionID.ToString And Not Request.Cookies("ASP.NET_SessionID").Value <> Session.SessionID Then
            If Not Page.IsPostBack Then
                'Response.Cache.SetCacheability(HttpCacheability.NoCache)
                'Response.Cache.SetNoStore()
                'Response.Cache.SetExpires(DateTime.UtcNow)
                'Response.Expires = -1
                If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
                Dim cmd As New SqlCommand("select userid,role,office,cir_code from  tbl_users where rtrim(userid) = @uname ", connectPrj)
                With cmd
                    .Parameters.Add(New SqlParameter("@uname", uid))
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
                    If Session("userrole") = "SDC_USER" Then
                        If IsDBNull(Session("cir_code")) Then
                            lberr.Text = "Circle code mapping with the user is yet pending. Please contact NIC DBA."
                            distcode = 0
                            Button1.Enabled = False
                        Else
                            distcode = Session("cir_code").ToString.Substring(0, 2)
                        End If
                    ElseIf Session("userrole") = "SCY_USER" Then
                        distcode = Nothing
                        Session("cir_code") = Nothing
                    Else
                        Response.Redirect("~/users/errorsession.aspx")
                    End If
                    Call loadDistrict()
                    'Dim label2 As Label
                    'label2 = CtlUserSR1.FindControl("lblUsrinfo")
                    'label2.Visible = True
                    'label2.Text = Session("office")
                Else
                    Response.Redirect("~/users/errorsession.aspx")
                End If

            End If

        Catch ex As Exception
            Response.Redirect("~/users/errorsession.aspx")
        End Try



    End Sub


    Private Sub loadDistrict()
        If IsDBNull(Session("cir_code")) Then
            lberr.Text = "Circle code mapping with the user is yet pending. Please contact NIC DBA."
            Button1.Enabled = False
            Exit Sub
        End If

        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim strQuery As String = "select * from UniDistrict where distcode = isnull(@distcode,distcode) order by distcode"
            Dim cmd As New SqlCommand(strQuery, connectPrj)
            If Not distcode Is Nothing Then

                cmd.Parameters.Add(New SqlParameter("@distcode", distcode))
            Else
                cmd.Parameters.Add(New SqlParameter("@distcode", DBNull.Value))

            End If
            Dim dtreader As SqlDataReader
            dtreader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dtreader.HasRows Then
                ddlDistrict.Items.Clear()
                ddlDistrict.Items.Add("Select District")
                While dtreader.Read
                    'ddddddlDistrict.Items.Add(dtreader.Item("distDesc").ToString & " - " & dtreader.Item("distcode"))
                    ddlDistrict.Items.Add(New ListItem(dtreader.Item("distDesc"), dtreader.Item("distcode")))
                End While

            Else
                ddlDistrict.Items.Clear()
                ddlDistrict.Items.Add("No Data!")
            End If
            connectPrj.Close()
        Catch ex As Exception
            ddlDistrict.Items.Clear()
            connectPrj.Close()
            Response.Redirect("~/users/errorsession.aspx")
        End Try
    End Sub

    Private Sub loadCircle()
        'MsgBox(ddlDistrict.SelectedItem.Value)
        'MsgBox(distcode)

        Try
            circode = Session("cir_code")
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim strQuery As String = "select * from UniCircle  where distCode=@lc1 and distcode+subcode+circode= isnull(@circode,distcode+subcode+circode) order by cirDesc"
            Dim cmd As New SqlCommand(strQuery, connectPrj)
            With cmd
                '.Parameters.Add(New SqlParameter("@lc1", Right(ddddlDistrict.SelectedItem.Text, 2))) '"07"))
                .Parameters.Add(New SqlParameter("@lc1", ddlDistrict.SelectedItem.Value))
            End With
            'MsgBox(circode)
            If Not circode Is Nothing Then
                cmd.Parameters.Add(New SqlParameter("@circode", circode))
            Else
                cmd.Parameters.Add(New SqlParameter("@circode", DBNull.Value))
            End If
            Dim dtreader As SqlDataReader
            dtreader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dtreader.HasRows Then
                ddlCircle.Items.Clear()
                ddlCircle.Items.Add("Select Circle")
                'While dtreader.Read
                '    ddddlCircle.Items.Add(dtreader.Item("cirDesc") & " - " & dtreader.Item("circode"))
                'End While
                While dtreader.Read
                    Dim circlecode As String = dtreader.Item("subcode").ToString + dtreader.Item("circode")
                    'ddddlCircle.Items.Add(dtreader.Item("cirDesc") & " - " & dtreader.Item("circode"))
                    'ddddlCircle.Items.Add(dtreader.Item("cirDesc") & " - " & circlecode)
                    ddlCircle.Items.Add(New ListItem(dtreader.Item("cirDesc"), circlecode))
                End While

            Else
                ddlCircle.Items.Clear()
                ddlCircle.Items.Add("No Data!")
            End If
            connectPrj.Close()
        Catch ex As Exception
            ddlCircle.Items.Clear()
            Response.Redirect("~/users/errorsession.aspx")  'ddddlCircle.Items.Add("Page error!")
        End Try
    End Sub

    Private Sub loadVilla()

        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            'Dim strQuery As String = "select * from UniLocation  where substring(locCd,1,2)=@lc1 and substring(locCd,3,2)=@lc2 and substring(locCd,5,3)= @lc3 order by locDesc"
            Dim strQuery As String = "select * from UniLocation  where substring(locCd,1,2)=@lc1 and substring(locCd,3,5)= @lc3 order by locDesc"
            Dim cmd As New SqlCommand(strQuery, connectPrj)

            'With cmd
            '    .Parameters.Add(New SqlParameter("@lc1", Right(ddlDistrict.SelectedItem.Text, 2))) '"07"))
            '    .Parameters.Add(New SqlParameter("@lc2", Right(lSubDiv.SelectedItem.Text, 2))) '"02"))
            '    .Parameters.Add(New SqlParameter("@lc3", Right(ddlCircle.SelectedItem.Text, 3))) '"002"))
            'End With
            With cmd
                '.Parameters.Add(New SqlParameter("@lc1", Right(ddlDistrict.SelectedItem.Text, 2))) '"07"))
                '.Parameters.Add(New SqlParameter("@lc3", Right(ddlCircle.SelectedItem.Text, 5))) '"002"))
                .Parameters.Add(New SqlParameter("@lc1", ddlDistrict.SelectedItem.Value))
                .Parameters.Add(New SqlParameter("@lc3", ddlCircle.SelectedItem.Value))
            End With

            Dim dtreader As SqlDataReader
            dtreader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dtreader.HasRows Then
                ddlVillage.Items.Clear()
                ddlVillage.Items.Add("Select Village")
                While dtreader.Read
                    'ddlVillage.Items.Add(dtreader.Item("locDesc") & " - " & dtreader.Item("locCd"))
                    ddlVillage.Items.Add(New ListItem(dtreader.Item("locdesc"), dtreader.Item("locCd")))
                End While
            Else
                ddlVillage.Items.Clear()
                ddlVillage.Items.Add("No Data!")
            End If
            connectPrj.Close()
        Catch ex As Exception
            ddlVillage.Items.Clear()
            Response.Redirect("~/users/errorsession.aspx")  'ddlVillage.Items.Add("Page error!")
        End Try
    End Sub
    Private Sub loadlandcl()
        If IsDBNull(Session("cir_code")) Then
            lberr.Text = "Circle code mapping with the user is yet pending. Please contact NIC DBA."
            Button1.Enabled = False
            Exit Sub
        End If
        Try
            'If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            'Dim strqry As String = "select distinct landclass from uniplot order by landclass"
            'Dim cmd As New SqlCommand(strqry, connectPrj)
            'Dim dtreader As SqlDataReader
            'dtreader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            'If dtreader.HasRows Then
            '    ddlLandclass.Items.Clear()
            '    ddlLandclass.Items.Add("Select Owner")
            '    While dtreader.Read
            '        ddlLandclass.Items.Add(dtreader.Item("landclass"))

            '    End While
            'Else
            '    ddlLandclass.Items.Clear()
            '    ddlLandclass.Items.Add("page error")
            'End If
            'dtreader.Dispose()
            ddlLandclass.Items.Clear()
            ddlLandclass.Items.Add("Select Owner")
            ddlLandclass.Items.Add("Individual")
            ddlLandclass.Items.Add("Encoarcher/Religious Site")
            ddlLandclass.Items.Add("Govt Land")


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlDistrict_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlDistrict.SelectedIndexChanged
        'ddlCircle.Items.Clear()
        'MsgBox(Session("cir_code"))
        ddlVillage.Items.Clear()
        ddlLandclass.Items.Clear()
        lblMessage.Text = ""
        lblMessage.Visible = False
        GridView1.DataSource = Nothing
        GridView1.DataBind()

        loadCircle()

    End Sub

    Protected Sub ddlCircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlCircle.SelectedIndexChanged
        'ddlVillage.Items.Clear()
        ddlLandclass.Items.Clear()
        CheckBox1.Checked = False
        CheckBox1.Visible = False
        Panel1.Visible = False
        txtareafrm.Text = Nothing
        txtareaTo.Text = Nothing

        lblMessage.Text = ""
        lblMessage.Visible = False
        GridView1.DataSource = Nothing
        GridView1.DataBind()
        loadVilla()

    End Sub

    Protected Sub ddlVillage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlVillage.SelectedIndexChanged
        loadlandcl()
        'ddlLandclass.Items.Clear()
        CheckBox1.Checked = False
        CheckBox1.Visible = False
        Panel1.Visible = False
        txtareafrm.Text = Nothing
        txtareaTo.Text = Nothing
        lblMessage.Text = ""
        lblMessage.Visible = False
        GridView1.DataSource = Nothing
        GridView1.DataBind()
    End Sub
    Protected Sub getplotdetail()
        'MsgBox(Session("cir_code"))
        'MsgBox(ddlVillage.SelectedItem.Value.ToString.Substring(0, 7))
        If IsDBNull(Session("cir_code")) Then
            lberr.Text = "Circle code mapping with the user is yet pending. Please contact NIC DBA."
            Button1.Enabled = False
            Exit Sub
        
        End If
        'MsgBox(ddlVillage.SelectedItem.Value.ToString.Substring(2, 5))
        Dim tloccd = ddlVillage.SelectedItem.Value.ToString
        Dim areaFrm As String = txtareafrm.Text
        Dim areaTo As String = txtareaTo.Text

        Dim villagename As String = ddlVillage.SelectedItem.Text
        Dim strqry As String = ""
        Dim cmd As New SqlCommand
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()


            Select Case ddlLandclass.SelectedItem.ToString
                Case "Individual"
                    'strqry = "select NewDagno,Newpattano,name,address from uniowner where (Name  not like  N'%ষ্টেট%' and name not like N'%স্টেট%' and name not like '%State%' and name not like '%te%' and name not like '%সস্টে%' and name not like '%ষেঠ%') and newpattano<>'' and newpattano<>'0' and ownno = '1' and loccd = @lc1 order by loccd+newdagno"
                    strqry = "select p.NewDagno,o.Newpattano,p.area,o.name,coalesce(nullif(o.address, ''),@Vlname)as address from uniowner as o ,uniplot as p where p.loccd = o.loccd and p.newdagno = o.newdagno and(o.Name  not like  N'%ষ্টেট%' and o.name not like N'%স্টেট%'and o.name not like '%State%' and o.name not like '%te%'and o.name not like '%সস্টে%' and o.name not like '%ষেঠ%' and o.name not like N'%সেটট%' and o.name not like N'%শ্টেট%' and o.name not like N'%স্ছেট%' and o.name not like N'%ষেটট%') and o.newpattano<>'' and o.newpattano<>'0' and o.ownno = '1' and p.loccd = @lc1 order by p.loccd+p.newdagno "
                    cmd = New SqlCommand(strqry, connectPrj)
                    With cmd
                        .Parameters.Add(New SqlParameter("@lc1", tloccd))
                        .Parameters.Add(New SqlParameter("@Vlname", villagename))
                        '.Parameters.Add("@lndCl", SqlDbType.NVarChar).Value = ddlLandclass.SelectedItem.ToString
                    End With
                Case "Encoarcher/Religious Site"
                    strqry = "select p.NewDagno,o.Newpattano,p.area,o.name,coalesce(nullif(o.address, ''),@Vlname)as address from uniowner as o ,uniplot as p where p.loccd = o.loccd and p.newdagno = o.newdagno and (o.Name  not like  N'%ষ্টেট%' and o.name not like N'%স্টেট%'and o.name not like '%State%' and o.name not like '%te%'and o.name not like '%সস্টে%' and o.name not like '%ষেঠ%' and  o.name not like N'%সেটট%' and o.name not like N'%শ্টেট%' and o.name not like N'%স্ছেট%' and o.name not like N'%ষেটট%') and (o.newpattano='' or o.newpattano is null or o.newpattano='0') and o.ownno='1' and  p.loccd = @lc1 order by p.loccd+p.newdagno"
                    cmd = New SqlCommand(strqry, connectPrj)
                    With cmd
                        .Parameters.Add(New SqlParameter("@lc1", tloccd))
                        .Parameters.Add(New SqlParameter("@Vlname", villagename))
                        '.Parameters.Add("@lndCl", SqlDbType.NVarChar).Value = ddlLandclass.SelectedItem.ToString
                    End With
                Case "Govt Land"
                    If CheckBox1.Checked = True Then



                        strqry = "select p.NewDagno, o.Newpattano,p.area, o.name, coalesce(nullif(o.address, ''),@Vlname)as address from uniowner as o , uniplot as p where p.loccd = o.loccd and p.newdagno = o.newdagno and  (o.Name like  N'%ষ্টেট%' or  o.name like N'%স্টেট%' or o.name  like '%State%' or o.name  like '%te%' or o.name  like '%সস্টে%' or o.name  like '%ষেঠ%' or o.name  like N'%সেটট%' or o.name  like N'%শ্টেট%' or o.name  like N'%স্ছেট%' or o.name  like N'%ষেটট%') and (p.newpattano='' or p.newpattano is null or p.newpattano='0') and p.loccd =@lc1 and p.area between @areaFrm and @areaTo order by p.loccd+p.newdagno"
                        cmd = New SqlCommand(strqry, connectPrj)
                        With cmd
                            .Parameters.Add(New SqlParameter("@lc1", tloccd))
                            .Parameters.Add(New SqlParameter("@Vlname", villagename))
                            .Parameters.Add(New SqlParameter("@areaFrm", areaFrm))
                            .Parameters.Add(New SqlParameter("@areaTo", areaTo))
                            '.Parameters.Add("@lndCl", SqlDbType.NVarChar).Value = ddlLandclass.SelectedItem.ToString
                        End With
                    Else
                        strqry = "select p.NewDagno, o.Newpattano,p.area, o.name, coalesce(nullif(o.address, ''),@Vlname)as address from uniowner as o , uniplot as p where p.loccd = o.loccd and p.newdagno = o.newdagno and  (o.Name like  N'%ষ্টেট%' or  o.name like N'%স্টেট%' or o.name  like '%State%' or o.name  like '%te%' or o.name  like '%সস্টে%' or o.name  like '%ষেঠ%' or o.name  like N'%সেটট%' or o.name  like N'%শ্টেট%' or o.name  like N'%স্ছেট%' or o.name  like N'%ষেটট%') and (p.newpattano='' or p.newpattano is null or p.newpattano='0') and p.loccd =@lc1  order by p.loccd+p.newdagno"
                        cmd = New SqlCommand(strqry, connectPrj)
                        With cmd
                            .Parameters.Add(New SqlParameter("@lc1", tloccd))
                            .Parameters.Add(New SqlParameter("@Vlname", villagename))

                            '.Parameters.Add("@lndCl", SqlDbType.NVarChar).Value = ddlLandclass.SelectedItem.ToString
                        End With
                    End If
            End Select

            'MsgBox(strqry)
            'Dim cmd As New SqlCommand(strqry, connectPrj)
            'With cmd
            '    .Parameters.Add(New SqlParameter("@lc1", tloccd))
            '    .Parameters.Add(New SqlParameter("@Vlname", villagename))
            '    '.Parameters.Add("@lndCl", SqlDbType.NVarChar).Value = ddlLandclass.SelectedItem.ToString
            'End With
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            Dim dt As New DataSet()
            da.Fill(dt)
            'MsgBox(dt.Tables(0).Rows.Item(1)(3).ToString)
            GridView1.DataSource = Nothing
            GridView1.DataSource = dt
            GridView1.DataBind()
            If dt.Tables(0).Rows.Count > 0 Then
                lblMessage.Visible = True
                lblMessage.Text = "Plot Details"
            Else
                lblMessage.Visible = True
                lblMessage.Text = "No such Plot found/Data not updated"
            End If
            da.Dispose()
            connectPrj.Close()
        Catch ex As Exception
            'MsgBox(ex.ToString)

        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If Session("userrole") = "SDC_USR" Then
            If IsDBNull(Session("cir_code")) Then
                lberr.Text = "Circle code mapping with the user is yet pending. Please contact NIC DBA."
                Button1.Enabled = False
                Exit Sub
            ElseIf Not Session("cir_code").ToString.Equals(ddlVillage.SelectedItem.Value.ToString.Substring(0, 7)) Then
                Session.Clear()
                Session.Abandon()
                Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
                Response.Expires = -1
                If Not Request.Cookies("TM") Is Nothing Then
                    Response.Cookies.Remove("TM")
                    Dim authcookie As HttpCookie
                    authcookie = New HttpCookie("TM")
                    authcookie.Expires = DateTime.Now.AddDays(-1D)
                    Response.Cookies.Add(authcookie)
                End If

                Response.Redirect("~/users/errorsession.aspx")
            End If
        End If
        
        Page.Validate()
        If Page.IsValid Then
            getplotdetail()

        End If

    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        getplotdetail()

    End Sub

    Protected Sub ddlLandclass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlLandclass.SelectedIndexChanged
        If ddlLandclass.SelectedItem.Text = "Govt Land" Then
            CheckBox1.Visible = True

        Else
            CheckBox1.Visible = False
            CheckBox1.Checked = False
            Panel1.Visible = False
            txtareafrm.Text = Nothing
            txtareaTo.Text = Nothing
        End If

        lblMessage.Text = ""
        lblMessage.Visible = False
        GridView1.DataSource = Nothing
        GridView1.DataBind()
    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Panel1.Visible = True
            CompareValidator1.Enabled = True
            RequiredFieldValidator1.Enabled = True
            rvTxtFrm.Enabled = True
        Else
            Panel1.Visible = False
            CompareValidator1.Enabled = False
            RequiredFieldValidator1.Enabled = False
            rvTxtFrm.Enabled = False
        End If
        txtareafrm.Text = Nothing
        txtareaTo.Text = Nothing
        lblMessage.Text = ""
        lblMessage.Visible = False
        GridView1.DataSource = Nothing
        GridView1.DataBind()
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
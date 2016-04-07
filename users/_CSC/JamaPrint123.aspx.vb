'' Module for: jamadandi in UniCoded HTML Report using CSS plus BGI printing
'' Author    : Jiten Singh Haobam Scientist B, NIC Manipur
'' Date      : 27/06/2013 [dd/mm/yyyy]
'' Techs.    : (a) AJAX Ridden therby enhancing page rendering to clients
''           : (b) uses parameterized SQL statements safegaurding SQL INJECTIONS
''           : (c) Data Grid to display bulky information in a fair fashion
Imports System.IO
Imports System.Web.SessionState
Imports System.Drawing
Imports System.Web.UI.WebControls
Imports System.Drawing.Printing
Imports System.Data
Imports System.Data.SqlClient
'Imports Microsoft.Reporting.WebForms
Imports System.Math
Imports System.Globalization
'Imports Microsoft.Win32

Partial Public Class JamaPrint123

    Inherits System.Web.UI.Page
    Dim connectPrj As New SqlConnection(ConfigurationManager.ConnectionStrings("connectionString").ToString)
    'This uses grid report
    Public hund, thou, lacs, crore As String
    Dim TotArea As String
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

    'Dim regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Internet Explorer").OpenSubKey("Main").OpenSubKey("Print_background")
    ''Get the current setting so that we can revert it after printjob
    'Dim defaultValue As Integer = new CType(regKey.GetValue("Print_Background"), Integer)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.UtcNow)
        Response.Expires = -1
        Try
            ''Get the current setting so that we can revert it after printjob
            'regKey.SetValue("Print_Background", 1)
            Call appearData()

            Dim hashP As String
            hashP = IIf(Session("hashPno") = "", "no hash pa", Session("hashPno"))
            imageBar1.ImageUrl = "~/users/_CSC/BarCode.aspx?code=" & Session("hashPno") & ""
            lbTimeStamp.Text = "Printed on " & Now.ToString & "  at CSC: " & Session("uid").ToString & " (Data as on date: " & dt_effect_data() & ")"
            lbWords.Text = "(" & NumInWords(TotArea) & " Hectare)"
            lbTotArea.Text = "Total Area: " & String.Format("{0:0.0000}", TotArea) & " H"
            lbsc.Text = GetIPAddress()

        Catch ex As Exception
            Response.Redirect("~/users/errorsession.aspx")
        End Try
        'Revert the registry key to the original value
        'regKey.SetValue("Print_Background", defaultValue)
    End Sub
    
    Public Function dt_effect_data() As String
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim cmd As New SqlCommand("select dt_upload from tbl_upload_dt", connectPrj)
            dt_effect_data = IIf(IsDBNull(cmd.ExecuteScalar), "", cmd.ExecuteScalar)
            connectPrj.Close()
        Catch ex As Exception
            dt_effect_data = ""
        End Try
    End Function
    Private Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        Try
            For Each myCell As TableCell In e.Row.Cells
                myCell.Style.Add("word-break", "break-all")
            Next
            e.Row.Cells(0).Width = 50
            e.Row.Cells(1).Width = 200 'writes District oldpattano exnew
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(2).Width = 80 'new pattano
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(3).Width = 140 'name father  address
            e.Row.Cells(4).Width = 100 ' 80 new dagno
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(5).Width = 200 '170 'writes circle
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(6).Width = 50
            e.Row.Cells(7).Width = 50 'area acre
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(8).Width = 50 'land class
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(9).Width = 50 'area hectare
            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(10).Width = 50 ' khajana tax revenue
            e.Row.Cells(11).Width = 200 ''writes villa no. and name
            e.Row.Cells(11).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(12).Width = 50
            e.Row.Cells(13).Width = 50 'order no.
            e.Row.Cells(13).HorizontalAlign = HorizontalAlign.Left
        Catch ex As Exception
            Response.Redirect("~/users/errorsession.aspx")
        End Try



    End Sub
    Private Sub appearData()
        Try

            'connection open
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim cmd As New SqlCommand("SELECT a.loccd,  b.p3 , b.p2, b.p10, b.p8," & _
                                           " b.p5, b.p9, b.p14, b.p11, b.p4, b.p31, b.p21, b.p101, b.p81, b.p51, b.p141, b.p41,padd,row_number() over(order by pattano) as rno " & _
                                        " From unilocation a, uniplotowner b  where a.loccd=b.loccd", connectPrj)


            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count < 1 Then
                GridView1.Visible = False
                Exit Sub
            End If

            Dim nr As Integer = 0
            'Dim id As Integer = 0


            Dim dtab1 As New DataTable
            dtab1.Columns.Add("id1")
            dtab1.Columns.Add("id2")
            dtab1.Columns.Add("id3")
            dtab1.Columns.Add("id4")
            dtab1.Columns.Add("id5")
            dtab1.Columns.Add("id6")
            dtab1.Columns.Add("id7")
            dtab1.Columns.Add("id8")
            dtab1.Columns.Add("id9")
            dtab1.Columns.Add("id10")
            dtab1.Columns.Add("id11")
            dtab1.Columns.Add("id12")
            dtab1.Columns.Add("id13")
            dtab1.Columns.Add("id14")

            Dim dtab2 As New DataTable
            dtab2.Columns.Add("id1")
            dtab2.Columns.Add("id2")
            dtab2.Columns.Add("id3")
            dtab2.Columns.Add("id4")
            dtab2.Columns.Add("id5")
            dtab2.Columns.Add("id6")
            dtab2.Columns.Add("id7")
            dtab2.Columns.Add("id8")
            dtab2.Columns.Add("id9")
            dtab2.Columns.Add("id10")
            dtab2.Columns.Add("id11")
            dtab2.Columns.Add("id12")
            dtab2.Columns.Add("id13")
            dtab2.Columns.Add("id14")

            Dim b1 As String = "    " ''use ascii 255 as blank
            Dim k As Integer
            For k = 1 To 5
                dtab2.Rows.Add(b1, b1, b1, b1, b1, b1, b1, b1, b1, b1, b1, b1, b1, b1)
            Next

            dtab2.Rows.Add(b1, Session("Dist").ToString, b1, b1, b1, Session("Circle").ToString, b1, b1, b1, b1, b1, Session("Village").ToString, b1, Now.Year.ToString)
            GridView2.Visible = True
            GridView2.DataSource = Nothing
            GridView2.DataSource = dtab2.DefaultView
            GridView2.DataBind()


            For k = 1 To 6
                dtab1.Rows.Add(b1, "", "", "", "", "", "", "", "", "", "", "", "", "")
            Next

            Dim vOldpatta, vexNewpatta, vexNewDag, newPatta, NewDag, hline1, hline2, colo As String
            vOldpatta = ""
            hline1 = ""
            hline2 = ""
            vexNewpatta = ""
            vexNewDag = ""
            newPatta = ""
            NewDag = ""

            'Dim recCount As Integer = 1
            'Dim nPage As Integer = 1
            Dim drow As DataRow
            For Each drow In dt.Rows

                ''oldpattano display compute
                If drow.Item("p2") = "" And drow.Item("p21") = "" Then vOldpatta = ""
                If drow.Item("p2") = drow.Item("p21") And drow.Item("p21") <> "" Then vOldpatta = drow.Item("p21")
                If drow.Item("p2") <> drow.Item("p21") Then
                    vOldpatta = IIf(drow.Item("p2") = "", "", drow.Item("p2"))
                    If drow.Item("p21") = "" Then

                    Else
                        vOldpatta = vOldpatta & "," & drow.Item("p21")
                    End If

                End If

                ''exnewpatta exist
                If drow.Item("p3") <> drow.Item("p31") And drow.Item("p3") <> "" Then
                    vexNewpatta = drow.Item("p3")
                    hline1 = "_____"
                    newPatta = drow.Item("p31")
                Else
                    vexNewpatta = drow.Item("p31")
                    hline1 = ""
                    newPatta = ""
                End If


                If drow.Item("p5") <> drow.Item("p51") And drow.Item("p5") <> "" Then
                    vexNewDag = drow.Item("p5")
                    hline2 = "_____"
                    NewDag = drow.Item("p51")
                Else
                    vexNewDag = drow.Item("p51")
                    hline2 = ""
                    NewDag = ""
                End If

                colo = IIf(drow.Item("p9").ToString = "", "", ": ")

                Dim vNam As String = IIf(drow.Item("p4") = "", "", drow.Item("rno").ToString & ") " & drow.Item("p4").ToString) ''changed on 04-09-14''drow.Item("rno").ToString & ") " & drow.Item("p4").ToString
                Dim vAcre As String = IIf(drow.Item("p81") = 0, "", String.Format("{0:0.0000}", drow.Item("p81")))
                Dim vHect As String = IIf(drow.Item("p10") = 0, "", String.Format("{0:0.0000}", drow.Item("p10")))
                Dim vTax As String = "" 'IIf(drow.Item("p11") = 0, "", String.Format("{0:0.0000}", drow.Item("p11")))

                ''exnewdagno exist  exnew is Numere and newDagNo is Denomerator
                dtab1.Rows.Add("", vOldpatta.Trim, vexNewpatta, vNam, vexNewDag, "", "", vAcre, drow.Item("p9").ToString, vHect, vTax, "", "", drow.Item("p14").ToString & colo)
                dtab1.Rows.Add("", "", hline1, "    " & drow.Item("p41"), hline2, "", "", "", "", "", "", "", "", drow.Item("p141").ToString)
                dtab1.Rows.Add("", "", newPatta, "    " & drow.Item("padd"), NewDag, "", "", "", "", "", "", "", "", "")
                TotArea = Val(TotArea) + Val(vHect)
                nr = (nr + 1)
                If nr >= 6 Then 'for 6 records paging control
                    If dt.Rows.Count = 6 Then
                        For k = 1 To (18 - (3 * nr))
                            dtab1.Rows.Add(b1, "", "", "", "", "", "", "", "", "", "", "", "", "")
                        Next
                    Else
                        For k = 1 To (18 - (3 * nr)) + 15 '19 -- for 5 records in a page
                            dtab1.Rows.Add(b1, "", "", "", "", "", "", "", "", "", "", "", "", "")
                        Next
                    End If
                    nr = 0
                End If

            Next
            'dtab1.Rows.Add(b1, "", "", "", "", "", "", "(" & String.Format("{0:0.0000}", TotArea) & ")", "", "", "", "", "", "")
            If nr < 6 And nr > 0 Then
                For k = 1 To 18 - (3 * nr)
                    dtab1.Rows.Add(b1, "", "", "", "", "", "", "", "", "", "", "", "", "")
                Next
            End If


            GridView1.Visible = True
            GridView1.DataSource = Nothing
            GridView1.DataSource = dtab1.DefaultView
            GridView1.DataBind()

            da.Dispose()
            connectPrj.Close()

        Catch ex As Exception
            Response.Redirect("~/users/errorsession.aspx")
        End Try
    End Sub
    Private Function DS(ByVal s As String) As String
        Try
            Dim b As Byte() = Convert.FromBase64String(s)
            DS = System.Text.Encoding.UTF32.GetString(b)
        Catch ex As Exception
            DS = ex.Message
            Response.Redirect("~/users/errorsession.aspx")
        End Try

    End Function

    Private Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        appearData()

    End Sub



    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            For Each myCell As TableCell In e.Row.Cells
                myCell.Style.Add("word-break", "break-all")
            Next
            e.Row.Cells(0).Width = 50
            e.Row.Cells(1).Width = 60 'oldpattano exnew
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(2).Width = 90 'new pattano
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(3).Width = 260 'name father  address
            e.Row.Cells(4).Width = 90 ' new dagno
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).Width = 80
            e.Row.Cells(6).Width = 90
            e.Row.Cells(7).Width = 90 'area acre
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).Width = 90 'land class
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(9).Width = 90 'area hectare
            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(10).Width = 80 ' khajana tax revenue
            e.Row.Cells(11).Width = 80
            e.Row.Cells(12).Width = 80
            e.Row.Cells(13).Width = 230 'order no.
            e.Row.Cells(13).HorizontalAlign = HorizontalAlign.Center
        Catch ex As Exception
            Response.Redirect("~/users/errorsession.aspx")
        End Try



    End Sub


    ''************* FUNCTIONS FOR CONVERTING NUMBER FIGURE IN WORDS UPTO 99999999999 VALUE****
    'function for single Sound
    Public Function ss(ByRef n As String) As String
        ss = ""
        Select Case n
            Case "0" : ss = ""
            Case "1" : ss = " One"
            Case "2" : ss = " Two"
            Case "3" : ss = " Three"
            Case "4" : ss = " Four"
            Case "5" : ss = " Five"
            Case "6" : ss = " Six"
            Case "7" : ss = " Seven"
            Case "8" : ss = " Eight"
            Case "9" : ss = " Nine"
        End Select
    End Function
    Public Function PointValSound(ByRef n As String) As String
        Dim i As Integer
        PointValSound = "Point "
        For i = 1 To Len(n)


            Select Case Mid(n, i, 1)
                Case "0" : PointValSound = PointValSound & "Zero "
                Case "1" : PointValSound = PointValSound & "One "
                Case "2" : PointValSound = PointValSound & "Two "
                Case "3" : PointValSound = PointValSound & "Three "
                Case "4" : PointValSound = PointValSound & "Four "
                Case "5" : PointValSound = PointValSound & "Five "
                Case "6" : PointValSound = PointValSound & "Six "
                Case "7" : PointValSound = PointValSound & "Seven "
                Case "8" : PointValSound = PointValSound & "Eight "
                Case "9" : PointValSound = PointValSound & "Nine "
            End Select
        Next i
    End Function
    'function for double sound
    Public Function dbs(ByRef n As String) As String
        dbs = ""
        Dim x1, x2 As String
        x1 = Mid(n, 1, 1)
        x2 = Mid(n, 2, 1)
        Select Case x1
            Case "0" : dbs = ss(x2)
            Case "1"
                If n = "10" Then
                    dbs = " Ten"
                ElseIf n = "11" Then
                    dbs = " Eleven"
                ElseIf n = "12" Then
                    dbs = " Twelve"
                ElseIf n = "13" Then
                    dbs = " Thirteen"
                Else
                    dbs = ss(x2) + " teen"
                End If
            Case "2" : dbs = " Twenty " + ss(x2)
            Case "3" : dbs = " Thirty " + ss(x2)
            Case "4" : dbs = " Forty " + ss(x2)
            Case "5" : dbs = " Fifty " + ss(x2)
            Case "6" : dbs = " Sixty " + ss(x2)
            Case "7" : dbs = " Seventy " + ss(x2)
            Case "8" : dbs = " Eighty " + ss(x2)
            Case "9" : dbs = " Ninety " + ss(x2)
        End Select
    End Function

    'function for converting figure to words
    Public Function IntegralValue(ByRef n As String) As String
        IntegralValue = ""
        Dim l As Integer
        l = Len(n)
        hund = " Hundred"
        thou = " Thousand"
        lacs = " Lacs"
        crore = " Crore"
        If Val(n) = 0 Then
            IntegralValue = ""
        Else
            Select Case l
                Case 1 : IntegralValue = ss(n)
                Case 2 : IntegralValue = dbs(n)
                Case 3 : IntegralValue = ss(Mid(n, 1, 1)) + IIf(ss(Mid(n, 1, 1)) = "", "", hund) + dbs(Mid(n, 2, 2))

                Case 4 : IntegralValue = ss(Mid(n, 1, 1)) + IIf(ss(Mid(n, 1, 1)) = "", "", thou) + ss(Mid(n, 2, 1)) + _
                                IIf(ss(Mid(n, 2, 1)) = "", "", hund) + dbs(Mid(n, 3, 2))

                Case 5 : IntegralValue = dbs(Mid(n, 1, 2)) + IIf(dbs(Mid(n, 1, 2)) = "", "", thou) + ss(Mid(n, 3, 1)) + _
                                IIf(ss(Mid(n, 3, 1)) = "", "", hund) + dbs(Mid(n, 4, 2))

                Case 6 : IntegralValue = ss(Mid(n, 1, 1)) + IIf(ss(Mid(n, 1, 1)) = "", "", lacs) + dbs(Mid(n, 2, 2)) + _
                                IIf(dbs(Mid(n, 2, 2)) = "", "", thou) + ss(Mid(n, 4, 1)) + IIf(ss(Mid(n, 4, 1)) = "", "", hund) + dbs(Mid(n, 5, 2))


                Case 7 : IntegralValue = dbs(Mid(n, 1, 2)) + IIf(dbs(Mid(n, 1, 2)) = "", "", lacs) + dbs(Mid(n, 3, 2)) + _
                                IIf(dbs(Mid(n, 3, 2)) = "", "", thou) + ss(Mid(n, 5, 1)) + IIf(ss(Mid(n, 5, 1)) = "", "", hund) + dbs(Mid(n, 6, 2))

                Case 8 : IntegralValue = ss(Mid(n, 1, 1)) + IIf(ss(Mid(n, 1, 1)) = "", "", crore) + dbs(Mid(n, 2, 2)) + _
                                IIf(dbs(Mid(n, 2, 2)) = "", "", lacs) + dbs(Mid(n, 4, 2)) + IIf(dbs(Mid(n, 4, 2)) = "", "", thou) + _
                                ss(Mid(n, 6, 1)) + IIf(ss(Mid(n, 6, 1)) = "", "", hund) + dbs(Mid(n, 7, 2))

                Case 9 : IntegralValue = dbs(Mid(n, 1, 2)) + IIf(dbs(Mid(n, 1, 2)) = "", "", crore) + dbs(Mid(n, 3, 2)) + _
                                IIf(dbs(Mid(n, 3, 2)) = "", "", lacs) + dbs(Mid(n, 5, 2)) + IIf(dbs(Mid(n, 5, 2)) = "", "", thou) + _
                                ss(Mid(n, 7, 1)) + IIf(ss(Mid(n, 7, 1)) = "", "", hund) + dbs(Mid(n, 8, 2))


                Case 10 : IntegralValue = Chk100Cr(Mid(n, 1, 3)) + dbs(Mid(n, 4, 2)) + IIf(dbs(Mid(n, 4, 2)) = "", "", lacs) + _
                                dbs(Mid(n, 6, 2)) + IIf(dbs(Mid(n, 6, 2)) = "", "", thou) + ss(Mid(n, 8, 1)) + IIf(ss(Mid(n, 8, 1)) = "", "", hund) + dbs(Mid(n, 9, 2))

                Case 11 : IntegralValue = Chk1000Cr(Mid(n, 1, 4)) + dbs(Mid(n, 5, 2)) + IIf(dbs(Mid(n, 5, 2)) = "", "", lacs) + _
                                dbs(Mid(n, 7, 2)) + IIf(dbs(Mid(n, 7, 2)) = "", "", thou) + ss(Mid(n, 9, 1)) + IIf(ss(Mid(n, 9, 1)) = "", "", hund) + dbs(Mid(n, 10, 2))


            End Select
        End If
    End Function
    'function for sounding 100 to 999 crore
    Public Function Chk100Cr(ByRef n As String) As String
        Chk100Cr = ss(Mid(n, 1, 1)) + IIf(ss(Mid(n, 1, 1)) = "", "", hund) + dbs(Mid(n, 2, 2))
        If (dbs(Mid(n, 2, 2)) = "" And ss(Mid(n, 1, 1)) <> "") Or dbs(Mid(n, 2, 2)) <> "" Then
            Chk100Cr = Chk100Cr + " " + crore
        ElseIf dbs(Mid(n, 2, 2)) = "" Then
            Chk100Cr = Chk100Cr + ""
        End If
    End Function
    

    'function for sounding 1000 to 9999 crore
    Public Function Chk1000Cr(ByRef n As String) As String
        Chk1000Cr = ss(Mid(n, 1, 1)) + IIf(ss(Mid(n, 1, 1)) = "", "", thou) + ss(Mid(n, 2, 1)) + _
        IIf(ss(Mid(n, 2, 1)) = "", "", hund) + dbs(Mid(n, 3, 2))

        If ((dbs(Mid(n, 3, 2)) = "" And ss(Mid(n, 2, 1)) = "") And ss(Mid(n, 1, 1)) <> "") Or (dbs(Mid(n, 3, 2)) <> "" Or ss(Mid(n, 2, 1)) <> "") Then
            Chk1000Cr = Chk1000Cr + " " + crore
        ElseIf dbs(Mid(n, 3, 2)) = "" And ss(Mid(n, 2, 1)) = "" Then
            Chk1000Cr = Chk1000Cr + ""
        End If
    End Function

    'function for converting number figure to indian wording
    'first it filters integral and decimal portions
    Public Function NumInWords(ByRef num As String) As String
        Dim DecPos As Integer
        Dim Integral As String
        Dim Dec As String

        num = Val(num) 'Round(Val(num), 4)

        DecPos = InStr(num, ".")

        If DecPos = 0 Then
            If Len(num) > 10 Then
                NumInWords = "OL of digits!!"
            Else
                NumInWords = IntegralValue(num)
            End If

        Else
            Integral = Mid(num, 1, DecPos - 1)
            Dec = Mid(num, DecPos + 1, 4)
            If Len(Integral) > 11 Then
                NumInWords = "OL!!" & " " & PointValSound(Dec)
            Else
                NumInWords = IntegralValue(Integral) & " " & PointValSound(Dec)
            End If

        End If
    End Function
    'function to authenticate user session
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
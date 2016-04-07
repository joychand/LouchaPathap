
'' Module for: To print ror jamadandi in UniCoded 
'' Author    : Jiten Singh Haobam Scientist B, NIC Manipur
'' Date      : 27/06/2013 [dd/mm/yyyy]
'' Techs.    : (a) AJAX Ridden therby enhancing page rendering to clients
''           : (b) uses parameterized SQL statements safegaurding SQL INJECTIONS
''           : (c) Data Grid to display bulky information in a fair fashion

Imports System.Data
Imports System.Data.SqlClient
'Imports Microsoft.Reporting.WebForms
Imports System.Math
Imports System.IO
Imports System.Text
Imports System.Drawing.Printing
Imports System.Drawing
Imports System.Globalization

Public Class Jama123
    Inherits System.Web.UI.Page
    Protected Shared lastvisit As String
    Dim WithEvents pDoc As PrintDocument
    Dim myReader As StringReader
    Dim line As String = Nothing

    Dim dPrint, noPLOT, noOWNER As String


    'Create a connection
    Dim connectPrj As New SqlConnection(ConfigurationManager.ConnectionStrings("connectionString").ToString)

    Dim tLcCode, tDagNo As String
    Dim vName, vFather, vAddress As String
    Dim pExDag, cExDag As String
    Dim PDag, cDag As String
    Dim pMcase, cMcase As String
    Dim pMcaseNo, cMcaseNo As String
    Dim pTax, cTax As String
    Dim pLClass, cLClass As String
    Dim pArea, cArea As String
    Dim pRev, cRev As String

    Dim pOldPno, cOldPno As String
    Dim pNewPno, cNewPno As String
    Dim txtRev As String
    Dim plotDR As DataRow
    Dim ownerDR As DataRow

    Private Shared ReadOnly Utf8Encoder As Encoding = Encoding.GetEncoding("UTF-8", New EncoderReplacementFallback(String.Empty), New DecoderExceptionFallback())
    Private utf8Text As VariantType
    Protected Shared Ses_Cookie As String
    Protected Shared uid As String


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
        Me.Jama123.Attributes.Add("autocomplete", "off")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.UtcNow)
        Response.Expires = -1
        Try
            'If Ses_Cookie.Equals(Session.SessionID.ToString) And Session.SessionID.ToString.Equals(Request.Cookies("ASP.NET_SessionID").Value.ToString) Then
            If Not Page.IsPostBack Then

                If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
                Dim cmd As New SqlCommand("select userid,role,work_area,office,cir_code from  tbl_users where rtrim(userid) = @uname ", connectPrj)

                With cmd
                    .Parameters.Add(New SqlParameter("@uname", uid))
                End With

                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 And dt.Rows(0).Item("role") = "CSC_USER" Then
                    Session("uid") = dt.Rows(0).Item("userid")
                    Session("userrole") = dt.Rows(0).Item("role")
                    Session("workarea") = dt.Rows(0).Item("work_area")
                    Session("office") = dt.Rows(0).Item("office")
                    Session("cir_code") = dt.Rows(0).Item("cir_code")
                    da.Dispose()
                    connectPrj.Close()
                    Call loadDistrict()


                Else
                    Response.Redirect("~/users/errorsession.aspx")
                End If

            End If
            'Else
            'Response.Redirect("~/users/errorsession.aspx")
            'End If
        Catch ex As Exception
            Response.Redirect("~/users/errorsession.aspx")
        End Try

    End Sub
    Private Sub loadDistrict()

        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim strQuery As String = "select * from UniDistrict order by distcode"
            Dim cmd As New SqlCommand(strQuery, connectPrj)

            Dim dtreader As SqlDataReader
            dtreader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dtreader.HasRows Then
                lDistrict.Items.Clear()
                lDistrict.Items.Add("Select District")
                While dtreader.Read
                    'lDistrict.Items.Add(dtreader.Item("distDesc").ToString & " - " & dtreader.Item("distcode"))
                    lDistrict.Items.Add(New ListItem(dtreader.Item("distDesc"), dtreader.Item("distcode")))
                End While

            Else
                lDistrict.Items.Clear()
                lDistrict.Items.Add("No Data!")
            End If
            connectPrj.Close()
        Catch ex As Exception
            lDistrict.Items.Clear()
            Response.Redirect("~/users/errorsession.aspx")
        End Try
    End Sub
    'Private Sub loadSubDiv()

    '    Try
    '        If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
    '        Dim strQuery As String = "select * from UniSubdiv where distCode=@lc1 order by subDesc"
    '        Dim cmd As New SqlCommand(strQuery, connectPrj)
    '        With cmd
    '            .Parameters.Add(New SqlParameter("@lc1", Right(lDistrict.SelectedItem.Text, 2))) '"07"))
    '        End With

    '        Dim dtreader As SqlDataReader
    '        dtreader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

    '        If dtreader.HasRows Then
    '            lSubDiv.Items.Clear()
    '            lSubDiv.Items.Add("Select Sub-Division")
    '            While dtreader.Read
    '                lSubDiv.Items.Add(dtreader.Item("subDesc").ToString & " - " & dtreader.Item("subcode"))
    '            End While

    '        Else
    '            lSubDiv.Items.Clear()
    '            lSubDiv.Items.Add("No Data!")
    '        End If
    '        connectPrj.Close()
    '    Catch ex As Exception
    '        lSubDiv.Items.Clear()
    '        lSubDiv.Items.Add("Page error!")
    '    End Try
    'End Sub
    Private Sub loadCircle()

        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim strQuery As String = "select * from UniCircle  where distCode=@lc1 order by cirDesc"
            Dim cmd As New SqlCommand(strQuery, connectPrj)
            With cmd
                '.Parameters.Add(New SqlParameter("@lc1", Right(lDistrict.SelectedItem.Text, 2))) '"07"))
                .Parameters.Add(New SqlParameter("@lc1", lDistrict.SelectedItem.Value))
            End With
            Dim dtreader As SqlDataReader
            dtreader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dtreader.HasRows Then
                lCircle.Items.Clear()
                lCircle.Items.Add("Select Circle")
                'While dtreader.Read
                '    lCircle.Items.Add(dtreader.Item("cirDesc") & " - " & dtreader.Item("circode"))
                'End While
                While dtreader.Read
                    Dim circlecode As String = dtreader.Item("subcode").ToString + dtreader.Item("circode")
                    'lCircle.Items.Add(dtreader.Item("cirDesc") & " - " & dtreader.Item("circode"))
                    'lCircle.Items.Add(dtreader.Item("cirDesc") & " - " & circlecode)
                    lCircle.Items.Add(New ListItem(dtreader.Item("cirDesc"), circlecode))
                End While

            Else
                lCircle.Items.Clear()
                lCircle.Items.Add("No Data!")
            End If
            connectPrj.Close()
        Catch ex As Exception
            lCircle.Items.Clear()
            Response.Redirect("~/users/errorsession.aspx")  'lCircle.Items.Add("Page error!")
        End Try
    End Sub
    Private Sub loadVilla()

        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            'Dim strQuery As String = "select * from UniLocation  where substring(locCd,1,2)=@lc1 and substring(locCd,3,2)=@lc2 and substring(locCd,5,3)= @lc3 order by locDesc"
            Dim strQuery As String = "select * from UniLocation  where substring(locCd,1,2)=@lc1 and substring(locCd,3,5)= @lc3 order by locDesc"
            Dim cmd As New SqlCommand(strQuery, connectPrj)

            'With cmd
            '    .Parameters.Add(New SqlParameter("@lc1", Right(lDistrict.SelectedItem.Text, 2))) '"07"))
            '    .Parameters.Add(New SqlParameter("@lc2", Right(lSubDiv.SelectedItem.Text, 2))) '"02"))
            '    .Parameters.Add(New SqlParameter("@lc3", Right(lCircle.SelectedItem.Text, 3))) '"002"))
            'End With
            With cmd
                '.Parameters.Add(New SqlParameter("@lc1", Right(lDistrict.SelectedItem.Text, 2))) '"07"))
                '.Parameters.Add(New SqlParameter("@lc3", Right(lCircle.SelectedItem.Text, 5))) '"002"))
                .Parameters.Add(New SqlParameter("@lc1", lDistrict.SelectedItem.Value))
                .Parameters.Add(New SqlParameter("@lc3", lCircle.SelectedItem.Value))
            End With

            Dim dtreader As SqlDataReader
            dtreader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dtreader.HasRows Then
                lVillage.Items.Clear()
                lVillage.Items.Add("Select Village")
                While dtreader.Read
                    'lVillage.Items.Add(dtreader.Item("locDesc") & " - " & dtreader.Item("locCd"))
                    lVillage.Items.Add(New ListItem(dtreader.Item("locdesc"), dtreader.Item("locCd")))
                End While
            Else
                lVillage.Items.Clear()
                lVillage.Items.Add("No Data!")
            End If
            connectPrj.Close()
        Catch ex As Exception
            lVillage.Items.Clear()
            Response.Redirect("~/users/errorsession.aspx")  'lVillage.Items.Add("Page error!")
        End Try
    End Sub



    Private Sub getPlotOwner()
        Try
            'connection open
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()


            ''cleaning uniplotowner table ''move here/commented/changed on 04-09-2014
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim delcmd As New SqlCommand("delete from uniplotowner", connectPrj)
            delcmd.ExecuteNonQuery()



            Dim cmd As New SqlCommand("select * from uniplot where locCd= @lc and newPattaNo = @pno and  newDagNo = @dno order by newdagno", connectPrj) 'and newDagNo=@dgno
            With cmd
                .Parameters.Add(New SqlParameter("@lc", tLcCode))
                .Parameters.Add(New SqlParameter("@pno", txtPattaNo.Text))
                .Parameters.Add(New SqlParameter("@dno", txtDagNo.Text))
            End With
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            If dt.Rows.Count < 1 Then
                noPLOT = "NA"
                lberror.Text = "No data available for the given parameters..."

                ' ''cleaning uniplotowner table ''/commented/changed on 04-09-2014
                'If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
                'Dim delcmd As New SqlCommand("delete from uniplotowner", connectPrj)
                'delcmd.ExecuteNonQuery()

                'da.Dispose()
                connectPrj.Close()
                Exit Sub
            End If

            noPLOT = "FOUND"
            ''init. for not repeating old by new patta no in case of multiple dags
            pOldPno = IIf(IsDBNull(dt.Rows(0).Item("exNewPatta")) Or dt.Rows(0).Item("exNewPatta") = "", dt.Rows(0).Item("newpattano"), dt.Rows(0).Item("exNewPatta"))
            ''pOldPno = IIf(String.IsNullOrEmpty(dt.Rows(0).Item("exNewPatta")), dt.Rows(0).Item("newpattano"), dt.Rows(0).Item("exNewPatta"))
            'If (IsDBNull(dt.Rows(0).Item("exNewPatta")) = True) OrElse (dt.Rows(0).Item("exNewPatta") = "") Then
            '    pOldPno = dt.Rows(0).Item("newpattano")
            'Else
            '    pOldPno = dt.Rows(0).Item("exNewPatta")
            'End If
            cOldPno = ""
            pNewPno = dt.Rows(0).Item("newpattano")
            cNewPno = ""
            vName = ""


            For Each Me.plotDR In dt.Rows
                'tDagNo = plotDR.Item("newdagno")

                ''init. for not repeating
                pExDag = plotDR.Item("exnewdag")
                cExDag = ""

                PDag = plotDR.Item("newdagno")
                cDag = ""
                pMcase = plotDR.Item("ordno")
                'pMcase = IIf(IsDBNull(plotDR.Item("ordno")), "", plotDR.Item("ordno"))
                cMcase = ""
                pMcaseNo = plotDR.Item("ordno")
                'pMcaseNo = IIf(IsDBNull(plotDR.Item("ordno")), "", plotDR.Item("ordno"))
                cMcaseNo = ""
                pTax = txtRev
                cTax = ""

                pRev = IIf(IsDBNull(plotDR.Item("Revenue")), "", plotDR.Item("Revenue"))
                cRev = ""
                pArea = IIf(IsDBNull(plotDR.Item("Area_acre")), "", plotDR.Item("Area_acre"))
                cArea = ""
                pLClass = IIf(IsDBNull(plotDR.Item("LandClass")), "", plotDR.Item("LandClass"))
                cLClass = ""


                ' ''cleaning uniplotowner table commented/changed on 04-09-2014
                'If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
                'Dim delcmd As New SqlCommand("delete from uniplotowner", connectPrj)
                'delcmd.ExecuteNonQuery()

                ''calling owner fetch module
                Call getOwnerNsaveToUniplotOwner(plotDR.Item("newdagno").ToString)


            Next plotDR

            da.Dispose()
            connectPrj.Close()
            lberror.Text = ""
        Catch ex As Exception
            lberror.ForeColor = Drawing.Color.Red
            Response.Redirect("~/users/errorsession.aspx")  'lberror.Text = "Page error!"
            connectPrj.Close()
        End Try
    End Sub
    Private Sub getOwnerNsaveToUniplotOwner(ByVal tdagno As String)
        Try
            'connection open
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()

            Dim ocmd As New SqlCommand("select * from uniowner where locCd= @lc and newPattaNo = @pno and newDagNo=@dgno order by newdagno", connectPrj)
            With ocmd
                .Parameters.Add(New SqlParameter("@lc", tLcCode))
                .Parameters.Add(New SqlParameter("@pno", txtPattaNo.Text.Trim))
                .Parameters.Add(New SqlParameter("@dgno", tdagno.ToString))
            End With
            Dim oda As New SqlDataAdapter(ocmd)
            Dim odt As New DataTable()
            oda.Fill(odt)

            If odt.Rows.Count < 1 Then
                noOWNER = "NA"
                lberror.Text = "No data available for the given parameters..."

                ''cleaning uniplotowner table
                If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
                Dim delcmd As New SqlCommand("delete from uniplotowner", connectPrj)
                delcmd.ExecuteNonQuery()

                oda.Dispose()
                connectPrj.Close()
                Exit Sub
            End If

            noOWNER = "FOUND"


            ''ready for inserting records to uniplotowner
            For Each Me.ownerDR In odt.Rows

                '' tax revenue calculation based on land class
                Dim vLandClass As String
                vLandClass = plotDR.Item("LandClass")
                If plotDR.Item("Area_acre") >= 0.01 And plotDR.Item("Area_acre") <= 0.3 Then
                    txtRev = 10
                Else

                    Select Case vLandClass
                        Case Is = "ফৌরেল" ''phouren
                            txtRev = Round(15.48 * 2 * plotDR.Item("Area_acre"), 2)
                        Case Is = "ইংখোল" ''ingkhol
                            txtRev = Round(16.88 * 2 * plotDR.Item("Area_acre"), 2)
                        Case Is = "তাউথবি" ''taothabi
                            txtRev = Round(12.38 * 2 * plotDR.Item("Area_acre"), 2)
                        Case Is = "অঙন্ফৌ"  ''Anganphou
                            txtRev = Round(13.92 * 2 * plotDR.Item("Area_acre"), 2)
                        Case Else
                            txtRev = ""
                    End Select
                End If
                '' tax revenue calculation here--


                vName = ownerDR.Item("Name")
                vFather = ownerDR.Item("father")
                vAddress = ownerDR.Item("address")

                ''--for Name father and Address non repeatition--
                Dim p2v, p3v, p4v, p41v, paddV, p5v, p6v, p8v, p9v, p10v, p11v, p14v, p21v, p31v, p51v, p81v, p101v, p141v As String
                Dim x, y As Integer
                p4v = "" '' name attribute
                p41v = "" '' father attribe
                paddV = "" '' address attribe
                x = 1
                y = 1
                Dim poselect As New SqlCommand("select padd from uniplotowner Where LocCd = @p1 AND pattaNo = @p2" & _
                " and p4 =@p3 and p41 =@p4 ", connectPrj)

                With poselect
                    .Parameters.Add(New SqlParameter("@p1", tLcCode))
                    .Parameters.Add(New SqlParameter("@p2", txtPattaNo.Text))
                    .Parameters.Add(New SqlParameter("@p3", vName))
                    .Parameters.Add(New SqlParameter("@p4", vFather))
                End With

                If IsNothing(poselect.ExecuteScalar) Then
                    If Len(Trim(ownerDR.Item("Name"))) > 150 Then
                        Do While Mid(ownerDR.Item("Name"), 1, 1) <> " "
                            x = x - 1
                        Loop
                        p4v = Mid((Left(Trim(ownerDR.Item("Name")), 1) + "-"), (Len(Trim(Str(Val(ownerDR.Item("Name")))))))  'str(NoRs!OwnNo) + ") " +
                        p41v = Mid(ownerDR.Item("Name"), x, 100)
                        paddV = Mid(ownerDR.Item("address"), 1, 100)
                    Else
                        p4v = Mid((Trim(ownerDR.Item("Name"))), (Len(Trim(Str(Val(ownerDR.Item("Name")))))), 100) 'str(NoRs!OwnNo) + ") " +
                        p41v = Mid(ownerDR.Item("father"), 1, 100)
                        paddV = Mid(ownerDR.Item("address"), 1, 150)
                    End If
                Else
                    Dim tt As String = poselect.ExecuteScalar
                    If Len(vAddress) > Len(tt) Then
                        ''updating the lengthiest address to address field of plotowner 
                        Dim poupdate As New SqlCommand("update uniplotowner set padd= @p1 Where LocCd = @p2" & _
                        " AND pattaNo = @p3 and p4 =@p4 and p41 =@p5 ", connectPrj)
                        With poupdate
                            With poselect
                                .Parameters.Add(New SqlParameter("@p1", vAddress))
                                .Parameters.Add(New SqlParameter("@p2", tLcCode))
                                .Parameters.Add(New SqlParameter("@p3", txtPattaNo.Text))
                                .Parameters.Add(New SqlParameter("@p4", vName))
                                .Parameters.Add(New SqlParameter("@p5", vFather))
                            End With
                        End With

                        poupdate.ExecuteNonQuery()
                        paddV = vAddress
                    End If
                End If
                ''--END for Name father and Address non repeatition--

                p2v = IIf(pOldPno = cOldPno, "", (IIf(IsDBNull(plotDR.Item("oldpattano")) Or plotDR.Item("oldpattano") = "", "", Left(Trim(plotDR.Item("oldpattano")), 10))))
                p3v = IIf(pOldPno = cOldPno, "", IIf(IsDBNull(plotDR.Item("exNewPatta")) Or plotDR.Item("exNewPatta") = "", plotDR.Item("newpattano"), plotDR.Item("exNewPatta"))) 'IIf(isdbnull(!exnewpatta), "", !exnewpatta
                p5v = IIf(pExDag = cExDag, "", IIf(IsDBNull(plotDR.Item("exnewdag")), "", plotDR.Item("exnewdag"))) ''IIf(isdbnull(!exnewdag) Or !exnewdag = "", IIf(PDag = cDag, "", !newdagno), !exnewdag)

                p6v = IIf(pTax = cTax, "", txtRev)
                p8v = IIf(pArea = cArea, 0, plotDR.Item("Area_acre"))
                p9v = IIf(pLClass = cLClass, "", Mid(plotDR.Item("LandClass"), 1, 10))
                p10v = IIf(pArea = cArea, 0, plotDR.Item("Area_acre"))
                p11v = IIf(pRev = cRev, 0, plotDR.Item("Revenue"))
                y = 1
                If Trim(plotDR.Item("ordno")) <> "" Then
                    Do While (Val(Mid(plotDR.Item("ordno"), y, 1)) = 0 And Mid(plotDR.Item("ordno"), y, 1) <> "")
                        y = y + 1
                    Loop
                    y = y - 1
                End If



                p14v = IIf(pMcase = cMcase, "", IIf(IsDBNull(plotDR.Item("ordno")) Or Len(plotDR.Item("ordno")) = 0, "", Left(Trim(plotDR.Item("ordno")), y)))
                'p14v = IIf(pMcase = cMcase, "", IIf(IsDBNull(plotDR.Item("ordno")) = True OrElse plotDR.Item("ordno") = "", "", Left(Trim(plotDR.Item("ordno")), y)))
                p141v = IIf(pMcaseNo = cMcaseNo, "", IIf(IsDBNull(plotDR.Item("ordno")) Or Len(plotDR.Item("ordno")) = 0, "", Mid(plotDR.Item("ordno"), (y + 1), 20)))
                'p141v = IIf(pMcaseNo = cMcaseNo, "", IIf(IsDBNull(plotDR.Item("ordno")) = True OrElse plotDR.Item("ordno") = "", "", Mid(plotDR.Item("ordno"), (y + 1), 20)))

                p21v = IIf(IsDBNull(plotDR.Item("oldpattano")) Or plotDR.Item("oldpattano") = "", "", Mid(Trim(plotDR.Item("oldpattano")), 11, 10))


                p31v = IIf(pNewPno = cNewPno, "", plotDR.Item("newpattano"))


                p51v = IIf(PDag = cDag, "", plotDR.Item("newdagno"))
                p81v = IIf(pArea = cArea, 0, plotDR.Item("area"))
                p101v = IIf(pArea = cArea, 0, plotDR.Item("area"))

                ''why we name p1,p2,p3 so on is reflection of coloumn no. from patta sheet..
                Dim insertcmd As New SqlCommand("insert into uniplotowner(loccd,pattano,p2,p3,p4,p41,p5,p6,p8,p9,p10,p11," & _
                                                " p14,p21,p31,p51,p81,p101,p141,padd)" & _
                                                " values(@p1,@p2,@p3," & _
                                                " @p4,@p5,@p6,@p7,@p8," & _
                                                " @p9,@p10,@p11," & _
                                                " @p12,@p13,@p14," & _
                                                " @p15,@p16,@p17," & _
                                                " @p18,@p19,@p20)", connectPrj)


                With insertcmd
                    .Parameters.Add(New SqlParameter("@p1", ownerDR.Item("loccd").ToString))
                    .Parameters.Add(New SqlParameter("@p2", txtPattaNo.Text))
                    .Parameters.Add(New SqlParameter("@p3", p2v))
                    .Parameters.Add(New SqlParameter("@p4", p3v))
                    .Parameters.Add(New SqlParameter("@p5", p4v))
                    .Parameters.Add(New SqlParameter("@p6", p41v))

                    .Parameters.Add(New SqlParameter("@p7", p5v))
                    .Parameters.Add(New SqlParameter("@p8", p6v))
                    .Parameters.Add(New SqlParameter("@p9", p8v))
                    .Parameters.Add(New SqlParameter("@p10", p9v))
                    .Parameters.Add(New SqlParameter("@p11", p10v))
                    .Parameters.Add(New SqlParameter("@p12", p11v))


                    .Parameters.Add(New SqlParameter("@p13", p14v))
                    .Parameters.Add(New SqlParameter("@p14", p21v))
                    .Parameters.Add(New SqlParameter("@p15", p31v))
                    .Parameters.Add(New SqlParameter("@p16", p51v))
                    .Parameters.Add(New SqlParameter("@p17", p81v))
                    .Parameters.Add(New SqlParameter("@p18", p101v))
                    .Parameters.Add(New SqlParameter("@p19", p141v))
                    .Parameters.Add(New SqlParameter("@p20", paddV))

                End With

                insertcmd.ExecuteNonQuery()

                ''sfift previouse record values to current variables
                cExDag = pExDag
                cDag = PDag
                cMcase = pMcase
                cMcaseNo = pMcaseNo
                cTax = pTax
                cRev = pRev
                cArea = pArea
                cLClass = pLClass
                ''for not repeating old by new patta no in case of multiple dags
                cOldPno = pOldPno
                cNewPno = pNewPno
            Next ownerDR  ''end loop for owner records

            oda.Dispose()
            connectPrj.Close()
            lberror.Text = ""
        Catch ex As Exception
            lberror.ForeColor = Drawing.Color.Red
            connectPrj.Close()
            Response.Redirect("~/users/errorsession.aspx")
        End Try
        'for squeezing recs of empty newdagno
        Call SqueezRecs()

    End Sub


    Private Sub SqueezRecs()

        Try
            'connection open
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()

            Dim cmd As New SqlCommand("select * from uniplotowner where locCd= @lc and PattaNo = @pno and   not(p4 = @p4) and p51 = @p51", connectPrj) 'and newDagNo=@dgno
            With cmd
                .Parameters.Add(New SqlParameter("@lc", tLcCode))
                .Parameters.Add(New SqlParameter("@pno", txtPattaNo.Text.Trim))
                .Parameters.Add(New SqlParameter("@p4", ""))
                .Parameters.Add(New SqlParameter("@p51", ""))
            End With
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            '' to be skipped set
            Dim cmd2 As New SqlCommand("select * from uniplotowner where locCd= @lc and PattaNo = @pno and p4 = @p4 and not(p51 = @p51) ", connectPrj) 'and newDagNo=@dgno
            With cmd2
                .Parameters.Add(New SqlParameter("@lc", tLcCode))
                .Parameters.Add(New SqlParameter("@pno", txtPattaNo.Text.Trim))
                .Parameters.Add(New SqlParameter("@p4", ""))
                .Parameters.Add(New SqlParameter("@p51", ""))
            End With
            Dim da2 As New SqlDataAdapter(cmd)
            Dim dtSkip As New DataTable()
            da2.Fill(dtSkip)

            If dt.Rows.Count < 1 Or dtSkip.Rows.Count < 1 Then Exit Sub
            Dim kk As Integer
            For kk = 0 To dt.Rows.Count - 1
                If dtSkip.Rows.Count > 0 Then

                    dt.Rows(kk).Item("p5") = dtSkip.Rows(kk).Item("p5")
                    dt.Rows(kk).Item("p51") = dtSkip.Rows(kk).Item("p51")
                    dt.Rows(kk).Item("p6") = dtSkip.Rows(kk).Item("p6")
                    dt.Rows(kk).Item("p8") = dtSkip.Rows(kk).Item("p8")
                    dt.Rows(kk).Item("p81") = dtSkip.Rows(kk).Item("p81")
                    dt.Rows(kk).Item("p9") = dtSkip.Rows(kk).Item("p9")
                    dt.Rows(kk).Item("p10") = dtSkip.Rows(kk).Item("p10")
                    dt.Rows(kk).Item("p14") = dtSkip.Rows(kk).Item("p14")
                    dt.Rows(kk).Item("p141") = dtSkip.Rows(kk).Item("p141")

                    dt.AcceptChanges()
                    dtSkip.Rows(kk).Delete()
                Else

                End If

            Next kk
            ''deleting rec 4 empty name, father and address
            Dim cmddel As New SqlCommand("delete from uniplotowner where p4=@p4 and p41=@p41 and p51=@p51", connectPrj)
            With cmddel
                .Parameters.Add(New SqlParameter("@p4", ""))
                .Parameters.Add(New SqlParameter("@p51", ""))
                .Parameters.Add(New SqlParameter("@p41", ""))
            End With


            cmddel.ExecuteNonQuery()

            connectPrj.Close()
            lberror.Text = ""
        Catch ex As Exception
            lberror.ForeColor = Drawing.Color.Red
            lberror.Text = "Page error!"
            connectPrj.Close()
            Response.Redirect("~/users/errorsession.aspx")
        End Try


    End Sub



    Protected Sub btnHtmlReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnHtmlReport.Click

        Page.Validate()
        If Page.IsValid() Then


            tLcCode = lVillage.SelectedItem.Value

            If txtPattaNo.Text.Trim = "" Then
                Exit Sub
            End If


            Call getPlotOwner()
            ''make text report of jamadandi
            Dim builder1 As StringBuilder = New StringBuilder("").AppendLine()
            'builder1.Append(" ".PadRight(20) & lDistrict.SelectedValue.ToString.PadRight(40) & " ".PadRight(2) & lSubDiv.SelectedValue.ToString.PadRight(25) & lCircle.SelectedValue.ToString.PadRight(38) & " ".PadRight(20) & lVillage.SelectedValue.ToString.PadRight(25) & Now.Year.ToString.PadRight(4))
            builder1.Append(" ".PadRight(20) & lDistrict.SelectedValue.ToString.PadRight(40) & " ".PadRight(2) & lCircle.SelectedValue.ToString.PadRight(38) & " ".PadRight(20) & lVillage.SelectedValue.ToString.PadRight(25) & Now.Year.ToString.PadRight(4))
            builder1.AppendLine()
            Session("txtHead") = ES(builder1.ToString)
            Session("hashPno") = md5Fun.md5Encr(md5Fun.md5Encr(txtPattaNo.Text) & md5Fun.md5Encr(GetIPAddress()))

            Session("Dist") = lDistrict.SelectedItem.Text
            Session("Circle") = lCircle.SelectedItem.Text
            Session("Village") = lVillage.SelectedItem.Text



            If noPLOT = "NA" Then
                lberror.Text = "No record found!"
            Else

                Call LogAdTrails("Process", "Success", "Jamabandi Generation of LocCd:" & tLcCode.ToString & " and PattaNo:" & txtPattaNo.Text)
                'Response.Redirect("./JamaPrint123.aspx")
                Response.Redirect("~/users/_CSC/JamaPrint123.aspx")
                'Dim script As String = "<script type=""text/javascript"">window.open('http://localhost:2727/users/_CSC/JamaPrint123.aspx');</script>"
                'ClientScript.RegisterStartupScript(Me.GetType, "openWindow", script)
            End If
        End If
    End Sub
    
    Private Sub LogAdTrails(ByVal Ltype As String, ByVal Lstatus As String, ByVal PWhat As String)

        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim inscmd As New SqlCommand("insert into AdTrails(userid,dateNtime,IPaddress,LoginType,LoginStatus,ProcessWhat,logID)" & _
            " values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", connectPrj)

            With inscmd
                .Parameters.Add(New SqlParameter("@p1", Session("uid").ToString))
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
   
   
    Private Function ES(ByVal s As String) As String
        ES = ""
        Try
            Dim b As Byte() = System.Text.Encoding.UTF32.GetBytes(s)
            ES = Convert.ToBase64String(b, 0, b.LongLength)
        Catch ex As Exception
            Response.Redirect("~/users/errorsession.aspx")
        End Try

    End Function

    Protected Sub lCircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lCircle.SelectedIndexChanged
        Call loadVilla()
        lberror.Text = ""
        txtPattaNo.Text = ""


    End Sub


    Protected Sub lDistrict_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lDistrict.SelectedIndexChanged
        'Call loadSubDiv()
        Call loadCircle()
        lVillage.Items.Clear()
        lberror.Text = ""
        txtPattaNo.Text = ""

    End Sub

    'Protected Sub lSubDiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lSubDiv.SelectedIndexChanged
    '    'Call loadCircle()
    'End Sub


    Protected Sub lVillage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lVillage.SelectedIndexChanged
        lberror.Text = ""
        txtPattaNo.Text = ""

    End Sub
    ' Function to check authentication of the session
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
            Dim delcmd As New SqlCommand("delete from tbl_usr_session where (userid=@userid and session_id=@sessionID and user_agent=@user_agent) or (DateDiff(minute,first_visit,GetDate())>=20)", connectPrj)

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
''Private Sub rdlcReport()
''    lberror.Text = ""
''    If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()

''    Dim cmd As New SqlCommand("SELECT a.Dis, a.Sub, a.Cir, a.Vil, b.p3 , b.p2, b.p10, b.p8," & _
''                                " b.p5, b.p9, b.p14, b.p11, b.p4, b.p31, b.p21, b.p101, b.p81, b.p51, b.p141, b.p41 " & _
''                                " From unilocation a, uniplotowner b  where a.loccd=b.loccd", connectPrj)
''    'cmd.Parameters.Add(New SqlParameter("@ptsno", txtPattaNo.Text))
''    'cmd.Parameters.Add(New SqlParameter("@ptsyear", txtYear.Text))
''    'cmd.Parameters.Add(New SqlParameter("@psubdiv", lSubDiv.SelectedValue & " Sub-division"))

''    Dim da As New SqlDataAdapter(cmd)
''    Dim ds As New DataSet
''    da.Fill(ds, "dataplotowner")

''    If ds.Tables(0).Rows.Count = 0 Then
''        ReportViewer2.Visible = False
''        lberror.Text = "No record found"
''        Exit Sub
''    End If


''    connectPrj.Close()

''    ReportViewer2.Visible = True

''    'dtR = "Employee's Date of Retirement is " & Format(dtDOR(StrtoDate(dtR)), "dd-MMM-yyyy")
''    'Dim Rparam As ReportParameter() = New ReportParameter(0) {}
''    'Rparam(0) = New ReportParameter("dtRetire", dtR)


''    ReportViewer2.ProcessingMode = ProcessingMode.Local
''    ReportViewer2.ShowPrintButton = True
''    ReportViewer2.ShowToolBar = True
''    ReportViewer2.ShowExportControls = True
''    ' Dim rPath As String = MapPath("RepFolder/")
''    ReportViewer2.LocalReport.ReportPath = "./RepFolder/rptror.rdlc"
''    'ReportViewer1.LocalReport.SetParameters(Rparam)

''    Dim rds As ReportDataSource = New ReportDataSource()
''    rds.Name = "dsROR_DataTable1"
''    rds.Value = ds.Tables(0)

''    ReportViewer2.LocalReport.DataSources.Clear()
''    ReportViewer2.LocalReport.DataSources.Add(rds)
''    ReportViewer2.LocalReport.Refresh()
''End Sub

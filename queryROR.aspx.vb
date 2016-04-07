'' Module for: To fetch and display plot and owner details of UniCoded LR data
'' Author    : Jiten Singh Haobam Scientist B, NIC Manipur
'' Date      : 27/09/2011 [dd/mm/yyyy]
'' Techs.    : (a) AJAX Ridden therby enhancing page rendering to clients
''           : (b) uses parameterized SQL statements safegaurding SQL INJECTIONS
''           : (c) Data Grid to display bulky information in a fair fashion

Imports System.Data
Imports System.Data.SqlClient
'Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.Text.RegularExpressions

'Create a connection
Partial Public Class queryROR
    Inherits System.Web.UI.Page
    Dim connectPrj As New SqlConnection(ConfigurationManager.ConnectionStrings("connectionString").ToString)
    Dim tLcCode, tDagNo As String

    Private Sub queryROR_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not Request.Cookies("TM") Is Nothing Then
            ViewStateUserKey = Session.SessionID.ToString
        End If
    End Sub

    

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            cap1.Text = ""
            cap2.Text = ""
            Call loadDistrict()
        End If

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
                    lDistrict.Items.Add(New ListItem(dtreader.Item("distDesc"), dtreader.Item("distcode")))
                End While
            Else
                lDistrict.Items.Clear()
                lDistrict.Items.Add("No Data!")
            End If
            connectPrj.Close()
        Catch ex As Exception
            lDistrict.Items.Clear()
            lDistrict.Items.Add("Page error!")
        End Try
    End Sub
   
    Private Sub loadCircle()
        'MsgBox(lDistrict.SelectedItem.Value)
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            'Dim strQuery As String = "select * from UniCircle  where distCode=@lc1 and subCode=@lc2  order by cirDesc"
            Dim strQuery As String = "select * from UniCircle  where distCode=@lc1 order by cirDesc"
            Dim cmd As New SqlCommand(strQuery, connectPrj)
            'With cmd
            '    .Parameters.Add(New SqlParameter("@lc1", Right(lDistrict.SelectedItem.Text, 2))) '"07"))
            '    .Parameters.Add(New SqlParameter("@lc2", Right(lSubDiv.SelectedItem.Text, 2))) '"02"))

            With cmd
                '.Parameters.Add(New SqlParameter("@lc1", Right(lDistrict.SelectedItem.Text, 2))) '"07"))
                .Parameters.Add(New SqlParameter("@lc1", lDistrict.SelectedValue))

            End With

            Dim dtreader As SqlDataReader
            dtreader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dtreader.HasRows Then
                lCircle.Items.Clear()
                lCircle.Items.Add("Select Circle")
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
            lCircle.Items.Add("Page error!")
        End Try
    End Sub
    Private Sub loadVilla()
        'MsgBox(lCircle.SelectedValue.Trim)
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            'Dim strQuery As String = "select * from UniLocation  where substring(locCd,1,2)=@lc1 and substring(locCd,3,2)=@lc2 and substring(locCd,5,3)= @lc3 order by locDesc"
            Dim strQuery As String = "select * from UniLocation  where substring(locCd,1,2)=@lc1 and substring(locCd,3,5)= @lc3 order by locDesc"
            Dim cmd As New SqlCommand(strQuery, connectPrj)

            'With cmd
            '    .Parameters.Add(New SqlParameter("@lc1", Right(lDistrict.SelectedItem.Text, 2))) '"07"))
            '    .Parameters.Add(New SqlParameter("@lc2", Right(lSubDiv.SelectedItem.Text, 2))) '"02"))
            '    .Parameters.Add(New SqlParameter("@lc3", Right(lCircle.SelectedItem.Text, 3))) '"002"))
            With cmd
                '.Parameters.Add(New SqlParameter("@lc1", Right(lDistrict.SelectedItem.Text, 2))) '"07"))
                .Parameters.Add(New SqlParameter("@lc1", lDistrict.SelectedValue))
                '.Parameters.Add(New SqlParameter("@lc3", Right(lCircle.SelectedItem.Text, 5))) '"002"))
                .Parameters.Add(New SqlParameter("@lc3", lCircle.SelectedValue.Trim))
            End With

            Dim dtreader As SqlDataReader
            dtreader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dtreader.HasRows Then
                lVillage.Items.Clear()
                lVillage.Items.Add("Select Village")
                While dtreader.Read
                    'lVillage.Items.Add(dtreader.Item("locDesc") & " - " & dtreader.Item("locCd"))
                    'lVillage.Items.Add(dtreader.Item("locDesc"))
                    'Me.dlCentre2.Items.Add(New ListItem(dr.Item("District"), dr.Item("DistOrder")))
                    lVillage.Items.Add(New ListItem(dtreader.Item("locDesc"), dtreader.Item("locCd")))
                End While
            Else
                lVillage.Items.Clear()
                lVillage.Items.Add("No Data!")
            End If
            connectPrj.Close()
        Catch ex As Exception
            lVillage.Items.Clear()
            lVillage.Items.Add("Page error!")
        End Try
    End Sub

    Private Sub apearPlot()
        tDagNo = ""
        Dim sql1 As String = ""
        sql1 = "select newdagno,OldPattaNo,NewPattaNo,landClass,area,area_acre from  uniplot where locCd= @lc and newPattaNo = @pno and newDagNo=@dgno  order by newdagno"
        Try
            'connection open
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open() '
            Dim cmd As New SqlCommand(sql1, connectPrj)

            With cmd
                .Parameters.Add(New SqlParameter("@lc", tLcCode))
                .Parameters.Add(New SqlParameter("@pno", txtPattaNo.Text))
                .Parameters.Add(New SqlParameter("@dgno", txtNewDagNo.Text))
            End With

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            Dim dt As New DataSet
            da.Fill(dt)
            If dt.Tables(0).Rows.Count > 0 Then
                Me.Button2.Visible = True
                lberror.Text = "Data shown is as on date: " & dt_effect_data()
                cap1.Text = "Plot Details..."
                tDagNo = dt.Tables(0).Rows(0).Item("NewdagNo").ToString
            Else
                Me.Button2.Visible = False
                GridViewPlot.DataSource = Nothing
                GridViewPlot.DataBind()
                lblRecord.Text = "No Record found..."
                lberror.Text = ""
                connectPrj.Close()
                Exit Sub
            End If

            Session("gridplot") = dt
            GridViewPlot.DataSource = Nothing
            GridViewPlot.DataSource = dt
            GridViewPlot.DataBind()
            da.Dispose()
            cap1.Text = ""
            lblRecord.Text = ""
           
            'connection close
            connectPrj.Close()

        Catch ex As Exception
            lberror.Text = "Page error!"
        End Try
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
    Private Sub apearOwner()
        Dim sql1 As String = ""
        sql1 = "select ownno,newdagno,replace(name,'N','') as name,replace(father,'N','') as father,replace(address,'N','') as address from  uniOwner where locCd= @lc and newPattaNo = @pno and newDagNo=@dgno order by ownno"
        Try
            'connection open
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim cmd As New SqlCommand(sql1, connectPrj)

            With cmd
                .Parameters.Add(New SqlParameter("@lc", tLcCode))
                .Parameters.Add(New SqlParameter("@pno", txtPattaNo.Text))
                .Parameters.Add(New SqlParameter("@dgno", tDagNo.ToString))
            End With

            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            Dim dt As New DataSet
            da.Fill(dt)

            If dt.Tables(0).Rows.Count > 0 Then
                Me.Button2.Visible = True
                GridViewOwner.Visible = True
                cap2.Text = " Pattadar Details..."
                lberror.Text = "Data shown is as on date: " & dt_effect_data()
            Else
                
                GridViewOwner.DataSource = Nothing
                GridViewOwner.DataBind()
                Me.Button2.Visible = False

                lberror.Text = ""
                lblRecord.Text = "No Record found..."
                connectPrj.Close()
                Exit Sub
            End If

            Session("gridown") = dt
            GridViewOwner.DataSource = Nothing
            GridViewOwner.DataSource = dt
            GridViewOwner.DataBind()
            da.Dispose()
            cap2.Text = ""
            lblRecord.Text = ""
           
            'connection close
            connectPrj.Close()

        Catch ex As Exception
            lberror.Text = "Page error!"
            'MsgBox(ex.ToString)
        End Try
    End Sub
    


    Protected Sub btnCheck_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCheck.Click
        lberror.Text = ""
        Page.Validate()
        If (Page.IsValid) Then


            'tLcCode = Right(lVillage.SelectedItem.Text, 10)
            tLcCode = lVillage.SelectedValue


            If txtPattaNo.Text.Trim = "" Then
                GridViewPlot.DataSource = Nothing
                GridViewPlot.DataBind()
                GridViewOwner.DataSource = Nothing
                GridViewOwner.DataBind()
                cap1.Text = ""
                cap2.Text = ""
                Exit Sub
            End If
            Call apearPlot()
            Call apearOwner()

            'Session("District") = lDistrict.SelectedItem.Text.ToString.Substring(0, lDistrict.SelectedItem.Text.ToString.IndexOf("-"))
            'Session("Circle") = lCircle.SelectedItem.Text.ToString.Substring(0, lCircle.SelectedItem.Text.ToString.IndexOf("-"))
            'Session("Village") = lVillage.SelectedItem.Text.ToString.Substring(0, lVillage.SelectedItem.Text.ToString.IndexOf("-"))
            'Session("Village") = lVillage.SelectedItem.ToString()
            'MsgBox(lVillage.SelectedItem.ToString())
            'Session("PattaNo") = txtPattaNo.Text.ToString
            'Dim 
            'MsgBox(lCircle.SelectedItem.Text.ToString.Substring(0, lDistrict.SelectedItem.Text.ToString.IndexOf("-")))
            Session("District") = lDistrict.SelectedItem.Text
            Session("Circle") = lCircle.SelectedItem.Text
            Session("Village") = lVillage.SelectedItem.Text
            'MsgBox(lCircle.SelectedItem.Text)

        End If
    End Sub


    Protected Sub lVillage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lVillage.SelectedIndexChanged
        'tLcCode = Right(lVillage.SelectedItem.Text, 10)
        'tLcCode = lVillage.SelectedItem.Value
        txtPattaNo.Text = ""
        txtNewDagNo.Text = ""
        GridViewPlot.DataSource = Nothing
        GridViewPlot.DataBind()
        GridViewOwner.DataSource = Nothing
        GridViewOwner.DataBind()
        cap1.Text = ""
        cap2.Text = ""
        Me.Button2.Visible = False
        lblRecord.Text = ""
    End Sub




    Protected Sub lDistrict_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lDistrict.SelectedIndexChanged
        'Call loadSubDiv()
        Call loadCircle()
        lVillage.Items.Clear()
        txtPattaNo.Text = ""
        txtNewDagNo.Text = ""
        GridViewPlot.DataSource = Nothing
        GridViewPlot.DataBind()
        GridViewOwner.DataSource = Nothing
        GridViewOwner.DataBind()
        cap1.Text = ""
        cap2.Text = ""
        Me.Button2.Visible = False
        lblRecord.Text = ""
    End Sub

    'Protected Sub lSubDiv_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lSubDiv.SelectedIndexChanged
    '    Call loadCircle()
    'End Sub

    Protected Sub lCircle_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lCircle.SelectedIndexChanged
        Call loadVilla()


        txtPattaNo.Text = ""
        txtNewDagNo.Text = ""
        GridViewPlot.DataSource = Nothing
        GridViewPlot.DataBind()
        GridViewOwner.DataSource = Nothing
        GridViewOwner.DataBind()
        cap1.Text = ""
        cap2.Text = ""
        Me.Button2.Visible = False
        lblRecord.Text = ""
    End Sub

    'Private Function GetData() As dsROR1
    '    tDagNo = ""
    '    Dim sql1 As String = ""
    '    sql1 = "select newdagno,area_acre,area from  uniplot where locCd= @lc and newPattaNo = @pno and newDagNo=@dgno  order by newdagno"

    '    'connection open
    '    If connectPrj.State = ConnectionState.Closed Then connectPrj.Open() '
    '    Dim cmd As New SqlCommand(sql1, connectPrj)

    '    With cmd
    '        .Parameters.Add(New SqlParameter("@lc", "0702001005"))
    '        .Parameters.Add(New SqlParameter("@pno", txtPattaNo.Text))
    '        .Parameters.Add(New SqlParameter("@dgno", txtNewDagNo.Text))
    '    End With

    '    Dim da As New SqlDataAdapter()
    '    da.SelectCommand = cmd


    '    Using ds As New dsROR1()
    '        da.Fill(ds, "DataTable1")
    '        Return ds
    '    End Using


    'End Function
    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
    '    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
    '    Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing

    '    Dim streamids As String() = Nothing

    '    Dim mimeType As String = Nothing

    '    Dim encoding As String = Nothing

    '    Dim extension As String = Nothing

    '    Dim deviceInfo As String

    '    Dim bytes As Byte()

    '    '    'Dim lr As New Microsoft.Reporting.WebForms.LocalReport

    '    '    'lr.ReportPath = "Reportdemo.rdlc"

    '    deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>"
    '    Dim ReportViewer1 As New ReportViewer()
    '    ReportViewer1.ProcessingMode = ProcessingMode.Local
    '    ReportViewer1.LocalReport.ReportPath = "Report1.rdlc"
    '    Dim ds As dsROR1 = GetData()
    '    Dim datasource As New ReportDataSource("dsROR1", ds.Tables(0))

    '    ReportViewer1.LocalReport.DataSources.Clear()
    '    ReportViewer1.LocalReport.DataSources.Add(datasource)
    '    Try
    '        bytes = ReportViewer1.LocalReport.Render("pdf", deviceInfo, mimeType, encoding, extension, streamids, warnings)
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        Exit Sub
    '    End Try
    '    Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '    '    'Dim path As String = Server.MapPath("Print_Files")
    '    '    'Dim rnd As Random = New Random()
    '    '    ''creates a number between 1 and 12
    '    '    'Dim month As Integer = rnd.Next(1, 13)
    '    '    ''creates a number between 1 and 6
    '    '    'Dim dice As Integer = rnd.Next(1, 7)
    '    '    ''; // creates a number between 0 and 51
    '    '    'Dim card As Integer = rnd.Next(9)
    '    '    ''; //save the file in unique name 
    '    '    'Dim file_name As String = "Installments" + "List" + ".pdf"
    '    '    'MsgBox(path)

    '    '    ''//3. After that use file stream to write from bytes to pdf file on your server path
    '    '    'Try


    '    '    '    Dim file As FileStream = New FileStream(path + "/" + file_name, FileMode.OpenOrCreate, FileAccess.ReadWrite)
    '    '    '    file.Write(bytes, 0, bytes.Length)
    '    '    '    '            Dim WebBrowser1 As Webbrowser
    '    '    '    '.navigate(@path + "/" + file_name)
    '    '    '    file.Dispose()

    '    '    '    '//4.path the file name by using query string to new page 

    '    '    '    Response.Write(String.Format("<script>window.open('{0}','_blank');</script>", "print.aspx?file=" + file_name))

    '    '    'Catch ex As Exception
    '    '    '    MsgBox(ex.ToString)
    '    '    'End Try
    '    '    'Response.ClearContent()

    '    '    'Response.ClearHeaders()

    '    '    'Response.ContentType = "application/pdf"
    '    '    'Response.AddHeader("Content-disposition", "attachment; filename=BarcodeReport.pdf")


    '    '    'Response.BinaryWrite(bytes)

    '    '    'Response.Flush()

    '    '    'Response.Close()
    '    Response.Buffer = True
    '    Response.Clear()
    '    Response.ContentType = "application/pdf"
    '    Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF")
    '    Response.AddHeader("content-length", bytes.Length.ToString())
    '    'Response.OutputStream.Write(bytes, 0, bytes.Length)
    '    Response.BinaryWrite(bytes)

    '    'Response.AddHeader("content-disposition", "attachment; filename=ror.pdf")
    '    'Response.BinaryWrite(bytes)
    '    Response.Flush()
    '    Response.End()
    '    'End Sub




    'End Sub
    'Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    '    ' Verifies that the control is rendered
    'End Sub

    'Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click

    '    'GridViewPlot.UseAccessibleHeader = True
    '    'GridViewPlot.HeaderRow.TableSection = TableRowSection.TableHeader
    '    'GridViewPlot.FooterRow.TableSection = TableRowSection.TableFooter
    '    'GridViewPlot.Attributes("style") = "border-collapse:separate"
    '    'For Each row As GridViewRow In GridViewPlot.Rows
    '    '    If row.RowIndex Mod 20 = 0 AndAlso row.RowIndex <> 0 Then
    '    '        row.Attributes("style") = "page-break-after:always;"
    '    '    End If
    '    'Next
    '    'Dim sw As New StringWriter()
    '    'Dim hw As New HtmlTextWriter(sw)

    '    'GridViewOwner.UseAccessibleHeader = True
    '    'GridViewOwner.RenderControl(hw)
    '    'GridViewOwner.DataBind()

    '    'GridViewPlot.RenderControl(hw)
    '    'Dim gridHTML As String = sw.ToString().Replace("""", "'").Replace(System.Environment.NewLine, "")
    '    'Dim sb As New StringBuilder()
    '    'sb.Append("<script type = 'text/javascript'>")
    '    'sb.Append("window.onload = new function(){")
    '    'sb.Append("var printWin = window.open('', '', 'left=0")
    '    'sb.Append(",top=0,width=1000,height=600,status=0');")
    '    'sb.Append("printWin.document.write(""")
    '    'Dim style As String = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>"
    '    'sb.Append(style & gridHTML)
    '    'sb.Append(""");")
    '    'sb.Append("printWin.document.close();")
    '    'sb.Append("printWin.focus();")
    '    'sb.Append("printWin.print();")
    '    'sb.Append("printWin.close();")
    '    'sb.Append("};")
    '    'sb.Append("</script>")
    '    'ClientScript.RegisterStartupScript(Me.[GetType](), "GridPrint", sb.ToString())
    '    'GridViewPlot.DataBind()

    '    GridViewPlot.AllowPaging = False
    '    GridViewPlot.DataBind()
    '    Dim sw As New StringWriter()
    '    Dim hw As New HtmlTextWriter(sw)
    '    GridViewPlot.RenderControl(hw)
    '    Dim gridHTML As String = sw.ToString().Replace("""", "'") _
    '         .Replace(System.Environment.NewLine, "")
    '    Dim sb As New StringBuilder()
    '    sb.Append("")
    '    ClientScript.RegisterStartupScript(Me.[GetType](), "GridPrint", sb.ToString())
    '    GridViewPlot.AllowPaging = True
    '    GridViewPlot.DataBind()
    'End Sub



    
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click

    End Sub
End Class
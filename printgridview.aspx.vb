
Imports System.Data
Imports System.Data.SqlClient
'Imports Microsoft.Reporting.WebForms
Imports System.IO
Partial Public Class printgridview
    Inherits System.Web.UI.Page

    Dim connectPrj As New SqlConnection(ConfigurationManager.ConnectionStrings("connectionString").ToString)
    Dim tLcCode, tDagNo As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblerror.Visible = "false"
        lbdteffect.Text = "(Data as on date: " & dt_effect_data() & ")"
        'If (Not (Me.Page.PreviousPage) Is Nothing) Then
        '    Dim GridViewPlot As GridView = CType(Me.Page.PreviousPage.FindControl("GridViewPlot"), GridView)
        '    'Dim GridView2 As GridView = CType(Me.Page.PreviousPage.FindControl("GridViewowner"), GridView)
        'End If
        'GridView1.DataBind()
        'GridView2.DataBind()
        'form1.Controls.Add(GridView1)
        If Not Page.IsPostBack Then
            Label1.Text = Session("District")
            Label2.Text = Session("Circle")
            Label3.Text = Session("Village")
            'Label4.Text = Session("PattaNo")
            GridView1.DataSource = Nothing
            GridView2.DataSource = Nothing
            If Not Session("gridplot") Is Nothing Then


                GridView1.DataSource = Session("gridplot")
                GridView1.DataBind()
            Else
                lblerror.Text = "Pagerror"
                lblerror.Visible = "true"
                lbdteffect.Text = ""
            End If

            If Not Session("gridown") Is Nothing Then
                GridView2.DataSource = Session("gridown")
                GridView2.DataBind()
                lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
            Else
                lblerror.Text = "Pagerror"
                lblerror.Visible = "true"
                lbdteffect.Text = ""
            End If
        End If
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

   
End Class
Imports System.Web
Imports System.Data
Imports System.DBNull
Imports System.Data.SqlClient.SqlConnection
Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlDataReader
Imports System.Data.SqlClient.SqlException
Imports System.Text
Imports System.IO

Imports Microsoft.VisualBasic.ControlChars
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Configuration



Partial Public Class guidanceLandValue

    Inherits System.Web.UI.Page
    Dim connectPrj As New SqlConnection(ConfigurationManager.ConnectionStrings("connectionString").ToString)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ddSoU.AppendDataBoundItems = True


            Dim cmd As New SqlCommand("Select sn, Unit from MasterLandValue", connectPrj)

            Try
                If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()

                ddSoU.DataSource = cmd.ExecuteReader()
                ddSoU.DataTextField = "unit"

                ddSoU.DataBind()

            Catch ex As Exception

                connectPrj.Close()
                connectPrj.Dispose()
            End Try


        End If




    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If (txtHectar.Text.Trim = "") Then
            MsgBox("Blank not Allowed", MsgBoxStyle.Information, "Verify")
        Else
            MsgBox(txtHectar.Text, MsgBoxStyle.Information, "Verify")
        End If

        'txtHectar.Clear()
        txtHectar.Focus()
    End Sub




    Protected Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButton1.CheckedChanged

        txtAcre.Enabled = False
        txtAcre.BackColor = Drawing.Color.WhiteSmoke
        txtSqrfeet.Enabled = False
        txtSqrfeet.BackColor = Drawing.Color.WhiteSmoke

        If RadioButton1.Checked Then
            txtHectar.Enabled = True
            txtHectar.BackColor = Drawing.Color.White

        End If


    End Sub

    Protected Sub RadioButton2_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButton2.CheckedChanged
        txtHectar.Enabled = False
        txtHectar.BackColor = Drawing.Color.WhiteSmoke
        txtSqrfeet.Enabled = False
        txtSqrfeet.BackColor = Drawing.Color.WhiteSmoke

        If RadioButton2.Checked Then
            txtAcre.Enabled = True
            txtAcre.BackColor = Drawing.Color.White
        End If

    End Sub

    Protected Sub RadioButton3_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButton3.CheckedChanged

        txtHectar.Enabled = False
        txtHectar.BackColor = Drawing.Color.WhiteSmoke
        txtAcre.Enabled = False
        txtAcre.BackColor = Drawing.Color.WhiteSmoke

        If RadioButton3.Checked Then
            txtSqrfeet.Enabled = True
            txtSqrfeet.BackColor = Drawing.Color.White
        End If

    End Sub


    Protected Sub ddSoU_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddSoU.SelectedIndexChanged
        'Dim stringRate As String
        Dim oTest As Object = DBNull.Value
        'Try
        If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()

        Dim cmd As New SqlCommand("select * from MasterLandValue where unit=@param", connectPrj)

        cmd.Parameters.AddWithValue("@param", ddSoU.SelectedValue)

        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        If dr.HasRows Then
            ddRate.Items.Clear()

            While dr.Read
                'ddRate.Items.Add(IIf(String.IsNullOrEmpty(dr.Item("rate1").ToString()), dr.Item("rate1"), ""))
                'IIf(String.IsNullOrEmpty(dr.Item("rate2").ToString()), dr.Item("rate2"), "")
                'ddRate.Items.Add(IIf(String.IsNullOrEmpty(dr.Item("rate2").ToString()), dr.Item("rate2"), ""))
                'ddRate.Items.Add(IIf(String.IsNullOrEmpty(dr.Item("rate3").ToString()), dr.Item("rate3"), ""))

                'ddRate.Items.Add(IIf(String.IsNullOrEmpty(dr.Item("rate1")), dr.Item("rate1"), ""))


                'ddRate.Items.Add(IIf(String.IsNullOrEmpty(dr.Item("rate2").ToString()), dr.Item("rate2").ToString(), ""))
                'ddRate.Items.Add(IIf(String.IsNullOrEmpty(dr.Item("rate3").ToString()), dr.Item("rate3").ToString(), ""))
                If Not String.IsNullOrEmpty(dr.Item("rate1").ToString()) Then
                    ddRate.Items.Add(dr.Item("rate1").ToString())
                End If
                If Not String.IsNullOrEmpty(dr.Item("rate2").ToString()) Then
                    ddRate.Items.Add(dr.Item("rate2").ToString())
                End If
                If Not String.IsNullOrEmpty(dr.Item("rate3").ToString()) Then
                    ddRate.Items.Add(dr.Item("rate3").ToString())
                End If

                'ddRate.Items.Add(dr.Item("rate1"))
                'ddRate.Items.Add(dr.Item("rate2"))
                'ddRate.Items.Add(dr.Item("rate3"))
                txtDetails.Text = dr.Item("Details")
            End While
        Else
            ddRate.Items.Clear()
            ddRate.Items.Add("No Data!")
        End If
        connectPrj.Close()
        'Catch ex As Exception
        'ddRate.Items.Clear()
        'ddRate.Items.Add("Page error!")
        'End Try

        connectPrj.Close()


    End Sub


    Protected Sub ddRate_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddRate.SelectedIndexChanged


    End Sub


    Protected Sub txtDetails_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtDetails.TextChanged

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Response.Redirect("WebForm1.aspx")
    End Sub



    Dim cachee As String = ""
    Protected Sub txtHectar_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtHectar.TextChanged
        txtAcre.Text = Val(txtHectar.Text) * 2.4710439202
        txtSqrfeet.Text = Val(txtHectar.Text) * 107638.67316
        txtComp.Text = Val(txtSqrfeet.Text) * Val(ddRate.Text)
        txtAct.Text = txtComp.Text


        'txtstmp.Text = 0.03 * Val(txtComp.Text)
        Dim vregFee, rr As Long
        rr = Val(txtComp.Text) Mod 1000

        If rr = 0 Then
            vregFee = Val(txtComp.Text)
        Else
            vregFee = Val(txtComp.Text) + 1000
            vregFee = Int(vregFee / 1000)
            vregFee = vregFee * 1000
        End If
        txtReg.Text = (Val(vregFee) * 0.01) + 5

    End Sub

    Protected Sub txtAcre_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAcre.TextChanged
        txtHectar.Text = Val(txtAcre.Text) * 0.40468726267
        txtSqrfeet.Text = Val(txtAcre.Text) * 43560
        txtComp.Text = Val(txtSqrfeet.Text) * Val(ddRate.Text)
        txtAct.Text = txtComp.Text

        'txtstmp.Text = 0.03 * Val(txtComp.Text)
        Dim vregFee, rr As Long
        rr = Val(txtComp.Text) Mod 1000

        If rr = 0 Then
            vregFee = Val(txtComp.Text)
        Else
            vregFee = Val(txtComp.Text) + 1000
            vregFee = Int(vregFee / 1000)
            vregFee = vregFee * 1000
        End If

        txtReg.Text = (Val(vregFee) * 0.01) + 5
    End Sub

    Protected Sub txtSqrfeet_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSqrfeet.TextChanged
        txtAcre.Text = Val(txtSqrfeet.Text) * 0.000022956841139
        txtHectar.Text = Val(txtSqrfeet.Text) * 0.0000092903412
        txtSqrfeet.Text = Val(txtSqrfeet.Text)
        txtComp.Text = Val(txtSqrfeet.Text) * Val(ddRate.Text)
        txtAct.Text = txtComp.Text

        'txtstmp.Text = 0.03 * Val(txtComp.Text)


        Dim vregFee, rr As Long
        rr = Val(txtComp.Text) Mod 1000

        If rr = 0 Then
            vregFee = Val(txtComp.Text)
        Else
            vregFee = Val(txtComp.Text) + 1000
            vregFee = Int(vregFee / 1000)
            vregFee = vregFee * 1000
        End If
        txtReg.Text = (Val(vregFee) * 0.01) + 5
        'If (cbHome.Checked = True) Then
        '    txtReg.Text = Val(txtReg.Text) + 50
        'Else
        '    txtReg.Text = Val(txtReg.Text) - 50
        'End If
    End Sub

    Protected Sub txtComp_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtComp.TextChanged

    End Sub

    Protected Sub cbHome_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbHome.CheckedChanged

        'txtReg.Text = IIf((cbHome.Checked = True), Val(txtReg.Text) + 50, Val(txtReg.Text) - 50)
        If (cbHome.Checked = True) Then
            txtReg.Text = Val(txtReg.Text) + 50
        Else
            txtReg.Text = Val(txtReg.Text) - 50
        End If

    End Sub

    Protected Sub rbNonMunicipal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbNonMunicipal.CheckedChanged
        If (rbNonMunicipal.Checked = True) Then
            txtstmp.Text = 0.03 * Val(txtComp.Text)
        Else
            txtstmp.Text = 0.04 * Val(txtComp.Text)
        End If
    End Sub

    Protected Sub rbMunicipal_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbMunicipal.CheckedChanged
        If (rbMunicipal.Checked = True) Then
            txtstmp.Text = 0.04 * Val(txtComp.Text)
        Else
            txtstmp.Text = 0.03 * Val(txtComp.Text)
        End If
    End Sub
End Class
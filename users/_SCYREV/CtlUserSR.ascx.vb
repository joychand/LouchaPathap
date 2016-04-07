Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Partial Public Class ctlUserSR
    Inherits System.Web.UI.UserControl
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblUsrinfo.Text = Session("office")
        lblUsrinfo.Visible = True

    End Sub
End Class
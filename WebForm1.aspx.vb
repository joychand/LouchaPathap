Imports System.Data
Imports System.Data.SqlClient
Imports System.String
Imports System.Globalization

Partial Public Class WebForm1
    Inherits System.Web.UI.Page
    Dim connectPrj As New SqlConnection(ConfigurationManager.ConnectionStrings("connectionString").ToString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If connectPrj.State = ConnectionState.Closed Then connectPrj.Open()
            Dim inscmd As New SqlCommand("insert into tbl_usr_session(userid,session_id,ip,user_agent,first_visit) values (@userid,@session_id,@ip,@user_agent,@first_visit)", connectPrj)
            With inscmd
                .Parameters.Add(New SqlParameter("@userid", "dfdfdfd"))
                .Parameters.Add(New SqlParameter("@session_id", "dfdfdfd"))
                .Parameters.Add(New SqlParameter("@ip", "dfdfdfd"))
                .Parameters.Add(New SqlParameter("@user_agent", Request.UserAgent.ToString))
                .Parameters.Add(New SqlParameter("@first_visit", DateTime.Now))

            End With
            inscmd.ExecuteNonQuery()
            connectPrj.Close()

        Catch ex As Exception
            Response.Write("error")
        End Try
        
    End Sub

End Class
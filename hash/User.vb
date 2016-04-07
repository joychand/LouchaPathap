Imports System.Reflection
Imports System.Web.Security
Imports System.Security
Imports System.Web.UI
Imports System.Security.Cryptography
Imports System.Text
Imports System.Web
Imports Microsoft.VisualBasic
Public NotInheritable Class User
    Private Sub New()

    End Sub
    Public Shared Function isAuthenticated(ByVal context As HttpContext) As Boolean
        Try
            Dim authcookie As HttpCookie = CookieSecurityProvider2.Decrypt(context.Request.Cookies("TM"))
            Dim uid As String = authcookie.Values("uid")
            Dim sess_cookie As String = authcookie.Values("sid")
            If sess_cookie.Equals(context.Session.SessionID) And context.Session.SessionID.ToString.Equals(context.Request.Cookies("ASP.NET_SessionID").Value) Then
                Return True
            Else
                Return False

            End If
        Catch ex As Exception
            context.Response.Redirect("~/users/errorsession.aspx")
        End Try
    End Function

End Class

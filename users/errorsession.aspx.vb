Public Partial Class errorsession
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("TM") Is Nothing Then
            Response.Redirect("~/users/frmUserLogin.aspx")
        End If
        
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.UtcNow)
        Response.Expires = -1
       
        If Not Request.Cookies("TM") Is Nothing Then
            Response.Cookies.Remove("TM")
            Dim authcookie As HttpCookie
            authcookie = New HttpCookie("TM")
            authcookie.Expires = DateTime.Now.AddDays(-1D)
            Response.Cookies.Add(authcookie)
        End If
        Session.Clear()
        Session.Abandon()
        Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        Response.Redirect("~/errorsession.htm")
    End Sub

End Class
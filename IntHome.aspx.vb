Public Partial Class IntHome
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetNoStore()
        Response.Cache.SetExpires(DateTime.UtcNow)
        Response.Expires = -1
        'MsgBox(Session.SessionID.ToString)
    End Sub

    
    'Protected Sub AdRotator1_AdCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AdCreatedEventArgs) Handles AdRotator2.AdCreated
    '    'Change the NavigateUrl to a custom page that logs the Ad click.
    '    'e.NavigateUrl = "rd1.aspx"
    'End Sub
End Class
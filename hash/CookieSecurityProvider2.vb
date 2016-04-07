Imports System.Reflection
Imports System.Web.Security
Imports System.Security
Imports System.Web.UI
Imports System.Security.Cryptography
Imports System.Text
Imports System.Web
Imports Microsoft.VisualBasic


Public NotInheritable Class CookieSecurityProvider2
    Private Sub New()
    End Sub
    Private Shared _encode As MethodInfo
    Private Shared _decode As MethodInfo
    ' CookieProtection.All enables 'temper proffing' and 'encryption' for cookie
    Private Shared _cookieProtection As CookieProtection = CookieProtection.All

    'Static constructor to get reference of Encode and Decode methods of Class CookieProtectionHelper
    'using Reflection.
    Shared Sub New()
        Dim systemWeb As Assembly = GetType(HttpContext).Assembly
        Dim cookieProtectionHelper As Type = systemWeb.[GetType]("System.Web.Security.CookieProtectionHelper")
        _encode = cookieProtectionHelper.GetMethod("Encode", BindingFlags.NonPublic Or BindingFlags.[Static])
        _decode = cookieProtectionHelper.GetMethod("Decode", BindingFlags.NonPublic Or BindingFlags.[Static])
    End Sub

    Public Shared Function Encrypt(ByVal Cookie As HttpCookie) As HttpCookie
        If Not Cookie.HasKeys Then
            'MsgBox("no key")
        Else

            Dim key2 As String
            Dim j As Integer
            Dim buffer_key As Byte()
            Dim buffer_value As Byte()
            Dim cookie_name As String = Cookie.Name
            Dim cookietemp As New HttpCookie(cookie_name)
            For j = 0 To Cookie.Values.Count - 1
                buffer_key = Encoding.[Default].GetBytes(Cookie.Values.AllKeys(j))
                buffer_value = Encoding.[Default].GetBytes(Cookie.Values(j))
                key2 = DirectCast(_encode.Invoke(Nothing, New Object() {_cookieProtection, buffer_key, buffer_key.Length}), String)
                'MsgBox(key2)
                cookietemp.Values(key2) = DirectCast(_encode.Invoke(Nothing, New Object() {_cookieProtection, buffer_value, buffer_value.Length}), String)
            Next
            'Dim buffer As Byte() = Encoding.[Default].GetBytes(Cookie.Value)

            'Referencing the Encode mehod of CookieProtectionHelper class
            'Cookie.Value = DirectCast(_encode.Invoke(Nothing, New Object() {_cookieProtection, buffer, buffer.Length}), String)
            Return cookietemp
        End If

    End Function
    Public Shared Function Decrypt(ByVal Cookie As HttpCookie) As HttpCookie
        If Cookie.HasKeys Then
            Dim key2 As String
            Dim j As Integer
            Dim buffer_key As Byte()
            Dim buffer_value As Byte()
            Dim cookie_name As String = Cookie.Name
            Dim cookietemp As New HttpCookie(cookie_name)
            For j = 0 To Cookie.Values.Count - 1
                buffer_key = DirectCast(_decode.Invoke(Nothing, New Object() {_cookieProtection, Cookie.Values.AllKeys(j)}), Byte())
                key2 = Encoding.[Default].GetString(buffer_key, 0, buffer_key.Length)
                buffer_value = DirectCast(_decode.Invoke(Nothing, New Object() {_cookieProtection, Cookie.Values(j)}), Byte())
                cookietemp.Values(key2) = Encoding.[Default].GetString(buffer_value, 0, buffer_value.Length)
            Next
            Return cookietemp
        End If
        ''Referencing the Decode mehod of CookieProtectionHelper class
        'Dim buffer As Byte() = DirectCast(_decode.Invoke(Nothing, New Object() {_cookieProtection, HttpCookie.Value}), Byte())
        'HttpCookie.Value = Encoding.[Default].GetString(buffer, 0, buffer.Length)

    End Function
End Class
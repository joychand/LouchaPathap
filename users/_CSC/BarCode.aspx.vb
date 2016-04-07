Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls


Partial Public Class BarCode
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request.Cookies("TM") Is Nothing Then
            If Session("userrole").Equals("CSC_USER") Then
                ' Get the Requested code to be created.
                Dim Code As String = Request("code").ToString()

                ' Multiply the lenght of the code by 40 (just to have enough width)
                Dim w As Integer = IIf(Code = "", ("no para".Length) * 40, Code.Length * 40)

                ' Create a bitmap object of the width that we calculated and height of 100
                Dim oBitmap As New Bitmap(w, 100)

                ' then create a Graphic object for the bitmap we just created.
                Dim oGraphics As Graphics = Graphics.FromImage(oBitmap)

                ' Now create a Font object for the Barcode Font
                ' (in this case the IDAutomationHC39M) of 18 point size
                Dim oFont As New Font("IDAutomationHC39M", 18)

                ' Let's create the Point and Brushes for the barcode
                Dim oPoint As New PointF(2.0F, 2.0F)
                Dim oBrushWrite As New SolidBrush(Color.Black)
                Dim oBrush As New SolidBrush(Color.White)

                ' Now lets create the actual barcode image
                ' with a rectangle filled with white color
                oGraphics.FillRectangle(oBrush, 0, 0, w, 100)

                ' We have to put prefix and sufix of an asterisk (*),
                ' in order to be a valid barcode
                oGraphics.DrawString("*" & Code & "*", oFont, oBrushWrite, oPoint)

                ' Then we send the Graphics with the actual barcode
                Response.ContentType = "image/jpeg"
                oBitmap.Save(Response.OutputStream, ImageFormat.Jpeg)
            Else
                Response.Redirect("~/users/errorsession.aspx")
            End If
        Else
            Response.Redirect("~/users/frmUserLogin.aspx")
        End If
       
    End Sub
   
End Class


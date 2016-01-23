Imports PdfSharp
Imports PdfSharp.Drawing
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports System.Data
Imports System.Data.OleDb
Partial Class Src_OpenBookIn
    Inherits System.Web.UI.Page
    Dim MD As New MainData
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DocPDF As PdfDocument = New PdfDocument

        ' Create an empty page
        Dim objPage As PdfPage = DocPDF.AddPage

        ' Get an XGraphics object for drawing
        Dim gfx As XGraphics = XGraphics.FromPdfPage(objPage)

        ' Create a font
        Dim font1 As XFont = New XFont("Verdana", 15, XFontStyle.Bold)
        Dim font2 As XFont = New XFont("Tahoma", 8, XFontStyle.Bold)
        Dim font3 As XFont = New XFont("Tahoma", 8, 0)

        ' Draw the text
        gfx.DrawString("My Customer", font1, XBrushes.Black, _
        New XRect(0, 50, objPage.Width.Point, objPage.Height.Point), XStringFormats.TopCenter)


        ' Draw the text
        gfx.DrawString("bookout_id", font2, XBrushes.Black, 65, 80, XStringFormats.TopLeft)
        gfx.DrawString("message", font2, XBrushes.Black, 140, 80, XStringFormats.TopLeft)
        'gfx.DrawString("Email", font2, XBrushes.Black, 215, 80, XStringFormats.TopLeft)
        'gfx.DrawString("CountryCode", font2, XBrushes.Black, 365, 80, XStringFormats.TopLeft)
        'gfx.DrawString("Budget", font2, XBrushes.Black, 425, 80, XStringFormats.TopLeft)
        'gfx.DrawString("Used", font2, XBrushes.Black, 485, 80, XStringFormats.TopLeft)

        ' Line
        Dim pen As XPen = New XPen(XColor.FromArgb(0, 0, 0))
        gfx.DrawLine(pen, New XPoint(65, 78), New XPoint(520, 78))
        gfx.DrawLine(pen, New XPoint(65, 90), New XPoint(520, 90))

        ' Customer From Database (Start)

        Dim objConn As New OleDbConnection
        Dim objCmd As New OleDbCommand
        Dim dtAdapter As New OleDbDataAdapter

        Dim ds As New DataSet
        Dim dt As DataTable
        Dim strConnString, strSQL As String

        'strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Server.MapPath("database/mydatabase.mdb") & ";Jet OLEDB:Database Password=;"
        strSQL = "SELECT * FROM bookout_data where bookout_id='2010-0038'"





        objConn.ConnectionString = MD.Strcon
        With objCmd
            .Connection = objConn
            .CommandText = strSQL
            .CommandType = CommandType.Text
        End With
        dtAdapter.SelectCommand = objCmd

        dtAdapter.Fill(ds)
        dt = ds.Tables(0)

        dtAdapter = Nothing
        objConn.Close()
        objConn = Nothing

        Dim intLine As Integer = 90
        Dim i As Integer

        For i = 0 To dt.Rows.Count - 1
            lblText1.Text = Convert.ToChar(dt.Rows(i)("message"))

            gfx.DrawString(dt.Rows(i)("bookout_id"), font3, XBrushes.Black, 65, intLine, XStringFormats.TopLeft)
            gfx.DrawString(dt.Rows(i)("message"), font3, XBrushes.Black, 140, intLine, XStringFormats.TopLeft)
            gfx.DrawString(dt.Rows(i)("message"), font3, XBrushes.Black, 215, intLine, XStringFormats.TopLeft)
            'gfx.DrawString(dt.Rows(i)("CountryCode"), font3, XBrushes.Black, 365, intLine, XStringFormats.TopLeft)
            'gfx.DrawString(dt.Rows(i)("Budget"), font3, XBrushes.Black, 425, intLine, XStringFormats.TopLeft)
            'gfx.DrawString(dt.Rows(i)("Used"), font3, XBrushes.Black, 485, intLine, XStringFormats.TopLeft)


            gfx.DrawImage(XImage.FromFile(Server.MapPath("../Images/HeadSmall.png")), 260, 160)



            intLine = intLine + 10
        Next

        ' Customer From Database (End)

        ' Save the document...
        Dim FileName As String = "../MyPDF/PdfDoc.pdf"
        DocPDF.Save(Server.MapPath(FileName))

        DocPDF.Close()
        DocPDF = Nothing

        Me.lblText.Text = "PDF Created <a href=" & FileName & ">click here</a> to view"

    End Sub
End Class

Imports Microsoft.VisualBasic
Imports System.Drawing
Imports System.Drawing.Imaging


Public Class ClassReziseImg

    Public Shared Function GetPreferedImageSize(ByVal image As Image, ByVal maxSize As Size) As Image
        Dim newSize As Size = GetPreferedSize(image, maxSize)
        Dim newImage As New Bitmap(newSize.Width, newSize.Height)
        Dim g As Graphics = Graphics.FromImage(newImage)
        g.DrawImage(image, 0, 0, newSize.Width + 1, newSize.Height + 1)
        Return newImage

    End Function

    Public Shared Function GetPreferedSize(ByVal image As Image, ByVal maxSize As Size) As Size
        Dim ratio As Double = 1
        If (image.Width > maxSize.Width) Or (image.Height > maxSize.Height) Then
            If (image.Width - maxSize.Width) > (image.Height - maxSize.Height) Then
                ratio = CType(maxSize.Width, Double) / CType(image.Width, Double)
            Else
                ratio = CType(maxSize.Height, Double) / CType(image.Height, Double)
            End If
        End If
        Return New Size(Math.Round(image.Width * ratio, 0), Math.Round(image.Height * ratio, 0))
    End Function

End Class


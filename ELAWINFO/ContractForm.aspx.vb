
Partial Class ContractForm
    Inherits System.Web.UI.Page

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            Dim MC As New MainClass
            Dim path As String = "ContractForm\"

            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "1.pdf")

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Try
            Dim MC As New MainClass
            Dim path As String = "ContractForm\"

            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "2.pdf")

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        Try
            Dim MC As New MainClass
            Dim path As String = "ContractForm\"

            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "3.pdf")

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        Try
            Dim MC As New MainClass
            Dim path As String = "ContractForm\"

            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "4.pdf")

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ImageButton5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton5.Click
        Try
            Dim MC As New MainClass
            Dim path As String = "ContractForm\"

            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "5.pdf")

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ImageButton6_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton6.Click
        Try
            Dim MC As New MainClass
            Dim path As String = "ContractForm\"

            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "6.pdf")

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ImageButton7_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton7.Click
        Try
            Dim MC As New MainClass
            Dim path As String = "ContractForm\"

            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "7.pdf")

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ImageButton8_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton8.Click
        Try
            Dim MC As New MainClass
            Dim path As String = "ContractForm\"

            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "8.pdf")

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ImageButton9_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton9.Click
        Try
            Dim MC As New MainClass
            Dim path As String = "ContractForm\"

            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "9.pdf")

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ImageButton10_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton10.Click
        Try
            Dim MC As New MainClass
            Dim path As String = "ContractForm\"

            MC.OpenWindow(Me, "http://" & Constant.BaseURL(Request) & "" & path & "" & "10.pdf")

        Catch ex As Exception
        End Try
    End Sub
End Class

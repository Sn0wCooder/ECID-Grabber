Imports System.Management
Imports System.Text.RegularExpressions

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM  Win32_PnPEntity")
            Dim regex As New Regex("ECID:#?[0-9A-Fa-f]{16}")
            Dim str As String = Nothing
            Dim obj2 As ManagementObject
            For Each obj2 In searcher.Get
                If (((Not obj2.Item("Service") Is Nothing) AndAlso obj2.Item("Name").ToString.Contains("Apple")) AndAlso regex.IsMatch(obj2.ToString)) Then
                    str = regex.Match(obj2.ToString).Value.Split(New Char() {":"c})(1)
                    If (Not str Is Nothing) Then
                        TextBox1.Text = str

                    Else
                        Interaction.MsgBox("Could not find ECID!", MsgBoxStyle.ApplicationModal, Nothing)
                    End If
                End If
            Next
            If (str Is Nothing) Then
                Interaction.MsgBox(("Could not find ECID!" & Environment.NewLine & "Is the device plugged into your computer in Recovery Mode?"), MsgBoxStyle.ApplicationModal, Nothing)
            End If
        Catch exception1 As Exception
            'ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            Interaction.MsgBox(exception.Message.ToString, MsgBoxStyle.Exclamation, Nothing)
            'ProjectData.ClearProjectError()
        End Try
    End Sub
End Class

Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports System.IO
Imports System.Threading

Public Class Form1
    Dim service As New DriveService
    Private Sub createservice()
        Dim clientid = "1029816439958-7r4jgk9m205mroj45dgulu4hjvh40tg5.apps.googleusercontent.com"
        Dim clientsecret = "qCvG6vveD3bFfOOOAHGoZVSb"

        Dim uc As UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(New ClientSecrets() With {.ClientId = clientid, .ClientSecret = clientsecret}, {DriveService.Scope.Drive}, "user", CancellationToken.None).Result
        service = New DriveService(New BaseClientService.Initializer() With {.HttpClientInitializer = uc, .ApplicationName = "Google Drive"})

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim f As New OpenFileDialog
        If f.ShowDialog = DialogResult.OK Then
            FilePath.Text = f.FileName
        Else
            Exit Sub
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Cursor = Cursors.WaitCursor
        If service.ApplicationName <> "Google Drive" Then createservice()
        Dim thefile As New Data.File
        thefile.Name = "Triptico"
        thefile.Description = "A test document"
        thefile.MimeType = "text/plain"
        Dim bytearry As Byte() = System.IO.File.ReadAllBytes(FilePath.Text)
        Dim stream As New System.IO.MemoryStream(bytearry)
        Dim UploadRequest As FilesResource.CreateMediaUpload = service.Files.Create(thefile, stream, thefile.MimeType)
        Dim Response = UploadRequest.Upload()
        Dim file As Data.File = uploadrequest.ResponseBody
        Me.Cursor = Cursors.Default
        MessageBox.Show(" Upload Successful ")
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FilePath.Text = "c:/backup/evea.bak"
        createservice()
    End Sub
End Class

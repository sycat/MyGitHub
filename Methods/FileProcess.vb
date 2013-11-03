Imports System.IO

Public Class FileProcess

    Public Shared Sub WriteLog(ByVal logTest As String)

        Dim path As String = "e:\LogPath"
        Dim filePath As String = path & "\log.txt"
        ' Specify the directories you want to manipulate.
        Dim di As DirectoryInfo = New DirectoryInfo(path)

        If Not di.Exists Then
            ' Try to create the directory.
            di.Create()
        End If

        Using sw As StreamWriter = File.AppendText(filePath)
            sw.WriteLine(String.Format("{0}:[{1}]", Now().ToString("yyyy/MM/dd HH:mm:ss.fffff"), logTest))
        End Using

    End Sub

    Public Shared Sub ReadData(ByVal filePath As String)

        Using sr As StreamReader = New StreamReader(filePath)
            Do While sr.Peek() >= 0
                ' Do something
                'Console.WriteLine(sr.ReadLine())
            Loop
        End Using

    End Sub


End Class

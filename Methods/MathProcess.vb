Imports System.Text.RegularExpressions

Public Class MathProcess

    ''' <summary>
    ''' If program version is used regular which like 1.2.3,
    ''' this function could check version is the newest or not.
    ''' </summary>
    ''' <param name="currVersion">The current version.</param>
    ''' <returns>Is the newest or not</returns>
    ''' <remarks></remarks>
    Public Shared Function isNewestVersion(ByVal currVersion As String) As Boolean

        Dim sampleVersion As String = "1.2.3"
        Dim newestAppVer() As String = sampleVersion.Split(".")
        Dim curr() As String
        Dim rgx As New Regex("^\d+.\d+.\d+$")
        Dim result As Boolean = False

        If Not rgx.IsMatch(currVersion) Then
            Return result
        End If

        curr = currVersion.Split(".")

        For i As Integer = 0 To newestAppVer.Length - 1
            If Integer.Parse(curr(i)) < Integer.Parse(newestAppVer(i)) Then
                Exit For
            End If

            If Integer.Parse(curr(i)) > Integer.Parse(newestAppVer(i)) Then
                result = True
                Exit For
            End If

            If i = newestAppVer.Length - 1 Then
                result = True
            End If
        Next

        Return result

    End Function

End Class

Imports System.Globalization

Public Class DateProcess

    ''' <summary>
    ''' Convert month name to number, but the month name must be 3 char.
    ''' For example 'Jan' to 1, 'FEB' to 2.
    ''' </summary>
    ''' <param name="monthName">Month Name</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertMonthNameToNumber(ByVal monthName As String) As Integer

        Dim monthNumber As Integer = DateTime.ParseExact(monthName, "MMM", CultureInfo.CreateSpecificCulture("en-US")).Month

        Return monthNumber

    End Function

    Public Shared Function CalculateAge(ByVal birthday As DateTime, ByVal targetDate As DateTime) As Integer

        Dim age As Integer = targetDate.Year - birthday.Year

        If birthday > targetDate.AddYears(-age) Then
            age = age - 1
        End If

        Return age

    End Function

    Public Shared Function RevertSpecialFormat(ByVal dateString As String) As DateTime
        Dim formatDateTime As DateTime
        Dim formatList() As String = {
            "yyyyMMdd HHmmss",
            "yyyyMMdd tt hhmmss",
            "yyyy/M/d tt hh:mm:ss",
            "yyyy/MM/dd tt hh:mm:ss",
            "yyyy/MM/dd HH:mm:ss",
            "yyyy/M/d HH:mm:ss",
            "yyyy/M/d",
            "yyyy/MM/dd",
            "ddMMMyy",
            "ddMMMyy HHmm"
        }

        formatDateTime = DateTime.ParseExact(dateString,
                                             formatList,
                                             CultureInfo.InvariantCulture,
                                             DateTimeStyles.AllowWhiteSpaces)

        Return formatDateTime
    End Function

    Public Shared Function TryRevertSpecialFormat(ByVal dateString As String, ByRef specialDate As DateTime) As Boolean
        Dim revertResult As Boolean = False

        Try
            specialDate = RevertSpecialFormat(dateString)
            revertResult = True
        Catch ex As Exception
            revertResult = False
        End Try

        Return revertResult
    End Function

End Class

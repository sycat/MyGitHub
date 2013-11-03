﻿Imports System.Globalization

Public Class DateProcess

    ''' <summary>
    ''' Convert month name to number, but the month name must be 3 char.
    ''' For example 'Jan' to 1, 'FEB' to 2.
    ''' </summary>
    ''' <param name="monthName">Month Name</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertMonthNameToNumber(ByVal monthName As String) As String

        Dim monthNumber As Integer = DateTime.ParseExact(monthName, "MMM", CultureInfo.CreateSpecificCulture("en-US")).Month

        Return monthNumber

    End Function

End Class
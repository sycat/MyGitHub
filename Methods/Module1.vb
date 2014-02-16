''' <summary>
''' This process is only for test.
''' </summary>
''' <remarks></remarks>
Module Module1

    Sub Main()

        Try
            Dim birthday As DateTime = New DateTime(2012, 2, 29)
            Dim targetDay As DateTime = New DateTime(2014, 2, 28)
            Console.WriteLine(DateProcess.CalculateAge(birthday, targetDay).ToString())

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Console.ReadKey()

    End Sub

End Module

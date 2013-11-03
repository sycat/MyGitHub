Module Module1

    Sub Main()

        Try
            Console.WriteLine(DateProcess.ConvertMonthNameToNumber("JAN"))
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Console.ReadKey()

    End Sub

End Module

''' <summary>
''' This process is only for test.
''' </summary>
''' <remarks></remarks>
Module Module1

    Sub Main()

        Try
            Dim date1 As DateTime = Date.Now.Date
            Dim date2 As DateTime = DateTime.Parse("16MAR14", System.Globalization.CultureInfo.InvariantCulture)
            Console.WriteLine(Date.Now.ToString())


            Console.WriteLine("fin")

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Console.ReadKey()

    End Sub

End Module

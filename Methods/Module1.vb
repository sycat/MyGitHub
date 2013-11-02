Module Module1

    Sub Main()

        Try
            FileProcess.ReadData("e:\log\log.txt")
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Console.ReadKey()

    End Sub

End Module

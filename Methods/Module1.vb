''' <summary>
''' This process is only for test.
''' </summary>
''' <remarks></remarks>
Module Module1

    Sub Main()

        Try
            Dim contactDoc As XDocument = _
                <?xml version="1.0"?>
                <contact>
                    <name>Patrick Hines</name>
                    <phone type="home">206-555-0144</phone>
                    <phone type="work">425-555-0145</phone>
                </contact>

            Dim xdocument As XDocument = xdocument.Load("..\..\Test.xml")
            Dim employees As IEnumerable(Of XElement) = contactDoc.Elements()
            ' Read the entire XML
            For Each employee In employees
                Console.WriteLine(employee)
            Next



            Console.WriteLine("fin")

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Console.ReadKey()

    End Sub

End Module

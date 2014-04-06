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

            Dim xd As XElement = XElement.Load("..\..\Test.xml")
            Dim employees As IEnumerable(Of XElement) =
                From el In xd.<Items>.<Item>
                Where el.<USPrice>.Value = "39.98"
                Select el
            ' Read the entire XML
            For Each employee As XElement In employees

                Console.WriteLine(employee.Element("ProductName").Value)
            Next

            'Dim root As XElement = XElement.Load("..\..\Test.xml")
            'Dim address As IEnumerable(Of XElement) = _
            '    From el In root.<Address> _
            '    Where el.@Type = "Billing" _
            '    Select el
            'For Each el As XElement In address
            '    Console.WriteLine(el)
            'Next



            Console.WriteLine("fin")

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Console.ReadKey()

    End Sub

End Module

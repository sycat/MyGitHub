Imports Oracle.DataAccess.Types
Imports Oracle.DataAccess.Client
Imports System.Xml
Imports System.Globalization

''' <summary>
''' This process is only for test.
''' </summary>
''' <remarks></remarks>
Module Module1

    Sub Main()

        Try
            '測試 LINQ
            'Dim contactDoc As XDocument = _
            '    <?xml version="1.0"?>
            '    <contact>
            '        <name>Patrick Hines</name>
            '        <phone type="home">206-555-0144</phone>
            '        <phone type="work">425-555-0145</phone>
            '    </contact>

            'Dim xd As XDocument = XDocument.Load("..\..\Test.xml")
            'Dim employees As IEnumerable(Of XElement) =
            '    From el In xd.Root.<Items>.<Item>
            '    Where el.@PartNumber = "926-AA"
            '    Select el

            'For Each employee As XElement In employees

            '    Console.WriteLine(employee.<ProductName>.Value)
            'Next


        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Console.ReadKey()

    End Sub

End Module
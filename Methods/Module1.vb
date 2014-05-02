Imports Oracle.DataAccess.Types
Imports Oracle.DataAccess.Client

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

            'Dim xd As XElement = XElement.Load("..\..\Test.xml")
            'Dim employees As IEnumerable(Of XElement) =
            '    From el In xd.<Items>.<Item>
            '    Where el.<USPrice>.Value = "39.98"
            '    Select el
            '' Read the entire XML
            'For Each employee As XElement In employees

            '    Console.WriteLine(employee.Element("ProductName").Value)
            'Next

            'Dim root As XElement = XElement.Load("..\..\Test.xml")
            'Dim address As IEnumerable(Of XElement) = _
            '    From el In root.<Address> _
            '    Where el.@Type = "Billing" _
            '    Select el
            'For Each el As XElement In address
            '    Console.WriteLine(el)
            'Next

            ' 測試 DB Date
            'Dim cmd As OracleCommand = New OracleCommand()
            ''cmd.CommandText = "select sysdate from dual"
            ''Dim result As String = DBProcess.ExecuteScalar(cmd)
            'Dim myDate As DateTime = New DateTime(2014, 4, 11, 4, 0, 50)
            'Dim dateString As String = "2014/04/15 130000"
            ''myDate = DateTime.ParseExact(DateString, "yyyy/MM/dd HHmmss", New System.Globalization.CultureInfo("en-US"))
            'Dim oDate As OracleDate = DateTime.Parse("2013/12/31 13:59:41")
            'cmd.CommandText = _
            '    "insert into TEST1 " & Environment.NewLine & _
            '    "  (COL1, COL2, COL3) " & Environment.NewLine & _
            '    "values " & Environment.NewLine & _
            '    "  (:1, :2, :3)"
            'cmd.Parameters.Add("1", OracleDbType.Decimal, 101D, ParameterDirection.Input)
            'cmd.Parameters.Add("2", OracleDbType.Varchar2, "TEST", ParameterDirection.Input)
            'cmd.Parameters.Add("3", OracleDbType.Date, oDate, ParameterDirection.Input)
            ''cmd.Parameters.Add("4", OracleDbType.Varchar2, "", ParameterDirection.Input)

            ''Dim result As Integer = OracleDBProcess.ExecuteNonQuery(cmd)
            'Dim b As Boolean = OracleDBProcess.ExecuteNonQuery(cmd)
            'Console.WriteLine(b.ToString())

            ' 測試 transfer date
            'Dim dateString As String = "2014/04/15 000000"
            ''Dim trueDate As Date = CDate(dateString)
            'Dim trueDate As Date = DateTime.Parse(dateString)

            'Dim s As String = "2014/11/12 10:12:34"
            'Dim d As OracleDate = New OracleDate(DateTime.Parse(s))
            'd = New OracleDate(New DateTime(2014, 1, 2, 3, 4, 5))

            'Console.WriteLine(trueDate.ToString("yyyy/MM/dd hhmmss"))

            ' 測試DB查詢
            'Dim cmd As OracleCommand = New OracleCommand()
            'Dim count As Integer = 0
            'Dim dt As New DataTable()
            'cmd.CommandText = "select COL1 from TEST1 where COL2 = :b "
            'cmd.Parameters.Add("b", OracleDbType.Varchar2, "TEST1", ParameterDirection.Input)
            ''cmd.Parameters.Add("a", OracleDbType.Decimal, 11, ParameterDirection.Input)
            'cmd.BindByName = True
            'dt = OracleDBProcess.GetDataTable(cmd)
            'Dim obj As Object = OracleDBProcess.ExecuteScalar(cmd)
            'If obj IsNot Nothing Then
            '    Console.WriteLine(obj.ToString())
            'End If

            'For Each dr As DataRow In dt.Rows
            '    'Dim d As DateTime
            '    'd = DateTime.Parse(dr("COL1"))
            '    Console.WriteLine(dr("COL1").ToString())
            'Next


            ' 測試 DB Update
            'Dim cmd As OracleCommand = New OracleCommand()
            ''Dim myDate As DateTime = New DateTime(2014, 4, 11, 4, 0, 50)
            ''Dim dateString As String = "2014/04/15 130000"
            ''myDate = DateTime.ParseExact(dateString, "yyyy/MM/dd HHmmss", New System.Globalization.CultureInfo("en-US"))
            'cmd.BindByName = True
            'cmd.CommandText = _
            '    "update TEST1 " & Environment.NewLine & _
            '    "  set COL1 = :col1 " & Environment.NewLine & _
            '    "where COL2 = :col2 "
            'cmd.Parameters.Add("col1", OracleDbType.Decimal, 123, ParameterDirection.Input)
            'cmd.Parameters.Add("col2", OracleDbType.Varchar2, "77777", ParameterDirection.Input)
            ''cmd.Parameters.Add("3", OracleDbType.Date, myDate, ParameterDirection.Input)


        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Console.ReadKey()

    End Sub

    Private Function RemoveTopZero(ByVal flightNo As String) As String
        Dim shorterNo As String = String.Empty

        If flightNo.StartsWith("000") Then
            shorterNo = flightNo.Substring(3)
        ElseIf flightNo.StartsWith("00") Then
            shorterNo = flightNo.Substring(2)
        ElseIf flightNo.StartsWith("0") Then
            shorterNo = flightNo.Substring(1)
        Else
            shorterNo = flightNo
        End If

        Return shorterNo

    End Function

End Module

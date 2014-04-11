Imports Oracle.DataAccess.Types
Imports Oracle.DataAccess.Client

''' <summary>
''' This process is only for test.
''' </summary>
''' <remarks></remarks>
Module Module1

    Sub Main()

        Try
            Dim cmd As OracleCommand = New OracleCommand()
            'cmd.CommandText = "select sysdate from dual"
            'Dim result As String = DBProcess.ExecuteScalar(cmd)
            Dim myDate As DateTime = New DateTime(2014, 4, 11)
            cmd.CommandText = _
                "insert into TEST1 " & Environment.NewLine & _
                "  (COL1, COL2, COL3) " & Environment.NewLine & _
                "values " & Environment.NewLine & _
                "  (:1, :2, :3)"
            cmd.Parameters.Add("1", OracleDbType.Decimal, 123456789, ParameterDirection.Input)
            cmd.Parameters.Add("2", OracleDbType.Varchar2, "TEST", ParameterDirection.Input)
            cmd.Parameters.Add("3", OracleDbType.Date, myDate, ParameterDirection.Input)

            Dim result As Integer = OracleDBProcess.ExecuteNonQuery(cmd)

            Console.WriteLine(result)

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try

        Console.ReadKey()

    End Sub

End Module

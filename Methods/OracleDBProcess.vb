Imports Oracle.DataAccess.Client

Public Class OracleDBProcess

    Private Const connectString As String = "Data Source=XE;User Id=user;Password=pwd;"

    Public Shared Function ExecuteScalar(ByVal cmd As OracleCommand) As String
        Dim result As String = String.Empty

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            cmd.Connection = connection

            Dim obj As Object = cmd.ExecuteScalar()

            If Not obj Is Nothing Then
                result = obj.ToString()
            End If
        End Using

        Return result

    End Function

    Public Shared Function GetDataTable(ByVal cmd As OracleCommand) As DataTable
        Dim dt As New DataTable()

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            cmd.Connection = connection
            Dim adapter As OracleDataAdapter = New OracleDataAdapter(cmd)
            adapter.Fill(dt)
        End Using

        Return dt

    End Function

    Public Shared Function ExecuteNonQuery(ByVal cmd As OracleCommand) As Integer
        Dim count As Integer = 0

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            cmd.Connection = connection
            count = cmd.ExecuteNonQuery()
        End Using

        Return count

    End Function

    Public Shared Function ExecuteMultiNonQuery(ByVal cmdAry() As OracleCommand) As Boolean
        Dim result As Integer = False

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            Dim transaction As OracleTransaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)

            Try
                For Each cmd As OracleCommand In cmdAry
                    cmd.Transaction = transaction
                    cmd.ExecuteNonQuery()
                Next

                transaction.Commit()
                result = True
            Catch ex As Exception
                transaction.Rollback()
            End Try

        End Using

        Return result
    End Function


End Class

Imports Oracle.DataAccess.Client

Public Class OracleDBProcess

    Private Const connectString As String = "Data Source=XE;User Id=ED39298;Password=asd39298;"

    Public Shared Function ExecNonQuery(ByVal cmd As OracleCommand) As Integer
        Dim count As Integer = 0

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            cmd.Connection = connection
            count = cmd.ExecuteNonQuery()
        End Using

        Return count

    End Function

    ''' <summary>
    ''' OracleCommand 的 Parameters 會依照 Parameters name 加入。
    ''' OracleCommand Parameters 預設是照順序加入至 OracleCommand，
    ''' 如需依 Parameters name 加入要額外設定。
    ''' </summary>
    ''' <param name="cmd">Oracle sql command with parameters</param>
    ''' <returns>資料變更筆數</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecNonQueryWithParam(ByVal cmd As OracleCommand) As Integer
        Dim count As Integer = 0

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            cmd.BindByName = True
            cmd.Connection = connection
            count = cmd.ExecuteNonQuery()
        End Using

        Return count

    End Function

    Public Shared Function ExecTable(ByVal cmd As OracleCommand) As DataTable
        Dim dt As New DataTable()

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            cmd.Connection = connection
            Dim adapter As OracleDataAdapter = New OracleDataAdapter(cmd)
            adapter.Fill(dt)
        End Using

        Return dt

    End Function

    Public Shared Function ExecTableWithParam(ByVal cmd As OracleCommand) As DataTable
        Dim dt As New DataTable()

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            cmd.BindByName = True
            cmd.Connection = connection
            Dim adapter As OracleDataAdapter = New OracleDataAdapter(cmd)
            adapter.Fill(dt)
        End Using

        Return dt

    End Function

    Public Shared Function ExecScalar(ByVal cmd As OracleCommand) As String
        Dim result As String = String.Empty

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            cmd.Connection = connection
            result = cmd.ExecuteScalar().ToString()
        End Using

        Return result

    End Function

    Public Shared Function ExecScalarWithParam(ByVal cmd As OracleCommand) As String
        Dim result As String = String.Empty

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            cmd.BindByName = True
            cmd.Connection = connection
            result = cmd.ExecuteScalar().ToString()
        End Using

        Return result

    End Function

    Public Shared Function ExecMulti(ByVal cmdList As List(Of OracleCommand)) As Boolean
        Dim result As Integer = False

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            Dim transaction As OracleTransaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)

            Try
                For Each cmd As OracleCommand In cmdList
                    cmd.Connection = connection
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

    Public Shared Function ExecMultiWithParam(ByVal cmdList As List(Of OracleCommand)) As Boolean
        Dim result As Boolean = False

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            Dim transaction As OracleTransaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)

            Try
                For Each cmd As OracleCommand In cmdList
                    cmd.BindByName = True
                    cmd.Connection = connection
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

    ''' <summary>
    ''' 取得查詢筆數，只適用於 select count() 的 sql command。
    ''' </summary>
    ''' <param name="cmd">Oracle sql command with parameters</param>
    ''' <returns>符合資料筆數</returns>
    ''' <remarks></remarks>
    Public Shared Function GetCountWithParam(ByVal cmd As OracleCommand) As Integer
        Dim count As Integer = 0

        Using connection As OracleConnection = New OracleConnection(connectString)
            connection.Open()
            cmd.BindByName = True
            cmd.Connection = connection
            Integer.TryParse(cmd.ExecuteScalar().ToString(), count)
        End Using

        Return count
    End Function

End Class

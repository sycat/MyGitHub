Imports System.Threading

Public Delegate Sub ThreadCallback(ByVal result As String)

Public Class ThreadProcess

    Public Shared Sub AsynTask()

        Dim myTasks(10) As Task

        If ThreadPool.SetMaxThreads(8, 8) Then
            For i As Integer = 0 To myTasks.Count - 1
                ' Start new task.
                myTasks(i) = Task.Factory.StartNew(Sub() Thread.Sleep(3000))
            Next
        Else
            ' SetMaxThreads failed.

        End If

        ' Wait all Task be over.
        Task.WaitAll(myTasks)

        ' When all Task is over.
        ' Do somthing.

    End Sub

    Public Sub AsynTaskWithParam(ByVal data As String)
        Dim OneThread As New ThreadWithParam(data, New ThreadCallback(AddressOf ThreadCallback))

        ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf OneThread.ProcessData))
    End Sub

    Public Sub ThreadCallback(ByVal text As String)
        ' Do something when callback.

    End Sub

End Class

Public Class ThreadWithParam
    Private _data As String
    Private _callback As ThreadCallback

    Public Sub New(ByVal data As String, ByVal callbackDelegate As ThreadCallback)
        _data = data
        _callback = callbackDelegate
    End Sub

    Public Sub ProcessData()
        Thread.Sleep(3000)
        _callback("callback process")
    End Sub

End Class



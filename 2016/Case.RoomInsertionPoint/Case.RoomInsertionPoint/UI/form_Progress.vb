Imports System.Windows.Forms

Public Class form_Progress

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="i"></param>
  ''' <remarks></remarks>
  Public Sub New(i As Integer)
    InitializeComponent()

    ' Setup
    With Me.ProgressBar1
      .Minimum = 0
      .Maximum = i
      .Value = 0
    End With

  End Sub

  ''' <summary>
  ''' Step Increment
  ''' </summary>
  ''' <remarks></remarks>
  Public Sub StepProgress()

    Try
      ProgressBar1.Increment(1)
      Application.DoEvents()
    Catch
    End Try

  End Sub

End Class
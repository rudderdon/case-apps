Imports System.Windows.Forms

Namespace UI

  Public Class form_Progress

    ''' <summary>
    ''' Display the Progress Form
    ''' </summary>
    ''' <param name="formTitle"></param>
    ''' <param name="maxValue"></param>
    ''' <remarks></remarks>
    Public Sub New(formTitle As String, maxValue As Integer)
      InitializeComponent()

      ' Form Setup
      Text = formTitle

      ' Progress Bar
      With ProgressBar1
        .Minimum = 0
        .Value = 0
        .Maximum = maxValue
      End With

    End Sub

    ''' <summary>
    ''' Step the Progress Value
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StepProgress()

      ' Step the Value
      Try
        ProgressBar1.Increment(1)
        Application.DoEvents()
      Catch
      End Try

    End Sub

  End Class
End Namespace
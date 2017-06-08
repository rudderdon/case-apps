Imports System.Windows.Forms
Imports System.IO
Imports [Case].RevClouds.API
Imports [Case].RevClouds.Data

Public Class form_RevisionClouds

  Private _s As clsSettings
  Private _error As Boolean = False

  ''' <summary>
  ''' Constructor
  ''' </summary>
  ''' <param name="p_settings"></param>
  ''' <remarks></remarks>
  Public Sub New(p_settings As clsSettings)
    InitializeComponent()

    ' Widen Scope
    _s = p_settings

    ' Form Displays
    Text = "Revision Cloud Reporting v" & _s.Version

    ' Load the Items
    LoadDatagrid()

  End Sub

  ''' <summary>
  ''' Load the Datagridview
  ''' </summary>
  ''' <remarks></remarks>
  Private Sub LoadDatagrid()

    Try

      ' Bind to Control
      DataGridViewRevs.DataSource = _s.RevisionClouds

    Catch
    End Try

  End Sub

  ''' <summary>
  ''' Write a Line to CSV using Filestream
  ''' </summary>
  ''' <param name="itemName"></param>
  ''' <remarks></remarks>
  Private Sub writeCSVline(ByVal itemName As String)

    Try

      ' Write a single line using a Filestream
      Using writer As StreamWriter = New StreamWriter(SaveFileDialogTXT.FileName, True)
        writer.WriteLine(itemName)
      End Using

    Catch

      If _error = False Then

        _error = True
        MsgBox("Failed to Write Line to CSV", MsgBoxStyle.Critical, "Error!")

      End If

    End Try

  End Sub

  ''' <summary>
  ''' Help Documentation
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonHelp_Click(sender As System.Object, e As System.EventArgs) Handles ButtonHelp.Click
    Process.Start("http://apps.case-inc.com/content/free-revision-cloud-data-export-text-file")
  End Sub

  ''' <summary>
  ''' Export the Data
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks></remarks>
  Private Sub ButtonExport_Click(sender As System.Object,
                                 e As System.EventArgs) Handles ButtonExport.Click

    Try

      ' Select a File to Save As
      SaveFileDialogTXT.ShowDialog()
      If Not String.IsNullOrEmpty(SaveFileDialogTXT.FileName) Then
        ' Start with clean file
        If File.Exists(SaveFileDialogTXT.FileName) = True Then
          Try
            ' Start with a clean file
            File.Delete(SaveFileDialogTXT.FileName)
          Catch ex As Exception
          End Try
        End If
      Else
        MsgBox("Please select a valid file name and location", MsgBoxStyle.Exclamation, "Error")
        Exit Sub
      End If
      ' Export the Headers
      Dim LineItem As String = ""
      For Each col As DataGridViewColumn In Me.DataGridViewRevs.Columns
        LineItem += col.HeaderText & vbTab
      Next
      ' Write the Line
      writeCSVline(LineItem)
      ' Iterate the Datarows and Export
      For Each x As DataGridViewRow In Me.DataGridViewRevs.Rows
        LineItem = ""
        For Each cell As DataGridViewCell In x.Cells
          Try
            LineItem += cell.Value.ToString & vbTab
          Catch
            LineItem += vbTab
          End Try
        Next
        ' Write the Line
        writeCSVline(LineItem)
      Next

    Catch
    End Try

    ' Close
    Close()

  End Sub

  Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
    Process.Start("http://www.case-inc.com")
  End Sub

End Class
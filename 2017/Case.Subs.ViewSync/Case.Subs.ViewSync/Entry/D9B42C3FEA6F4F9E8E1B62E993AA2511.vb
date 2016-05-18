Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Subs.ViewSync.Data

Namespace Entry

  ''' <summary>
  ''' Config
  ''' </summary>
  ''' <remarks></remarks>
  <Transaction(TransactionMode.Manual)>
  Public Class D9B42C3FEA6F4F9E8E1B62E993AA2511

    Implements IExternalCommand

    ''' <summary>
    ''' Command Entry Point
    ''' </summary>
    ''' <param name="commandData"></param>
    ''' <param name="message"></param>
    ''' <param name="elements"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Execute(ByVal commandData As ExternalCommandData,
                            ByRef message As String,
                            ByVal elements As ElementSet) As Result Implements IExternalCommand.Execute

      Try

        ' Version
        If Not commandData.Application.Application.VersionName.Contains("2017") Then

          Using td As New TaskDialog("Cannot Continue")
            With td
              .TitleAutoPrefix = False
              .MainInstruction = "Incompatible Revit Version"
              .MainContent = "This Add-In was built for Revit 2017, please contact CASE for assistance..."
              .Show()
            End With
          End Using
          Return Result.Cancelled

        End If


          ' Settings
          Dim m_s As New clsSettings(commandData, elements)

          ' Name Required
          If String.IsNullOrEmpty(m_s.DocName) Then
            message = "This tool requires that the model first be saved prior to use..."
            Return Result.Failed
          End If

          ' Master State
          If m_s.ConfigData.ConfigDataState = EnumCfgState.IsMissingParameter Then

            ' Add it for them?
            Using td As New TaskDialog("Action Required")
              With td

                .TitleAutoPrefix = False

                ' Info
                .MainInstruction = "Add Missing Parameter?"
                .MainContent = "This tool requires a 'Project Information' text parameter" & vbCr &
                               "named 'Case View Sync INI Path'... I can add this parameter for you if you like."

                ' Option Commands
                .AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Add Parameter: 'Case View Sync INI Path'")
                .AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "Cancel and do Nothing")

              End With

              ' Get Response
              Select Case td.Show

                Case TaskDialogResult.CommandLink1
                  If m_s.ConfigData.AddIniParameter(m_s.Doc, m_s.UiApp) = False Then
                    Return Result.Failed
                  Else
                    m_s.ConfigData.ConfigDataState = EnumCfgState.IsNullValue
                  End If

                Case Else
                  Return Result.Cancelled

              End Select

            End Using

          End If

          ' Settings Form
          Using d As New form_Config(m_s)
            d.ShowDialog()
          End Using

          ' Success
          Return Result.Succeeded

      Catch ex As Exception

        ' Failure
        message = ex.Message
        Return Result.Failed

      End Try

    End Function

  End Class
End Namespace
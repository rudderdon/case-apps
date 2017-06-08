Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Subs.Exceler8.Data

Namespace Entry

  ''' <summary>
  ''' Schedule Export
  ''' </summary>
  ''' <remarks></remarks>
  <Transaction(TransactionMode.Manual)>
  Public Class DE309158ACE741799F90E9210DEEF5F3

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


          ' Do they have Excel Installed?
          Dim m_s As New clsSettings(commandData, elements)
          If m_s.OfficeInstallVersion = EnumOfficeVersion.IsNotInstalled Then
            message = "This command depends on the installation of Microsoft Excel and was not found to be installed on your system. Cannot continue."
            Return Result.Failed
          End If
          If m_s.OfficeInstallVersion = EnumOfficeVersion.IsNotSupported Then
            message = "The version of Excel detected on your machine is not supported with this tool. We currently only support Excel 2010 and up. Cannot continue."
            Return Result.Failed
          End If

          ' Construct and Display the Form
          Using d As New form_ExportSchedule(m_s)
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
Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports [Case].Subs.Renamer.Data

Namespace Entry

  ''' <summary>
  ''' Revit 2013 Command Class
  ''' </summary>
  ''' <remarks></remarks>
  <Transaction(TransactionMode.Manual)>
  Public Class DD108DD4BDB24224BFB1848AEEA2582D

    Implements IExternalCommand

    ''' <summary>
    ''' Command Entry Point
    ''' </summary>
    ''' <param name="commandData">Input argument providing access to the Revit application and documents</param>
    ''' <param name="message">Return message to the user in case of error or cancel</param>
    ''' <param name="elements">Return argument to highlight elements on the graphics screen if Result is not Succeeded.</param>
    ''' <returns>Cancelled, Failed or Succeeded</returns>
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
          ' Construct and Display the Form
          Using d As New form_Rename(New clsSettings(commandData))

            ' Show It
            d.ShowDialog()

          End Using

          ' Success
          Return Result.Succeeded


      Catch ex As Exception

        ' Failure Message
        message = ex.Message
        Return Result.Failed

      End Try

    End Function

 

  End Class
End Namespace
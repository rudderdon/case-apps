Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Entry

  <Transaction(TransactionMode.Manual)>
  Public Class D4E12C09AFB84891A24F3AF186B13DD3

    Implements IExternalCommand

    ''' <summary>
    ''' 
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


          ' Construct and Display the Form
          Dim d As New form_ImportMain(New clsSettings(commandData))
          ' Show It
          d.ShowDialog()

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
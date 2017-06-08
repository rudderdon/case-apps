Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports System.Linq
Imports [Case].RoomInsertionPoint.API
Imports [Case].RoomInsertionPoint.Data

Namespace Entry

  <Transaction(TransactionMode.Manual)>
  Public Class CmdMain

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
        If Not commandData.Application.Application.VersionName.Contains("2018") Then

          ' Failure
          Using td As New TaskDialog("Cannot Continue")
            With td
              .TitleAutoPrefix = False
              .MainInstruction = "Incompatible Version of Revit"
              .MainContent = "This Add-In was built, please contact CASE for assistance."
              .Show()
            End With
          End Using
          Return Result.Cancelled

        End If

        ' Rooms with Area > 0
        Dim m_listRooms As IEnumerable(Of Architecture.Room) = From e In New FilteredElementCollector(commandData.Application.ActiveUIDocument.Document) _
                                                               .OfCategory(BuiltInCategory.OST_Rooms)
                                                               Let r = TryCast(e, Architecture.Room)
                                                               Select r Where r.Area > 0

        ' Spaces with Area > 0
        Dim m_listSpaces As IEnumerable(Of Autodesk.Revit.DB.Mechanical.Space) = From e In New FilteredElementCollector(commandData.Application.ActiveUIDocument.Document) _
                                                                                 .OfCategory(BuiltInCategory.OST_MEPSpaces)
                                                                                 Let r = TryCast(e, Autodesk.Revit.DB.Mechanical.Space)
                                                                                 Select r Where r.Area > 0

        ' Total
        Dim iTotal As Integer = 0
        If Not m_listRooms Is Nothing Then iTotal += m_listRooms.Count
        If Not m_listSpaces Is Nothing Then iTotal += m_listSpaces.Count

        ' Progress
        Using p As New form_Progress(iTotal)
          p.Show()

          ' Process Each Room
          If Not m_listRooms Is Nothing Then
            For Each x In m_listRooms.ToList

              Try
                p.StepProgress()
              Catch
              End Try

              ' Helper
              Dim m_e As New clsRoom(x)
              m_e.MoveToCentroid()

            Next
          End If

          ' Process Each Space
          If Not m_listSpaces Is Nothing Then
            For Each x In m_listSpaces.ToList

              Try
                p.StepProgress()
              Catch
              End Try

              ' Helper
              Dim m_e As New clsRoom(x)
              m_e.MoveToCentroid()

            Next
          End If

          ' Progress
          p.Close()

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
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Namespace Data

  ''' <summary>
  ''' A class to hold general settings in memory
  ''' </summary>
  ''' <remarks></remarks>
  Public Class clsSettings

    Private _ecd As ExternalCommandData

#Region "public Properties"

    Public ReadOnly Property Version As String
      Get
        Try
          Return My.Application.Info.Version.ToString
        Catch ex As Exception
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property Doc As Document
      Get
        Try
          Return _ecd.Application.ActiveUIDocument.Document
        Catch
          Return Nothing
        End Try
      End Get
    End Property
    Public ReadOnly Property DocName As String
      Get
        Try
          If Doc.IsWorkshared = True Then
            Try
              Return Doc.GetWorksharingCentralModelPath.CentralServerPath
            Catch ex As Exception
              Return "Detached Model - No Name"
            End Try
            If String.IsNullOrEmpty(DocName) Then DocName = "Detached Model - No Name"
          Else
            Return Doc.PathName
          End If
        Catch ex As Exception
          Return ""
        End Try
      End Get
    End Property
    Public Property RevisionClouds As SortableBindingList(Of clsRevcloud)
    Public Property Sheets As List(Of ViewSheet)

#End Region

    ''' <summary>
    ''' General Class Constructor
    ''' </summary>
    ''' <param name="cmddata"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal cmddata As ExternalCommandData)

      ' Widen Scope
      _ecd = cmddata

      ' Get the Rev Item Data
      RevisionClouds = New SortableBindingList(Of clsRevcloud)
      GetRevisionItemData()

    End Sub


    ''' <summary>
    ''' Get The Dat from Each Cloud Entity 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRevisionItemData()
      ' Collect Sheets
      Sheets = New List(Of ViewSheet)
      Dim m_shts As New List(Of Element)
      Dim m_sheetcol As New FilteredElementCollector(Doc)
      m_sheetcol.OfCategory(BuiltInCategory.OST_Sheets)
      m_shts = m_sheetcol.ToElements
      ' Cast each as sheets
      For Each x As ViewSheet In m_shts
        Sheets.Add(x)
      Next
      ' Collect Revision Clouds
      Dim m_rc As New List(Of Element)
      Dim m_RevCloudCollector As New FilteredElementCollector(Doc)
      m_RevCloudCollector.OfCategory(BuiltInCategory.OST_RevisionClouds)
      m_rc = m_RevCloudCollector.ToElements
      ' Iterate Each Element
      For Each rc As Element In m_rc
        ' Skip Types
        If rc.OwnerViewId.IntegerValue < 0 Then Continue For
        ' Construct the Data
        Dim m_revcloud As New clsRevcloud(rc)
        ' Directly on Sheet
        If m_revcloud.SheetNumber = "" Then
          ' What Sheet Was the View On?
          For Each x As ViewSheet In Sheets
            For Each y As ElementId In x.GetAllPlacedViews()
              If y = m_revcloud._v.Id Then
                ' This is the sheet
                m_revcloud.SheetNumber = x.SheetNumber
                m_revcloud.SheetName = x.ViewName
                GoTo GotSheetData
              End If
            Next
          Next
          m_revcloud.SheetNumber = "<not on sheet>"
          m_revcloud.SheetName = ""
GotSheetData:
        End If
        ' Add the Item to the master list
        RevisionClouds.Add(m_revcloud)
      Next
    End Sub

  End Class
End Namespace
Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsRevcloud

    Private _e As Element

#Region "Public Properties"

    Public Property SheetNumber As String
    Public Property SheetName As String
    Public Property ViewName As String
    Public ReadOnly Property RevisionNumber As String
      Get
        Try
          Return _e.Parameter(BuiltInParameter.REVISION_CLOUD_REVISION_NUM).AsString
        Catch
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property Description As String
      Get
        Try
          Return _e.Parameter(BuiltInParameter.REVISION_CLOUD_REVISION_DESCRIPTION).AsString
        Catch
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property RevisionDate As String
      Get
        Try
          Return _e.Parameter(BuiltInParameter.REVISION_CLOUD_REVISION_DATE).AsString
        Catch
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property IssuedTo As String
      Get
        Try
          Return _e.Parameter(BuiltInParameter.REVISION_CLOUD_REVISION_ISSUED_TO).AsString
        Catch
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property IssuedBy As String
      Get
        Try
          Return _e.Parameter(BuiltInParameter.REVISION_CLOUD_REVISION_ISSUED_BY).AsString
        Catch
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property Comments As String
      Get
        Try
          Return _e.LookupParameter("Comments").AsString
        Catch
          Return "{error}"
        End Try
      End Get
    End Property
    Public ReadOnly Property Mark As String
      Get
        Try
          Return _e.Parameter(BuiltInParameter.ALL_MODEL_MARK).AsString()
        Catch
          Return "{error}"
        End Try
      End Get
    End Property

    Public Property _v As View

#End Region

    ''' <summary>
    ''' General Class Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal elem As Element)

      ' Widen Scope
      _e = elem

      ' Setup
      doSetup()

    End Sub

    ''' <summary>
    ''' Class Setup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doSetup()

      Try

        ' Get the Full Data Stream
        GetItemData()

      Catch
      End Try

    End Sub

    ''' <summary>
    ''' Get all Required Data Items
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetItemData()

      Try

        ' Get the View's ID
        Dim m_v1id As New ElementId(_e.OwnerViewId.IntegerValue)
        _v = _e.Document.GetElement(m_v1id)
        ViewName = ""

        ' What type of View is it?
        If TypeOf _v Is ViewSheet Then

          ' It's a Sheet
          Dim m_sht As ViewSheet = TryCast(_v, ViewSheet)
          SheetNumber = m_sht.SheetNumber
          SheetName = m_sht.Name
          ViewName = "<directly on sheet>"

        Else

          ' We know the previous view was not a sheet
          ViewName = _v.ViewName

          ' Is it placed on a sheet?
          If _v.OwnerViewId.IntegerValue > 0 Then

            ' It's Not a Sheet
            Dim m_v2id As New ElementId(_v.OwnerViewId.IntegerValue)
            Dim m_view2 As View = _e.Document.GetElement(m_v1id)

            ' Cast to a Sheet
            Dim m_sht As ViewSheet = TryCast(m_view2, ViewSheet)
            SheetNumber = m_sht.SheetNumber
            SheetName = m_sht.Name

          Else

            ' It is NOT on a sheet
            SheetNumber = ""
            SheetName = ""

          End If

        End If

      Catch
      End Try

    End Sub

  End Class
End Namespace
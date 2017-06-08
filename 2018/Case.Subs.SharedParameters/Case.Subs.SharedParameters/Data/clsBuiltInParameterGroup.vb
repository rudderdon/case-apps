Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsBuiltInParameterGroup

    Private _pg As BuiltInParameterGroup

#Region "Public Properties"

    ''' <summary>
    ''' Name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DisplayName As String
      Get
        Try
          Return LabelUtils.GetLabelFor(_pg)
        Catch
        End Try
        Return "{error}"
      End Get
    End Property

    ''' <summary>
    ''' Group
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ParameterGroup As BuiltInParameterGroup
      Get
        Return _pg
      End Get
    End Property

#End Region

    ''' <summary>
    ''' Helper for Builtin Parameter Groups
    ''' </summary>
    ''' <param name="pg"></param>
    ''' <remarks></remarks>
    Public Sub New(pg As BuiltInParameterGroup)

      ' Widen Scope
      _pg = pg

    End Sub

  End Class
End Namespace
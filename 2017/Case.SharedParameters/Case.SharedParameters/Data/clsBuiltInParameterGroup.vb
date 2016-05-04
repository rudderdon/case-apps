Imports Autodesk.Revit.DB

Namespace Data

  Public Class clsBuiltInParameterGroup

    Public ReadOnly Property DisplayName As String
      Get
        Try
          Return LabelUtils.GetLabelFor(ParameterGroup)
        Catch
        End Try
        Return "{error}"
      End Get
    End Property
    Public Property ParameterGroup As BuiltInParameterGroup

    ''' <summary>
    ''' Helper for Builtin Parameter Groups
    ''' </summary>
    ''' <param name="pg"></param>
    ''' <remarks></remarks>
    Public Sub New(pg As BuiltInParameterGroup)

      ' Widen Scope
      ParameterGroup = pg
      
    End Sub

  End Class
End Namespace
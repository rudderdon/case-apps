Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI

Imports RMA.OpenNURBS

''' <summary>
''' Some form utilities
''' </summary>
''' <remarks></remarks>
Public Class clsFormUtil

  Private _doc As Document
  Private _app As Autodesk.Revit.UI.UIApplication

  Public Sub New(ByVal p_doc As Document, ByVal p_app As Autodesk.Revit.UI.UIApplication)

    'Widen Scope
    _doc = p_doc
    _app = p_app

  End Sub

  ''' <summary>
  ''' Creates a plane from 3 points.
  ''' </summary>
  ''' <param name="xyz1"></param>
  ''' <param name="xyz2"></param>
  ''' <param name="xyz3"></param>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Function CreateSketchPlane(ByVal xyz1 As XYZ, ByVal xyz2 As XYZ, ByVal xyz3 As XYZ) As SketchPlane
    'define sketch plane
    Dim worldOrigin As XYZ = XYZ.Zero
    Dim plnLine1 As Line = Line.CreateBound(xyz1, xyz2)
    Dim plnLine2 As Line = Line.CreateBound(xyz1, xyz3)

    Dim crvArray As New CurveArray()
    crvArray.Append(plnLine1)
    crvArray.Append(plnLine2)

    Dim myPlane As Plane = _app.Application.Create.NewPlane(crvArray)

    'create the SketchPlanes and ModelCurves
    Dim skplane As SketchPlane
    If _doc.IsFamilyDocument Then
      skplane = SketchPlane.Create(_doc, myPlane)
    Else
      skplane = SketchPlane.Create(_doc, myPlane)
    End If

    Return skplane

  End Function

End Class
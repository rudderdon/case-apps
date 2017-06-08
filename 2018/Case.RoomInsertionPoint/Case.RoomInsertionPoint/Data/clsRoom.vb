Imports System.Drawing
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.DB.Architecture

Namespace Data

  Public Class clsRoom

    Private _r As Room
    Private _s As Mechanical.Space
    Private _ip As XYZ

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="r"></param>
    ''' <remarks></remarks>
    Public Sub New(r As Room)

      ' Widen Scope
      _r = r

      ' Setup
      doSetup()

    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="s"></param>
    ''' <remarks></remarks>
    Public Sub New(s As Mechanical.Space)

      ' Widen Scope
      _s = s

      ' Setup
      doSetup()

    End Sub

    ''' <summary>
    ''' Setup the Class
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doSetup()

      ' Find the Centroid of Room
      If Not _r Is Nothing Then ExtractBoundaryPointsFromRoom(_r)
      If Not _s Is Nothing Then ExtractBoundaryPointsFromSpace(_s)

    End Sub

    ''' <summary>
    ''' Find 2D Centroid
    ''' </summary>
    ''' <param name="pts">Collection of Points Describing the Polygon</param>
    ''' <param name="pArea">The Area of the Polygon</param>
    ''' <returns>2D Point (Pointf)</returns>
    ''' <remarks>This Function Kicks Ass</remarks>
    Private Function GetCentroid(ByVal pts() As PointF, pArea As Single) As PointF

      Try

        ' Add the First PT to the End of the Array (full circulation)
        ReDim Preserve pts(pts.Length)
        pts(pts.Length - 1) = New PointF(pts(0).X, pts(0).Y)

        ' Get the Centroid
        Dim X As Single = 0
        Dim Y As Single = 0
        Dim sf As Single

        ' This is Where the Magic Happens
        For i As Integer = 0 To pts.Length - 2 'Step 2
          sf = pts(i).X * pts(i + 1).Y - pts(i + 1).X * pts(i).Y
          X += (pts(i).X + pts(i + 1).X) * sf
          Y += (pts(i).Y + pts(i + 1).Y) * sf
        Next i

        ' Divide by 6X the Are of the Polygon
        X /= (6 * pArea)
        Y /= (6 * pArea)

        ' This is the Final Result
        Return New PointF(X, Y)

      Catch
      End Try

    End Function

#Region "Private Members - Spaces"

    ''' <summary>
    ''' Extract a List of 2D Points from a Room's Boundary
    ''' </summary>
    ''' <param name="s"></param>
    ''' <remarks></remarks>
    Private Sub ExtractBoundaryPointsFromSpace(s As Mechanical.Space)

      Try

        ' The Points List
        Dim m_pts As New System.Collections.Generic.List(Of PointF)

        ' The Z Height
        Dim m_z As Double = 0

        ' Work with the Boundary
        Dim m_opt As New SpatialElementBoundaryOptions

        Dim m_bsaa As IList(Of IList(Of Autodesk.Revit.DB.BoundarySegment)) = s.GetBoundarySegments(m_opt) ' r.Boundary

        ' Segment Array at Floor Level
        For Each bsa As IList(Of Autodesk.Revit.DB.BoundarySegment) In m_bsaa
          Try

            For Each bs As Autodesk.Revit.DB.BoundarySegment In bsa

              Dim m_c As Curve = bs.GetCurve()
              If TypeOf m_c Is Arc Then

                ' First Endpoint
                Dim m_EndPoint1 As XYZ = m_c.GetEndPoint(0)
                m_z = m_EndPoint1(2)
                Dim m_PointF1 As New PointF(m_EndPoint1.X, m_EndPoint1.Y)
                m_pts.Add(m_PointF1)

                ' Midpoint
                Dim m_EndPoint2 As XYZ = m_c.Evaluate(0.5, True)
                Dim m_PointF2 As New PointF(m_EndPoint2.X, m_EndPoint2.Y)
                m_pts.Add(m_PointF2)

                ' Second Endpoint
                Dim m_EndPoint3 As XYZ = m_c.GetEndPoint(1)
                Dim m_PointF3 As New PointF(m_EndPoint3.X, m_EndPoint3.Y)
                m_pts.Add(m_PointF3)

              Else

                ' First Endpoint
                Dim m_EndPoint1 As XYZ = m_c.GetEndPoint(0)
                m_z = m_EndPoint1(2)
                Dim m_PointF1 As New PointF(m_EndPoint1.X, m_EndPoint1.Y)
                m_pts.Add(m_PointF1)

                ' Second Endpoint
                Dim m_EndPoint2 As XYZ = m_c.GetEndPoint(1)
                Dim m_PointF2 As New PointF(m_EndPoint2.X, m_EndPoint2.Y)
                m_pts.Add(m_PointF2)

              End If

            Next
          Catch
          End Try

        Next

        ' Return the 2D Centroid
        Dim m_2Dcentroid As PointF = GetCentroid(m_pts.ToArray, _s.Area)

        ' Add the Floor Level of Boundary for Z Elevation
        _ip = New XYZ(m_2Dcentroid.X, m_2Dcentroid.Y, m_z + 1)

      Catch
      End Try

    End Sub

#End Region

#Region "Private Members - Rooms"

    ''' <summary>
    ''' Extract a List of 2D Points from a Room's Boundary
    ''' </summary>
    ''' <param name="r"></param>
    ''' <remarks></remarks>
    Private Sub ExtractBoundaryPointsFromRoom(r As Room)

      Try

        ' The Points List
        Dim m_pts As New System.Collections.Generic.List(Of PointF)

        ' The Z Height
        Dim m_z As Double = 0

        ' Work with the Boundary
        Dim m_opt As New SpatialElementBoundaryOptions

        Dim m_bsaa As IList(Of IList(Of Autodesk.Revit.DB.BoundarySegment)) = r.GetBoundarySegments(m_opt) ' r.Boundary

        '' ''Dim m_bsaa As Autodesk.Revit.DB.Architecture.BoundarySegmentArrayArray = r.GetBoundarySegments(m_opt) ' r.Boundary

        ' Segment Array at Floor Level
        For Each bsa As IList(Of Autodesk.Revit.DB.BoundarySegment) In m_bsaa
          Try

            For Each bs As Autodesk.Revit.DB.BoundarySegment In bsa

              Dim m_c As Curve = bs.GetCurve()
              If TypeOf m_c Is Arc Then

                ' First Endpoint
                Dim m_EndPoint1 As XYZ = m_c.GetEndPoint(0)
                m_z = m_EndPoint1(2)
                Dim m_PointF1 As New PointF(m_EndPoint1.X, m_EndPoint1.Y)
                m_pts.Add(m_PointF1)

                ' Midpoint
                Dim m_EndPoint2 As XYZ = m_c.Evaluate(0.5, True)
                Dim m_PointF2 As New PointF(m_EndPoint2.X, m_EndPoint2.Y)
                m_pts.Add(m_PointF2)

                ' Second Endpoint
                Dim m_EndPoint3 As XYZ = m_c.GetEndPoint(1)
                Dim m_PointF3 As New PointF(m_EndPoint3.X, m_EndPoint3.Y)
                m_pts.Add(m_PointF3)

              Else

                ' First Endpoint
                Dim m_EndPoint1 As XYZ = m_c.GetEndPoint(0)
                m_z = m_EndPoint1(2)
                Dim m_PointF1 As New PointF(m_EndPoint1.X, m_EndPoint1.Y)
                m_pts.Add(m_PointF1)

                ' Second Endpoint
                Dim m_EndPoint2 As XYZ = m_c.GetEndPoint(1)
                Dim m_PointF2 As New PointF(m_EndPoint2.X, m_EndPoint2.Y)
                m_pts.Add(m_PointF2)

              End If

            Next
          Catch
          End Try

        Next

        ' Return the 2D Centroid
        Dim m_2Dcentroid As PointF = GetCentroid(m_pts.ToArray, _r.Area)

        ' Add the Floor Level of Boundary for Z Elevation
        _ip = New XYZ(m_2Dcentroid.X, m_2Dcentroid.Y, m_z + 1)

      Catch
      End Try

    End Sub

#End Region

#Region "Public Members"

    ''' <summary>
    ''' Move Room to Centroid
    ''' </summary>
    ''' <remarks></remarks>
    Public Function TestMoveToCentroid() As Boolean

      Try

        ' Document
        Dim m_doc As Document = Nothing

        ' Room?
        If Not _r Is Nothing Then

          m_doc = _r.Document

        Else

          m_doc = _s.Document

        End If

        ' New Transaction
        Using t As New Transaction(m_doc, "Room Insertion Point")
          If t.Start = TransactionStatus.Started Then

            Try

              If Not _r Is Nothing Then

                ' Location
                Dim m_loc As Location = TryCast(_r.Location, Location)
                Dim m_inpt As XYZ = TryCast(_r.Location, LocationPoint).Point
                Dim m_newXyz As XYZ = Nothing 'GetRoomCentroid(_r)

                ' Valid Point?
                If _r.IsPointInRoom(m_newXyz) = True Then

                  ' Move Room
                  m_loc.Move(m_newXyz - m_inpt)

                End If

              Else

                ' Location
                Dim m_loc As Location = TryCast(_s.Location, Location)
                Dim m_inpt As XYZ = TryCast(_s.Location, LocationPoint).Point

                ' Difference
                Dim m_XYZ As New XYZ(_ip.X - m_inpt.X,
                                     _ip.Y - m_inpt.Y,
                                     _ip.Y)

                ' Valid Point?
                If _s.IsPointInSpace(_ip) = True Then

                  ' Move Room
                  m_loc.Move(m_XYZ)

                End If

              End If

              ' Success
              t.Commit()
              Return True

            Catch
            End Try

          End If
        End Using

      Catch
      End Try

      ' Failure
      Return False

    End Function

    ''' <summary>
    ''' Move Room to Centroid
    ''' </summary>
    ''' <remarks></remarks>
    Public Function MoveToCentroid() As Boolean

      Try

        ' Document
        Dim m_doc As Document = Nothing

        ' Room?
        If Not _r Is Nothing Then

          m_doc = _r.Document

        Else

          m_doc = _s.Document

        End If

        ' New Transaction
        Using t As New Transaction(m_doc, "Room Insertion Point")
          If t.Start = TransactionStatus.Started Then

            Try

              If Not _r Is Nothing Then

                ' Location
                Dim m_loc As Location = TryCast(_r.Location, Location)
                Dim m_inpt As XYZ = TryCast(_r.Location, LocationPoint).Point

                ' Difference
                Dim m_XYZ As New XYZ(_ip.X - m_inpt.X,
                                     _ip.Y - m_inpt.Y,
                                     _ip.Z)

                ' Valid Point?
                If _r.IsPointInRoom(_ip) = True Then

                  ' Move Room
                  m_loc.Move(m_XYZ)

                End If

              Else

                ' Location
                Dim m_loc As Location = TryCast(_s.Location, Location)
                Dim m_inpt As XYZ = TryCast(_s.Location, LocationPoint).Point

                ' Difference
                Dim m_XYZ As New XYZ(_ip.X - m_inpt.X,
                                     _ip.Y - m_inpt.Y,
                                     _ip.Y)

                ' Valid Point?
                If _s.IsPointInSpace(_ip) = True Then

                  ' Move Room
                  m_loc.Move(m_XYZ)

                End If

              End If

              ' Success
              t.Commit()
              Return True

            Catch
            End Try

          End If
        End Using

      Catch
      End Try

      ' Failure
      Return False

    End Function

#End Region

  End Class
End Namespace
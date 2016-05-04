Imports Autodesk.Revit.ApplicationServices
Imports Autodesk.Revit.Attributes
Imports Autodesk.Revit.DB
Imports Autodesk.Revit.UI
Imports Autodesk.Revit.UI.Selection

<Transaction(TransactionMode.Automatic)> _
Class Application
    Implements IExternalApplication
    ''' <summary>
    ''' Implement the external application
    ''' </summary>
    ''' <param name="application">An object that is passed to the external application which contains the controlled application.</param>
    ''' <returns>Return the status of the external application. A result of Succeeded means that the external application successfully started. Cancelled can be used to signify that the user cancelled the external operation at some point. If false is returned then Revit should inform the user that the external application failed to load and the release the internal reference.</returns>
    Public Function OnStartup(ByVal application As UIControlledApplication) As Result Implements IExternalApplication.OnStartup
        ' Add your code here


        ' Return Success
        Return Result.Succeeded
    End Function

    ''' <summary>
    ''' Implement the external application
    ''' </summary>
    ''' <param name="application">An object that is passed to the external application which contains the controlled application.</param>
    ''' <returns>Return the status of the external application. A result of Succeeded means that the external application successfully shutdown. Cancelled can be used to signify that the user cancelled the external operation at some point. If false is returned then the Revit user should be warned of the failure of the external application to shut down correctly.</returns>
    Public Function OnShutdown(ByVal application As UIControlledApplication) As Result Implements IExternalApplication.OnShutdown
        ' Return Success
        Return Result.Succeeded
    End Function
End Class
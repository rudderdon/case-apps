using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Case.Subs.KeyMatcher.Utility;

namespace Case.Subs.KeyMatcher.Entry
{

  [Transaction(TransactionMode.Manual)]
  public class CmdMain : IExternalCommand, IExternalCommandAvailability
  {

    /// <summary>
    /// Report Groups by View
    /// </summary>
    /// <param name="commandData"></param>
    /// <param name="message"></param>
    /// <param name="elements"></param>
    /// <returns></returns>
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {

      try
      {

        // Version
        if (!commandData.Application.Application.VersionName.Contains(clsGlobals.RevitYear))
        {
          clsGlobals.ShowTaskDialog(
            "Cannot Continue",
            "Revit Version Not Compatible",
            string.Format("This Add-In was built for Revit {0} and is not compatible with this version of Revit.\n\nPlease contact info@case-inc.com for assistance...", clsGlobals.RevitYear));
          return Result.Cancelled;
        }

        // Success
        return Result.Succeeded;

      }
      catch (Exception ex)
      {

        // Failure
        message = ex.Message;
        return Result.Failed;

      }

    }

    /// <summary>
    /// Command Availability
    /// </summary>
    /// <param name="applicationData"></param>
    /// <param name="selectedCategories"></param>
    /// <returns></returns>
    public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
    {
      return applicationData.ActiveUIDocument != null;
    }

  }
}
using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Case.FamilySubcategories.Data;
using Case.FamilySubcategories.UI;

namespace Case.FamilySubcategories.Entry
{
  [Transaction(TransactionMode.Manual)]
  public class CmdMain : IExternalCommand
  {
    /// <summary>
    ///   Report Groups by View
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
        if (!commandData.Application.Application.VersionName.Contains("2017"))
        {
          // Failure
          using (var td = new TaskDialog("Cannot Continue"))
          {
            td.TitleAutoPrefix = false;
            td.MainInstruction = "Incompatible Version of Revit";
            td.MainContent = "This Add-In was built for Revit 2017, please contact CASE for assistance.";
            td.Show();
          }
          return Result.Cancelled;
        }

        // Settings
        var m_s = new clsSettings(commandData);

        // Form
        using (var d = new form_Main(m_s))
        {
          d.ShowDialog();
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
  }
}
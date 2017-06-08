// Case.DimensionOverrides
// CmdMain.cs
// mnelson-CASE
// 2017/05/18/9:27 PM

using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Case.DimensionOverrides.Data;
using Case.DimensionOverrides.UI;

namespace Case.DimensionOverrides.Entry
{
  [Transaction(TransactionMode.Manual)]
  public class CmdMain : IExternalCommand
  {
    /// <summary>
    ///   Override Dimensions
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
          using (TaskDialog td = new TaskDialog("Cannot Continue"))
          {
            td.TitleAutoPrefix = false;
            td.MainInstruction = "Incompatible Version of Revit";
            td.MainContent = "This Add-In was built for Revit 2017, please contact CASE for assistance.";
            td.Show();
          }
          return Result.Cancelled;
        }

        // Settings
        clsSettings m_s = new clsSettings(commandData);


        // Form
        using (form_Main d = new form_Main(m_s))
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
using System.Windows.Forms.Integration;
using Autodesk.Navisworks.Api.Plugins;
using System.Windows;

namespace CASE.Navis2BCF
{

  /// <summary>
  /// Navis custom panel generation
  /// </summary>
  [Plugin("Navis2BCF.Plugin", "CASE", DisplayName = "CASE BCF Exporter", ToolTip = "Navisworks to BCF")]
  [DockPanePlugin(400, 200, FixedSize = false)]
  class Plugin : DockPanePlugin
  {

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override System.Windows.Forms.Control CreateControlPane()
    {
      
      //create an ElementHost
      ElementHost eh = new ElementHost();
      eh.AutoSize = true;
      eh.Child = new Navis2JiraWin();
      eh.CreateControl();

      //return the ElementHost
      return eh;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pane"></param>
    public override void DestroyControlPane(System.Windows.Forms.Control pane)
    {
      pane.Dispose();
    }

  }
}
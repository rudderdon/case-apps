using System;
using System.Linq;
using Autodesk.Navisworks.Api.Plugins;


namespace Navis2BCF
{
    [Plugin("Navis2BCF.Addin", "CASE",
      DisplayName = "CASE BCF Exporter",
      ToolTip = "CASE Navisworks to BCF Exporter")]
    class Addin : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            
            if (Autodesk.Navisworks.Api.Application.IsAutomated)
            {
                throw new InvalidOperationException("Invalid when running using Automation");
            }

            
                   //Find the plugin
            PluginRecord pr =
               Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("Navis2BCF.Plugin.CASE");

            if (pr != null && pr is DockPanePluginRecord && pr.IsEnabled)
            {
                Autodesk.Navisworks.Api.Application.Plugins.AddPluginAssembly(@"C:\Program Files\Autodesk\Navisworks Manage 2014\Plugins\Navis2BCF\Ionic.Zip.dll");
     
                //check if it needs loading
                if (pr.LoadedPlugin == null)
                {
                      
                    string exeConfigPath = this.GetType().Assembly.Location;
                  
                    pr.LoadPlugin();
                }

                DockPanePlugin dpp = pr.LoadedPlugin as DockPanePlugin;
                if (dpp != null)
                {
                    //switch the Visible flag
                    dpp.Visible = !dpp.Visible;
                }
            }
            return 0;
        }
    }
}

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Autodesk.Navisworks.Api.Plugins;

namespace CASE.Navis2BCF
{

  [Plugin("CASERibbon", "CASE", DisplayName = "CASE Ribbon")]
  [RibbonLayout("CustomRibbon.xaml")]
  [RibbonTab("ID_CustomTab_1", DisplayName = "CASE")]
  [Command("ID_Navis2BCF", DisplayName = "BCF Exporter", Icon = "navis2bcf16x16.ico", LargeIcon = "navis2bcf32x32.ico", ToolTip = "CASE BCF Exporter", ExtendedToolTip = "Export viewpoints as BIM Collaboration Format")]
  class CASERibbon : CommandHandlerPlugin
  {

    /// <summary>
    /// Constructor
    /// </summary>
    public CASERibbon()
    {
     
    }

    #region Public Members

    /// <summary>
    /// Execute Main Command
    /// </summary>
    /// <param name="commandId"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override int ExecuteCommand(string commandId, params string[] parameters)
    {

      // Version
      if (!Autodesk.Navisworks.Api.Application.Version.RuntimeProductName.Contains("2018"))
      {
        MessageBox.Show("Incompatible Navisworks Version" +
                        "\nThis Add-In was built for Navisworks 2018, please contact info@case-inc for assistance...",
                        "Cannot Continue!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        return 0;
      }

      switch (commandId)
      {
        case "ID_Navis2BCF":
          {
            LaunchNavis2Bcf();
            break;
          }

        default:
          {
            MessageBox.Show("You have clicked on the command with ID = '" + commandId + "'");
            break;
          }
      }

      return 0;
    }

    /// <summary>
    /// Help Button Visibility Handler
    /// </summary>
    /// <param name="commandId"></param>
    /// <returns></returns>
    public override bool TryShowCommandHelp(String commandId)
    {
      MessageBox.Show("Showing Help for command with the Id " + commandId);
      return true;
    }

    /// <summary>
    /// Launch Main Command
    /// </summary>
    public void LaunchNavis2Bcf()
    {

      // Don't Support Automation
      if (Autodesk.Navisworks.Api.Application.IsAutomated)
      {
        throw new InvalidOperationException("Invalid when running using Automation");
      }

      //Find the plugin
      PluginRecord m_pr = Autodesk.Navisworks.Api.Application.Plugins.FindPlugin("Navis2BCF.Plugin.CASE");
      if (m_pr != null && m_pr is DockPanePluginRecord && m_pr.IsEnabled)
      {
          //need to load the assebly from here otherwise navis will crash!
          string m_dllfolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Ionic.Zip.dll");
          if (!File.Exists(m_dllfolder))
          {
              MessageBox.Show("Required Ionic Library Not Found");
              return;
          }
          Assembly.LoadFrom(m_dllfolder);

        // check if it needs loading
        if (m_pr.LoadedPlugin == null)
        {
          string exeConfigPath = this.GetType().Assembly.Location;
          m_pr.LoadPlugin();
        }

        DockPanePlugin m_dpp = m_pr.LoadedPlugin as DockPanePlugin;
        if (m_dpp != null)
        {

          // switch the Visible flag
          m_dpp.Visible = !m_dpp.Visible;

        }
      }

    }

    #endregion

  }
}
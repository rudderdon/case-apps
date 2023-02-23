using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;
using Autodesk.Navisworks.Api;
using Microsoft.VisualBasic.ApplicationServices;
using ComApi = Autodesk.Navisworks.Api.Interop.ComApi;
using ComBridge = Autodesk.Navisworks.Api.ComApi.ComApiBridge;

namespace CASE.Navis2BCF
{

    /// <summary>
    /// Interaction logic for Navis2JiraWin.xaml
    /// </summary>
    public partial class Navis2JiraWin : UserControl
    {

        Jira jira = new Jira();
        // double _ftConversion = 3.2808;
        string _tempFolder = "";
        string _zipFileName = "";
        private int _elemCheck = 2;

        //navis stuff
        Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;
        ComApi.InwOpState10 oState = null;
        ComApi.InwOaPropertyVec options = null;

        List<ModelItem> elementList = new List<ModelItem>();

        /// <summary>
        /// Constructor
        /// </summary>
        public Navis2JiraWin()
        {

            InitializeComponent();
            DataContext = jira;
            jira.IssuesBCFCollection = new ObservableCollection<IssueBCF>();
            oState = ComBridge.State;

            Refresh();

        }

        #region Form Controls & Events

        /// <summary>
        /// user clicks the send button
        /// prompts for a save locatio 
        /// creates a background worker to update the progressbar and package the issues
        /// </summary>
        private void send_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                // Assembly Data for Tracking
                AssemblyInfo m_a = new AssemblyInfo(Assembly.GetExecutingAssembly());

 
            }
            catch { }

            try
            {

                if (all.IsChecked.Value)
                    _elemCheck = 0;
                else if (selected.IsChecked.Value)
                    _elemCheck = 1;

                // Configure save file dialog box
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.Title = "Save as BCF report file...";
                saveFileDialog.DefaultExt = ".bcfzip"; // Default file extension
                saveFileDialog.Filter = "BIM Collaboration Format (*.bcfzip)|*.bcfzip"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = saveFileDialog.ShowDialog();

                // Process save file dialog box results
                if (result == true && saveFileDialog.FileName != "")
                {
                    _zipFileName = saveFileDialog.FileName;
                    if (File.Exists(_zipFileName))
                        File.Delete(_zipFileName);
                    //to avoid double clicks
                    mainwin.IsEnabled = false;
                    progress.Visibility = System.Visibility.Visible;
                    progress.Value = 0;
                    progress.IsIndeterminate = false;
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerReportsProgress = true;
                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    worker.ProgressChanged += new ProgressChangedEventHandler(MyWorker_ProgressChanged);
                    worker.RunWorkerAsync();
                    worker.Dispose();
                }
            }
            catch (System.Exception ex1)
            {
                MessageBox.Show("exception: " + ex1);
            }
        }

        /// <summary>
        /// manual refresh of the list
        /// </summary>
        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// removes seleted items from the list
        /// </summary>
        private void RemoveSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                List<int> selectedInts = new List<int>();
                for (int i = 0; i < issueList.SelectedItems.Count; i++)
                {
                    int index = issueList.Items.IndexOf(issueList.SelectedItems[i]);
                    selectedInts.Add(index);
                }
                selectedInts.Sort();
                selectedInts.Reverse();

                for (int l = 0; l < selectedInts.Count; l++)
                {
                    jira.IssuesBCFCollection.RemoveAt(selectedInts[l]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// go to case help page
        /// </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://apps.case-inc.com/content/free-bcf-exporter-navisworks-manage-2014");
        }

        #endregion

        #region Private Members

        /// <summary>
        /// loops and flattens the savedviewpoints in navisworks and then adds them to collection used
        /// as datacontext
        /// </summary>
        private void Refresh()
        {
            try
            {
                Document oDoc = Autodesk.Navisworks.Api.Application.ActiveDocument;
                jira.IssuesBCFCollection = new ObservableCollection<IssueBCF>();
                foreach (SavedItem oSI in oDoc.SavedViewpoints.ToSavedItemCollection())
                {
                    RecurseItems(oSI);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// forther iteration on the three of saved viewpoints
        /// </summary>
        private void RecurseItems(SavedItem oSI)
        {
            try
            {
                Autodesk.Navisworks.Api.GroupItem group = oSI as Autodesk.Navisworks.Api.GroupItem;
                if (null != group)//is a group
                {
                    foreach (SavedItem oSII in group.Children)
                    {
                        RecurseItems(oSII);
                    }
                }
                else
                {
                    IssueBCF ib = new IssueBCF();
                    ib.viewpoint = oSI as SavedViewpoint;
                    jira.IssuesBCFCollection.Add(ib);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        /// <summary>
        /// Background Worker
        /// </summary>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            //create a temporary directory
            _tempFolder = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "CASE.Navis2BCF", System.IO.Path.GetRandomFileName());
            if (!Directory.Exists(_tempFolder))
            {
                Directory.CreateDirectory(_tempFolder);
            }

            //if set to attach selected elements, do it only once
            if (_elemCheck == 1)
                elementList = oDoc.CurrentSelection.SelectedItems.Where(o => o.InstanceGuid != Guid.Empty).ToList<ModelItem>();

            for (int i = 0; i < jira.IssuesBCFCollection.Count(); i++)
            {
                try
                {
                    IssueBCF issue = jira.IssuesBCFCollection[i];

                    //for each issue create a subdirectory named as the guid
                    string g = Guid.NewGuid().ToString();
                    string issueFolder = System.IO.Path.Combine(_tempFolder, g);
                    DirectoryInfo di = Directory.CreateDirectory(issueFolder);

                    // get the state of COM
                    ComApi.InwOpState10 oState = ComBridge.State;
                    // get the IO plugin for image
                    ComApi.InwOaPropertyVec options = oState.GetIOPluginOptions("lcodpimage");
                    // configure the option "export.image.format" to export png and image size
                    foreach (ComApi.InwOaProperty opt in options.Properties())
                    {
                        if (opt.name == "export.image.format")
                            opt.value = "lcodpexpng";
                        if (opt.name == "export.image.width")
                            opt.value = 1600;
                        if (opt.name == "export.image.height")
                            opt.value = 900;
                    }


                    string snapshot = System.IO.Path.Combine(issueFolder, "snapshot.png");

                    XDocument v = new XDocument();

                    //need to use a dispatcher to access resource on a different process
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        oDoc.SavedViewpoints.CurrentSavedViewpoint = issue.viewpoint;
                        v = generateViewpoint(issue.viewpoint.Viewpoint);
                    }));
                    XDocument m = new XDocument();
                    m = generateMarkup(issue, g);
                    //set the view to the saved one

                    //export the viewpoint to the image
                    oState.DriveIOPlugin("lcodpimage", snapshot, options);
                    System.Drawing.Bitmap oBitmap = new System.Drawing.Bitmap(snapshot);
                    System.IO.MemoryStream ImageStream = new System.IO.MemoryStream();
                    oBitmap.Save(ImageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    oBitmap.Dispose();
                    v.Save(issueFolder + @"\viewpoint.bcfv");
                    m.Save(issueFolder + @"\markup.bcf");
                } // END TRY
                catch (System.Exception ex1)
                {
                    MessageBox.Show("exception: " + ex1);
                }
                worker.ReportProgress((100 * (i + 1)) / jira.IssuesBCFCollection.Count());// HAS TO BE OUT OF THE DISPATCHER!
            }// END LOOP
        }

        /// <summary>
        /// Worker Completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                //create a zip file  
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                {
                    zip.AddDirectory(_tempFolder);
                    zip.Save(_zipFileName);
                }

                //delete temp dir
                DeleteDirectory(_tempFolder);
                string c;
                if (jira.IssuesBCFCollection.Count() == 1)
                    c = " Issue";
                else
                    c = " Issues";
                MessageBox.Show(jira.IssuesBCFCollection.Count().ToString() + c + " exported successfully!");
                progress.Value = 0;
                progress.Visibility = Visibility.Hidden;
                mainwin.IsEnabled = true;
            } // END TRY
            catch (System.Exception ex1)
            {
                MessageBox.Show("exception: " + ex1);
            }

        }

        /// <summary>
        /// Progress Changed Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress.Value = Math.Min(e.ProgressPercentage, 100);
        }

        /// <summary>
        /// Delete a Directory
        /// </summary>
        /// <param name="target_dir"></param>
        private static void DeleteDirectory(string target_dir)
        {
            if (Directory.Exists(target_dir))
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string dir in dirs)
                {
                    DeleteDirectory(dir);
                }

                Directory.Delete(target_dir, false);
            }
        }

        #endregion

        #region Private Members - tools to generate viewpoint and stuff

        /// <summary>
        /// Generate a Markup
        /// </summary>
        /// <param name="issue"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        private XDocument generateMarkup(IssueBCF issue, string g)
        {
            DateTime m_now = DateTime.Now;
            XDocument m_markup = new XDocument(
                                      new XElement("Markup",
                                         new XElement("Header",
                                             new XElement("File", new XAttribute("IfcProject", ""),
                                                 new XElement("Filename", System.IO.Path.GetFileName(Autodesk.Navisworks.Api.Application.ActiveDocument.FileName)),
                                                 new XElement("Date", m_now.ToString("o")))),
                                         new XElement("Topic", new XAttribute("Guid", g),
                                             new XElement("ReferenceLink"),
                                             new XElement("Title", issue.viewpoint.DisplayName))));

            foreach (var comm in issue.viewpoint.Comments)
            {
                m_markup.Element("Markup").Add(new XElement("Comment", new XAttribute("Guid", Guid.NewGuid().ToString()),
                    new XElement("VerbalStatus", comm.Status.ToString()),
                    new XElement("Status", "Unknown"),
                    new XElement("Date", comm.CreationDate.ToLocalTime().ToString("o")),
                    new XElement("Author", comm.Author),
                    new XElement("Comment", comm.Body),
                    new XElement("Topic", new XAttribute("Guid", g))));

            }
            return m_markup;
        }

        /// <summary>
        /// convert navisworks coordinates to the ones used by BCF
        /// </summary>
        /// <param name="oVP"></param>
        /// <returns></returns>
        private XDocument generateViewpoint(Viewpoint oVP)
        {

            double units = GetGunits();

            Vector3D vi = getViewDir(oVP);
            Vector3D up = getViewUp(oVP);
            Point3D c = new Point3D(oVP.Position.X / units, oVP.Position.Y / units, oVP.Position.Z / units);
            string type = "";
            string zoom = "";
            double zoomValue = 1;

            oVP = oVP.CreateCopy();
            if (!oVP.HasFocalDistance)
                oVP.FocalDistance = 1;


            if (oVP.Projection == ViewpointProjection.Orthographic) //IS ORTHO
            {
                type = "OrthogonalCamera";
                zoom = "ViewToWorldScale";
                // zoomValue = oVP.VerticalExtentAtFocalDistance / 2 / _ftConversion;

                double dist = oVP.VerticalExtentAtFocalDistance / 2 / units;
                zoomValue = 3.125 * dist / up.Length;

            }
            else // it is a perspective view
            {
                type = "PerspectiveCamera";
                zoom = "FieldOfView";
                zoomValue = oVP.FocalDistance;
            }

            XDocument v = new XDocument(
            new XElement("VisualizationInfo",
                new XElement("Components"),
                new XElement(type,
                    new XElement("CameraViewPoint",
                        new XElement("X", c.X.ToString(CultureInfo.InvariantCulture)),
                        new XElement("Y", c.Y.ToString(CultureInfo.InvariantCulture)),
                        new XElement("Z", c.Z.ToString(CultureInfo.InvariantCulture))),
                    new XElement("CameraDirection",
                        new XElement("X", vi.X.ToString(CultureInfo.InvariantCulture)),
                        new XElement("Y", vi.Y.ToString(CultureInfo.InvariantCulture)),
                        new XElement("Z", vi.Z.ToString(CultureInfo.InvariantCulture))),
                    new XElement("CameraUpVector",
                        new XElement("X", up.X.ToString(CultureInfo.InvariantCulture)),
                        new XElement("Y", up.Y.ToString(CultureInfo.InvariantCulture)),
                        new XElement("Z", up.Z.ToString(CultureInfo.InvariantCulture))),
                    new XElement(zoom, zoomValue.ToString(CultureInfo.InvariantCulture)))));

            try
            {


                //COMPONENTS PART
                if (_elemCheck == 0)//visible (0)
                    elementList = oDoc.Models.First.RootItem.DescendantsAndSelf.Where(o => o.InstanceGuid != Guid.Empty && ChechHidden(o.AncestorsAndSelf) && o.FindFirstGeometry() != null && !o.FindFirstGeometry().Item.IsHidden).ToList<ModelItem>();

                if (null != elementList && elementList.Any() && _elemCheck != 2) //not if none (2)
                {
                    string appname = Autodesk.Navisworks.Api.Application.Title;
                    for (var i = 0; i < elementList.Count(); i++)
                    {

                        string ifcguid = IfcGuid.ToIfcGuid(elementList.ElementAt(i).InstanceGuid).ToString();
                        v.Element("VisualizationInfo").Element("Components").Add(
                            new XElement("Component", new XAttribute("IfcGuid", ifcguid),
                            new XElement("OriginatingSystem", appname),
                            new XElement("AuthoringToolId", "")));

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return v;
        }

        private bool ChechHidden(ModelItemEnumerableCollection items)
        {
            if (items.Any(o => o.IsHidden))
                return false; //an anchestor is hidden, so it the item
            return true; // all anchestors are visible


        }

        /// <summary>
        /// Get a View Direction
        /// </summary>
        /// <param name="oVP"></param>
        /// <returns></returns>
        private Vector3D getViewDir(Viewpoint oVP)
        {
            double units = GetGunits();

            Rotation3D oRot = oVP.Rotation;
            // calculate view direction
            Rotation3D oNegtiveZ = new Rotation3D(0, 0, -1, 0);
            Rotation3D otempRot = MultiplyRotation3D(oNegtiveZ, oRot.Invert());
            Rotation3D oViewDirRot = MultiplyRotation3D(oRot, otempRot);
            // get view direction
            Vector3D oViewDir = new Vector3D(oViewDirRot.A, oViewDirRot.B, oViewDirRot.C);

            return oViewDir.Normalize();
        }

        /// <summary>
        /// Get View Normal
        /// </summary>
        /// <param name="oVP"></param>
        /// <returns></returns>
        private Vector3D getViewUp(Viewpoint oVP)
        {
            double units = GetGunits();

            Rotation3D oRot = oVP.Rotation;
            // calculate view direction
            Rotation3D oNegtiveZ = new Rotation3D(0, 1, 0, 0);
            Rotation3D otempRot = MultiplyRotation3D(oNegtiveZ, oRot.Invert());
            Rotation3D oViewDirRot = MultiplyRotation3D(oRot, otempRot);
            // get view direction
            Vector3D oViewDir = new Vector3D(oViewDirRot.A, oViewDirRot.B, oViewDirRot.C);

            return oViewDir.Normalize();
        }

        /// <summary>
        /// help function: Multiply two Rotation3D
        /// </summary>
        /// <param name="r2"></param>
        /// <param name="r1"></param>
        /// <returns></returns>
        private Rotation3D MultiplyRotation3D(Rotation3D r2, Rotation3D r1)
        {

            Rotation3D oRot =
                new Rotation3D(r2.D * r1.A + r2.A * r1.D +
                                    r2.B * r1.C - r2.C * r1.B,
                                r2.D * r1.B + r2.B * r1.D +
                                    r2.C * r1.A - r2.A * r1.C,
                                r2.D * r1.C + r2.C * r1.D +
                                    r2.A * r1.B - r2.B * r1.A,
                                r2.D * r1.D - r2.A * r1.A -
                                    r2.B * r1.B - r2.C * r1.C);

            oRot.Normalize();

            return oRot;

        }
        private double GetGunits()
        {
            string units = oDoc.Units.ToString();
            double factor = 1;
            switch (units)
            {
                case "Centimeters":
                    factor = 100;
                    break;
                case "Feet":
                    factor = 3.28084;
                    break;
                case "Inches":
                    factor = 39.3701;
                    break;
                case "Kilometers":
                    factor = 0.001;
                    break;
                case "Meters":
                    factor = 1;
                    break;
                case "Micrometers":
                    factor = 1000000;
                    break;
                case "Miles":
                    factor = 0.000621371;
                    break;
                case "Millimeters":
                    factor = 1000;
                    break;
                case "Mils":
                    factor = 39370.0787;
                    break;
                case "Yards":
                    factor = 1.09361;
                    break;
                default:
                    MessageBox.Show("Units " + units + " not recognized.");
                    factor = 1;
                    break;
            }
            return factor;
        }

        #endregion

    }

}

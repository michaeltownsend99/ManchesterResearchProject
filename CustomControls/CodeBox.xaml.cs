using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using Windows.Storage;
using Windows.UI.Core;
using Windows.ApplicationModel;
using Windows.Storage.Streams;
using Windows.Foundation.Metadata;
// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CustomControls
{
    public sealed partial class CodeBox : UserControl
    {
        //holds the connections and their canvases
        public List<string> connectionsList = new List<string>();

        public List<InkCanvas> canvasList = new List<InkCanvas>(); //used to store canvases of connected diagram boxes

        public CodeBox()
        {
            this.InitializeComponent();
        }

        //checks if the box is being moved or resized
        private bool _isResizing;

        //method to alter the _isresizing based on how the control is being moved or changed
        private void Manipulator_OnManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            if (e.Position.X > Width - ResizeRectangle.Width && e.Position.Y > Height - ResizeRectangle.Height) _isResizing = true;
            else _isResizing = false;
        }

        //if the control is being resized, only change the height as we are insde the dialog
        private void Manipulator_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (_isResizing)
            {
                //Width += e.Delta.Translation.X;
                Height += e.Delta.Translation.Y;
            }
            //else
            //{
            //    Canvas.SetLeft(this, Canvas.GetLeft(this) + e.Delta.Translation.X);
            //    Canvas.SetTop(this, Canvas.GetTop(this) + e.Delta.Translation.Y);
            //}
        }

        //removes the control from the dialog
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            var parent = this.Parent as StackPanel;
            parent.Children.Remove(this);
        }

        //outputs the data for the control in the expected format as a string
        public override string ToString()
        {
            string connectionsListString = "";
            foreach (string connection in connectionsList)
            {
                var newConnection = connection.Replace(",", "/");
                connectionsListString += newConnection + "-";
            }
            return this.CodeBoxTitle.Text + "," + this.CodeBoxContent.Text + ","  + this.Height + "," + this.Width + "," + connectionsListString + "," + canvasList.Count;
        }

        //this runs the code in teh box
        private async void RunButton_Click(object sender, RoutedEventArgs e)
        {
            //creates a new file containing the code for the box
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var file = await localFolder.CreateFileAsync(CodeBoxTitle.Text + ".py", CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(file, CodeBoxContent.Text);

            Windows.Storage.Provider.FileUpdateStatus hwStatus = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
            
            //writes the path of the new file to an existing file which can then be read by a trusted process
            //this may need to be configured to work on a new computer
            var txt = await localFolder.CreateFileAsync("paths.txt", CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(txt, file.Path);
            Windows.Storage.Provider.FileUpdateStatus Status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(txt);
            //this calls a process from launcher which runs the script at the filepath with python
            await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync("Python");
        }

        private void connectionsButton_Click(object sender, RoutedEventArgs e)
        {
            displayConnections();
        }

        //calls the main page display connetions method, main page will always be the parent so could also use that
        public void displayConnections()
        {
            MainPage.createConnectionsDialogAsync(this.connectionsList, this, canvasList);
        }

        //outputs the connections for this box as a string
        public string connectionsToString()
        {
            return this.CodeBoxTitle.Text + "," + this.CodeBoxContent.Text + "," + this.Height + "," + this.Width;
        }
    }
}

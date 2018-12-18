using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CustomControls
{
    public sealed partial class FloatingCodeBox : UserControl
    {
        //same as the normal code box but the resizing and moving is enabled 
        //as we want this for the whiteboard style page

        public List<string> connectionsList = new List<string>();

        public List<InkCanvas> canvasList = new List<InkCanvas>(); //used to store canvases of connected diagram boxes

        public FloatingCodeBox()
        {
            this.InitializeComponent();
        }

        private bool _isResizing;

        private void Manipulator_OnManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            if (e.Position.X > Width - ResizeRectangle.Width && e.Position.Y > Height - ResizeRectangle.Height) _isResizing = true;
            else _isResizing = false;
        }

        private void Manipulator_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (_isResizing)
            {
                Width += e.Delta.Translation.X;
                Height += e.Delta.Translation.Y;
            }
            else
            {
                Canvas.SetLeft(this, Canvas.GetLeft(this) + e.Delta.Translation.X);
                Canvas.SetTop(this, Canvas.GetTop(this) + e.Delta.Translation.Y);
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            var parent = this.Parent as StackPanel;
            parent.Children.Remove(this);
        }

        public override string ToString()
        {
            string connectionsListString = "";
            foreach (string connection in connectionsList)
            {
                var newConnection = connection.Replace(",", "/");
                connectionsListString += newConnection + "-";
            }
            return this.GetType() + "," + this.CodeBoxTitle.Text + "," + this.CodeBoxContent.Text + "," + this.Height + "," + this.Width + "," + Canvas.GetLeft(this) + "," + Canvas.GetTop(this) + "," + connectionsListString + "," + canvasList.Count;
        }

        private async void RunButton_Click(object sender, RoutedEventArgs e)
        {

            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var file = await localFolder.CreateFileAsync(CodeBoxTitle.Text + ".py", CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(file, CodeBoxContent.Text);

            Windows.Storage.Provider.FileUpdateStatus hwStatus = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
            //RuntimeVariables.Variables.path = file.Path;
            var txt = await localFolder.CreateFileAsync("paths.txt", CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(txt, file.Path);
            Windows.Storage.Provider.FileUpdateStatus Status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(txt);
            await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync("Python");
        }

        private void connectionsButton_Click(object sender, RoutedEventArgs e)
        {
            displayConnections();
        }

        public void displayConnections()
        {
            //MainPage.createConnectionsDialogAsync(this.connectionsList, this, canvasList);
        }

        public string connectionsToString()
        {
            return this.CodeBoxTitle.Text + "," + this.CodeBoxContent.Text + "," + this.Height + "," + this.Width + "," + Canvas.GetLeft(this) + "," + Canvas.GetTop(this);
        }
    }
}

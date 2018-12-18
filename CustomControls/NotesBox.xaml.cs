using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
using Windows.UI.Popups;
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
    public sealed partial class NotesBox : UserControl
    {
        //holds details of connections and any canvases they have 
        public List<string> connectionsList = new List<string>();

        public List<InkCanvas> canvasList = new List<InkCanvas>(); //used to store canvases of connected diagram boxes

        public NotesBox()
        {
            this.InitializeComponent();
        }

        //this and teh next two methods are used when resizing the box within the dialog
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
                //Width += e.Delta.Translation.X;
                Height += e.Delta.Translation.Y;
            }
            //else
            //{
            //    Canvas.SetLeft(this, Canvas.GetLeft(this) + e.Delta.Translation.X);
            //    Canvas.SetTop(this, Canvas.GetTop(this) + e.Delta.Translation.Y);
            //}
        }

        //outputs the data for this box as a string
        public override string ToString()
        {
            string connectionsListString = "";
            foreach(string connection in connectionsList)
            {
                var newConnection = connection.Replace(",", "/");
                connectionsListString += newConnection + "-";
            }
            return this.NotesBoxContent.Text + "," + this.Height + "," + this.Width + "," + connectionsListString + "," + canvasList.Count;
        }

        //outputs the connections data as a string
        public string connectionsToString()
        {
            return this.NotesBoxContent.Text + "," + this.Height + "," + this.Width + ",";
        }

        //removes the box from its parent
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            var parent = this.Parent as StackPanel;
            parent.Children.Remove(this);
        }

        private void connectionsButton_Click(object sender, RoutedEventArgs e)
        {
            displayConnections();
        }

        public void displayConnections()
        {
            MainPage.createConnectionsDialogAsync(this.connectionsList, this, canvasList);
        }

    }
}

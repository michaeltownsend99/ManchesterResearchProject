using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class FloatingNotesBox : UserControl
    {

        //same as the normal notes box but the resizing and moving is enabled 
        //as we want this for the whiteboard style page

        public FloatingNotesBox()
        {
            this.InitializeComponent();
        }

        public List<string> connectionsList = new List<string>();

        public List<InkCanvas> canvasList = new List<InkCanvas>(); //used to store canvases of connected diagram boxes

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


        public override string ToString()
        {
            //string connectionsListString = "";
            //foreach (string connection in connectionsList)
            //{
            //    var newConnection = connection.Replace(",", "/");
            //    connectionsListString += newConnection + "-";
            //}
            return this.GetType() + "," + this.NotesBoxContent.Text + "," + this.Height + "," + this.Width + "," + Canvas.GetLeft(this) + "," + Canvas.GetTop(this);
        }

        public string connectionsToString()
        {
            return this.NotesBoxContent.Text + "," + this.Height + "," + this.Width + ",";
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            var parent = this.Parent as Canvas;
            parent.Children.Remove(this);
        }

        private void connectionsButton_Click(object sender, RoutedEventArgs e)
        {
            displayConnections();
        }

        public void displayConnections()
        {
            //MainPage.createConnectionsDialogAsync(this.connectionsList, this, canvasList);
        }
    }
}

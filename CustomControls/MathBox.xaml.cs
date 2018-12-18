using MyScript.IInk;
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
using Windows.UI.Popups;
using Windows.Storage;
using MyScript.IInk.UIReferenceImplementation.UserControls;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CustomControls
{
    public sealed partial class MathBox : UserControl
    {

        /*

        THIS BOX IS BROKEN DUE TO AN ISSUE WITH AN EXTERNAL SDK, MOST OF THE CODE WORKS BUT NOT SAVING AN LOADING, WHICH IS A 
        KEY PART OF TEH APPLCIATION. IT IS DUE TO THIS THAT THE BOX IS BROKEN AND SO SHOULDNT BE USED UNTIL IT IS FIXED

        */
        private Engine _engine;

        public List<string> connectionsList = new List<string>();

        public List<InkCanvas> canvasList = new List<InkCanvas>(); //used to store canvases of connected diagram boxes

        //PointerPoint _anchorPoint;
        //PointerPoint _currentPoint;
        //bool _isInDrag; readonly TranslateTransform _transform = new TranslateTransform();

        //private void MathBox_PointerPressed(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("PoinertPressed");
        //    var element = sender as FrameworkElement;
        //    _anchorPoint = e.GetCurrentPoint((sender as MathBox).Parent as UIElement);
        //    if (element != null) element.CapturePointer(e.Pointer);
        //    _isInDrag = true;
        //    e.Handled = true;

        //}

        //private void MathBox_PointerReleased(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("PoinertReleased");
        //    if (!_isInDrag) return;
        //    var element = sender as FrameworkElement;
        //    if (element != null) element.ReleasePointerCapture(e.Pointer);
        //    _isInDrag = false;
        //    e.Handled = true;

        //}

        //private void MathBox_PointerMoved(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("PoinertMoved");
        //    if (!_isInDrag) return;
        //    _currentPoint = e.GetCurrentPoint((sender as MathBox).Parent as UIElement);
        //    _transform.X += _currentPoint.Position.X - _anchorPoint.Position.X;
        //    _transform.Y += (_currentPoint.Position.Y - _anchorPoint.Position.Y);
        //    RenderTransform = _transform;
        //    _anchorPoint = _currentPoint;
        //}

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


        public MathBox()
        {
            this.InitializeComponent();
            _engine = Engine.Create((byte[])(Array)MyScript.Certificate.MyCertificate.Bytes);
            // Get the configuration object
            var configuration = _engine.Configuration;

            // Set the recognition resources path
            string[] folders = new string[1];
            folders[0] = "conf";
            configuration.SetStringArray("configuration-manager.search-path", folders);
            configuration.SetString("lang", "en_US");
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            _engine.Configuration.SetString("debug.log-file", System.IO.Path.Combine(localFolder, "log.txt"));


            // Set the temporary directory
            var tempFolder = System.IO.Path.Combine(localFolder.ToString(), "tmp");
            configuration.SetString("content-package.temp-folder", tempFolder);

            // Set the math fractional part precision
            editor.Engine = _engine;
            OpenContent();
        }

        private ContentPart _part;
        private ContentPackage _package;
        private static int count = 0;
        private string _packageName = "File" + count + ".iink";
        private string fileName;




        public void OpenContent()
        {
            if (System.IO.File.Exists(_packageName))
            {
                _package = _engine.OpenPackage(Windows.Storage.ApplicationData.Current.LocalFolder.Path + _packageName);
                _part = _package.GetPart(0);
                // Load history from part metadata
                LoadHistory();
                count++;
            }
            else
            {
                // Create a content package to provide the SDK with a place to work
                MakeUntitledFilename();
                _package = _engine.CreatePackage(_packageName);
                // Create a Math part
                _part = _package.CreatePart("Math");
            }
            editor.Editor.Part = _part;
            _package.Save();
        }

        private void MakeUntitledFilename()
        {
            var localFolder = ApplicationData.Current.LocalFolder.Path;
            var num = 0;
            string file;
            string name;

            do
            {
                var baseName = "File" + (++num) + ".iink";
                name = System.IO.Path.Combine(localFolder, baseName);
                file = baseName;
            }
            while (System.IO.File.Exists(name));

            this._packageName = name;
            this.fileName = file;
        }

        private List<string> history = new List<string>();

        void LoadHistory()
        {
            var parameterSet = _part.Metadata;
            history = parameterSet.GetStringArray("history").ToList();
        }

        void SaveHistory()
        {
            var parameterSet = editor.Editor.Part.Metadata;
            parameterSet.SetStringArray("history", history.ToArray());
            editor.Editor.Part.Metadata = parameterSet;
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            editor.Editor.Undo();
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            editor.Editor.Redo();
        }

        public void Clear()
        {
            editor.Editor.Clear();
        }

        private void recognize_Click(object sender, RoutedEventArgs e)
        {
            editor.Editor.Convert(editor.Editor.GetRootBlock(), ConversionState.DIGITAL_EDIT);
            var latexStr = editor.Editor.Export_(editor.Editor.GetRootBlock(), MimeType.LATEX);
            if (!String.IsNullOrEmpty(latexStr) && (history.Count == 0 || history.Last() != latexStr))
                history.Add(latexStr);
            SaveHistory();

        }

        private async void Go_Click(object sender, RoutedEventArgs e)
        {
            double height;
            double width;
            if (double.TryParse(WidthBox.Text, out width))
                this.Width = double.Parse(WidthBox.Text);
            else
            {
                var error = new MessageDialog("Width and height must both be integers");
                await error.ShowAsync();
            }

            if (double.TryParse(HeightBox.Text, out height))
                this.Height = double.Parse(HeightBox.Text);
            else
            {
                var error = new MessageDialog("Width and height must both be integers");
                await error.ShowAsync();
            }
        }

        public void OpenSaved(StorageFile file)
        {
            _package = _engine.OpenPackage(Windows.Storage.ApplicationData.Current.LocalFolder.Path + file.Name);
            _part = _package.GetPart(0);
            // Load history from part metadata
            LoadHistory();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.SaveContent(Windows.Storage.ApplicationData.Current.LocalFolder);
        }

        public void SaveContent(StorageFolder folder)
        {
            _package.Save();
        }

        private async void closeButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
            await file.DeleteAsync();
            var parent = this.Parent as StackPanel;
            parent.Children.Remove(this);
            editor.Editor.Part.Package.RemovePart(_part);
            _part = null;
            editor.Editor.Part = null;
            _package = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            editor.Editor.Engine.DeletePackage(_packageName);
            editor.Engine.DeletePackage(_packageName);
        }

        public string connectionsToString()
        {
            return this.Height.ToString() + "," + this.Width.ToString();
        }
    }
}

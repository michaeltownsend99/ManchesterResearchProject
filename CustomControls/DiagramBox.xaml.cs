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
using Windows.UI.Input.Inking;
using Windows.UI.Input.Inking.Analysis;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Input;
using System.Diagnostics;
using Windows.UI.Popups;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CustomControls
{
    public sealed partial class DiagramBox : UserControl
    {
        //PointerPoint _anchorPoint;
        //PointerPoint _currentPoint;
        //bool _isInDrag; readonly TranslateTransform _transform = new TranslateTransform();

        //private void DiagramBox_PointerPressed(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("PoinertPressed");
        //    var element = sender as FrameworkElement;
        //    _anchorPoint = e.GetCurrentPoint((sender as DiagramBox).Parent as UIElement);
        //    if (element != null) element.CapturePointer(e.Pointer);
        //    _isInDrag = true;
        //    e.Handled = true;

        //}

        //private void DiagramBox_PointerReleased(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("PoinertReleased");
        //    if (!_isInDrag) return;
        //    var element = sender as FrameworkElement;
        //    if (element != null) element.ReleasePointerCapture(e.Pointer);
        //    _isInDrag = false;
        //    e.Handled = true;

        //}

        //private void DiagramBox_PointerMoved(object sender, PointerRoutedEventArgs e)
        //{
        //    Debug.WriteLine("PoinertMoved");
        //    if (!_isInDrag) return;
        //    _currentPoint = e.GetCurrentPoint((sender as DiagramBox).Parent as UIElement);
        //    _transform.X += _currentPoint.Position.X - _anchorPoint.Position.X;
        //    _transform.Y += (_currentPoint.Position.Y - _anchorPoint.Position.Y);
        //    RenderTransform = _transform;
        //    _anchorPoint = _currentPoint;
        //}

        public List<string> connectionsList = new List<string>();

        public List<InkCanvas> canvasList = new List<InkCanvas>(); //used to store canvases of connected diagram boxes


        //this holds the analyzer that recognises the smart ink
        InkAnalyzer inkAnalyzer = new InkAnalyzer();
        //holds a list of the strokes made on the page
        IReadOnlyList<InkStroke> inkStrokes = null;
        //holds the result of any analysis done on the ink
        InkAnalysisResult inkAnalysisResults = null;

        //a canvas to import and export data to and from
        public InkCanvas workingCanvas = new InkCanvas();


        public DiagramBox()
        {
            //sets the different input devices and the click method for the recognize button
            this.InitializeComponent();
            inkCanvas.InkPresenter.InputDeviceTypes =
            Windows.UI.Core.CoreInputDeviceTypes.Mouse |
            Windows.UI.Core.CoreInputDeviceTypes.Pen |
            Windows.UI.Core.CoreInputDeviceTypes.Touch;
            // Set initial ink stroke attributes.
            //InkDrawingAttributes drawingAttributes = new InkDrawingAttributes();
            //drawingAttributes.Color = Windows.UI.Colors.Black;
            //drawingAttributes.IgnorePressure = false;
            //drawingAttributes.FitToCurve = true;
            //inkCanvas.InkPresenter.UpdateDefaultDrawingAttributes(drawingAttributes);

            // Listen for button click to initiate recognition.
            recognize.Click += RecognizeStrokes_Click;
        }


        public async void recognizeStrokes()
        {
            //move ink from canvas into the ink strokes list
            inkStrokes = inkCanvas.InkPresenter.StrokeContainer.GetStrokes();
            // Ensure an ink stroke is present.
            if (inkStrokes.Count > 0)
            {
                //add the data from the strokes list into the sanalyzer
                inkAnalyzer.AddDataForStrokes(inkStrokes);

               //analyse the ink and put in result
                inkAnalysisResults = await inkAnalyzer.AnalyzeAsync();

                // Have ink strokes on the canvas changed?
                if (inkAnalysisResults.Status == InkAnalysisStatus.Updated)
                {


                    // Find all strokes that are recognized as handwriting and 
                    // create a corresponding ink analysis InkWord node.
                    var inkwordNodes =
                        inkAnalyzer.AnalysisRoot.FindNodes(
                            InkAnalysisNodeKind.InkWord);

                    // Iterate through each InkWord node.
                    // Draw primary recognized text on recognitionCanvas 
                    // (for this example, we ignore alternatives), and delete 
                    // ink analysis data and recognized strokes.
                    foreach (InkAnalysisInkWord node in inkwordNodes)
                    {
                        // Draw a TextBlock object on the recognitionCanvas.
                        DrawText(node.RecognizedText, node.BoundingRect);

                        foreach (var strokeId in node.GetStrokeIds())
                        {
                            var stroke =
                                inkCanvas.InkPresenter.StrokeContainer.GetStrokeById(strokeId);
                            stroke.Selected = true;
                        }
                        getStrokesAsync();
                        inkAnalyzer.RemoveDataForStrokes(node.GetStrokeIds());
                    }
                    inkCanvas.InkPresenter.StrokeContainer.DeleteSelected();

                    // Find all strokes that are recognized as a drawing and 
                    // create a corresponding ink analysis InkDrawing node.
                    var inkdrawingNodes =
                        inkAnalyzer.AnalysisRoot.FindNodes(
                            InkAnalysisNodeKind.InkDrawing);
                    // Iterate through each InkDrawing node.
                    // Draw recognized shapes on recognitionCanvas and
                    // delete ink analysis data and recognized strokes.
                    foreach (InkAnalysisInkDrawing node in inkdrawingNodes)
                    {
                        if (node.DrawingKind == InkAnalysisDrawingKind.Drawing)
                        {
                            // Catch and process unsupported shapes (lines and so on) here.
                        }
                        // Process generalized shapes here (ellipses and polygons).
                        else
                        {
                            // Draw an Ellipse object on the recognitionCanvas (circle is a specialized ellipse).
                            if (node.DrawingKind == InkAnalysisDrawingKind.Circle || node.DrawingKind == InkAnalysisDrawingKind.Ellipse)
                            {
                                DrawEllipse(node);
                                getStrokesAsync();
                            }
                            // Draw a Polygon object on the recognitionCanvas.
                            else
                            {
                                DrawPolygon(node);
                                getStrokesAsync();
                            }
                            foreach (var strokeId in node.GetStrokeIds())
                            {
                                var stroke = inkCanvas.InkPresenter.StrokeContainer.GetStrokeById(strokeId);
                                stroke.Selected = true;
                            }
                        }

                        inkAnalyzer.RemoveDataForStrokes(node.GetStrokeIds());
                    }
                    inkCanvas.InkPresenter.StrokeContainer.DeleteSelected();
                }
            }
        }


        private void RecognizeStrokes_Click(object sender, RoutedEventArgs e)
        {
            recognizeStrokes();
        }

        //draws any text which has been recognised
        private void DrawText(string recognizedText, Rect boundingRect)
        {
            TextBlock text = new TextBlock();
            Canvas.SetTop(text, boundingRect.Top);
            Canvas.SetLeft(text, boundingRect.Left);

            text.Text = recognizedText;
            text.FontSize = boundingRect.Height;

            recognitionCanvas.Children.Add(text);
        }

        // Draw an ellipse on the recognitionCanvas.
        private void DrawEllipse(InkAnalysisInkDrawing shape)
        {
            var points = shape.Points;
            Ellipse ellipse = new Ellipse();

            ellipse.Width = shape.BoundingRect.Width;
            ellipse.Height = shape.BoundingRect.Height;

            Canvas.SetTop(ellipse, shape.BoundingRect.Top);
            Canvas.SetLeft(ellipse, shape.BoundingRect.Left);

            var brush = new SolidColorBrush(Windows.UI.ColorHelper.FromArgb(255, 0, 0, 255));
            ellipse.Stroke = brush;
            ellipse.StrokeThickness = 2;
            recognitionCanvas.Children.Add(ellipse);
        }

        // Draw a polygon on the recognitionCanvas.
        private void DrawPolygon(InkAnalysisInkDrawing shape)
        {
            List<Point> points = new List<Point>(shape.Points);
            Polygon polygon = new Polygon();

            foreach (Point point in points)
            {
                polygon.Points.Add(point);
            }

            var brush = new SolidColorBrush(Windows.UI.ColorHelper.FromArgb(255, 0, 0, 255));
            polygon.Stroke = brush;
            polygon.StrokeThickness = 2;
            recognitionCanvas.Children.Add(polygon);
        }

        //move strokes from current canvas to the working canvas
        public async void getStrokesAsync()
        {
            var strokes = inkCanvas.InkPresenter.StrokeContainer.GetStrokes();
            foreach(var stroke in strokes)
            {
                try
                {
                    workingCanvas.InkPresenter.StrokeContainer.AddStroke(stroke.Clone());
                }
                catch(Exception e)
                {
                    var error = new MessageDialog(e.ToString());
                    await error.ShowAsync();
                }
            }
        }

        //this variable and the next two methods deal with moving and resizing the box
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

        //removes the box from the dialog 
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            var parent = this.Parent as StackPanel;
            parent.Children.Remove(this);
        }

        //outputs the data for the box as a string
        public override string ToString()
        {
            string connectionsListString = "";
            foreach (string connection in connectionsList)
            {
                var newConnection = connection.Replace(",", "/");
                connectionsListString += newConnection + "-";
            }
            return this.DiagramBoxTitle.Text + "," + this.Height.ToString() + "," + this.Width.ToString() + "," + connectionsListString + "," + canvasList.Count;
        }

        //returns the data for this boxes connections as a string
        public string connectionsToString()
        {
            return this.DiagramBoxTitle.Text + "," + this.Height.ToString() + "," + this.Width.ToString();
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

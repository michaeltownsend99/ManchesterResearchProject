using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Input.Inking;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CustomControls
{
    public sealed partial class Connections : UserControl
    {
        //holds the data for the connections
        List<string> connections;

        //holds any canvases that are required
        List<InkCanvas> canvases;

        //holds the caller of this process
        private UserControl caller;

        public Connections()
        {
            this.InitializeComponent();
        }

        public void loadConnections()
        {
            //check if there are no connections
            if(connections.Count != 0)
            {
                string[] details;
                int canvasIndex = 0;


                //for every connections in the list
                foreach (string connection in connections)
                {
                    //split the list and add a box to the display depending on the data in the same way as usual
                    details = Regex.Split(connection, ",");
                    if(details[0] == "NotesBox")
                    {
                        NotesBox notes = new NotesBox();
                        notes.NotesBoxContent.Text = details[1];
                        notes.Height = double.Parse(details[2]);
                        notes.Width = double.Parse(details[3]);
                        var stack = notes.closeAndConnectionsStack;
                        stack.Children.Remove(notes.closeButton);
                        stack.Children.Remove(notes.connectionsButton);
                        connectionsStack.Children.Add(notes);
                    }
                    else if (details[0] == "CodeBox")
                    {
                        CodeBox code = new CodeBox();
                        code.CodeBoxTitle.Text = details[1];
                        code.CodeBoxContent.Text = details[2];
                        code.Height = double.Parse(details[3]);
                        code.Width = double.Parse(details[4]);
                        var stack = code.closeAndConnectionsStack;
                        stack.Children.Remove(code.closeButton);
                        stack.Children.Remove(code.connectionsButton);
                        connectionsStack.Children.Add(code);
                    }
                    else if (details[0] == "DiagramBox")
                    {
                        
                        DiagramBox diagram = new DiagramBox();
                        diagram.DiagramBoxTitle.Text = details[1];
                        diagram.Height = double.Parse(details[2]);
                        diagram.Width = double.Parse(details[3]);
                        diagram.inkCanvas.InkPresenter.StrokeContainer = canvases.ElementAt<InkCanvas>(canvasIndex).InkPresenter.StrokeContainer;
                        canvasIndex++;
                        var stack = diagram.closeAndConnectionsStack;
                        stack.Children.Remove(diagram.closeButton);
                        stack.Children.Remove(diagram.connectionsButton);
                        connectionsStack.Children.Add(diagram);
                    }
                }
            }
            //if there are no connections then let the user know
            else
            {
                TextBlock noConnections = new TextBlock();
                //noConnections.HorizontalTextAlignment = TextAlignment.Center;
                noConnections.Width = double.NaN;
                noConnections.TextAlignment = TextAlignment.Center;
                noConnections.Text = "This note has no connections";
                connectionsStack.Children.Add(noConnections);
                removeConnectionButton.IsEnabled = false;
            }

        }

        //set the connections list
        public void setConnections(List<string> requiredConnections)
        {
            connections = requiredConnections;
        }

        //set the canvas list
        public void setCanvases(List<InkCanvas> requiredCanvases)
        {
            canvases = requiredCanvases;
        }

        //set the caller 
        public void setCaller(UserControl requiredControl)
        {
            caller = requiredControl;
        }

        //runs when the add connections button is clicked on a box
        private void addConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            //gets the parent of the caller
            var parent = this.Parent as ConnectionsDialog;
            //add this popup to the stack
            MainPage.openedConnectionsStack.Push(parent);
            //hide the parent popup
            parent.Hide();
            //set the frame and page and call the main page method to continue to connection process
            var frame = (Frame)Window.Current.Content;
            var page = (MainPage)frame.Content;
            page.createRectangle(caller);
        }

        //used when we need to remove a connection
        //uses a similar process to the add connection methods in mainpage
        private void removeConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            createRectangle();
        }

        StackPanel rectangleStack;

        //creates the rectangle that the user will click to select the connection to remove
        //including a scrollbar to allow you to scroll throught the connections page
        public void createRectangle()
        {
            rectangleStack = new StackPanel();
            ConnectionsGrid.Children.Add(rectangleStack);
            Grid.SetRow(rectangleStack, 1);
            ScrollViewer scroll = new ScrollViewer();
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            scroll.HorizontalScrollMode = ScrollMode.Disabled;
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            scroll.VerticalScrollMode = ScrollMode.Auto;
            scroll.Height = ConnectionsGrid.RowDefinitions[1].ActualHeight;
            scroll.Name = "rectangleScroller";
            scroll.ViewChanged += scrollChanged;


            Rectangle rectangle = new Rectangle();
            double height = 0;
            foreach (UserControl control in connectionsStack.Children)
                height += control.Height;
            
            rectangle.Height = height;
            rectangle.Fill = new SolidColorBrush(Colors.Transparent);
            scroll.Content = rectangle;
            rectangleStack.Children.Add(scroll);
            rectangle.PointerPressed += rectangle_PointerPressed;
        }

        //called when the rectanlge we created to remove a connection is pressed
        private async void rectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //gets where the user clicked and transforms it to something useful
            Point p = e.GetCurrentPoint(this.Parent as ConnectionsDialog).Position;
            ConnectionsGrid.Children.Remove(rectangleStack);
            GeneralTransform gt = connectionsStack.TransformToVisual(connectionsStack);
            Point pagePoint = gt.TransformPoint(p);
            //gets the element to remove
            var elements = VisualTreeHelper.FindElementsInHostCoordinates(pagePoint, this);
            UIElement element;

            //foreach (UIElement el in elements)
            //{
            //    var msg = new MessageDialog(el.ToString());
            //    await msg.ShowAsync();
            //}

            try
            {

                //calls a method to remove the connection from the list
                var stackChildrenList = connectionsStack.Children;
                if (elements.ElementAt<UIElement>(11).GetType() == typeof(NotesBox))
                {
                    element = elements.ElementAt<UIElement>(11);
                    getControlType(caller, element, stackChildrenList.IndexOf(elements.ElementAt(11)));
                }
                else if (elements.ElementAt<UIElement>(10).GetType() == typeof(CodeBox))
                {
                    element = elements.ElementAt<UIElement>(10);
                    getControlType(caller, element, stackChildrenList.IndexOf(elements.ElementAt(10)));
                }
                else if (elements.ElementAt<UIElement>(4).GetType() == typeof(DiagramBox))
                {
                    element = elements.ElementAt<UIElement>(4);
                    getControlType(caller, element, stackChildrenList.IndexOf(elements.ElementAt(4)));
                }
                else
                {
                    var box = new MessageDialog("You may only remove a connection to a note, please try again");
                    await box.ShowAsync();
                }
            }
            catch(Exception ex)
            {
                var box = new MessageDialog(ex.ToString());
                await box.ShowAsync();
            }
        }

        //allows the rectangle and stack scroll to sync
        private void scrollChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            ScrollViewer scroller = sender as ScrollViewer;

            StackScroller.ChangeView(0, scroller.VerticalOffset, 1);
        }

        //runs when the popup is closed
        private async void closeButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = this.Parent as ConnectionsDialog;
            dialog.Hide();
            //if the user came here from another popup then display it
            if(MainPage.openedConnectionsStack.Count != 0)
            {
                var log = MainPage.openedConnectionsStack.Pop();
                await log.ShowAsync();
            }
        }

        public UserControl getCaller()
        {
            return caller;
        }

        //helper method to get the type for a box 
        public void getControlType(UserControl control, UIElement element, int index)
        {
            if (control.GetType() == typeof(NotesBox))
            {
                NotesBox box = (NotesBox)control;
                removeConnectionFromNotesBox(box, element, index);
            }
            else if (control.GetType() == typeof(CodeBox))
            {
                CodeBox box = (CodeBox)control;
                removeConnectionFromCodeBox(box, element, index);
            }
            else if (control.GetType() == typeof(DiagramBox))
            {
                DiagramBox box = (DiagramBox)control;
                removeConnectionFromDiagramBox(box, element, index);
            }
            else if (control.GetType() == typeof(MathBox))
            {
                MathBox box = (MathBox)control;
                removeConnectionFromMathBox(box, element, index);
            }
        }

        //methods to remove a connection from each type of box
        private void removeConnectionFromNotesBox(NotesBox boxToRemoveFrom, UIElement element, int index)
        {
            //if it was a diagram then remove the data and the canvas 
            //then hide this dialog and reload it so that it displays without the connection we removed
            if (element.GetType() == typeof(DiagramBox))
            {
                boxToRemoveFrom.connectionsList.RemoveAt(index);
                boxToRemoveFrom.canvasList.RemoveAt(index);
                var parent = this.Parent as ConnectionsDialog;
                parent.Hide();
                boxToRemoveFrom.displayConnections();
            }
            else
            {
                boxToRemoveFrom.connectionsList.RemoveAt(index);
                var parent = this.Parent as ConnectionsDialog;
                parent.Hide();
                boxToRemoveFrom.displayConnections();
            }
        }

        private void removeConnectionFromCodeBox(CodeBox boxToRemoveFrom, UIElement element, int index)
        {
            if (element.GetType() == typeof(DiagramBox))
            {
                boxToRemoveFrom.connectionsList.RemoveAt(index);
                boxToRemoveFrom.canvasList.RemoveAt(index);
                var parent = this.Parent as ConnectionsDialog;
                parent.Hide();
                boxToRemoveFrom.displayConnections();
            }
            else
            {
                boxToRemoveFrom.connectionsList.RemoveAt(index);
                var parent = this.Parent as ConnectionsDialog;
                parent.Hide();
                boxToRemoveFrom.displayConnections();
            }
        }

        private void removeConnectionFromDiagramBox(DiagramBox boxToRemoveFrom, UIElement element, int index)
        {
            if (element.GetType() == typeof(DiagramBox))
            {
                boxToRemoveFrom.connectionsList.RemoveAt(index);
                boxToRemoveFrom.canvasList.RemoveAt(index);
                var parent = this.Parent as ConnectionsDialog;
                parent.Hide();
                boxToRemoveFrom.displayConnections();
            }
            else
            {
                boxToRemoveFrom.connectionsList.RemoveAt(index);
                var parent = this.Parent as ConnectionsDialog;
                parent.Hide();
                boxToRemoveFrom.displayConnections();
            }
        }

        private void removeConnectionFromMathBox(MathBox boxToRemoveFrom, UIElement element, int index)
        {
            if (element.GetType() == typeof(DiagramBox))
            {
                boxToRemoveFrom.connectionsList.RemoveAt(index);
                boxToRemoveFrom.canvasList.RemoveAt(index);
                var parent = this.Parent as ConnectionsDialog;
                parent.Hide();
                //boxToRemoveFrom.displayConnections();
            }
            else
            {
                boxToRemoveFrom.connectionsList.RemoveAt(index);
                var parent = this.Parent as ConnectionsDialog;
                parent.Hide();
                //boxToRemoveFrom.displayConnections();
            }
        }
    }
}

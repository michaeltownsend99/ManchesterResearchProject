using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Input.Inking;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CustomControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FloatingPage : Page
    {
        public FloatingPage()
        {
            this.InitializeComponent();
            //FloatingBoxArea.PointerPressed += FloatingBoxArea_PointerPressed;
            //FloatingDiagramBox background = new FloatingDiagramBox();
            //background.Height = 20100;
            //background.Width = 20100;
            //Canvas.SetLeft(background, 0);
            //Canvas.SetTop(background, -60);
            
        }

        //run when this page is navigated to
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //set the parameters for the content and diagrams
            var parameters = (Parameters)e.Parameter;
            if(parameters.content != pageContent)
            {
                pageContent = parameters.content;
                floatingDialogCanvases = parameters.floatingDialogCanvases;
                floatingCanvases = parameters.floatingCanvases;
                await loadContent();
            }
        }


        private List<InkCanvas> floatingCanvases = new List<InkCanvas>();
        private List<InkCanvas> floatingDialogCanvases = new List<InkCanvas>();


        private string pageContent = "";

        //this is used when adding boxes
        private int state = 0;

        //when navigating back to the main page
        private void NavigateToMainPageButton_Click(object sender, RoutedEventArgs e)
        {
            //save the page
            saveContent();
            //save the parameters and pass to the main page
            Parameters floating = new Parameters(floatingCanvases, floatingDialogCanvases, pageContent);
            this.Frame.Navigate(typeof(MainPage), floating);
        }

        //helper method to get the count of each tpye of box
        public int[] getBoxCount(DependencyObject depObj)
        {
            int codeBoxCount = 0;
            foreach (CodeBox box in FindVisualChildren<CodeBox>(depObj))
                codeBoxCount++;
            int notesBoxCount = 0;
            foreach (NotesBox box in FindVisualChildren<NotesBox>(depObj))
                notesBoxCount++;
            int diagramBoxCount = 0;
            foreach (DiagramBox box in FindVisualChildren<DiagramBox>(depObj))
                diagramBoxCount++;
            int mathsBoxCount = 0;
            foreach (FloatingNotesDialog box in FindVisualChildren<FloatingNotesDialog>(depObj))
                mathsBoxCount++;
            int[] result = new int[4] { codeBoxCount, notesBoxCount, diagramBoxCount, mathsBoxCount };
            return result;
        }

        //used to save the state of the page, the result is passed to the main page
        public void saveContent()
        {
            //string which will holds the state of the page
            string content = "";
            //for each box in the page
            foreach (UserControl control in FindVisualChildren<UserControl>(FloatingBoxArea))
            {
                //get the state of any notes or code boxes in teh page and add them to the string
                if (control.GetType() == typeof(FloatingNotesBox))
                {
                    content += control.ToString() + Environment.NewLine;
                }
                else if (control.GetType() == typeof(FloatingCodeBox))
                {
                    content += control.ToString() + Environment.NewLine;
                }
            }
            //if it is a diagram box, get the state of the box and also add its canvas to the list
            foreach(FloatingDiagramBox control in FindVisualChildren<FloatingDiagramBox>(FloatingBoxArea))
            {
                content += control.ToString() + Environment.NewLine;
                (control as FloatingDiagramBox).getStrokesAsync();
                floatingCanvases.Add((control as FloatingDiagramBox).workingCanvas);
            }
            //if it is a dialog box, need to go through it to get the data for its child boxes
            foreach (FloatingNotesDialog control in FindVisualChildren<FloatingNotesDialog>(FloatingBoxArea))
            {
                //gets a reference to the dialog we are looking at
                var dialog = control as FloatingNotesDialog;

                //do the same as in the main page save method for this dialog box
                var count = getBoxCount(dialog.NotesStack);
                int notesBoxSavedCount = 0;
                string[] notesBoxSavedArray = new string[count[1]];
                foreach (NotesBox box in FindVisualChildren<NotesBox>(dialog.NotesStack))
                {
                    notesBoxSavedArray[notesBoxSavedCount] = box.ToString();
                    notesBoxSavedCount++;
                }


                int codeBoxSavedCount = 0;
                string[] codeBoxSavedArray = new string[count[0]];
                foreach (CodeBox box in FindVisualChildren<CodeBox>(dialog.NotesStack))
                {
                    codeBoxSavedArray[codeBoxSavedCount] = box.ToString();
                    codeBoxSavedCount++;
                }


                InkCanvas workingCanvas = new InkCanvas();
                int diagramBoxSavedCount = 0;
                string[] diagramBoxSavedArray = new string[count[2]];
                //int diagramBoxCount = count[2];
                Debug.WriteLine(diagramBoxSavedArray.Length + " is length of list");
                foreach (DiagramBox box in FindVisualChildren<DiagramBox>(dialog.NotesStack))
                {
                    diagramBoxSavedArray[diagramBoxSavedCount] = box.ToString();
                    diagramBoxSavedCount++;
                    box.getStrokesAsync();
                    floatingDialogCanvases.Add(box.workingCanvas);
                }
                string[] listOfBoxTypes = new string[dialog.NotesStack.Children.Count];
                int number = 0;
                foreach (var item in dialog.NotesStack.Children)
                {
                    listOfBoxTypes[number] = item.GetType().ToString();
                    number++;
                }
                //Prevent updates to the remote version of the file until
                // we finish making changes and call CompleteUpdatesAsync.
                // write to file
                content +=
                    "CustomControls.FloatingNotesDialog," + dialog.DialogTitleBox.ToString() + Environment.NewLine +
                    string.Join(",", listOfBoxTypes.Select(x => x.ToString()).ToArray()) + Environment.NewLine +
                    string.Join("/", notesBoxSavedArray.Select(x => x.ToString()).ToArray()) + Environment.NewLine +
                    string.Join("/", codeBoxSavedArray.Select(x => x.ToString()).ToArray()) + Environment.NewLine +
                    string.Join("/", diagramBoxSavedArray.Select(x => x.ToString()).ToArray()) + Environment.NewLine;
            }
            //sets the page parameter to the result of this method
            pageContent = content;
        }

        //called when navigating to this page
        //loads content from the string and lists 
        public async Task loadContent()
        {
            int contentIndex = 0;
            int floatingCanvasesIndex = 0;
            int floatingDialogCanvasesIndex = 0;
            //splits the list into an array with each element being a box
            string[] listOfContent = Regex.Split(pageContent, Environment.NewLine);
            //while there is another array element to look at
            while(contentIndex < listOfContent.Length)
            {
                //do the same as load method in main page
                string[] item = Regex.Split(listOfContent[contentIndex], ",");
                if(item[0] == "CustomControls.FloatingNotesBox")
                {
                    FloatingNotesBox box = new FloatingNotesBox();
                    box.NotesBoxContent.Text = item[1];
                    box.Height = Double.Parse(item[2]);
                    box.Width = Double.Parse(item[3]);
                    Canvas.SetLeft(box, Double.Parse(item[4]));
                    Canvas.SetTop(box, Double.Parse(item[5]));
                    FloatingBoxArea.Children.Add(box);
                }
                else if(item[0] == "CustomControls.FloatingCodeBox")
                {
                    FloatingCodeBox box = new FloatingCodeBox();
                    box.CodeBoxTitle.Text = item[1];
                    box.CodeBoxContent.Text = item[2];
                    box.Height = Double.Parse(item[3]);
                    box.Width = Double.Parse(item[4]);
                    Canvas.SetLeft(box, Double.Parse(item[5]));
                    Canvas.SetTop(box, Double.Parse(item[6]));
                    FloatingBoxArea.Children.Add(box);
                }
                else if(item[0] == "CustomControls.FloatingDiagramBox")
                {
                    FloatingDiagramBox box = new FloatingDiagramBox();
                    box.DiagramBoxTitle.Text = item[1];
                    box.Height = Double.Parse(item[2]);
                    box.Width = Double.Parse(item[3]);
                    Canvas.SetLeft(box, Double.Parse(item[4]));
                    Canvas.SetTop(box, Double.Parse(item[5]));

                    StorageFile sampleFile = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("tempfile", CreationCollisionOption.ReplaceExisting);

                    Windows.Storage.CachedFileManager.DeferUpdates(sampleFile);
                    IRandomAccessStream stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                    using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
                    {
                        await floatingCanvases[floatingCanvasesIndex].InkPresenter.StrokeContainer.SaveAsync(outputStream);
                        await outputStream.FlushAsync();
                    }
                    stream.Dispose();
                    stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
                    using (var inputStream = stream.GetInputStreamAt(0))
                    {
                        await box.inkCanvas.InkPresenter.StrokeContainer.LoadAsync(inputStream);
                    }
                    stream.Dispose();
                    Windows.Storage.Provider.FileUpdateStatus gifStatus = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(sampleFile);

                    floatingCanvasesIndex++;
                    FloatingBoxArea.Children.Add(box);
                }
                else if(item[0] == "CustomControls.FloatingNotesDialog")
                {
                    FloatingNotesDialog dialog = new FloatingNotesDialog();
                    dialog.DialogTitleBox.Text = item[1];
                    Canvas.SetLeft(dialog, Double.Parse(item[2]));
                    Canvas.SetTop(dialog, Double.Parse(item[3]));
                    contentIndex++;
                    string[] thisBoxList;
                    string[] listOfBoxes = Regex.Split(listOfContent[contentIndex], ",");
                    contentIndex++;
                    int notesBoxListIndex = 0;
                    string[] notesBoxList = Regex.Split(listOfContent[contentIndex], "/");
                    contentIndex++;
                    int codeBoxListIndex = 0;
                    string[] codeBoxList = Regex.Split(listOfContent[contentIndex], "/");
                    contentIndex++;
                    int diagramBoxListIndex = 0;
                    string[] diagramBoxList = Regex.Split(listOfContent[contentIndex], "/");

                    foreach (string savedBox in listOfBoxes)
                    {
                        if (savedBox == "CustomControls.NotesBox")
                        {
                            thisBoxList = Regex.Split(notesBoxList[notesBoxListIndex], ",");
                            NotesBox box = new NotesBox();
                            box.NotesBoxContent.Text = thisBoxList[0];
                            box.Height = Double.Parse(thisBoxList[1]);
                            box.Width = Double.Parse(thisBoxList[2]);
                            dialog.NotesStack.Children.Add(box);
                            notesBoxListIndex++;
                        }
                        else if (savedBox == "CustomControls.CodeBox")
                        {
                            thisBoxList = Regex.Split(codeBoxList[codeBoxListIndex], ",");
                            CodeBox box = new CodeBox();
                            box.CodeBoxTitle.Text = thisBoxList[0];
                            box.CodeBoxContent.Text = thisBoxList[1];
                            box.Height = Double.Parse(thisBoxList[2]);
                            box.Width = Double.Parse(thisBoxList[3]);
                            dialog.NotesStack.Children.Add(box);
                            codeBoxListIndex++;
                        }
                        else if (savedBox == "CustomControls.DiagramBox")
                        {
                            thisBoxList = Regex.Split(diagramBoxList[diagramBoxListIndex], ",");
                            DiagramBox box = new DiagramBox();
                            box.DiagramBoxTitle.Text = thisBoxList[0];
                            box.Height = Double.Parse(thisBoxList[1]);
                            box.Width = Double.Parse(thisBoxList[2]);

                            StorageFile sampleFile = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("tempfile", CreationCollisionOption.ReplaceExisting);
                            Windows.Storage.CachedFileManager.DeferUpdates(sampleFile);
                            IRandomAccessStream stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                            using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
                            {
                                await floatingDialogCanvases[floatingDialogCanvasesIndex].InkPresenter.StrokeContainer.SaveAsync(outputStream);
                                await outputStream.FlushAsync();
                            }
                            stream.Dispose();
                            stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
                            using (var inputStream = stream.GetInputStreamAt(0))
                            {
                                await box.inkCanvas.InkPresenter.StrokeContainer.LoadAsync(inputStream);
                            }
                            stream.Dispose();
                            Windows.Storage.Provider.FileUpdateStatus gifStatus = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(sampleFile);

                            floatingDialogCanvasesIndex++;
                            dialog.NotesStack.Children.Add(box);
                            diagramBoxListIndex++;
                        }
                    }
                    FloatingBoxArea.Children.Add(dialog);
                }

                contentIndex++;
            }
            return;
        }

        //hepler method to find all of a certain type of object in a given container
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }
                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        //These 4 methods change the state of the page so that new boxes can be added
        private void CodeBoxButton_Click(object sender, RoutedEventArgs e)
        {
            state = 2;
        }

        private void NotesBoxButton_Click(object sender, RoutedEventArgs e)
        {
            state = 1;
        }

        private void DiagramButton_Click(object sender, RoutedEventArgs e)
        {
            state = 3;
        }

        private void MathBoxButton_Click(object sender, RoutedEventArgs e)
        {
            state = 4;
        }

        //when the background of the notes page is pressed, used to add new boxes to the area
        private void FloatingBoxArea_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //checks to see if state is 0
            if (state != 0)
            {
                //if we need to add a box
                //get the point the user clicked on and transform it into something useful
                Point p = e.GetCurrentPoint(this).Position;
                GeneralTransform gt = this.TransformToVisual(FloatingBoxArea);
                Point pagePoint = gt.TransformPoint(p);
                //add the relevant box based on the state value
                if (state == 1)
                {
                    FloatingNotesBox box = new FloatingNotesBox();
                    Canvas.SetTop(box, pagePoint.Y);
                    Canvas.SetLeft(box, pagePoint.X);
                    box.Height = 300;
                    box.Width = 800;
                    FloatingBoxArea.Children.Add(box);
                    state = 0;
                }
                if (state == 2)
                {
                    FloatingCodeBox box = new FloatingCodeBox();
                    Canvas.SetTop(box, pagePoint.Y);
                    Canvas.SetLeft(box, pagePoint.X);
                    box.Height = 300;
                    box.Width = 800;
                    FloatingBoxArea.Children.Add(box);
                    state = 0;
                }
                if (state == 3)
                {
                    FloatingDiagramBox box = new FloatingDiagramBox();
                    Canvas.SetTop(box, pagePoint.Y);
                    Canvas.SetLeft(box, pagePoint.X);
                    box.Height = 300;
                    box.Width = 800;
                    FloatingBoxArea.Children.Add(box);
                    state = 0;
                }
                if (state == 4)
                {
                    FloatingNotesDialog box = new FloatingNotesDialog();
                    Canvas.SetTop(box, pagePoint.Y);
                    Canvas.SetLeft(box, pagePoint.X);
                    box.Height = 700;
                    box.Width = 800;
                    FloatingBoxArea.Children.Add(box);
                    state = 0;
                }
            }
        }
    }
}

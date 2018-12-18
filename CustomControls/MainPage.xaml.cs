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
using MyScript.IInk;
using Windows.UI.Popups;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.UI;
using System.Diagnostics;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CustomControls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        //holds the value to see if this is the first time this page has been saved
        private bool firstTime = true;

        //holds the data from the floating page
        private string floatingContent = "";


        //when this page is navigated to
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //if it isn't the first time the page has been navigated to or we have moved here from the floating page
            //and that page isn't empty
            if(!firstTime || floatingContent != "")
            {
                //grabs the paramters from the floating page
                var parameters = (Parameters)e.Parameter;
                //sets the variables in this class to hold the data for the floating
                //class when the save button is clicked
                floatingContent = parameters.content;
                floatingDialogCanvases = parameters.floatingDialogCanvases;
                floatingCanvases = parameters.floatingCanvases;
            }
        }


        //list of the canvases for each of the drawing boxes so they can be sent to a gif file easily
        //when saved
        private List<InkCanvas> floatingDialogCanvases = new List<InkCanvas>();

        private List<InkCanvas> floatingCanvases = new List<InkCanvas>();


        public static Stack<ConnectionsDialog> openedConnectionsStack = new Stack<ConnectionsDialog>();

        //generic method to find all elements of a certain type in a given area/element
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

        //used in the connections method to find where the user has clicked to get the
        //box in that position
        private Rectangle rectangle;

        private ScrollViewer scroll;

        private UserControl callerControl;


        //contains save, load and getboxcount from an old version of the code, used during some development for reference
        /*
        //private async void SaveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    int[] count = getBoxCount();

        //    int notesBoxSavedCount = count[1] - 1;
        //    string[] notesBoxSavedArray = new string[count[1]];
        //    foreach(NotesBox box in FindVisualChildren<NotesBox>(NotesArea))
        //    {
        //            //workingNotesBox = new NotesBoxSaved(box.NotesBoxTitle.Text, box.NotesBoxContent.Text, Canvas.GetLeft(box), Canvas.GetTop(box));
        //            notesBoxSavedArray[notesBoxSavedCount] = box.ToString();
        //            notesBoxSavedCount--;
        //    }

        //    int codeBoxSavedCount = count[0] - 1;
        //    string[] codeBoxSavedArray = new string[count[0]];
        //    foreach(CodeBox box in FindVisualChildren<CodeBox>(NotesArea))
        //    {
        //        //workingNotesBox = new NotesBoxSaved(box.NotesBoxTitle.Text, box.NotesBoxContent.Text, Canvas.GetLeft(box), Canvas.GetTop(box));
        //        codeBoxSavedArray[codeBoxSavedCount] = box.ToString();
        //        codeBoxSavedCount--;
        //    }

        //    //int diagramBoxSavedCount = diagramBoxCount - 1;
        //    //string[] diagramBoxSavedArray = new string[diagramBoxCount];
        //    //foreach(DiagramBox box in FindVisualChildren<DiagramBox>(NotesArea))
        //    //{

        //    //}


        //    var savePicker = new Windows.Storage.Pickers.FileSavePicker();
        //    savePicker.SuggestedStartLocation =
        //        Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
        //    // Dropdown of file types the user can save the file as
        //    savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
        //    // Default file name if the user does not type one in or select a file to replace
        //    savePicker.SuggestedFileName = "New Document";

        //    Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
        //    if (file != null)
        //    {

        //        string gifName = file.Name.Substring(0, file.Name.Length - 4);
        //        Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        //        Windows.Storage.StorageFolder newFolder = await storageFolder.CreateFolderAsync(gifName, Windows.Storage.CreationCollisionOption.ReplaceExisting);
        //        Windows.Storage.StorageFolder gifs = await newFolder.CreateFolderAsync(gifName + "gifs", Windows.Storage.CreationCollisionOption.ReplaceExisting);
        //        Windows.Storage.StorageFolder iink = await newFolder.CreateFolderAsync(gifName + "iink", Windows.Storage.CreationCollisionOption.ReplaceExisting);
        //        Windows.Storage.StorageFile sampleFile = null;
        //        InkCanvas workingCanvas = new InkCanvas();

        //        int diagramBoxSavedCount = count[2] - 1;
        //        string[] diagramBoxSavedArray = new string[count[2]];

        //        foreach (DiagramBox box in FindVisualChildren<DiagramBox>(NotesArea))
        //        {

        //            diagramBoxSavedArray[diagramBoxSavedCount] = box.ToString();
        //            diagramBoxSavedCount--;
        //            int diagramBoxCount = count[2];
        //            box.getStrokesAsync();
        //            sampleFile = await gifs.CreateFileAsync(gifName + diagramBoxCount + ".gif", Windows.Storage.CreationCollisionOption.ReplaceExisting);
        //            diagramBoxCount--;

        //            // Prevent updates to the file until updates are
        //            // finalized with call to CompleteUpdatesAsync.
        //            Windows.Storage.CachedFileManager.DeferUpdates(sampleFile);
        //            // Open a file stream for writing.
        //            IRandomAccessStream stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
        //            // Write the ink strokes to the output stream.
        //            using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
        //            {
        //                await box.workingCanvas.InkPresenter.StrokeContainer.SaveAsync(outputStream);
        //                await outputStream.FlushAsync();
        //            }
        //            stream.Dispose();

        //            // Finalize write so other apps can update file.
        //            Windows.Storage.Provider.FileUpdateStatus gifStatus =
        //                await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(sampleFile);

        //        }


        //        foreach(MathBox box in FindVisualChildren<MathBox>(NotesArea))
        //        {
        //            box.SaveContent(iink);
        //        }

        //        // Prevent updates to the remote version of the file until
        //        // we finish making changes and call CompleteUpdatesAsync.
        //        Windows.Storage.CachedFileManager.DeferUpdates(file);
        //        // write to file
        //        await Windows.Storage.FileIO.WriteTextAsync(file,
        //            string.Join(",", notesBoxSavedArray.Select(x => x.ToString()).ToArray()) + Environment.NewLine +
        //            string.Join(",", codeBoxSavedArray.Select(x => x.ToString()).ToArray()) + Environment.NewLine +
        //            string.Join(",", diagramBoxSavedArray.Select(x => x.ToString()).ToArray()) + Environment.NewLine);
        //        // Let Windows know that we're finished changing the file so
        //        // the other app can update the remote version of the file.
        //        // Completing updates may require Windows to ask for user input.
        //        Windows.Storage.Provider.FileUpdateStatus hwStatus =
        //            await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);

        //        //foreach (MathBox box in FindVisualChildren<MathBox>(NotesArea))
        //        //{
        //        //    box.SaveContent(latex);
        //        //}


        //    }
        //    else
        //    {
        //        var cancelled = new MessageDialog("Operation cancelled");
        //        await cancelled.ShowAsync();
        //    }
        //}

        //private async void LoadButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var picker = new Windows.Storage.Pickers.FileOpenPicker();
        //    picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
        //    picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
        //    picker.FileTypeFilter.Add(".txt");

        //    Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

        //    if (file != null)
        //    {
        //        NotesArea.Children.Clear();
        //        string text = await Windows.Storage.FileIO.ReadTextAsync(file);
        //        string[] lines = Regex.Split(text, Environment.NewLine);





        //        string[] words = Regex.Split(lines[0], ",");
        //        int numberOfNotesBoxes = words.Length / 6;
        //        NotesBox workingNotesBox;
        //        for (int i = 0; i<numberOfNotesBoxes; i++)
        //        {
        //            int modifier = i*6;
        //            workingNotesBox = new NotesBox();
        //            workingNotesBox.NotesBoxTitle.Text = words[modifier + 0];
        //            workingNotesBox.NotesBoxContent.Text = words[modifier + 1];
        //            NotesArea.Children.Add(workingNotesBox);
        //            Canvas.SetLeft(workingNotesBox, double.Parse(words[modifier + 2]));
        //            Canvas.SetLeft(workingNotesBox, double.Parse(words[modifier + 3]));
        //            workingNotesBox.Height = double.Parse(words[modifier + 4]);
        //            workingNotesBox.Width = double.Parse(words[modifier + 5]);
        //        }


        //        words = Regex.Split(lines[1], ",");

        //        //string test = "";
        //        //foreach (string item in words)
        //        //    test += (item + "//");
        //        //var outp = new MessageDialog(test);
        //        //await outp.ShowAsync();

        //        int numberOfCodeBoxes = words.Length / 6;
        //        CodeBox workingCodeBox;
        //        for (int i = 0; i < numberOfCodeBoxes; i++)
        //        {
        //            int modifier = i * 6;
        //            workingCodeBox = new CodeBox();
        //            workingCodeBox.CodeBoxTitle.Text = words[modifier + 0];
        //            workingCodeBox.CodeBoxContent.Text = words[modifier + 1];
        //            NotesArea.Children.Add(workingCodeBox);
        //            Canvas.SetLeft(workingCodeBox, double.Parse(words[modifier + 2]));
        //            Canvas.SetLeft(workingCodeBox, double.Parse(words[modifier + 3]));
        //            workingCodeBox.Height = double.Parse(words[modifier + 4]);
        //            workingCodeBox.Width = double.Parse(words[modifier + 5]);
        //        }

        //        words = Regex.Split(lines[2], ",");
        //        int numberOfDiagramBoxes = words.Length / 2;
        //        int count = 0;
        //        DiagramBox workingDiagramBox;
        //        string gifName = file.Name.Substring(0, file.Name.Length - 4);
        //        Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        //        StorageFolder workingFolder = await storageFolder.GetFolderAsync(gifName);
        //        workingFolder = await workingFolder.GetFolderAsync(gifName + "gifs");
        //        IReadOnlyList<StorageFile> fileList = (IReadOnlyList<StorageFile>) await workingFolder.GetFilesAsync();
        //        foreach(StorageFile gifFile in fileList)
        //        {
        //            workingDiagramBox = new DiagramBox();
        //            workingDiagramBox.Height = double.Parse(words[count]);
        //            count++;
        //            workingDiagramBox.Width = double.Parse(words[count]);
        //            count++;
        //            numberOfDiagramBoxes--;
        //            IRandomAccessStream stream = await gifFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
        //            // Read from file.
        //            using (var inputStream = stream.GetInputStreamAt(0))
        //            {
        //                await workingDiagramBox.inkCanvas.InkPresenter.StrokeContainer.LoadAsync(inputStream);
        //            }
        //            stream.Dispose();

        //            NotesArea.Children.Add(workingDiagramBox);
        //            //workingDiagramBox.recognizeStrokes();
        //        }

        //        MathBox workingMathBox;
        //        workingFolder = await storageFolder.GetFolderAsync(gifName);
        //        workingFolder = await workingFolder.GetFolderAsync(gifName + "iink");
        //        fileList = (IReadOnlyList<StorageFile>)await workingFolder.GetFilesAsync();
        //        foreach (StorageFile gifFile in fileList)
        //        {
        //            workingMathBox = new MathBox();
        //            workingMathBox.Height = 400;
        //            workingMathBox.Width = 500;
        //            workingMathBox.OpenSaved(gifFile);
        //            NotesArea.Children.Add(workingMathBox);
        //            //workingDiagramBox.recognizeStrokes();
        //        }


        //    }
        //    else
        //    {
        //        var cancelled = new MessageDialog("Operation cancelled");
        //        await cancelled.ShowAsync();
        //    }
        //}
        */

        //gets the count of each type of box
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
            foreach (MathBox box in FindVisualChildren<MathBox>(depObj))
                mathsBoxCount++;
            int[] result = new int[4] { codeBoxCount, notesBoxCount, diagramBoxCount, mathsBoxCount };
            return result;
        }

        //method for the button to add a new dialog box to the main page
        private void AddDialogButton_Click(object sender, RoutedEventArgs e)
        {
            //create a column definition for the box
            ColumnDefinition col = new ColumnDefinition();
            //add this definition to the page
            DialogArea.ColumnDefinitions.Add(col);
            col.Width = new GridLength(0, GridUnitType.Auto);

            //create the new box
            NotesDialog dialog = new NotesDialog();
            DialogArea.Children.Add(dialog);

            //put the box into the column the add dialog button was in and move the button to the new column
            var len = DialogArea.ColumnDefinitions.Count;
            Grid.SetColumn(dialog, len - 2);
            Grid.SetColumn(AddDialogButton, len - 1);
        }

        public async void SaveButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            //tell the page that the first save has been done
            firstTime = false;
            //create a save picker so that the user can select where the file is saved
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";

            //wait for the user to save the file
            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                //this gets the name of the file the user just saved, with the file extension removed
                string gifName = file.Name.Substring(0, file.Name.Length - 4);

                //create a folder to store the gifs for the smart ink from each dialog box
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFolder newFolder = await storageFolder.CreateFolderAsync(gifName, Windows.Storage.CreationCollisionOption.ReplaceExisting);
                int dialogCount = 0;

                //get the count for the dialog boxes
                foreach (var dialog in FindVisualChildren<NotesDialog>(DialogArea))
                {
                    dialogCount++;
                }
                //create an array for the titles of the boxes
                string[] dialogTitles = new string[dialogCount];

                int index = 0;
                string title = "";
                //gets the  title for each of the dialog boxes and adds to a string
                foreach (var dialog in FindVisualChildren<NotesDialog>(DialogArea))
                {
                    title = dialog.ToString();
                    if (dialogTitles.Contains(title))
                        title += index.ToString();
                    dialogTitles[index] = title;
                    index++;
                }
                //write the string to the save file
                Windows.Storage.CachedFileManager.DeferUpdates(file);

                //gets the data for each of the dialog boxes and adds them to a string which is written to a text file.
                await Windows.Storage.FileIO.WriteTextAsync(file,
                    string.Join(",", dialogTitles.Select(x => x.ToString()).ToArray()) + Environment.NewLine);

                int dialogIndex = 0;
                //resest the dialogIndex


                foreach (var dialog in FindVisualChildren<NotesDialog>(DialogArea))
                {
                    //create folders for mathbox data, dialogbox data and canvases from connections
                    Windows.Storage.StorageFolder dialogFolder = await newFolder.CreateFolderAsync(dialogTitles[dialogIndex], Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    Windows.Storage.StorageFolder gifs = await dialogFolder.CreateFolderAsync(dialogTitles[dialogIndex] + "gifs", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    Windows.Storage.StorageFolder iink = await dialogFolder.CreateFolderAsync(dialogTitles[dialogIndex] + "iink", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    Windows.Storage.StorageFolder canvasFolder = await dialogFolder.CreateFolderAsync(dialogTitles[dialogIndex] + "canvases", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    dialogIndex++;
                    Windows.Storage.StorageFile sampleFile = null;

                    //gets the box count for each type of box
                    var count = getBoxCount(dialog);
                    //used as an index
                    int notesBoxSavedCount = 0;
                    //creates an array of size number of notes boxes
                    string[] notesBoxSavedArray = new string[count[1]];
                    int canvasCount = 0;
                    //for each of the notes boxes in the dialog
                    foreach (NotesBox box in FindVisualChildren<NotesBox>(dialog))
                    {
                        //puts the data for each of the notes boxes in the array
                        notesBoxSavedArray[notesBoxSavedCount] = box.ToString();
                        notesBoxSavedCount++;
                        //for each of the diagram boxes in the connections of the notes box
                        foreach (InkCanvas canvas in box.canvasList)
                        {
                            //create a file for the canvas
                            sampleFile = await canvasFolder.CreateFileAsync("canvas" + canvasCount + ".gif", Windows.Storage.CreationCollisionOption.GenerateUniqueName);
                            canvasCount++;

                            // Prevent updates to the file until updates are
                            // finalized with call to CompleteUpdatesAsync.
                            Windows.Storage.CachedFileManager.DeferUpdates(sampleFile);
                            // Open a file stream for writing.
                            IRandomAccessStream stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                            // Write the ink strokes to the output stream.
                            using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
                            {
                                await canvas.InkPresenter.StrokeContainer.SaveAsync(outputStream);
                                await outputStream.FlushAsync();
                            }
                            stream.Dispose();

                            // Finalize write so other apps can update file.
                            Windows.Storage.Provider.FileUpdateStatus gifStatus =
                                await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(sampleFile);
                        }
                    }

                    //same as above but for the code boxes
                    int codeBoxSavedCount = 0;
                    string[] codeBoxSavedArray = new string[count[0]];
                    foreach (CodeBox box in FindVisualChildren<CodeBox>(dialog))
                    {
                        codeBoxSavedArray[codeBoxSavedCount] = box.ToString();
                        codeBoxSavedCount++;
                        foreach (InkCanvas canvas in box.canvasList)
                        {
                            sampleFile = await canvasFolder.CreateFileAsync("canvas" + canvasCount + ".gif", Windows.Storage.CreationCollisionOption.GenerateUniqueName);
                            canvasCount++;
                            //diagramBoxCount--;
                            // Prevent updates to the file until updates are
                            // finalized with call to CompleteUpdatesAsync.
                            Windows.Storage.CachedFileManager.DeferUpdates(sampleFile);
                            // Open a file stream for writing.
                            IRandomAccessStream stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                            // Write the ink strokes to the output stream.
                            using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
                            {
                                await canvas.InkPresenter.StrokeContainer.SaveAsync(outputStream);
                                await outputStream.FlushAsync();
                            }
                            stream.Dispose();

                            // Finalize write so other apps can update file.
                            Windows.Storage.Provider.FileUpdateStatus gifStatus =
                                await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(sampleFile);
                        }
                    }


                    //used as a canvas to write to during hte method
                    InkCanvas workingCanvas = new InkCanvas();

                    int savedCount = 0;
                    string[] diagramBoxSavedArray = new string[count[2]];

                    //for each diagram box
                    foreach (DiagramBox box in FindVisualChildren<DiagramBox>(dialog.NotesStack))
                    {
                        diagramBoxSavedArray[savedCount] = box.ToString();
                        //diagramBoxSavedCount++;
                        box.getStrokesAsync();
                        sampleFile = await gifs.CreateFileAsync(dialog.ToString() + /*diagramBoxCount*/ savedCount + ".gif", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                        //diagramBoxCount--;
                        savedCount++;

                        // Prevent updates to the file until updates are
                        // finalized with call to CompleteUpdatesAsync.
                        Windows.Storage.CachedFileManager.DeferUpdates(sampleFile);
                        // Open a file stream for writing.
                        IRandomAccessStream stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                        // Write the ink strokes to the output stream.
                        using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
                        {
                            await box.workingCanvas.InkPresenter.StrokeContainer.SaveAsync(outputStream);
                            await outputStream.FlushAsync();
                        }
                        stream.Dispose();

                        // Finalize write so other apps can update file.
                        Windows.Storage.Provider.FileUpdateStatus gifStatus =
                            await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(sampleFile);

                        //same as before for any connections for the box
                        foreach (InkCanvas canvas in box.canvasList)
                        {
                            //create a file for the canvas
                            sampleFile = await canvasFolder.CreateFileAsync("canvas" + canvasCount + ".gif", Windows.Storage.CreationCollisionOption.GenerateUniqueName);
                            canvasCount++;

                            // Prevent updates to the file until updates are
                            // finalized with call to CompleteUpdatesAsync.
                            Windows.Storage.CachedFileManager.DeferUpdates(sampleFile);
                            // Open a file stream for writing.
                            stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                            // Write the ink strokes to the output stream.
                            using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
                            {
                                await canvas.InkPresenter.StrokeContainer.SaveAsync(outputStream);
                                await outputStream.FlushAsync();
                            }
                            stream.Dispose();

                            // Finalize write so other apps can update file.
                            gifStatus = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(sampleFile);
                        }
                    }

                    //gets a list of all of the types for each of the boxes in the dialog box
                    string[] listOfBoxTypes = new string[dialog.NotesStack.Children.Count];
                    int number = 0;
                    foreach (var item in dialog.NotesStack.Children)
                    {
                        listOfBoxTypes[number] = item.GetType().ToString();
                        number++;
                    }
                    //Prevent updates to the remote version of the file until
                    // we finish making changes and call CompleteUpdatesAsync.
                    Windows.Storage.CachedFileManager.DeferUpdates(file);
                    // write to file all of teh arrays we have created for the different types of boxes
                    await Windows.Storage.FileIO.AppendTextAsync(file,
                        string.Join(",", listOfBoxTypes.Select(x => x.ToString()).ToArray()) + Environment.NewLine +
                        string.Join(",", notesBoxSavedArray.Select(x => x.ToString()).ToArray()) + Environment.NewLine +
                        string.Join(",", codeBoxSavedArray.Select(x => x.ToString()).ToArray()) + Environment.NewLine +
                        string.Join(",", diagramBoxSavedArray.Select(x => x.ToString()).ToArray()) + Environment.NewLine);
                }
                // Let Windows know that we're finished changing the file so
                // the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                Windows.Storage.Provider.FileUpdateStatus hwStatus =
                        await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                await saveFloating(newFolder);
            }
            else
            {
                var cancelled = new MessageDialog("Operation cancelled");
                await cancelled.ShowAsync();
            }
        }

        //this is a method to save all of the data in the floating page of the app
        //since I only wanted the app to be saved from one of the pages to redce the code complexity
        //this page needs to be able to read data from the other page, this is done using the parameters
        //which were set in the onnavigatedto method
        private async Task saveFloating(StorageFolder folderToUse)
        {
            //create a folder and file for the floating page
            StorageFolder floatingFolder = await folderToUse.CreateFolderAsync("FloatingFolder");
            StorageFile file = await floatingFolder.CreateFileAsync("content.txt");

            //save the file
            Windows.Storage.CachedFileManager.DeferUpdates(file);
            await Windows.Storage.FileIO.AppendTextAsync(file, floatingContent);
            Windows.Storage.Provider.FileUpdateStatus hwStatus = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);

            //create a folder for the diagrams from the floating page in normal boxes and boxes inside dialogs
            StorageFolder canvases = await floatingFolder.CreateFolderAsync("gifs", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            StorageFolder dialogCanvases = await floatingFolder.CreateFolderAsync("dialoggifs", Windows.Storage.CreationCollisionOption.ReplaceExisting);

            StorageFile sampleFile = null;
            //gets the floating content in an array
            string[] contentList = Regex.Split(floatingContent, Environment.NewLine);
            int contentIndex = 0;
            int floatingCanvasIndex = 0;
            int floatingDialogCanvasIndex = 0;
            int floatingSavedCount = 0;

            //for each element in the aray made from the floating content string being split
            //each element in the array represents a different box from the floating page
            foreach (string line in contentList)
            {
                string[] item = Regex.Split(line, ",");
                //if it was a dialog box
                if(item[0] == "CustomControls.FloatingNotesDialog")
                {
                    int savedCount = 0;

                    foreach (string box in Regex.Split(contentList[contentIndex + 1], ","))
                    {
                        //if the box in the dialog box was a diagram, we need to save the drawing
                        if(box == "CustomControls.DiagramBox")
                        {
                            sampleFile = await dialogCanvases.CreateFileAsync("canvas" + /*diagramBoxCount*/ savedCount + ".gif", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                            //diagramBoxCount--;
                            savedCount++;

                            // Prevent updates to the file until updates are
                            // finalized with call to CompleteUpdatesAsync.
                            Windows.Storage.CachedFileManager.DeferUpdates(sampleFile);
                            // Open a file stream for writing.
                            IRandomAccessStream stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                            // Write the ink strokes to the output stream.
                            using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
                            {
                                //use the list of canvases in dialog boxes we got from being navigted to
                                await floatingDialogCanvases[floatingDialogCanvasIndex].InkPresenter.StrokeContainer.SaveAsync(outputStream);
                                await outputStream.FlushAsync();
                            }
                            stream.Dispose();

                            // Finalize write so other apps can update file.
                            Windows.Storage.Provider.FileUpdateStatus gifStatus =
                                await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(sampleFile);
                            floatingDialogCanvasIndex++;
                        }
                    }

                }
                //if the box was a diagram box we need to save the drawing
                else if(item[0] == "CustomControls.FloatingDiagramBox")
                {
                    sampleFile = await canvases.CreateFileAsync(item[1] + floatingSavedCount +  ".gif", Windows.Storage.CreationCollisionOption.GenerateUniqueName);
                    floatingSavedCount++;
                    // Prevent updates to the file until updates are
                    // finalized with call to CompleteUpdatesAsync.
                    Windows.Storage.CachedFileManager.DeferUpdates(sampleFile);
                    // Open a file stream for writing.
                    IRandomAccessStream stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                    // Write the ink strokes to the output stream.
                    using (IOutputStream outputStream = stream.GetOutputStreamAt(0))
                    {
                        //use the list of canvases we got from being navigated to
                        await floatingCanvases[floatingCanvasIndex].InkPresenter.StrokeContainer.SaveAsync(outputStream);
                        await outputStream.FlushAsync();
                    }
                    stream.Dispose();

                    // Finalize write so other apps can update file.
                    Windows.Storage.Provider.FileUpdateStatus gifStatus =
                        await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(sampleFile);
                    floatingCanvasIndex++;
                }
                contentIndex++; //this needs to be the last thing that hapens in the foreach loop
            }
        }



        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            //tells the app that this has been saved before once it is loaded
            firstTime = false;

            //select the file the user wants to load and assume that the file is from this app
            //there is no check for this, perhaps it could be added in future
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".txt");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                string fileName = file.Name.Substring(0, file.Name.Length - 4);
                //gets the filename minus the file extension
                //clear the page
                clearPage();
                //creates the addidalogbox button again
                DialogArea.ColumnDefinitions.Clear();
                ColumnDefinition firstCol = new ColumnDefinition();
                DialogArea.ColumnDefinitions.Add(firstCol);
                firstCol.Width = new GridLength(0, GridUnitType.Auto);
                Grid.SetColumn(AddDialogButton, 0);

                //reads the file the user selected ands split it by new line
                string text = await Windows.Storage.FileIO.ReadTextAsync(file);
                string[] lines = Regex.Split(text, Environment.NewLine);

                //gets the titles of the dialog boxes
                string[] dialogs = Regex.Split(lines[0], ",");

                //creates indexes and lists for the method to keep track of each kind of box
                ColumnDefinition col = null;
                NotesDialog newDialog = null;
                int listLineIndex = 1;
                int notesListIndex = 0;
                int codeListIndex = 0;
                int diagramListIndex = 0;
                int diagramGifIndex = 0;
                string[] list = null;
                string[] notesList = null;
                string[] codeList = null;
                string[] diagramList = null;
                NotesBox notes = null;
                CodeBox code = null;
                DiagramBox diagram = null;
                floatingCanvases = new List<InkCanvas>();
                floatingDialogCanvases = new List<InkCanvas>();

                
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                StorageFolder workingFolder = await storageFolder.GetFolderAsync(fileName);
                StorageFolder floatingFolder = await workingFolder.GetFolderAsync("FloatingFolder");
                StorageFile floatingFile = await floatingFolder.GetFileAsync("content.txt");
                //gets the content for the floating page
                floatingContent = System.IO.File.ReadAllText(floatingFile.Path);
                StorageFolder workingCanvasFolder = await floatingFolder.GetFolderAsync("gifs");
                //gets a list of the canavs files
                IReadOnlyList<StorageFile> listOfFloatingCanvasFiles = await workingCanvasFolder.GetFilesAsync();

                //add the data for each of the canvases to the list
                foreach(StorageFile canvasFile in listOfFloatingCanvasFiles)
                {
                    InkCanvas workingFloatingCanvas = new InkCanvas();

                    IRandomAccessStream stream = await canvasFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
                    // Read from file.
                    using (var inputStream = stream.GetInputStreamAt(0))
                    {
                        await workingFloatingCanvas.InkPresenter.StrokeContainer.LoadAsync(inputStream);
                    }
                    stream.Dispose();
                    floatingCanvases.Add(workingFloatingCanvas);
                }

                //get the fodler for the digram data from the diagrams in each dialog box
                workingCanvasFolder = await floatingFolder.GetFolderAsync("dialoggifs");
                listOfFloatingCanvasFiles = await workingCanvasFolder.GetFilesAsync();
                foreach (StorageFile canvasFile in listOfFloatingCanvasFiles)
                {
                    InkCanvas workingFloatingCanvas = new InkCanvas();

                    IRandomAccessStream stream = await canvasFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
                    // Read from file.
                    using (var inputStream = stream.GetInputStreamAt(0))
                    {
                        await workingFloatingCanvas.InkPresenter.StrokeContainer.LoadAsync(inputStream);
                    }
                    stream.Dispose();
                    floatingDialogCanvases.Add(workingFloatingCanvas);
                }

                //for each of the dialogs to be created
                foreach (string dialog in dialogs)
                {
                    //get the folder for that dialog
                    workingFolder = await storageFolder.GetFolderAsync(fileName);


                    workingFolder = await workingFolder.GetFolderAsync(dialog);
                    StorageFolder canvasFolder = await workingFolder.GetFolderAsync(dialog + "canvases");
                    workingFolder = await workingFolder.GetFolderAsync(dialog + "gifs");
                    //get all the diagrams for that dialog
                    IReadOnlyList<StorageFile> canvasFiles = await canvasFolder.GetFilesAsync();

                    //create a new dialog and add it to the page
                    //in future could make this a method 
                    //then create some indices for use by teh next method
                    newDialog = new NotesDialog();
                    newDialog.DialogTitleBox.Text = dialog;
                    notesListIndex = 0;
                    codeListIndex = 0;
                    diagramListIndex = 0;
                    diagramGifIndex = 0;
                    col = new ColumnDefinition();
                    DialogArea.ColumnDefinitions.Add(col);
                    col.Width = new GridLength(0, GridUnitType.Auto);
                    newDialog = new NotesDialog();
                    DialogArea.Children.Add(newDialog);
                    var len = DialogArea.ColumnDefinitions.Count;
                    Grid.SetColumn(newDialog, len - 2);
                    Grid.SetColumn(AddDialogButton, len - 1);

                    //get a list of all the type of box for this dialog
                    list = Regex.Split(lines[listLineIndex], ",");
                    //get a list of all the data for the note boxes, code boxes and diagram boxes 
                    notesList = Regex.Split(lines[listLineIndex + 1], ",");
                    codeList = Regex.Split(lines[listLineIndex + 2], ",");
                    diagramList = Regex.Split(lines[listLineIndex + 3], ",");
                    int canvasIndex = 0;
                    //for each of the boxes to be added to the dialog
                    foreach (string box in list)
                    {
                        //if the box is a notes box
                        if(box == "CustomControls.NotesBox")
                        {
                            //create the notes box and add it to the dialog
                            notes = new NotesBox();
                            notes.NotesBoxContent.Text = notesList[notesListIndex];
                            notesListIndex++;
                            notes.Height = Double.Parse(notesList[notesListIndex]);
                            notesListIndex++;
                            notes.Width = Double.Parse(notesList[notesListIndex]);
                            notesListIndex++;
                            var connections = notesList[notesListIndex];
                            notesListIndex++;
                            //creates a list of the connections to the box
                            connections = connections.Replace("/", ",");
                            string[] tempList = Regex.Split(connections, "-");
                            //add each connection to the connnections list
                            foreach(string s in tempList)
                            {
                                notes.connectionsList.Add(s);
                            }
                            notes.connectionsList.RemoveAt(notes.connectionsList.Count - 1);

                            //add all of the diagrams into the canvas list for this box
                            InkCanvas workingCanvas = new InkCanvas();
                            int count = canvasIndex;
                            for(int i = count; i < int.Parse(notesList[notesListIndex]) + count; i++)
                            {
                                IRandomAccessStream stream = await canvasFiles.ElementAt(canvasIndex).OpenAsync(Windows.Storage.FileAccessMode.Read);
                                // Read from file.
                                using (var inputStream = stream.GetInputStreamAt(0))
                                {
                                    await workingCanvas.InkPresenter.StrokeContainer.LoadAsync(inputStream);
                                }
                                stream.Dispose();
                                notes.canvasList.Add(workingCanvas);
                                canvasIndex++;
                            }
                            notesListIndex++;

                            newDialog.NotesStack.Children.Add(notes);
                        }

                        //same as above but for code box
                        else if(box == "CustomControls.CodeBox")
                        {
                            code = new CodeBox();
                            code.CodeBoxTitle.Text = codeList[codeListIndex];
                            codeListIndex++;
                            code.CodeBoxContent.Text = codeList[codeListIndex];
                            codeListIndex++;
                            code.Height = Double.Parse(codeList[codeListIndex]);
                            codeListIndex++;
                            code.Width = Double.Parse(codeList[codeListIndex]);
                            codeListIndex++;
                            var connections = codeList[codeListIndex];
                            codeListIndex++;
                            connections = connections.Replace("/", ",");
                            string[] tempList = Regex.Split(connections, "-");
                            foreach (string s in tempList)
                            {
                                code.connectionsList.Add(s);
                            }
                            code.connectionsList.RemoveAt(code.connectionsList.Count - 1);
                            InkCanvas workingCanvas = new InkCanvas();
                            int count = canvasIndex;
                            for (int i = count; i < int.Parse(codeList[codeListIndex]) + count; i++)
                            {
                                IRandomAccessStream stream = await canvasFiles.ElementAt(canvasIndex).OpenAsync(Windows.Storage.FileAccessMode.Read);
                                // Read from file.
                                using (var inputStream = stream.GetInputStreamAt(0))
                                {
                                    await workingCanvas.InkPresenter.StrokeContainer.LoadAsync(inputStream);
                                }
                                stream.Dispose();
                                code.canvasList.Add(workingCanvas);
                                canvasIndex++;
                            }
                            codeListIndex++;
                            newDialog.NotesStack.Children.Add(code);
                        }

                        //same as above but for diagram box
                        else if(box == "CustomControls.DiagramBox")
                        {

                            diagram = new DiagramBox();
                            diagram.DiagramBoxTitle.Text = diagramList[diagramListIndex];
                            diagramListIndex++;
                            diagram.Height = Double.Parse(diagramList[diagramListIndex]);
                            diagramListIndex++;
                            diagram.Width = Double.Parse(diagramList[diagramListIndex]);
                            diagramListIndex++;
                            var connections = diagramList[diagramListIndex];
                            diagramListIndex++;
                            connections = connections.Replace("/", ",");
                            string[] tempList = Regex.Split(connections, "-");
                            foreach (string s in tempList)
                            {
                                diagram.connectionsList.Add(s);
                            }
                            diagram.connectionsList.RemoveAt(diagram.connectionsList.Count - 1);
                            int count = canvasIndex;
                            for (int i = count; i < int.Parse(diagramList[diagramListIndex]) + count; i++)
                            {
                                InkCanvas workingCanvas = new InkCanvas();
                                IRandomAccessStream stream1 = await canvasFiles.ElementAt(canvasIndex).OpenAsync(Windows.Storage.FileAccessMode.Read);
                                // Read from file.
                                using (var inputStream = stream1.GetInputStreamAt(0))
                                {
                                    await workingCanvas.InkPresenter.StrokeContainer.LoadAsync(inputStream);
                                }
                                stream1.Dispose();
                                diagram.canvasList.Add(workingCanvas);
                                canvasIndex++;
                            }
                            diagramListIndex++;

                            IReadOnlyList<StorageFile> fileList = (IReadOnlyList<StorageFile>)await workingFolder.GetFilesAsync();
                            //diagramGifIndex = fileList.Count() - count - 1;
                            IRandomAccessStream stream = await fileList[diagramGifIndex].OpenAsync(Windows.Storage.FileAccessMode.Read);
                            // Read from file.
                            using (var inputStream = stream.GetInputStreamAt(0))
                            {
                                await diagram.inkCanvas.InkPresenter.StrokeContainer.LoadAsync(inputStream);
                            }
                            stream.Dispose();
                            diagramGifIndex++;
                            newDialog.NotesStack.Children.Add(diagram);
                        }
                    }
                    //increments the index to get the data for the next dialog
                    listLineIndex += 4;
                }
            }
            else
            {
                var cancelled = new MessageDialog("Operation cancelled");
                await cancelled.ShowAsync();
            }
        }

        private void clearPage()
        {
            foreach (NotesDialog dialog in FindVisualChildren<NotesDialog>(DialogArea))
            {
                DialogArea.Children.Remove(dialog);
            }
        }

        //creates the pop up for the connections for each box
        public static async void createConnectionsDialogAsync(List<string> requiredList, UserControl caller, List<InkCanvas> requiredCanvases)
        {
            //hide the current popups and add them to the stack so they can be redisplayed once the current
            //one is closed
            var popups = VisualTreeHelper.GetOpenPopups(Window.Current);
            foreach (var popup in popups)
            {
                if (popup.Child is ConnectionsDialog)
                {
                    var openDialog = (ConnectionsDialog)popup.Child;
                    openedConnectionsStack.Push(openDialog);
                    openDialog.Hide();
                }
            }
            //create the new popup
            Connections connections = new Connections();
            var window = new ConnectionsDialog
            {
                Content = connections
            };
            //set the content for the connection popup
            connections.setConnections(requiredList);
            connections.setCanvases(requiredCanvases);
            connections.loadConnections();
            connections.setCaller(caller);
            await window.ShowAsync();
        }

        //create the rectangle used in the connections mechanism, finds where the user has clicked
        public void createRectangle(UserControl control)
        {
            callerControl = control;

            scroll = new ScrollViewer();
            Grid.SetRow(scroll, 1);
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            scroll.HorizontalScrollMode = ScrollMode.Auto;
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            scroll.VerticalScrollMode = ScrollMode.Disabled;
            scroll.Name = "rectangleScroller";
            scroll.ViewChanged += scrollChanged;

            Rectangle rectangle = new Rectangle();
            rectangle.SetValue(Grid.ColumnSpanProperty, DialogArea.ColumnDefinitions.Count);
            //rectangle.Stretch = Stretch.UniformToFill;
            //rectangle.HorizontalAlignment = HorizontalAlignment.Stretch;
            //rectangle.VerticalAlignment = VerticalAlignment.Stretch;
            rectangle.Width = DialogArea.ActualWidth;
            Grid.SetRow(rectangle, 1);//change to be added to scrollviewr instead
            rectangle.Fill = new SolidColorBrush(Colors.Transparent);
            scroll.Content = rectangle;
            Notes.Children.Add(scroll);
            rectangle.PointerPressed += rectangle_PointerPressed;
        }

        private async void rectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //get the point which the rectangle was clicked
            Point p = e.GetCurrentPoint(this).Position;

            //remove the scroll for the rectangle
            Notes.Children.Remove(scroll);
            //transforms the point into something useable
            GeneralTransform gt = Notes.TransformToVisual(this);
            Point pagePoint = gt.TransformPoint(p);
            //gets the element at the point the user has clicked
            var elements = VisualTreeHelper.FindElementsInHostCoordinates(pagePoint, Notes);

            UIElement element;
            
            //gets the box the user clicked on
            //the numbers are the layer at which the box is at, dependent on what is in the box
            if (elements.ElementAt<UIElement>(11).GetType() == typeof(NotesBox))
            {
                element = elements.ElementAt<UIElement>(11);
                getControlType(callerControl, element);
            }
            else if (elements.ElementAt<UIElement>(10).GetType() == typeof(CodeBox))
            {
                element = elements.ElementAt<UIElement>(10);
                getControlType(callerControl, element);
            }
            else if (elements.ElementAt<UIElement>(4).GetType() == typeof(DiagramBox))
            {
                element = elements.ElementAt<UIElement>(4);
                getControlType(callerControl, element);
            }
            else
            {
                var box = new MessageDialog("You may only connect a box to another box, please try again");
                await box.ShowAsync();
                openedConnectionsStack.Pop();
            }
        }

        //syncs the rectabgle scroll with the page scroll
        private void scrollChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            ScrollViewer scroller = sender as ScrollViewer;

            NotesScroller.ChangeView(scroller.HorizontalOffset, 0, 1);
        }

        //helper method to get the type of a box
        public void getControlType(UserControl control, UIElement element)
        {
            if (control.GetType() == typeof(NotesBox))
            {
                NotesBox box = (NotesBox)callerControl;
                addConnectionToNotesBox(box, element);
            }
            else if (control.GetType() == typeof(CodeBox))
            {
                CodeBox box = (CodeBox)callerControl;
                addConnectionToCodeBox(box, element);
            }
            else if (control.GetType() == typeof(DiagramBox))
            {
                DiagramBox box = (DiagramBox)callerControl;
                addConnectionToDiagramBox(box, element);
            }
            else if (control.GetType() == typeof(MathBox))
            {
                MathBox box = (MathBox)callerControl;
                addConnectionToMathBox(box, element);
            }
        }


        //adds a connection to the connections list of a box
        //box to add to is the box we want to add the connection to and element 
        //is the connection we want to add
        private void addConnectionToNotesBox(NotesBox boxToAddTo, UIElement element)
        {
            //add the box to the connections and display teh connections for the boxtoaddto
            if (element.GetType() == typeof(NotesBox))
            {
                element = (NotesBox)element;
                boxToAddTo.connectionsList.Add("NotesBox," + (element as NotesBox).connectionsToString());
                boxToAddTo.displayConnections();
            }
            //same as above but for a code box
            else if (element.GetType() == typeof(CodeBox))
            {
                element = (CodeBox)element;
                boxToAddTo.connectionsList.Add("CodeBox," + (element as CodeBox).connectionsToString());
                boxToAddTo.displayConnections();
            }
            //same as above but for diagram box, but this time we add the canvas as well
            else if (element.GetType() == typeof(DiagramBox))
            {
                element = (DiagramBox)element;
                boxToAddTo.connectionsList.Add("DiagramBox," + (element as DiagramBox).connectionsToString());
                InkCanvas canvas = new InkCanvas();
                var strokes = (element as DiagramBox).inkCanvas.InkPresenter.StrokeContainer.GetStrokes();
                foreach (var stroke in strokes)
                {
                    canvas.InkPresenter.StrokeContainer.AddStroke(stroke.Clone());
                }
                boxToAddTo.canvasList.Add(canvas);
                boxToAddTo.displayConnections();
            }
            //same as above but for math box, this doesn't work properly as I couldn't implement the box fully
            else if (element.GetType() == typeof(MathBox))
            {
                element = (MathBox)element;
                boxToAddTo.connectionsList.Add("MathBox," + (element as MathBox).connectionsToString());
                boxToAddTo.displayConnections();
            }
            //remove the popup from the stack
            //this allows for connections of connected boxes to be seen
            openedConnectionsStack.Pop();
        }
        //same as above but for each of the respective box types
        private void addConnectionToCodeBox(CodeBox boxToAddTo, UIElement element)
        {
            if (element.GetType() == typeof(NotesBox))
            {
                element = (NotesBox)element;
                boxToAddTo.connectionsList.Add("NotesBox," + (element as NotesBox).connectionsToString());
                boxToAddTo.displayConnections();
            }
            else if (element.GetType() == typeof(CodeBox))
            {
                element = (CodeBox)element;
                boxToAddTo.connectionsList.Add("CodeBox," + (element as CodeBox).connectionsToString());
                boxToAddTo.displayConnections();
            }
            else if (element.GetType() == typeof(DiagramBox))
            {
                element = (DiagramBox)element;
                boxToAddTo.connectionsList.Add("DiagramBox," + (element as DiagramBox).connectionsToString());
                InkCanvas canvas = new InkCanvas();
                var strokes = (element as DiagramBox).inkCanvas.InkPresenter.StrokeContainer.GetStrokes();
                foreach (var stroke in strokes)
                {
                    canvas.InkPresenter.StrokeContainer.AddStroke(stroke.Clone());
                }
                boxToAddTo.canvasList.Add(canvas);
                boxToAddTo.displayConnections();
            }
            else if (element.GetType() == typeof(MathBox))
            {
                element = (MathBox)element;
                boxToAddTo.connectionsList.Add("MathBox," + (element as MathBox).connectionsToString());
                boxToAddTo.displayConnections();
            }
            openedConnectionsStack.Pop();
        }

        private void addConnectionToDiagramBox(DiagramBox boxToAddTo, UIElement element)
        {
            if (element.GetType() == typeof(NotesBox))
            {
                element = (NotesBox)element;
                boxToAddTo.connectionsList.Add("NotesBox," + (element as NotesBox).connectionsToString());
                boxToAddTo.displayConnections();
            }
            else if (element.GetType() == typeof(CodeBox))
            {
                element = (CodeBox)element;
                boxToAddTo.connectionsList.Add("CodeBox," + (element as CodeBox).connectionsToString());
                boxToAddTo.displayConnections();
            }
            else if (element.GetType() == typeof(DiagramBox))
            {
                element = (DiagramBox)element;
                boxToAddTo.connectionsList.Add("DiagramBox," + (element as DiagramBox).connectionsToString());
                InkCanvas canvas = new InkCanvas();
                var strokes = (element as DiagramBox).inkCanvas.InkPresenter.StrokeContainer.GetStrokes();
                foreach (var stroke in strokes)
                {
                    canvas.InkPresenter.StrokeContainer.AddStroke(stroke.Clone());
                }
                boxToAddTo.canvasList.Add(canvas);
                boxToAddTo.displayConnections();
            }
            else if (element.GetType() == typeof(MathBox))
            {
                element = (MathBox)element;
                boxToAddTo.connectionsList.Add("MathBox," + (element as MathBox).connectionsToString());
                boxToAddTo.displayConnections();
            }
            openedConnectionsStack.Pop();
        }

        private void addConnectionToMathBox(MathBox boxToAddTo, UIElement element)
        {
            if (element.GetType() == typeof(NotesBox))
            {
                element = (NotesBox)element;
                boxToAddTo.connectionsList.Add("NotesBox," + (element as NotesBox).connectionsToString());
                //boxToAddTo.displayConnections();
            }
            else if (element.GetType() == typeof(CodeBox))
            {
                element = (CodeBox)element;
                boxToAddTo.connectionsList.Add("CodeBox," + (element as CodeBox).connectionsToString());
                //boxToAddTo.displayConnections();
            }
            else if (element.GetType() == typeof(DiagramBox))
            {
                element = (DiagramBox)element;
                boxToAddTo.connectionsList.Add("DiagramBox," + (element as DiagramBox).connectionsToString());
                InkCanvas canvas = new InkCanvas();
                var strokes = (element as DiagramBox).inkCanvas.InkPresenter.StrokeContainer.GetStrokes();
                foreach (var stroke in strokes)
                {
                    canvas.InkPresenter.StrokeContainer.AddStroke(stroke.Clone());
                }
                boxToAddTo.canvasList.Add(canvas);
                //boxToAddTo.displayConnections();
            }
            else if (element.GetType() == typeof(MathBox))
            {
                element = (MathBox)element;
                boxToAddTo.connectionsList.Add("MathBox," + (element as MathBox).connectionsToString());
                //boxToAddTo.displayConnections();
            }
            openedConnectionsStack.Pop();
        }

        //navigate to the floating page
        private void NavigateToFloatingPageButton_Click(object sender, RoutedEventArgs e)
        {
            firstTime = false;
            Parameters parameters = new Parameters(floatingCanvases, floatingDialogCanvases, floatingContent);
            this.Frame.Navigate(typeof(FloatingPage), parameters);
        }

    }
}

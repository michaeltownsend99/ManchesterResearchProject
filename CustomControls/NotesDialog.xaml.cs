using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class NotesDialog : UserControl
    {
        public NotesDialog()
        {
            this.InitializeComponent();
        }

        //these 4 methods are used to add new boxes to the dialog

        private void CodeBoxButton_Click(object sender, RoutedEventArgs e)
        {
            CodeBox box = new CodeBox();
            box.Height = 300;
            box.Width = 800;
            NotesStack.Children.Add(box);
        }

        private void NotesBoxButton_Click(object sender, RoutedEventArgs e)
        {
            NotesBox box = new NotesBox();
            box.Height = 300;
            box.Width = 800;
            NotesStack.Children.Add(box);
        }

        private void DiagramButton_Click(object sender, RoutedEventArgs e)
        {
            DiagramBox box = new DiagramBox();
            box.Height = 300;
            box.Width = 800;
            NotesStack.Children.Add(box);
        }

        private void MathBoxButton_Click(object sender, RoutedEventArgs e)
        {
            MathBox box = new MathBox();
            box.Height = 300;
            box.Width = 800;
            NotesStack.Children.Add(box);
        }

        //removes the dialog from the page, makes sure teh user is sure as it also removes all the child boxes
        private async void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Popups.MessageDialog showDialog = new Windows.UI.Popups.MessageDialog("Are you sure you want to delete this note?");
            showDialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            showDialog.Commands.Add(new UICommand("No") { Id = 1 });
            showDialog.DefaultCommandIndex = 0;
            showDialog.CancelCommandIndex = 1;
            var result = await showDialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                var parent = this.Parent as Grid;
                parent.Children.Remove(this);
            }
        }

        

        public override string ToString()
        {
            return DialogTitleBox.Text;
        }
    }
}

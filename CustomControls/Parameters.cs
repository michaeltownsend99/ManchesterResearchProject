using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace CustomControls
{
    class Parameters
    {

        //used as a holder for parameters to pass between the two page as can only pass 1 argument

        public List<InkCanvas> floatingCanvases = new List<InkCanvas>();
        public List<InkCanvas> floatingDialogCanvases = new List<InkCanvas>();
        public string content = "";

        public Parameters(List<InkCanvas> requiredFloatingList, List<InkCanvas> requiredFloatingDialogList, string requiredContent)
        {
            floatingCanvases = requiredFloatingList;
            content = requiredContent;
            floatingDialogCanvases = requiredFloatingDialogList;
        }
    }
}

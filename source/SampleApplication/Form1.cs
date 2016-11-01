using System;
using System.Windows.Forms;
using OverlayMessageBox;

namespace SampleApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OverlayMessageBoxSettings settings = new OverlayMessageBoxSettings();
            
            settings.Caption = "Title goes here";
            settings.Headline = "My Headline";
            settings.Text = "Sample text";
            settings.Symbol = OverlayMessageBoxSymbol.Warning;
            settings.ButtonSet = OverlayMessageBoxButtonSet.RetryCancel;

            //Note: If you are getting an error "Unable to find an entry point named..."
            //you need to create an application manifest so Windows will use the "new" ComCtl32.dll that
            //contans TaskDialog.
            //See here: http://stackoverflow.com/questions/719251/unable-to-find-an-entry-point-named-taskdialogindirect-in-dll-comctl32

            if (OverlayMessageBox.OverlayMessageBox.Show(settings) == OverlayMessageBoxButton.Cancel)
            {
                MessageBox.Show("Cancel selected!");
            }


        }
    }
}

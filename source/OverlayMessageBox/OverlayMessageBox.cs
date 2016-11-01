#region License
/*
Copyright (c) 2012 TeX HeX (http://www.texhex.info/)

Permission is hereby granted, free of charge, to any person obtaining a copy of 
this software and associated documentation files (the "Software"), to deal in the 
Software without restriction, including without limitation the rights to use, copy, 
modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
and to permit persons to whom the Software is furnished to do so, subject to the 
following conditions:

The above copyright notice and this permission notice shall be included in all 
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/
#endregion
using System;
using System.Text;
using System.Collections.Generic;


namespace OverlayMessageBox
{
    public enum OverlayMessageBoxSymbol
    {
        Question = 1,
        Information = 2,
        Warning = 4,
        Error = 8
    }

    public enum OverlayMessageBoxButtonSet
    {
        OK = 1,
        OKCancel = 2,
        RetryCancel = 4,
        YesNo = 8,
        YesNoCancel = 16
    }

    public enum OverlayMessageBoxButton
    {
        OK = 101,
        Cancel = 102,
        Retry = 103,
        Yes = 104,
        No = 105
    }

    internal sealed class EnumHelper
    {
        internal static string GetOverlayMessageBoxSymbolsText() {
            var items = Enum.GetNames(typeof(OverlayMessageBoxSymbol));
            return string.Join(", ", items);
        }

        internal static string GetOverlayMessageBoxButtonSetText()
        {
            var items = Enum.GetNames(typeof(OverlayMessageBoxButtonSet));
            return string.Join(", ", items);
        }

        internal static string GetOverlayMessageBoxButtonText()
        {
            Array list = Enum.GetValues(typeof(OverlayMessageBoxButton));
            
            List<string> text = new List<string>();
            foreach (OverlayMessageBoxButton val in list)
            {
                text.Add(
                          string.Format(
                                      "{1}: {0}", 
                                      (int)val, 
                                             Enum.GetName(typeof(OverlayMessageBoxButton), val)
                                       )
                        );
            }

            return string.Join(", ",text.ToArray());

        }

    }

    internal class WindowWrapper : System.Windows.Forms.IWin32Window
    {
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }


    public class OverlayMessageBoxSettings
    {
        private string _caption=AssemblyInformationHelper.TitleAndVersion;
        /// <summary>
        /// Title of the message
        /// </summary>
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        private string _text="";
        
        /// <summary>
        /// Message text
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private string _headline=String.Empty;

        /// <summary>
        /// Headline, displayed above text 
        /// </summary>
        public string Headline
        {
            get { return _headline; }
            set { _headline = value; }
        }
        

        private OverlayMessageBoxButtonSet _buttonSet=OverlayMessageBoxButtonSet.OK;
        /// <summary>
        /// Buttons to be used
        /// </summary>
        public OverlayMessageBoxButtonSet ButtonSet
        {
            get { return _buttonSet; }
            set { _buttonSet = value; }
        }


        private OverlayMessageBoxSymbol _symbol=OverlayMessageBoxSymbol.Information;

        /// <summary>
        /// Symbol of message to be shown
        /// </summary>
        public OverlayMessageBoxSymbol Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }
        
        
    }


    public sealed class OverlayMessageBox
    {
        private OverlayMessageBox()
        {
        }

        /// <summary>
        /// Show a message on a dimmed desktop, based on Settings object
        /// </summary>
        /// <param name="settings">The settings object</param>
        /// <returns>OverlayMessageBoxButton</returns>
        public static OverlayMessageBoxButton Show(OverlayMessageBoxSettings settings) 
        {
            System.OperatingSystem osInfo = System.Environment.OSVersion;
            OverlayMessageBoxButton retButton;
            
            using (OverlayForm overlayFrm = new OverlayForm())
            {
                overlayFrm.TopMost = true;

                overlayFrm.Show();                

                overlayFrm.Focus();
                overlayFrm.BringToFront();
                
                WindowWrapper overlayFrmHandle=new WindowWrapper(overlayFrm.Handle);
               
                if (osInfo.Version.Major >= 6)
                {
                    //we could use TaskDialog as we are running on Vista or better
                    if (settings.Headline.Length > 0)
                    {
                        retButton = TDialog.Show(settings, overlayFrmHandle);
                    }
                    else
                    {
                        //No headline = No task dialog
                        retButton = MBox.Show(settings, overlayFrmHandle);
                    }
                }
                else
                {
                    retButton = MBox.Show(settings, overlayFrmHandle);
                }                
                
                overlayFrm.Hide();
            }


            return retButton;
        }
    }
}

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
using System.Windows.Forms;

namespace OverlayMessageBox
{
    internal sealed class MBox
    {
        private MBox()
        {
        }


        internal static OverlayMessageBoxButton Show(OverlayMessageBoxSettings settings, IWin32Window owner)
        {
            MessageBoxButtons buttons;            
            switch (settings.ButtonSet)
            {
                case OverlayMessageBoxButtonSet.OKCancel:
                    buttons = MessageBoxButtons.OKCancel;
                    break;

                case OverlayMessageBoxButtonSet.RetryCancel:
                    buttons=MessageBoxButtons.RetryCancel;
                    break;

                case OverlayMessageBoxButtonSet.YesNo:
                    buttons=MessageBoxButtons.YesNo;
                    break;

                case OverlayMessageBoxButtonSet.YesNoCancel:
                    buttons = MessageBoxButtons.YesNoCancel;
                    break;

                default:
                    buttons = MessageBoxButtons.OK;
                    break;
            }

            MessageBoxIcon icon;

            switch (settings.Symbol) 
            {
                case OverlayMessageBoxSymbol.Error:
                    icon = MessageBoxIcon.Error;
                    break;

                case OverlayMessageBoxSymbol.Question:
                    icon = MessageBoxIcon.Question;
                    break;

                case OverlayMessageBoxSymbol.Warning:
                    icon = MessageBoxIcon.Warning;
                    break;

                default:
                    icon = MessageBoxIcon.Information;
                    break;            
            }


            //Finally show the messagebox!
            DialogResult res = MessageBox.Show(owner, settings.Text, settings.Caption, buttons, icon);

            
            OverlayMessageBoxButton buttonResult;
            switch (res)
            {
                case DialogResult.Cancel:
                    buttonResult = OverlayMessageBoxButton.Cancel;
                    break;

                case DialogResult.No:
                    buttonResult = OverlayMessageBoxButton.No;
                    break;

                case DialogResult.OK:
                    buttonResult = OverlayMessageBoxButton.OK;
                    break;

                case DialogResult.Retry:
                    buttonResult = OverlayMessageBoxButton.Retry;
                    break;

                case DialogResult.Yes:
                    buttonResult = OverlayMessageBoxButton.Yes;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("MessageBox result does not match expected value");
            }


            return buttonResult;
        }

    }
}

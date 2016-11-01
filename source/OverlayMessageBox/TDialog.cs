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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace OverlayMessageBox
{

    internal sealed class TDialog
    {
        [Flags]
        enum TaskDialogButtons
        {
            OK = 0x0001,
            Cancel = 0x0008,
            Yes = 0x0002,
            No = 0x0004,
            Retry = 0x0010,
            Close = 0x0020
        }

        enum TaskDialogIcon
        {
            Information = UInt16.MaxValue - 2,
            Warning = UInt16.MaxValue,
            Stop = UInt16.MaxValue - 1,
            Question = 0,
            SecurityWarning = UInt16.MaxValue - 5,
            SecurityError = UInt16.MaxValue - 6,
            SecuritySuccess = UInt16.MaxValue - 7,
            SecurityShield = UInt16.MaxValue - 3,
            SecurityShieldBlue = UInt16.MaxValue - 4,
            SecurityShieldGray = UInt16.MaxValue - 8
        }

        enum TaskDialogResult
        {
            None,
            OK,
            Cancel,
            Yes,
            No,
            Retry,
            Close
        }

        class TaskDialogHelper
        {
            [DllImport("comctl32.dll", CharSet = CharSet.Unicode, EntryPoint = "TaskDialog")]
            static extern int _TaskDialog(IntPtr hWndParent, IntPtr hInstance, String pszWindowTitle, String pszMainInstruction, String pszContent, int dwCommonButtons, IntPtr pszIcon, out int pnButton);

            private static TaskDialogResult ShowInternal(IntPtr owner, string text, string instruction, string caption, TaskDialogButtons buttons, TaskDialogIcon icon)
            {
                int p;
                if (_TaskDialog(owner, IntPtr.Zero, caption, instruction, text, (int)buttons, new IntPtr((int)icon), out p) != 0)
                    throw new InvalidOperationException("Windows API call TaskDialog has failed");

                switch (p)
                {
                    case 1: return TaskDialogResult.OK;
                    case 2: return TaskDialogResult.Cancel;
                    case 4: return TaskDialogResult.Retry;
                    case 6: return TaskDialogResult.Yes;
                    case 7: return TaskDialogResult.No;
                    case 8: return TaskDialogResult.Close;
                    default: return TaskDialogResult.None;
                }
            }


            internal static TaskDialogResult Show(IWin32Window owner, string text, string instruction, string caption, TaskDialogButtons buttons, TaskDialogIcon icon)
            {
                return ShowInternal(owner.Handle, text, instruction, caption, buttons, icon);
            }

            internal static TaskDialogResult Show(string text, string instruction, string caption, TaskDialogButtons buttons, TaskDialogIcon icon)
            {
                return ShowInternal(IntPtr.Zero, text, instruction, caption, buttons, icon);
            }


        }

        private TDialog()
        {
        }

        internal static OverlayMessageBoxButton Show(OverlayMessageBoxSettings settings, IWin32Window owner)
        {
            TaskDialogButtons buttons;
            switch (settings.ButtonSet)
            {
                case OverlayMessageBoxButtonSet.OKCancel:
                    buttons = TaskDialogButtons.OK | TaskDialogButtons.Cancel;
                    break;

                case OverlayMessageBoxButtonSet.RetryCancel:
                    buttons = TaskDialogButtons.Retry | TaskDialogButtons.Cancel;
                    break;

                case OverlayMessageBoxButtonSet.YesNo:
                    buttons = TaskDialogButtons.Yes | TaskDialogButtons.No;
                    break;

                case OverlayMessageBoxButtonSet.YesNoCancel:
                    buttons = TaskDialogButtons.Yes | TaskDialogButtons.No | TaskDialogButtons.Cancel;
                    break;

                default:
                    buttons = TaskDialogButtons.OK;
                    break;
            }

            TaskDialogIcon icon;

            switch (settings.Symbol)
            {
                case OverlayMessageBoxSymbol.Error:
                    icon = TaskDialogIcon.Stop;
                    break;

                case OverlayMessageBoxSymbol.Question:
                    icon = TaskDialogIcon.Question;
                    break;

                case OverlayMessageBoxSymbol.Warning:
                    icon = TaskDialogIcon.Warning;
                    break;

                default:
                    icon = TaskDialogIcon.Information;
                    break;
            }


            //Finally show the message
            TaskDialogResult res = TaskDialogHelper.Show(owner, settings.Text, settings.Headline, settings.Caption, buttons, icon);


            
            OverlayMessageBoxButton buttonResult;
            switch (res)
            {
                case TaskDialogResult.Cancel:
                    buttonResult = OverlayMessageBoxButton.Cancel;
                    break;

                case TaskDialogResult.No:
                    buttonResult = OverlayMessageBoxButton.No;
                    break;

                case TaskDialogResult.OK:
                    buttonResult = OverlayMessageBoxButton.OK;
                    break;

                case TaskDialogResult.Retry:
                    buttonResult = OverlayMessageBoxButton.Retry;
                    break;

                case TaskDialogResult.Yes:
                    buttonResult = OverlayMessageBoxButton.Yes;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("TaskDialog result does not match expected value");
            }


            return buttonResult;
        }

    }
}

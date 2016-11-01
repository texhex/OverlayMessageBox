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
using System.IO;
using System.Text;
using OverlayMessageBox;

namespace OverlayMessageBox
{
    public sealed class CommandLineExecutorResult
    {
        public bool Result = false;
        public string HelpText = String.Empty;
        public int ReturnCode = -9;
    }

    public sealed class CommandLineExecutor
    {
       
        public CommandLineExecutorResult Execute(string[] CommandLineArguments)
        {
            CommandLineExecutorResult ret = new CommandLineExecutorResult();

            ArgumentsParser argParser = new ArgumentsParser(CommandLineArguments);

            if (argParser.CanStart)
            {
                //Parsing OK, we can execute
                OverlayMessageBoxSettings settings = new OverlayMessageBoxSettings();
                settings.Caption = argParser.Caption;
                settings.Symbol = argParser.Symbol;
                settings.Text = FixLineBreaks(argParser.Text);
                settings.ButtonSet = argParser.ButtonSet;
                settings.Headline = argParser.Headline;

                OverlayMessageBoxButton retButton = OverlayMessageBox.Show(settings);

                ret.ReturnCode = (int)retButton;
                ret.Result = true;

            }
            else
            {
                //We don't need to do anything but the user has maybe given us data that we can not parse.
                //If ParserError contains something, show it. Else, only show the help screen
                ret.HelpText = "";
                if (argParser.ParserError != "")
                {
                    ret.HelpText = "\r\nError: " + argParser.ParserError + "\r\n";
                }
                ret.HelpText += argParser.HelpText;
            }

            return ret;
        }

        private string FixLineBreaks(string input)
        {
            //Replace CR/LF
            string temp = input.Replace("\\r\\n", "\r\n");
            //replace "lonely" \r or \n
            return temp.Replace("\\n", "\r\n").Replace("\\r", "\r\n");
        }


    }
}

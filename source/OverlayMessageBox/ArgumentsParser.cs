using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMDLine;

namespace OverlayMessageBox
{
    internal class ArgumentsParser
    {

        private ArgumentsParser()
        {
        }

        internal ArgumentsParser(string[] CommandlineArguments)
        {
            CMDLineParser parser = new CMDLineParser();
            parser.throwInvalidOptionsException = true;

            CMDLineParser.Option TextParameter = parser.AddStringParameter("-Text", "Text to display in the message (use \\n for line breaks)", false); //can't make it required or the help option does not work

            CMDLineParser.Option CaptionParameter = parser.AddStringParameter("-Caption", "Text to display in title of message", false);

            CMDLineParser.Option HeadlineParameter = parser.AddStringParameter("-Headline", "Text to display in the headline above the text (when set, a Task Dialog is used)", false);

            SymbolOption SymbolParameter = new SymbolOption("-Symbol", "Which <Symbol> (Question, Information, Warning, Error) to display (default: Information)", false);
            parser.AddOption(SymbolParameter);
            //SymbolParameter.AddAlias("--Symbol");
            //SymbolParameter.AddAlias("--symbol");

            ButtonSetOption ButtonSetParameter = new ButtonSetOption("-Buttonset", "Which <ButtonSet> (OK, OKCancel, RetryCancel, YesNo, YesNoCancel) to display (default: OK)", false);
            parser.AddOption(ButtonSetParameter);
            //ButtonSetParameter.AddAlias("--ButtonSet");
            //ButtonSetParameter.AddAlias("--Buttonset");

            CMDLineParser.Option HelpOption = parser.AddBoolSwitch("-?", "Displays help");
            //HelpOption.AddAlias("/?");

            //Add help text     
            StringBuilder help = new StringBuilder();
            help.AppendLine(parser.HelpMessage().TrimEnd('\n'));

            help.AppendLine("");
            help.AppendLine("Options:");
            help.AppendLine("");
            help.AppendLine("  <Symbol>: " + EnumHelper.GetOverlayMessageBoxSymbolsText());
            help.AppendLine("  <ButtonSet>: " + EnumHelper.GetOverlayMessageBoxButtonSetText());
            help.AppendLine("");
            help.AppendLine("  The return code contains the value of the button the user has selected:");
            help.AppendLine("  " + EnumHelper.GetOverlayMessageBoxButtonText());
            help.AppendLine("");
            help.AppendLine("Examples:");
            help.AppendLine("");
            help.AppendLine("  OverlayMessageBox -Text \"My text\"");
            help.AppendLine("  OverlayMessageBox -Caption \"Title of message\" -Text \"My text\" -Symbol Warning");
            help.AppendLine("  OverlayMessageBox -Caption \"Title of message\" -Text \"My text\" -Headline \"The headline\" -Symbol Error -Buttonset RetryCancel");
            this.HelpText = help.ToString();

            try
            {
                //set default options
                this.CanStart = false;
                this.ParserError = "";

                parser.Parse(CommandlineArguments);

                //OK, no parser errors. Now check if the user has requested the help option

                if (HelpOption.isMatched)
                {
                    //Only show the help screen, nothing to start
                    this.CanStart = false;
                }
                else
                {
                    //Now check if we have a Text parameter. If not, we can't start
                    if (OptionIsMatchedAndNotEmpty(TextParameter))
                    {

                        this.Caption = OptionIsMatchedAndNotEmpty(CaptionParameter) ? CaptionParameter.Value.ToString() : AssemblyInformationHelper.TitleAndVersion;
                        this.Headline = OptionIsMatchedAndNotEmpty(HeadlineParameter) ? HeadlineParameter.Value.ToString() : "";
                        this.Text = OptionIsMatchedAndNotEmpty(TextParameter) ? TextParameter.Value.ToString() : "NO TEXT!";

                        this.Symbol = OptionIsMatchedAndNotEmpty(SymbolParameter) ? (OverlayMessageBoxSymbol)SymbolParameter.Value : OverlayMessageBoxSymbol.Information;
                        this.ButtonSet = OptionIsMatchedAndNotEmpty(ButtonSetParameter) ? (OverlayMessageBoxButtonSet)ButtonSetParameter.Value : OverlayMessageBoxButtonSet.OK;

                        this.CanStart = true;
                    }
                }

            }
            catch (CMDLineParser.CMDLineParserException ex)
            {
                this.CanStart = false;
                this.ParserError = ex.Message;
            }
        }

        /// <summary>
        /// Return TRUE if an option is both matched and is not null or empty
        /// </summary>
        /// <param name="Option">Option to check</param>
        /// <returns>TRUE if an option is both matched and is not null or empty</returns>
        private bool OptionIsMatchedAndNotEmpty(CMDLineParser.Option Option)
        {
            bool returnvalue = false;

            if (Option.isMatched)
            {
                if (Option.Value != null)
                {
                    if (string.IsNullOrWhiteSpace(Option.Value.ToString()) == false)
                    {
                        returnvalue = true;
                    }
                }
            }

            return returnvalue;
        }

        /// <summary>
        /// True if we have something to execute
        /// </summary>
        public bool CanStart { get; private set; }

        /// <summary>
        /// Contains an error message if the arguments couldn't be parsed
        /// </summary>
        public string ParserError { get; private set; }

        /// <summary>
        /// Help text
        /// </summary>
        public string HelpText { get; private set; }

        /// <summary>
        /// Caption, title on the box
        /// </summary>
        public string Caption { get; private set; }

        /// <summary>
        /// Headline, bigger text before the TEXT
        /// </summary>
        public string Headline { get; private set; }

        /// <summary>
        /// Main text
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Symbol to be used
        /// </summary>
        public OverlayMessageBoxSymbol Symbol { get; private set; }

        /// <summary>
        /// ButtonSet to be used
        /// </summary>
        public OverlayMessageBoxButtonSet ButtonSet { get; private set; }


    }

    //Custom implementation for the Symbol option
    class SymbolOption : CMDLineParser.Option
    {
        //constuctor
        public SymbolOption(string name, string description, bool required)
            : base(name, description, typeof(string), true, required) { }


        //implementation of parseValue
        public override object parseValue(string Parameter)
        {
            OverlayMessageBoxSymbol symbol = OverlayMessageBoxSymbol.Information;

            try
            {
                symbol = (OverlayMessageBoxSymbol)Enum.Parse(typeof(OverlayMessageBoxSymbol), Parameter);
            }
            catch
            {
                throw new System.ArgumentException(string.Format("Symbol [{0}] is not valid", Parameter));
            }

            return symbol;
        }
    }

    //Custom implementation for the Button option
    class ButtonSetOption : CMDLineParser.Option
    {
        //constuctor
        public ButtonSetOption(string name, string description, bool required)
            : base(name, description, typeof(string), true, required) { }


        //implementation of parseValue
        public override object parseValue(string Parameter)
        {
            OverlayMessageBoxButtonSet buttonSet = OverlayMessageBoxButtonSet.OK;

            try
            {
                buttonSet = (OverlayMessageBoxButtonSet)Enum.Parse(typeof(OverlayMessageBoxButtonSet), Parameter);
            }
            catch
            {
                throw new System.ArgumentException(string.Format("ButtonSet [{0}] is not valid", Parameter));
            }

            return buttonSet;
        }
    }
}

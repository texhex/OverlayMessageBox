using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayMessageBox
{
    public sealed class ProgramBanner
    {
        public static string TitleAndVersion
        {
            get
            {
                return AssemblyInformationHelper.TitleAndVersion;
            }
        }
        public static string FullBanner
        {
            get
            {
                return AssemblyInformationHelper.FullBanner;
            }
        }
    }
}

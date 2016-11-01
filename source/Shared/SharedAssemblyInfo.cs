using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

//SharedAssemblyInfo.cs taken from
//[Sharing assembly version across projects in a solution](http://weblogs.asp.net/ashishnjain/sharing-assembly-version-across-projects-in-a-solution) by Ashish Jain


[assembly: AssemblyProduct(AssemblyInformationHelper.Title)] //Explorer: Product name
[assembly: AssemblyCompany(AssemblyInformationHelper.Title)]
[assembly: AssemblyTitle(AssemblyInformationHelper.Homepage)] //Explorer: File description

[assembly: AssemblyFileVersion(AssemblyInformationHelper.Version)] //Explorer: File version
[assembly: AssemblyVersion("2.0")] //Explorer: Product version

[assembly: AssemblyCopyright(AssemblyInformationHelper.Copyright)]

[assembly: AssemblyDescription("For more information, please visit https://github.com/texhex/OverlayMessageBox")] //It's actually a comment: $File.VersionInfo.Comments

[assembly: AssemblyTrademark("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCulture("")]


// Setting ComVisible to false makes the types in this assembly not visible
// to COM components. If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]


static class AssemblyInformationHelper
{
    internal const string Title = "Overlay Message Box";
    internal const string Version = "2.1.0";

    internal const string Homepage = "https://github.com/texhex/OverlayMessageBox";
    internal const string Copyright = "Copyright (C) 2012-2016 Michael Hex";
    internal const string License = "Licensed under the Apache License, Version 2.0";

    internal const string TitleAndVersion = Title + " " + Version;
    internal const string TitleVersionAndHomepage = Title + " " + Version + "\r\n" + Homepage;

    internal const string FullBanner = TitleAndVersion + "\r\n" + Copyright + "\r\n" + Homepage + "\r\n" + License;
}
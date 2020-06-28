using Android.App;
using System.Reflection;
using System.Runtime.InteropServices;
using Xamarin.Forms;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("SimTuning")]
[assembly: AssemblyDescription("Ein Tool, um Simson-Begeisterte etwas unter die Arme zu greifen und zu helfen.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("tuke-productions")]
[assembly: AssemblyProduct("SimTuning")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Add some common permissions, these can be removed if not needed
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
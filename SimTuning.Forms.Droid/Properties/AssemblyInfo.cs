﻿// project=SimTuning.Forms.Droid, file=AssemblyInfo.cs, creation=2020:6:30 Copyright (c)
// 2021 tuke productions. All rights reserved.

// General Information about an assembly is controlled through the following set of
// attributes. Change these attribute values to modify the information associated with an
// assembly.
using Android.App;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyDescription("Ein Tool, um Simson-Begeisterte etwas unter die Arme zu greifen.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("tuke-productions")]
[assembly: AssemblyProduct("SimTuning")]
[assembly: AssemblyCopyright("Copyright © 2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
// Major Version Minor Version Build Number Revision
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Add some common permissions, these can be removed if not needed
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
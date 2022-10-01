Append XRefs To Ass
=======================
Based off this [3ds Max Menu Sample .NET API plug-in](https://github.com/ADN-DevTech/3dsMax-Menu-Sample)

This plugin just writes the xrefs to the selected .ASS file. Everything has to be in the correct folder path to get the path to the data folder for the xrefs.

If you compile this plugin for another version of max, please send it to me so I can add it to the release

Getting Started
============
The plugin source code and project was built and tested against 3ds Max 2015. In the project file you will find 
environment variable $(ADSK_3DSMAX_SDK_2015) referring to the 3ds Max .NET assemblies (edit the csproj file). You can change this to match 
the version of 3ds Max you are working with. You will also have to adjust the .NET Framework version to match the 3ds Max version.

Debugging
============
To setup debugging, you will need to go to the properties page for the project and go to "debug". Choose "Start external program" and put the path to 3ds max in text box. Set the text box for the "Working Directory" to be the directory that the 3ds max exe is in (make sure it is not a shortcut).

Go to Tools > Options > Debugging Symbols and add http://symbols.autodesk.com/symbols to the list of symbol file locations, and make sure it is selected (checked).

Now when you start the project in debug mode, your breakpoints should catch properly.  

Additional Information
=================
This plug-ins framework was written by Kevin Vandecar - Autodesk Developer Network.  
The plugin iteself is based off of the append xrefs to ass max script Inferno gave me.  

Version
=======
1.0 - Initial Release

License
=======
MIT License.

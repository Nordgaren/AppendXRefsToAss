Append XRefs To Ass
=======================
Based off this [3ds Max Menu Sample .NET API plug-in](https://github.com/ADN-DevTech/3dsMax-Menu-Sample)

This plugin just writes the xrefs to the selected .ASS file. Everything has to be in the correct folder path to get the path to the data folder for the xrefs.

If you compile this plugin for another version of max, please send it to me so I can add it to the release

Getting Started
============
The plugin source code and project was built and tested against 3ds Max 2015. In the project file you will find 
environment variable $(ADSK_3DSMAX_SDK_2015) referring to the 3ds Max .NET assemblies. You an change this to match 
the version of 3ds Max you are working with. You will also have to adjust the .NET Framework version to match the 3ds Max version.

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

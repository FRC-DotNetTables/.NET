DotNetTables
============

DotNetTables is an extention of the NetworkTables interface provided by FRC:
	http://firstforge.wpi.edu/sf/projects/network_tables_2_0

The goal of this project is two-fold:

1. To provide an easy-to-integrate .NET conversion of NetworkTables, for use in non-Java driver station interfaces
2. To provide enforced directionality and other wrapper functionality to more strictly manage the use of NetworkTables

While our team prefers the functionality of the DotNetTables API, this project is intended to provide complete access to the underlying NetworkTables implementation, if you prefer its API. Both the Java and .NET projects expose all classes, methods, and data types provided by the NetworkTables API.

The DotNetTables API is documented here:
	http://frc-dotnettables.github.io/Java/

The corresponding Java project is available here:
	https://github.com/FRC-DotNetTables/Java

If you have questions feel free to mail:
	zach@kotlarek.com

.NET Implementation
-------------------

In the [dist](dist/) folder you'll find [DotNetTables.dll](dist/DotNetTables.dll), [networktables-desktop.dll](dist/networktables.dll), and [ApplicationEvents.vb](dist/ApplicationEvents.vb).

DotNetTables will need to be added as a reference and distributed with your project. 

networktables-desktop.dll is a .NET conversion of java networktables. It should also be added as a reference to your project but doesn't need to be distributed with the project.	

Other necessary assemblies are embedded in DotNetTables. A handler will need to be added to ApplicationEvents to properly handle the AssemblyResolve event.

http://msdn.microsoft.com/en-us/library/system.appdomain.assemblyresolve(v=vs.110).aspx 

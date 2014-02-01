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

In the [Dist](Dist/) folder you'll find [DotNetTables.dll](Dist/DotNetTables.dll), [networktables-desktop.dll](Dist/networktables.dll), and [ApplicationEvents.vb](Dist/ApplicationEvents.vb).

DotNetTables.dll contains all of the code from this project. It must be referenced in your project at build time and distributed with any resulting exectuables.

networktables-desktop.dll is a .NET conversion of the FRC's Java-based NetworkTables implementation. You may use it indepently, but be aware that the converted code relies on a number of IKVM DLLs, so even if you do not intend to use DotNetTables you may wish to take advantage of the embedded assembly loading it provides.

DotNetTables.dll depends on networktables-desktop.dll at build time, but DotNetTables.dll already embeds a copy of networktables-desktop.dll for use at runtime, so it is not necessary to distribute networktables-desktop.dll with executables built against DotNetTables.dll.

Because DotNetTables embeds other assemblies your project must include an ApplicationEvents handler that facilitates loading from the embedded resources. Specifically you'll need to handle the AssemblyResolve event, which is documented here: http://msdn.microsoft.com/en-us/library/system.appdomain.assemblyresolve(v=vs.110).aspx

It is important to register this event early in the loading process, before you reference DotNetTables or any of its dependencies. An easy way to do this is to register the AssemblyResolve handler during the Me.Startup event, which typically dispatches the AppStart method. An example of this registration is provided in [ApplicationEvents.vb](Dist/ApplicationEvents.vb).

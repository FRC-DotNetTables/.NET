Imports System.Text.RegularExpressions

Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication


        Private Sub AppStart(ByVal sender As Object,
            ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup

            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf ResolveAssemblies

        End Sub

        Public Shared Function ResolveAssemblies(sender As Object, e As System.ResolveEventArgs) As Reflection.Assembly
            Dim desiredAssembly = New Reflection.AssemblyName(e.Name)

            'Convert the requested assembly name to the embedded assembly name
            Dim AssemblyName As String = desiredAssembly.Name
            Dim rgx As New Regex("\W")
            AssemblyName = rgx.Replace(AssemblyName, "_")
            MsgBox(AssemblyName)

            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf DotNetTables.DotNetTables.ResolveAssemblies

            Return Reflection.Assembly.Load(My.Resources.ResourceManager.GetObject(AssemblyName))

        End Function
    End Class


End Namespace


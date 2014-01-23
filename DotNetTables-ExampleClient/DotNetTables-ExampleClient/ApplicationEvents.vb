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

        Private Function ResolveAssemblies(sender As Object, e As System.ResolveEventArgs) As Reflection.Assembly
            Dim desiredAssembly = New Reflection.AssemblyName(e.Name)

            Select Case desiredAssembly.Name
                Case "DotNetTables"
                    Return Reflection.Assembly.Load(My.Resources.DotNetTables)
                Case "IKVM.OpenJDK.Core"
                    Return Reflection.Assembly.Load(My.Resources.IKVM_OpenJDK_Core)
                Case "IKVM.OpenJDK.Util"
                    Return Reflection.Assembly.Load(My.Resources.IKVM_OpenJDK_Util)
                Case "IKVM.Runtime"
                    Return Reflection.Assembly.Load(My.Resources.IKVM_Runtime)
                Case "networktables-desktop"
                    Return Reflection.Assembly.Load(My.Resources.networktables_desktop)
                Case Else
                    Return Nothing
            End Select

        End Function
    End Class


End Namespace


Namespace My
    Partial Friend Class MyApplication
        Private Sub AppStart(ByVal sender As Object,
            ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf DotNetTables.DotNetTables.ResolveAssemblies
        End Sub
    End Class
End Namespace

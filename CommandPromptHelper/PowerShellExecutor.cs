using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace CommandPromptHelper
{
    public class PowerShellExecutor : IDisposable
    {
        public static PowerShellExecutor Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = new PowerShellExecutor();
                }
                return _singleton;
            }
        }
        private static PowerShellExecutor _singleton;

        private static InitialSessionState _iss;
        private static Runspace _runspace;

        private PowerShellExecutor()
        {
            _runspace?.Dispose();

            _iss = InitialSessionState.CreateDefault();
            _runspace = RunspaceFactory.CreateRunspace(_iss);
            _runspace.Open();
        }

        public async Task<PSDataCollection<PSObject>> ExecutePowerShellAsync(string[] scripts)
        {
            Trace.WriteLine("Run powerShell with scripts:");

            foreach (var script in scripts)
            {
                Trace.WriteLine($"   {script}");
            }

            PSDataCollection<PSObject> results;
            using (var ps = .Create())
            {
                ps.Runspace = _runspace;
                foreach (var script in scripts)
                {
                    ps.AddScript(script);
                }
                results = await ps.InvokeAsync();
            }

            foreach (var psObject in results)
            {
                Trace.WriteLine(psObject.BaseObject.ToString());
            }
            return results;
        }

        public async Task<PSDataCollection<PSObject>> CreateSymbolicLinkAsync(string linkDir, string realDir)
        {
            var scripts = new string[]
            {
                $"New-Item -Path {linkDir} -ItemType SymbolicLink -Value {realDir}"
            };

            return await ExecutePowerShellAsync(scripts);
        }

        public void Dispose()
        {
            _runspace?.Dispose();
        }
    }
}

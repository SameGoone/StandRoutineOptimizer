using System.Collections.ObjectModel;
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

        public Collection<PSObject> ExecutePowerShell(string[] scripts)
        {
            System.Diagnostics.Debug.WriteLine("Run powerShell with scripts:");

            foreach (var script in scripts)
            {
                System.Diagnostics.Debug.WriteLine($"   {script}");
            }

            Collection<PSObject> results;
            using (var ps = PowerShell.Create())
            {
                ps.Runspace = _runspace;
                foreach (var script in scripts)
                {
                    ps.AddScript(script);
                }
                results = ps.Invoke();
            }

            foreach (var psObject in results)
            {
                System.Diagnostics.Debug.WriteLine(psObject.BaseObject.ToString());
            }
            return results;
        }

        public Collection<PSObject> CreateSymbolicLink(string linkDir, string realDir)
        {
            var scripts = new string[]
            {
                $"New-Item -Path {linkDir} -ItemType SymbolicLink -Value {realDir}"
            };

            return ExecutePowerShell(scripts);
        }

        public void Dispose()
        {
            _runspace?.Dispose();
        }
    }
}

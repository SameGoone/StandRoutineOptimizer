namespace CommandPromptHelper
{
    public class GitCommandExecutor
    {
        public static void ExecuteClone(string destinationPath, string repoUrl)
        {
            var scripts = new string[]
            {
                $"cd {destinationPath}",
                $"git clone {repoUrl}"
            };

            PowerShellExecutor.Singleton.ExecutePowerShell(scripts);
        }

        public static void ExecutePull(string repositoryPath)
        {
            var scripts = new string[]
            {
                $"cd {repositoryPath}",
                $"git pull -q"
            };

            PowerShellExecutor.Singleton.ExecutePowerShell(scripts);
        }

        public static void ExecuteCheckout(string repositoryPath, string branchName)
        {
            var scripts = new string[]
            {
                $"cd {repositoryPath}",
                $"git checkout {branchName}"
            };

            PowerShellExecutor.Singleton.ExecutePowerShell(scripts);
        }

        public static string GetCurrentBranchName(string repositoryPath)
        {
            var scripts = new string[]
            {
                $"cd {repositoryPath}",
                $"git rev-parse --abbrev-ref HEAD"
            };

            return PowerShellExecutor.Singleton.ExecutePowerShell(scripts)[0].BaseObject.ToString();
        }
    }
}

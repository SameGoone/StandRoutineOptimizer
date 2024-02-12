namespace CommandPromptHelper
{
    public class GitCommandExecutor
    {
        public static async void ExecuteCloneAsync(string destinationPath, string repoUrl)
        {
            var scripts = new string[]
            {
                $"cd {destinationPath}",
                $"git clone {repoUrl}"
            };

            await PowerShellExecutor.Singleton.ExecutePowerShellAsync(scripts);
        }

        public static async void ExecutePullAsync(string repositoryPath)
        {
            var scripts = new string[]
            {
                $"cd {repositoryPath}",
                $"git pull -q"
            };

            await PowerShellExecutor.Singleton.ExecutePowerShellAsync(scripts);
        }

        public static async void ExecuteCheckoutAsync(string repositoryPath, string branchName)
        {
            var scripts = new string[]
            {
                $"cd {repositoryPath}",
                $"git checkout {branchName}"
            };

            await PowerShellExecutor.Singleton.ExecutePowerShellAsync(scripts);
        }

        public static async Task<string> GetCurrentBranchNameAsync(string repositoryPath)
        {
            var scripts = new string[]
            {
                $"cd {repositoryPath}",
                $"git rev-parse --abbrev-ref HEAD"
            };

            return (await PowerShellExecutor.Singleton.ExecutePowerShellAsync(scripts))[0].BaseObject.ToString();
        }
    }
}

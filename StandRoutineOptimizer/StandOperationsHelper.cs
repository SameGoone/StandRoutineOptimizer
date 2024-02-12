using CommandPromptHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CloneRepoHelper
{
    internal class StandOperationsHelper
    {
        private DirectoryInfo _rootFolder;
        private DirectoryInfo _webAppFolder;
        private DirectoryInfo _configurationFolder;
        private DirectoryInfo _pkgFolder;
        private Panel _mainPanel;

        private const string _webAppPostfix = @"\BPMSoft.WebApp";
        private const string _configurationPostfix = @"\BPMSoft.Configuration";
        private const string _pkgPostfix = @"\Pkg";

        public StandOperationsHelper(string standRootPath)
        {
            _rootFolder = new DirectoryInfo(standRootPath);
            _webAppFolder = new DirectoryInfo(_rootFolder.FullName + _webAppPostfix);
            _configurationFolder = new DirectoryInfo(_webAppFolder.FullName + _configurationPostfix);
            _pkgFolder = new DirectoryInfo(_configurationFolder.FullName + _pkgPostfix);
        }

        public void CloneRepos(List<Repository> repositories)
        {
            foreach (Repository repository in repositories)
            {
                repository.Clone(_webAppFolder.FullName);
            }
        }

        public async void CreateSymLinksAsync()
        {
            var directories = _webAppFolder.GetDirectories();

            var repositories = directories
                .Where(d => d.Name.StartsWith("Srm") || d.Name.StartsWith("SRM"))
                .Select(d => new DirectoryInfo(Path.Combine(d.FullName, "Pkg", d.Name)))
                .ToList();

            foreach (var repo in repositories)
            {
                var realDir = repo.FullName;
                var linkDir = Path.Combine(_pkgFolder.FullName, repo.Name);
                _ = await PowerShellExecutor.Singleton.CreateSymbolicLinkAsync(linkDir, realDir);
            }
        }
    }
}

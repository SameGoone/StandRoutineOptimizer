using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CloneRepoHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int _rowHeight = 35;
        const int _leftColumnWidth = 200;
        const int _rightColumnWidth = 100;

        string _sizesFilePath;

        Sizes _sizes = new Sizes(
            _rowHeight,
            _leftColumnWidth,
            _rightColumnWidth);

        List<Repository> _repositories;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            RepoPanel.Children.Clear();

            XElement repositoriesElem = XElement.Load(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Repositories.xml"));
            _repositories = repositoriesElem
                .Elements("repository")
                .Select(r => new Repository(
                    r.Attribute("name").Value,
                    r.Attribute("uri").Value,
                    RepoPanel,
                    _sizes))
                .ToList();
        }

        private void CloneButton_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(StandPathField.Text))
            {
                Clone();
            }
        }

        private void Clone()
        {
            StandOperationsHelper cloneStandsRepoHelper = new StandOperationsHelper(StandPathField.Text);
            cloneStandsRepoHelper.CloneRepos(_repositories);
        }

        private void ChooseStandButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    StandPathField.Text = dialog.SelectedPath;
                }
            }
        }

        private void CreateSymLinksButton_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(StandPathField.Text))
            {
                CreateSymLinks();
            }
        }

        private void CreateSymLinks()
        {
            StandOperationsHelper cloneStandsRepoHelper = new StandOperationsHelper(StandPathField.Text);
            cloneStandsRepoHelper.CreateSymLinks();
        }
        
    }
}

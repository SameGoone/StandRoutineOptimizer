using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using CommandPromptHelper;
using CheckBox = System.Windows.Controls.CheckBox;
using Label = System.Windows.Controls.Label;
using Panel = System.Windows.Controls.Panel;

namespace CloneRepoHelper
{
    class Repository
    {

        public bool DoClone
        {
            get
            {
                return _doCloneCheckBox.IsChecked ?? false;
            }
        }

        Panel _parentPanel { get; set; }
        Grid _grid { get; set; }
        CheckBox _doCloneCheckBox { get; set; }
        Label _nameLabel { get; set; }
        Sizes _sizes { get; set; }

        public string Name { get; set; }
        public string CloneUri { get; set; }

        public Repository(string name, string cloneUri, Panel parentPanel, Sizes sizes)
        {
            Name = name;
            CloneUri = cloneUri;
            _parentPanel = parentPanel;
            _sizes = sizes;
            Render();
        }

        private void Render()
        {
            _doCloneCheckBox = new CheckBox();
            _nameLabel = new Label();
            _nameLabel.Content = Name;

            _grid = new Grid();
            _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(_sizes.LeftColumnWidth) });
            _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(_sizes.RightColumnWidth) });
            _grid.RowDefinitions.Add(new RowDefinition {  Height = new GridLength(_sizes.RowHeight) });

            Grid.SetColumn(_nameLabel, 0);
            Grid.SetColumn(_doCloneCheckBox, 1);
            Grid.SetRow(_nameLabel, 0);
            Grid.SetRow(_doCloneCheckBox, 0);

            _grid.Children.Add(_nameLabel);
            _grid.Children.Add(_doCloneCheckBox);

            _parentPanel.Children.Add(_grid);
        }

        public void Clone(string destinationPath)
        {
            if (DoClone)
            {
                GitCommandExecutor.ExecuteCloneAsync(destinationPath, CloneUri);
            }
        }
    }
}

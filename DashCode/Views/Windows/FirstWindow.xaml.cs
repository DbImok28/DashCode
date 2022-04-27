using DashCode.Models;
using DashCode.View.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DashCode.Views.Windows
{
    /// <summary>
    /// Interaction logic for FirstWindow.xaml
    /// </summary>
    public partial class FirstWindow : Window
    {
        public FirstWindow()
        {
            InitializeComponent();
        }

        public void OpenFolder(ProjectFolder folder)
        {
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
            Close();
        }

        private void ListBox_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ListBox list && list.SelectedItem is ProjectFolder folder)
            {
                OpenFolder(folder);
            }
        }

        private void Open_Folder_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ProjectFolder folder = new ProjectFolder(dialog.SelectedPath);
                    App.VMService.FirstVM.Files.Add(folder);
                    OpenFolder(folder);
                }
            }
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            var loginStr = login.Text;
            var passStr = password.Text;
        }
    }
}

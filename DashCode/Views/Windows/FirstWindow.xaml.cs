﻿using DashCode.Models;
using DashCode.View.Windows;
using DashCode.Views.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            frame.Content = App.AuthenticateService.CurrentPage;
            App.AuthenticateService.OnUpdatePage += (s, a) => frame.Content = App.AuthenticateService.CurrentPage;
        }
        public void OpenFolder(ProjectFolder folder)
        {
            if (Application.Current.MainWindow == this)
            {
                Application.Current.MainWindow = new MainWindow();
                Application.Current.MainWindow.Show();
                Close();
                App.VMService.MainVM.OpenFolderCommand.Execute(folder);
            }
        }

        private void ListBox_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ListBox list && list.SelectedItem is ProjectFolder folder)
            {
                //OpenFolder(folder);
            }
        }

        private void Open_Folder_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ProjectFolder folder = new ProjectFolder(dialog.SelectedPath);
                    App.VMService.FirstVM.AddFolderCommand.Execute(folder);
                    OpenFolder(folder);
                }
            }
        }

        private void ProjectFolder_Filter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is ProjectFolder folder) || folder is null) return;
            var filterText = FilterBox.Text;
            if (folder.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase)) return;
            e.Accepted = false;
        }

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var box = sender as TextBox;
            var collection = box.FindResource("ProjectFolderSource") as CollectionViewSource;
            collection.View.Refresh();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedFolder = (sender as ListBox).SelectedItem as ProjectFolder;
        }
        public ProjectFolder SelectedFolder { get; set; }

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFolder(SelectedFolder);
        }
    }
}

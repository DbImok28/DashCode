using DashCode.Models;
using DashCode.Views.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DashCode.View.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MessageFrame.NavigationService.Navigate(new Uri("/Views/Pages/MessageMainPage.xaml", UriKind.Relative));
            App.VMService.ChatVM.Update();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            textEditor.ReadAndUpdate();
            App.VMService.MainVM.SaveFileCommand.Execute(null);
        }

        private void FolderTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is TreeView tree && tree.SelectedItem is ProjectFile file)
            {
                App.VMService.MainVM.TrySelectNewFile(file);
                textEditor.Update();
            }
        }

        private void OpenFirstWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow == this)
            {
                Application.Current.MainWindow = new FirstWindow();
                Application.Current.MainWindow.Show();
                Close();
            }
        }

        private void Button_UpdateClick(object sender, RoutedEventArgs e)
        {
            textEditor.ReadAndUpdate();
        }

        private void Title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                App.RestoreWindow();
            }
            else
            {
                App.DragWindow();
            }
        }

        private void Minimize_Button_Click(object sender, RoutedEventArgs e)
        {
            App.MinimizeWindow();
        }

        private void Restore_Button_Click(object sender, RoutedEventArgs e)
        {
            App.RestoreWindow();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            App.CloseWindow();
        }
    }
}

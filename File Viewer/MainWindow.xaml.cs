using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace File_Viewer
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            SetPropertyItem.Visibility = Visibility.Hidden;
        }

        private string Files;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string OpenedFiles
        {
            get { return Files; }
            set 
            { 
                Files = value;
                OnPropertyChanged();
            }
        }

        private void Addfilesbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Please pick file(s)";
            fileDialog.Multiselect = false;

            bool? success = fileDialog.ShowDialog();
            if (success == true)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Content = fileDialog.FileName;

                FileHolder.Items.Add(listViewItem);
            }
            else
            {
                MessageBox.Show("Please select file(s)", "Error!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
        }

        // now insted of typeing all of this PropertyChanged?.Invoke(Files, new PropertyChangedEventArgs("OpenedFiles"));
        // we can now just add a lot of those in there and just use OnPropertyChanged(); then having to do whats above this line
        private void OnPropertyChanged([CallerMemberName] string Propertyname = null)
        {
            PropertyChanged?.Invoke(Files, new PropertyChangedEventArgs("OpenedFiles"));
        }

        private void RSI_Click(object sender, RoutedEventArgs e)
        {
            int index = FileHolder.SelectedIndex;
            FileHolder.Items.RemoveAt(index);
        }
    }
}
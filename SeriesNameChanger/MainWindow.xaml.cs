using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Shapes;

namespace MovieNameChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Classes.MiddleClass middleClass = new Classes.MiddleClass();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseFilesBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] files = Directory.GetFiles(FBD.SelectedPath);
                
                middleClass.SeriesSearch(SeriesSearchInput.Text, SeriesSeasonInput.Text, files, System.IO.Path.GetFileName(files[0]));

            }
        }

        private void PlaceHolder(object sender, RoutedEventArgs e)
        {
            if (SeriesSearchInput.Text == "Search for a series to change name on")
            {
                SeriesSearchInput.Text = "";
            }
        }

        private void PlaceHolderAdd(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SeriesSearchInput.Text))
            {
                SeriesSearchInput.Text = "Search for a series to change name on";
            }
        }

        private void SeasonPlaceholder(object sender, RoutedEventArgs e)
        {
            if (SeriesSeasonInput.Text == "Season of that seires")
            {
                SeriesSeasonInput.Text = "";
            }
        }

        private void SeasonPlaceholderAdd(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SeriesSeasonInput.Text))
            {
                SeriesSeasonInput.Text = "Season of that series";
            }
        }
    }
}

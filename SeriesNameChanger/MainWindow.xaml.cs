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
        List<string> listOfSeries = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        public List<string> ListOfSeries { get => listOfSeries; set => listOfSeries = value; }

        private void ChooseFilesBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] files = Directory.GetFiles(FBD.SelectedPath);
                string[] dirs = Directory.GetDirectories(FBD.SelectedPath);
                
                middleClass.SeriesSearch(SeriesSearchInput.Text, SeriesSeasonInput.Text, files, System.IO.Path.GetFileName(files[0]));

            }
        }

        private void SeriesSearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ListOfSeries = middleClass.SeriesPickOptions(SeriesSeasonInput.Text);

            }
            catch(Exception err)
            {
                Console.WriteLine(err);
            }
            foreach (string series in ListOfSeries)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = series;
                seriesSelector.Items.Add(comboBoxItem);
            }
        }
    }
}


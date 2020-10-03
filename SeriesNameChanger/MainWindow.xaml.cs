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
using DataFormats = System.Windows.Forms.DataFormats;
using SeriesNameChanger;
using System.Text.RegularExpressions;

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
            combBoxApi.Items.Add("Series");
            combBoxApi.Items.Add("Movie");
        }

        private void dropStackPanel_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if(combBoxApi.Text == "Series")
                {
                    if (dropedFilesTextBlock.Text != "")
                    {
                        dropedFilesTextBlock.Text = "";
                    }
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    foreach (string file in files)
                    {
                        string renamedfile = file.Replace(@"\", "/");
                        dropedFilesTextBlock.Text += $"{renamedfile}\n";
                    }
                    if (seriesChoice.Text != "" && seriesSeasonPick.Text != "")
                    {
                        int index = GetNumberOfFiles();
                        for (int i = 0; i < index; i++)
                        {
                            finalLocationText.Text += $"{asd(i)}\n";
                        }
                    }
                } else if(combBoxApi.Text == "Movie")
                {
                    string[] movies = (string[])e.Data.GetData(DataFormats.FileDrop);
                    Console.WriteLine(movies[0]);
                    foreach (string movie in movies)
                    {
                        string movieString = movie.Replace(@"\", "/");
                        string fileType = movie.Split('.').Last();
                        fileType = $".{fileType}";
                        dropedFilesTextBlock.Text += $"{movieString}\n";
                        string fileName = movieString.Split('/').Last();
                        string filePath = movieString.Replace($"{fileName}", "");
                        movieString = movieString.Replace($"/{fileName}", "");
                        movieString = movieString.Split('/').Last();
                        if (movieString.Any(char.IsDigit))
                        {
                            string data = Regex.Match(movieString, @"\d+").Value;
                            if(data.Length >= 4)
                            {
                                int indexOfNumbers = movieString.IndexOf(data);
                                if (!Char.IsLetter(movieString[indexOfNumbers - 1]))
                                {

                                    string movieYear = movieString.Split(movieString[indexOfNumbers - 1]).Last();

                                    string movieName = movieString.Replace(movieString[indexOfNumbers - 1], ' ');
                                    movieName = movieName.Replace(data, "");
                                    string newFileName = middleClass.PickMovieStringWithYear(movieName, movieYear);
                                    finalLocationText.Text += $"{filePath}{newFileName}{fileType}\n";
                                }
                                else
                                {
                                    string movieYear = data;
                                    string movieName = movieString.Replace(data, "");
                                    string newFileName = middleClass.PickMovieStringWithYear(movieName, movieYear);
                                    finalLocationText.Text += $"{filePath}{newFileName}{fileType}\n";
                                }
                            }
                            else if(data.Length < 4)
                            {
                                int indexOfNumbers = movieString.IndexOf(data);
                                if(!Char.IsLetter(movieString[indexOfNumbers - 1]) && movieString[indexOfNumbers -1] != ' ')
                                {
                                    movieString = movieString.Replace(movieString[indexOfNumbers - 1], ' ');
                                    string newFileName = middleClass.PickMovieString($"{movieString}");
                                    finalLocationText.Text += $"{filePath}{newFileName}{fileType}\n";
                                }
                                else
                                {
                                    movieString.Replace(data, $" {data}");
                                    string newFileName = middleClass.PickMovieString($"{movieString}");
                                    finalLocationText.Text += $"{filePath}{newFileName}{fileType}\n";
                                }
                            }
                        }
                    }
                }
            }
        }

        private string asd(int i)
        {
            string[] files = dropedFilesTextBlock.Text.Split('\n');
            Array.Sort(files, new AlphaNumericComparer());
            if (files[0] == "")
            {
                files = files.Where(w => w != files[0]).ToArray();
            }
            string filename = files[i].Split('/').Last();
            string fileLocation = files[i].Replace(filename, "");
            string fileType = files[i].Split('.').Last();
            string finalLocation = middleClass.getId(seriesChoice.Text, seriesSeasonPick.Text, i, fileLocation, fileType);
            return finalLocation;
        }

        private int GetNumberOfFiles()
        {
            string[] files = dropedFilesTextBlock.Text.Split('\n');
            return files.Length-1;
        }

        private void renameFinalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (combBoxApi.Text == "Series")
            {
                int index = GetNumberOfFiles();
                Console.WriteLine(index);
                for (int i = 0; i < index; i++)
                {
                    renameFile(i);
                }
            }
            else if(combBoxApi.Text == "Movie")
            {
                middleClass.GetMovieName(seriesChoice.Text, dropedFilesTextBlock.Text.Split('\n').First());
            }
        }

        private void renameFile(int i)
        {
            string[] files = dropedFilesTextBlock.Text.Split('\n');
            Array.Sort(files, new AlphaNumericComparer());
            if(files[0] == "")
            {
                files = files.Where(w => w != files[0]).ToArray();
            }
            string filename = files[i].Split('/').Last();
            string fileLocation = files[i].Replace(filename, "");
            string fileType = files[i].Split('.').Last();
            string oldFileLocation = files[i];
            middleClass.SeriesSearch(seriesChoice.Text, seriesSeasonPick.Text, files, fileLocation, fileType, oldFileLocation, i);
        }

        private void addComboItems(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(combBoxApi.Text == "Series")
            {
                List<string> stringOfSeriesNames = middleClass.GetSeriesNames(seriesNameInput.Text);
                foreach (string name in stringOfSeriesNames)
                {
                    if(seriesChoice.Items.Count > 4)
                    {
                        seriesChoice.Items.Clear();
                        seriesChoice.Items.Add(name);
                    }
                    else
                    {
                        seriesChoice.Items.Add(name);
                    }
                }
            }
            //else if(combBoxApi.Text == "Movie")
            //{
            //    List<string> listOfMovies = middleClass.PickMovieString(seriesNameInput.Text);
            //    foreach (string movieName in listOfMovies)
            //    {
            //        if(seriesChoice.Items.Count > 4)
            //        {
            //            seriesChoice.Items.Clear();
            //            seriesChoice.Items.Add(movieName);
            //        }
            //        else
            //        {
            //            seriesChoice.Items.Add(movieName);
            //        }
            //    }
            //}
            
        }

        private void pickNowSeason(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(combBoxApi.Text == "Series")
            {
                if(seriesChoice.Text != "")
                {
                    List<string> stringName = middleClass.GetSeriesNames(seriesChoice.Text);
            
                    List<string> seasonCount = middleClass.GetSeason(stringName[0]);
                    foreach (string seasonNumber in seasonCount)
                    {
                        if(seasonNumber != "0")
                        {
                            seriesSeasonPick.Items.Add(seasonNumber);
                        }
                    }
                }
            }
        }

        
    }
}
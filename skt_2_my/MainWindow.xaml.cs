using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace skt_2_my
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Data3
    {
        public double xComp { get; set; }
        public double yComp { get; set; }
        public double zComp { get; set; }

    }
    public partial class MainWindow : Window
    {
        OpenFileDialog openFileDialog;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 taskWindow = new Window1();
            taskWindow.Show();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                string line;
                string shortFileName;
                StreamReader reader = File.OpenText(filename);
                ObservableCollection<Data3> source = new ObservableCollection<Data3>();
                shortFileName = filename.Split('\\').Last();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] items = line.Split('\t');
                    source.Add(new Data3() { xComp = double.Parse(items[1]),
                        yComp=double.Parse(items[2]), zComp= double.Parse(items[3]) });
                }
                reader.Close();
                //_data.Add(source);
            }
        }
    }
}

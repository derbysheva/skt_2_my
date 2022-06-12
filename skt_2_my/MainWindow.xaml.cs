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
using Path = System.IO.Path;

namespace skt_2_my
{
    public class Data3
    {
        public double xComp { get; set; }
        public double yComp { get; set; }
        public double zComp { get; set; }

    }
    public partial class MainWindow : Window
    {
        OpenFileDialog openFileDialog;
        double xmin, xmax, ymin, ymax, zmin, zmax;
        int nx, ny, nz;
        Data3 pAnom;
        double xrecmin, xrecmax, yrecmin, yrecmax, zrec;
        int npr, nrec;
        ObservableCollection<Data3> B, P;

        private void xMin_TextChanged(object sender, TextChangedEventArgs e) => xmin = Convert.ToDouble(xMin.Text);
        private void yMin_TextChanged(object sender, TextChangedEventArgs e) => ymin = Convert.ToDouble(yMin.Text);

        private void zMin_TextChanged(object sender, TextChangedEventArgs e) => zmin = Convert.ToDouble(zMin.Text);
        private void xMax_TextChanged(object sender, TextChangedEventArgs e) => xmax = Convert.ToDouble(xMax.Text);

        private void yMax_TextChanged(object sender, TextChangedEventArgs e) => ymax = Convert.ToDouble(yMax.Text);

        private void zMax_TextChanged(object sender, TextChangedEventArgs e) => zmax = Convert.ToDouble(zMax.Text);

        private void nX_TextChanged(object sender, TextChangedEventArgs e) => nx = Convert.ToInt32(nX.Text);

        private void nY_TextChanged(object sender, TextChangedEventArgs e) => ny = Convert.ToInt32(nY.Text);
        private void nZ_TextChanged(object sender, TextChangedEventArgs e) => nz = Convert.ToInt32(nZ.Text);

        private void xRecMin_TextChanged(object sender, TextChangedEventArgs e) => xrecmin = Convert.ToDouble(xRecMin.Text);

        private void yRecMin_TextChanged(object sender, TextChangedEventArgs e) => yrecmin = Convert.ToDouble(yRecMin.Text);

        private void zRec_TextChanged(object sender, TextChangedEventArgs e) => zrec = Convert.ToDouble(zRec.Text);

        private void xRecMax_TextChanged(object sender, TextChangedEventArgs e) => xrecmax = Convert.ToDouble(xRecMax.Text);

        private void yRecMax_TextChanged(object sender, TextChangedEventArgs e) => yrecmax = Convert.ToDouble(yRecMax.Text);

        private void nPr_TextChanged(object sender, TextChangedEventArgs e) => npr = Convert.ToInt32(nPr.Text);

        private void nRec_TextChanged(object sender, TextChangedEventArgs e) => nrec = Convert.ToInt32(nRec.Text);

        List<ObservableCollection<Data3>> _cellMesh = new List<ObservableCollection<Data3>>();

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
                    source.Add(new Data3() { xComp = double.Parse(items[0]),
                        yComp=double.Parse(items[1]), zComp= double.Parse(items[2]) });
                }
                reader.Close();
                //_cellMesh.Add(source);
            }
        }
        private void OpenMesh(object sender, RoutedEventArgs e)
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
                    source.Add(new Data3()
                    {
                        xComp = double.Parse(items[0]),
                        yComp = 0,
                        zComp = double.Parse(items[1])
                    });
                }
                reader.Close();
                _cellMesh.Add(source);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|RichText Files (*.rtf)|*.rtf|XAML Files (*.xaml)|*.xaml|All files (*.*)|*.*";
            if (sfd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(SetAnoms.Document.ContentStart, SetAnoms.Document.ContentEnd);
                using (FileStream fs = File.Create(sfd.FileName))
                {
                    if (Path.GetExtension(sfd.FileName).ToLower() == ".rtf")
                        doc.Save(fs, DataFormats.Rtf);
                    else if (System.IO.Path.GetExtension(sfd.FileName).ToLower() == ".txt")
                        doc.Save(fs, DataFormats.Text);
                    else
                        doc.Save(fs, DataFormats.Xaml);
                }
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "RichText Files (*.rtf)|*.rtf|All files (*.*)|*.*";

            if (ofd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(SetAnoms.Document.ContentStart, SetAnoms.Document.ContentEnd);
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                {
                    if (Path.GetExtension(ofd.FileName).ToLower() == ".rtf")
                        doc.Load(fs, DataFormats.Rtf);
                    else if (Path.GetExtension(ofd.FileName).ToLower() == ".txt")
                        doc.Load(fs, DataFormats.Text);
                    else
                        doc.Load(fs, DataFormats.Xaml);
                }
            }
        }
    }
}
}

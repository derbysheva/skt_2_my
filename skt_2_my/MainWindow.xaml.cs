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
    public class Draw

    {
        //public double scaleX, scaleY;
        public int coorX, coorY;

        int Width;
        int Height;

        public Draw()
        {
            //scaleX = 0; scaleY = 0;
            coorX = 0; coorY = 0;
            Width = 496; Height = 350;
        }

        protected Point TrasformMetresToPixels(Data3 tmp, int minX, int maxX, int minY, int maxY)
        {
            //int h = Height - 60;
            Point newtmp = new Point();

            newtmp.X = coorX + Convert.ToInt32((tmp.xComp - minX) * Width/(maxX-minX));
            newtmp.Y = coorY + Height - Convert.ToInt32((tmp.yComp - minY) * Height / (maxY - minY));

            return newtmp;
        }

        //protected List<double> TrasformPixelsToMetres(Point tmp, double kX, double kY, int minX, int minY)
        //{
        //    int h = Height - 60;
        //    List<double> newtmp = new List<double>();

        //    newtmp.Add((tmp.X - 40 - coorX) / kX + minX);
        //    newtmp.Add(minY - (tmp.Y - 30 - h - coorY) / kY);
        //    return newtmp;
        //}

        public void DrawRec (ObservableCollection<Data3> data, Canvas chart)
        {
            chart.Children.Clear();

        }
        public void DrawPoint(ObservableCollection<Data> data, Canvas chart)
        {
            chart.Children.Clear();

            int minX = data.Min(a => a.X) - 1;
            int maxX = data.Max(a => a.X) + 1;
            int minY = data.Min(a => a.Y);
            int maxY = data.Max(a => a.Y);

            maxY = Convert.ToInt32(maxY + maxY * 0.2);

            int nX = maxX - minX;
            int nY = maxY - minY;

            double kX = Convert.ToDouble(Height - 10) / nX;
            double kY = Convert.ToDouble(Height - 60) / nY;

            //возьмем дату и порисуем линию
            System.Windows.Shapes.Path pth = new System.Windows.Shapes.Path();
            pth.Stroke = Brushes.Blue;
            pth.StrokeThickness = 1;
            StreamGeometry geo = new StreamGeometry();
            geo.FillRule = FillRule.EvenOdd;
            using (StreamGeometryContext ctx = geo.Open())
            {
                for (int i = 0; i < data.Count - 1; i++)
                {
                    Point a = TrasformMetresToPixels(data[i], kX + scaleX, kY + scaleY, minX, minY);
                    Point b = TrasformMetresToPixels(data[i + 1], kX + scaleX, kY + scaleY, minX, minY);
                    Point[] pointes = { a, b };

                    ctx.BeginFigure(a, true, false);
                    ctx.LineTo(b, true, false);
                }
            }
            geo.Freeze();
            pth.Data = geo;
            chart.Children.Add(pth);

            Rectangle rect1;
            rect1 = new Rectangle();
            rect1.Stroke = Brushes.White;
            rect1.Fill = Brushes.White;
            rect1.Width = 40;
            rect1.Height = Height;
            Canvas.SetLeft(rect1, 0);
            Canvas.SetTop(rect1, 0);
            chart.Children.Add(rect1);

            Rectangle rect2;
            rect2 = new Rectangle();
            rect2.Stroke = Brushes.White;
            rect2.Fill = Brushes.White;
            rect2.Width = Width;
            rect2.Height = Height;
            Canvas.SetLeft(rect2, 0);
            Canvas.SetTop(rect2, Height - 30);
            chart.Children.Add(rect2);

            Rectangle rect3;
            rect3 = new Rectangle();
            rect3.Stroke = Brushes.White;
            rect3.Fill = Brushes.White;
            rect3.Width = Width;
            rect3.Height = 30;
            Canvas.SetLeft(rect3, 0);
            Canvas.SetTop(rect3, 0);
            chart.Children.Add(rect3);

            Rectangle rect4;
            rect4 = new Rectangle();
            rect4.Stroke = Brushes.White;
            rect4.Fill = Brushes.White;
            rect4.Width = Width;
            rect4.Height = Height;
            Canvas.SetLeft(rect4, Height + 30);
            Canvas.SetTop(rect4, 0);
            chart.Children.Add(rect4);

            //нарисуем разбиения по осям
            int hX = Convert.ToInt32((Height - 10) / 4);
            int hY = Convert.ToInt32((Height - 60) / 4);
            int tmpX = 40, tmpY = Height - 30;

            //по х
            for (int i = 0; i < 5; i++)
            {
                System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
                path.Stroke = Brushes.Black;
                path.StrokeThickness = 1;
                StreamGeometry geom = new StreamGeometry();
                geom.FillRule = FillRule.EvenOdd;
                using (StreamGeometryContext ctx = geom.Open())
                {
                    ctx.BeginFigure(new Point(tmpX, Height - 30), true, false);
                    ctx.LineTo(new Point(tmpX, 30), true, false);
                }
                geom.Freeze();
                path.Data = geom;
                chart.Children.Add(path);
                List<double> beg = TrasformPixelsToMetres(new Point(tmpX, 0), kX + scaleX, kY + scaleY, minX, minY);
                TextBlock textBlock = new TextBlock();
                textBlock.Text = beg[0].ToString("0.#");
                Canvas.SetLeft(textBlock, tmpX - 5);
                Canvas.SetTop(textBlock, Height - 25);
                chart.Children.Add(textBlock);
                tmpX += hX;
            }
            //по y
            for (int i = 0; i < 5; i++)
            {
                System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
                path.Stroke = Brushes.Black;
                path.StrokeThickness = 1;
                StreamGeometry geom = new StreamGeometry();
                geom.FillRule = FillRule.EvenOdd;
                using (StreamGeometryContext ctx = geom.Open())
                {
                    ctx.BeginFigure(new Point(40, tmpY), true, false);
                    ctx.LineTo(new Point(Height + 30, tmpY), true, false);
                }
                geom.Freeze();
                path.Data = geom;
                chart.Children.Add(path);
                List<double> beg = TrasformPixelsToMetres(new Point(0, tmpY), kX + scaleX, kY + scaleY, minX, minY);
                TextBlock textBlock = new TextBlock();
                textBlock.Text = beg[1].ToString("0.#");
                Canvas.SetLeft(textBlock, 13);
                Canvas.SetTop(textBlock, tmpY - 8);
                chart.Children.Add(textBlock);
                tmpY -= hY;
            }
        }
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

        List<ObservableCollection<Data3>> _cellMesh = new List<ObservableCollection<Data3>>();
        public MainWindow()
        {
            InitializeComponent();
        }
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

        private void SolveDirect_Click(object sender, RoutedEventArgs e)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(@"C:\Users\derby\source\repos\skt_2_my\skt_1_my_new.exe");
            if (fi.Exists)
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = @"C:\Users\derby\source\repos\skt_2_my\skt_1_my_new.exe";
                p.StartInfo.Arguments = @"C:\Users\derby\source\repos\skt_2_my\";
                p.Start();
            }
        }
        private void openB_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void openP_Click(object sender, RoutedEventArgs e)
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
                        zComp = 0
                    });
                }
                reader.Close();
                _cellMesh.Add(source);
            }
        }


        private void SaveArea_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter writer = new StreamWriter("area.txt");
            writer.WriteLine($"{xmin} {xmax} {nx}");
            writer.WriteLine($"{ymin} {ymax} {ny}");
            writer.WriteLine($"{zmin} {zmax} {nz}");
            writer.Close();
        }
        private void SaveReceivers_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter writer = new StreamWriter("receivers_n_min_max.txt");
            writer.WriteLine($"{npr} {nrec}");
            writer.WriteLine($"{xrecmin} {xrecmax}");
            writer.WriteLine($"{yrecmin} {yrecmax}");
            writer.WriteLine($"{zrec}");
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

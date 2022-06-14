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
using System.Globalization;

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
            Width = 350; Height = 180;
        }

        protected Point TrasformMetresToPixels(Data3 tmp, int minX, int minY, int nX, int nY)
        {
            //int h = Height - 60;
            Point newtmp = new Point();

            newtmp.X = coorX + Convert.ToInt32((tmp.xComp - minX) * Width / nX);
            newtmp.Y = coorY + Height - Convert.ToInt32((tmp.zComp - minY) * Height / nY);

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

        //public void DrawRec (ObservableCollection<Data3> data, Canvas chart, int xMin, int zMin, int nX, int nZ, Color col)
        //{
        //    System.Windows.Shapes.Path pth = new System.Windows.Shapes.Path();
        //    pth.Stroke = new SolidColorBrush(col);
        //    pth.StrokeThickness = 1;
        //    StreamGeometry geo = new StreamGeometry();
        //    geo.FillRule = FillRule.EvenOdd;
        //    using (StreamGeometryContext ctx = geo.Open())
        //    {
        //        Point p1 = TrasformMetresToPixels(data[0], xMin, zMin, nX, nZ);
        //        Point p2 = TrasformMetresToPixels(data[1], xMin, zMin, nX, nZ);
        //        Point p3 = TrasformMetresToPixels(data[2], xMin, zMin, nX, nZ);
        //        Point p4 = TrasformMetresToPixels(data[3], xMin, zMin, nX, nZ);
        //        ctx.BeginFigure(p1, true, false);
        //        ctx.LineTo(p2, true, false);
        //        ctx.LineTo(p3, true, false);
        //        ctx.LineTo(p4, true, false);
        //    }
        //    geo.Freeze();
        //    pth.Data = geo;
        //    chart.Children.Add(pth);
        //}
        public void DrawRec(ObservableCollection<Data3> data, Canvas chart, int xMin, int zMin, int nX, int nZ, Color col)
        {
            Rectangle rect;
            rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(col);
            rect.Fill = new SolidColorBrush(col);
            rect.Width = TrasformMetresToPixels(data[1],xMin, zMin, nX, nZ).X- TrasformMetresToPixels(data[0], xMin, zMin, nX, nZ).X;
            rect.Height = Math.Abs(TrasformMetresToPixels(data[0], xMin, zMin, nX, nZ).Y - TrasformMetresToPixels(data[2], xMin, zMin, nX, nZ).Y);
            Canvas.SetLeft(rect, TrasformMetresToPixels(data[0], xMin, zMin, nX, nZ).X); 
            Canvas.SetTop(rect, TrasformMetresToPixels(data[0], xMin, zMin, nX, nZ).Y);
            chart.Children.Add(rect);
        }
        public void DrawRec1(Canvas chart, double Width, double Height, double left, double top, Color col)
        {
            Rectangle rect;
            rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(col);
            rect.Fill = new SolidColorBrush(col);
            rect.Width = Width;
            rect.Height = Height;
            Canvas.SetLeft(rect, left);
            Canvas.SetTop(rect, top);
            chart.Children.Add(rect);
        }

        private int CheckInterval( Data3 val, ObservableCollection<Data3> values)
        {
            if (val.xComp >= values[0].xComp && val.xComp < values[1].xComp) return 0;
            else
            if (val.xComp >= values[1].xComp && val.xComp < values[2].xComp) return 1;
            else
            if (val.xComp >= values[2].xComp && val.xComp < values[3].xComp) return 2;
            else
            if (val.xComp >= values[3].xComp && val.xComp < values[4].xComp) return 3;
            else
            if (val.xComp >= values[4].xComp && val.xComp < values[5].xComp) return 4;
            else
            if (val.xComp >= values[5].xComp && val.xComp < values[6].xComp) return 5;
            else
            if (val.xComp >= values[6].xComp && val.xComp < values[7].xComp) return 6;
            else
            if (val.xComp >= values[7].xComp && val.xComp < values[8].xComp) return 7;
            else
            if (val.xComp >= values[8].xComp && val.xComp < values[9].xComp) return 8;
            else
            if (val.xComp >= values[9].xComp && val.xComp < values[10].xComp) return 9;
            else
            if (val.xComp >= values[10].xComp && val.xComp < values[11].xComp) return 10;
            else
            if (val.xComp >= values[11].xComp && val.xComp < values[12].xComp) return 11;
            else
            if (val.xComp >= values[12].xComp && val.xComp < values[13].xComp) return 12;
            else
            if (val.xComp >= values[13].xComp && val.xComp < values[14].xComp) return 13;
            //else
            //if (val.xComp >= values[14].xComp && val.xComp <= values[15].xComp) return 14;
            else return 13;
        }

        public void DrawAll(List<ObservableCollection<Data3>> meshData, ObservableCollection<Data3> Pdata,
            double xMin, double zMin, double xMax, double zMax, int numX, int numZ, Canvas chart)
        {
            int nX = Convert.ToInt32(xMax - xMin);
            int nZ = Convert.ToInt32(zMax - zMin);
            int xMin1 = Convert.ToInt32(xMin);
            int zMin1 = Convert.ToInt32(zMin);

            double maxP = Pdata.Max(a => a.xComp);
            double minP = Pdata.Min(a => a.xComp);
            double dP = maxP - minP;
            double hP = dP / 15;
            ObservableCollection<Color> cols = new ObservableCollection<Color>();
            for (int i = 1; i < 15; i++)
            {
                byte r = Convert.ToByte(255 - i * 17);
                byte g = Convert.ToByte(255 - i * 17);
                cols.Add(Color.FromRgb(r, g, 255));
            }
            ObservableCollection<Data3> values = new ObservableCollection<Data3>();
            for (int i = 0; i <15; i++)
            {
                var tmp = i * hP;
                values.Add(new Data3()
                {
                    xComp = minP + tmp,
                    yComp = 0,
                    zComp = 0
                });
            }
            chart.Children.Clear();
            for (int i = 0; i < meshData.LongCount(); i++)
            {
                var indcol = CheckInterval(Pdata[i], values);
                DrawRec(meshData[i], chart, xMin1, zMin1, nX, nZ, cols[indcol]);
            }
            //нарисуем разбиения по осям
            int hX = Convert.ToInt32((Width) / numX);
            int hY = Convert.ToInt32((Height) / numZ);
            int tmpX = 0, tmpY = Height-10;

            List<double> xcoords = new List<double>();
            List<double> zcoords = new List<double>();
            double stepX = (xMax - xMin) / numX;
            double stepZ = (zMax - zMin) / numZ;
            for (int i = 0; i < numX; i++)
                xcoords.Add(xMin + i * stepX);
            xcoords.Add(xMax);
            for (int i = 0; i < numZ; i++)
                zcoords.Add(zMin + i * stepZ);
            zcoords.Add(zMax);

            //по х
            for (int i = 0; i <= numX; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = xcoords[i].ToString("0.#");
                Canvas.SetLeft(textBlock, tmpX - 5);
                Canvas.SetTop(textBlock, -20);
                chart.Children.Add(textBlock);
                tmpX += hX;
            }
            //по z
            for (int i = 0; i <= numZ; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = zcoords[i].ToString("0.#");
                Canvas.SetRight(textBlock, Width+20);
                Canvas.SetTop(textBlock, tmpY);
                chart.Children.Add(textBlock);
                tmpY -= hY;
            }

            //нарисуем шкалу
            double hscal = (Height+50)/15;
            for(int i=0; i<14; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = values[i].xComp.ToString("e3");
                textBlock.FontSize = 9;
                //Canvas.SetLeft(textBlock, i * hscal - 45);
                //Canvas.SetTop(textBlock, Height+20);
                Canvas.SetLeft(textBlock, Width+55);
                Canvas.SetTop(textBlock, i * 14 -15);
                chart.Children.Add(textBlock);
                tmpX += hX;
                DrawRec1(chart, 15, hscal, Width+30, -15 + i * 14, cols[i]);
            }

        }
    }
    public partial class MainWindow : Window
    {
        double xmin, xmax, ymin, ymax, zmin, zmax;
        int nx, ny, nz;
        Data3 pAnom;
        double xrecmin, xrecmax, yrecmin, yrecmax, zrec;
        int npr, nrec;
        ObservableCollection<Data3> B, P;
        List<ObservableCollection<Data3>> _cellMesh = new List<ObservableCollection<Data3>>();

        Draw drawobj = new Draw();

        public MainWindow()
        {
            InitializeComponent();
        }
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
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                    string filename = ofd.FileName;
                    string line;
                    StreamReader reader = File.OpenText(filename);
                    ObservableCollection<Data3> source = new ObservableCollection<Data3>();
                    while ((line = reader.ReadLine()) != null)
                    {
                        //line.TrimEnd('\t');
                    //if (line.Last() == char.Parse("")) line.Remove(line.IndexOf(""));
                        string[] items = line.Split('\t');
                    
                    foreach (var i in items)
                    {
                        if (i.LongCount() != 0)
                        {
                            source.Add(new Data3()
                            {
                                xComp = double.Parse(i, CultureInfo.InvariantCulture),
                                yComp = 0,
                                zComp = 0
                            });
                        }
                    }
                    }
                    reader.Close();
                    P = source;
            }
        }

        private void OpenArea_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                string filename = ofd.FileName;
                string line;
                StreamReader reader = File.OpenText(filename);
                ObservableCollection<Data3> source = new ObservableCollection<Data3>();
                string[] items;
                line = reader.ReadLine();
                items= line.Split(' ');
                xmin = double.Parse( items[0], CultureInfo.InvariantCulture);
                xmax = double.Parse(items[1], CultureInfo.InvariantCulture);
                nx = int.Parse(items[2], CultureInfo.InvariantCulture);
                line = reader.ReadLine();
                items = line.Split(' ');
                ymin = double.Parse(items[0], CultureInfo.InvariantCulture);
                ymax = double.Parse(items[1], CultureInfo.InvariantCulture);
                ny = int.Parse(items[2], CultureInfo.InvariantCulture);
                line = reader.ReadLine();
                items = line.Split(' ');
                zmin = double.Parse(items[0], CultureInfo.InvariantCulture);
                zmax = double.Parse(items[1], CultureInfo.InvariantCulture);
                nz = int.Parse(items[2], CultureInfo.InvariantCulture);
                reader.Close();
            }
        }


        private void DrawSolution_Click(object sender, RoutedEventArgs e)
        {
            drawobj.DrawAll(_cellMesh, P, xmin, zmin, xmax, zmax, nx, nz, cvs);
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
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                string filename = ofd.FileName;
                string line;
                string shortFileName;
                StreamReader reader = File.OpenText(filename);
                ObservableCollection<Data3> source = new ObservableCollection<Data3>();
                shortFileName = filename.Split('\\').Last();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] items = line.Split('\t');
                    source.Add(new Data3() { xComp = double.Parse(items[0], CultureInfo.InvariantCulture),
                        yComp=double.Parse(items[1], CultureInfo.InvariantCulture), zComp= double.Parse(items[2], CultureInfo.InvariantCulture) });
                }
                reader.Close();
                //_cellMesh.Add(source);
            }
        }
        private void OpenMesh (object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                if (!String.IsNullOrEmpty(ofd.FileName))
                {
                    string filename = ofd.FileName;
                    string line;
                    string shortFileName;
                    StreamReader reader = File.OpenText(filename);
                    
                    shortFileName = filename.Split('\\').Last();
                    while ((line = reader.ReadLine()) != null)
                    {
                        ObservableCollection<Data3> source = new ObservableCollection<Data3>();
                        string[] items = line.Split('\t');
                        for (int i = 0; i < 8; i+=2)
                        {
                            source.Add(new Data3()
                            {
                                xComp = double.Parse(items[i], CultureInfo.InvariantCulture),
                                yComp = 0,
                                zComp = double.Parse(items[i+1], CultureInfo.InvariantCulture)
                            });
                        }
                        _cellMesh.Add(source);
                    }
                    reader.Close();
                    
                }
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

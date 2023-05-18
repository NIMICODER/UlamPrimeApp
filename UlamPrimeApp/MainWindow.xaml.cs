using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UlamPrimeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int size = 5;
        public int Size
        {
            get { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    GenerateSpiral();
                    OnPropertyChanged(nameof(Size));
                }
            }
        }


        private ObservableCollection<SpiralCell> spiral;
        public ObservableCollection<SpiralCell> Spiral
        {
            get { return spiral; }
            set
            {
                if (spiral != value)
                {
                    spiral = value;
                    OnPropertyChanged(nameof(Spiral));
                }
            }
        }








        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            GenerateSpiral();
            //Int32 width = 60;
            //Int32 height = 20;

            //Width = Math.Min(width, 120);
            //Height = Math.Min(height, 60);
            //ResizeMode = ResizeMode.NoResize;
            //WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Int32 limit = (Int32)Math.Pow(Math.Min(width, height) - 2, 2);

            //for (Int32 n = 1; n <= limit; n++)
            //{
            //    Point point = NumberToPoint(n - 1, width / 2 - 1, height / 2);

            //    Brush brush = IsPrimeD(n) ? Brushes.DarkBlue : Brushes.DarkGray;

            //    Rectangle rectangle = new Rectangle
            //    {
            //        Width = 5,
            //        Height = 5,
            //        Fill = brush
            //    };

            //    Canvas.SetLeft(rectangle, point.X);
            //    Canvas.SetTop(rectangle, point.Y);
            //    canvas.Children.Add(rectangle);

            //    Thread.Sleep(10);
            //}
        }

        private void GenerateSpiral()
        {
            int[,] grid = new int[Size, Size];
            int x = Size / 2;
            int y = Size / 2;
            int direction = 0; // 0 = right, 1 = up, 2 = left, 3 = down

            int number = 1;
            for (int i = 1; i <= Size * Size; i++)
            {
                grid[x, y] = number;
                number++;

                switch (direction)
                {
                    case 0:
                        if (y == Size - 1 || grid[x, y + 1] != 0)
                        {
                            direction = 1;
                            x--;
                        }
                        else
                        {
                            y++;
                        }
                        break;
                    case 1:
                        if (x == 0 || grid[x - 1, y] != 0)
                        {
                            direction = 2;
                            y--;
                        }
                        else
                        {
                            x--;
                        }
                        break;
                    case 2:
                        if (y == 0 || grid[x, y - 1] != 0)
                        {
                            direction = 3;
                            x++;
                        }
                        else
                        {
                            y--;
                        }
                        break;
                    case 3:
                        if (x == Size - 1 || grid[x + 1, y] != 0)
                        {
                            direction = 0;
                            y++;
                        }
                        else
                        {
                            x++;
                        }
                        break;
                }
            }

            Spiral = new ObservableCollection<SpiralCell>();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int num = grid[j, i];
                    Brush color = IsPrime(num) ? Brushes.Yellow : Brushes.Red;
                    Spiral.Add(new SpiralCell { Number = num, Color = color });
                }
            }
        }




        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static bool IsPrime(int n)
        {
            if (n <= 1) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;

            int limit = (int)Math.Sqrt(n);
            for (int i = 3; i <= limit; i += 2)
            {
                if (n % i == 0) return false;
            }
            return true;
        }
      

    }

    public class SpiralCell
    {
        public int Number { get; set; }
        public Brush Color { get; set; }
    }
}

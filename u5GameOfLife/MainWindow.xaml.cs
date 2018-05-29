/**
 * Written by Nolan Meehan.
 * Simple version of John Conway's Game of Life.
 * 
 */using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;

namespace u5GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        Cell[,] board = new Cell[20,20];
        bool[,] temp = new bool[20, 20];
public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("creating board");
            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int c = 0; c < board.GetLength(1); c++)
                {
                    temp[i, c] = false;
                    board[i, c] = new Cell(false, new Point(c * Cell.size, i * Cell.size), canvas);
                }
            }
            //Console.WriteLine("setting timer");
            gameTimer.Tick += gameTick;
            gameTimer.Interval += new TimeSpan(0, 0, 0, 0, 1000 / 10);
            gameTimer.Start();
            gameTimer.Stop();
        }

        private void gameTick(object sender, EventArgs e)
        {
            //Console.WriteLine("updating");

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int c = 0; c < board.GetLength(1); c++)
                {
                    temp[c, i]= false;
                }
            }

            for (int i = 0; i < temp.GetLength(0);i++)
            {
                for(int c = 0; c < temp.GetLength(1);c++)
                {
                    //Console.WriteLine(board[0, 0].isAlive);//getCellNeighbours(board[0, 0]));
                    int neighbours = getCellNeighbours(board[i, c]);
                    if ((neighbours > 3 || neighbours < 2)) temp[i, c]=(false);
                    else if (!board[i, c].isAlive && neighbours == 2) temp[i, c]=(false);
                    else temp[i, c]=(true);
                }
            }
            //Console.WriteLine("updated");
            copyArray(temp);
        }

        public int getCellNeighbours(Cell cell)
        {
            int y = (int)(cell.Location.Y / Cell.size);
            int x = (int)(cell.Location.X / Cell.size);
            int count = 0;
            for (int i =y-1; i<=y+1; i++)
            {
                for (int c = x - 1; c <= x+1; c++)
                {
                    if (c < 0 || c >= board.GetLength(1) || i < 0 || i >= board.GetLength(0)) continue;
                    if ((!(c==x&&i==y))&& board[i, c].isAlive) count++;

                }
            }
            return count;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            gameTimer.Stop();
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            gameTimer.Start();
        }


        private void copyArray(bool[,] source)
        {
            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int c = 0; c < source.GetLength(1); c++)
                {
                    board[i, c].setAlive(source[i, c]);
                }
            }
        }
    }
}

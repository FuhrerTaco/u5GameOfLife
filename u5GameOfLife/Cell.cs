using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace u5GameOfLife
{
    public class Cell
    {
        public static int size = 30;
        public bool isAlive;
        Rectangle body = new Rectangle();
        Point location;
        public Point Location { get => location; }
        public Cell(bool alive, Point loc, Canvas c)
        {
            this.setAlive(alive);
            this.location = loc;
            this.body.Width = Cell.size;
            this.body.Height = Cell.size;
            this.body.Stroke = Brushes.Black;
            Canvas.SetTop(this.body, location.Y);
            Canvas.SetLeft(this.body, location.X);
            this.body.StrokeThickness = 1;
            this.body.MouseDown += Body_MouseDown;
            c.Children.Add(this.body);
        }

        private void Body_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.setAlive(!this.isAlive);
            Console.WriteLine(this.ToString());
        }

        public void setAlive(bool living)
        {
            this.isAlive = living;
            this.body.Fill = this.isAlive ? Brushes.Yellow : Brushes.Transparent;
        }
        public override string ToString()
        {
            return String.Format("a {0} Cell at: {1}, {2}", isAlive ? "Live" : "dead",this.location.X,this.location.Y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideTheHorse
{
    public class BoardSquare
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public BoardSquare(int x, int y, int value = 0)
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
        }
        public bool visited { get; set; }
    } 
}

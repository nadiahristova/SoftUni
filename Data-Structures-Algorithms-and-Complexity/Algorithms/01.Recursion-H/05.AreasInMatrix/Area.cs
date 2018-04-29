using System;
using System.Collections.Generic;
using System.Linq;

namespace Recursion_Homework
{
    class Area : IComparable<Area>
    {
        public int Name { get; set; }
        public int Size { get; set; }        
        public Tuple<int, int> BaseCell { get; set; }

        public Area(int name, int row, int col)
        {
            this.Name = name;
            this.BaseCell = new Tuple<int, int>(row, col);
        }
        public override string ToString()
        {
            return string.Format("Area #{0} at ({1}, {2}), size: {3}",
                Name, BaseCell.Item1, BaseCell.Item2, Size);
        }

        public int CompareTo(Area other)
        {
            if (this.Size == other.Size)
            {
                if (this.BaseCell.Item1 == other.BaseCell.Item1)
                {
                    return this.BaseCell.Item2.CompareTo(other.BaseCell.Item2);
                }

                return this.BaseCell.Item1.CompareTo(other.BaseCell.Item1);
            }

            return other.Size.CompareTo(this.Size);
        }
    }
}

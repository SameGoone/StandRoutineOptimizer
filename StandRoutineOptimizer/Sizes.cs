using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneRepoHelper
{
    struct Sizes
    {
        public int RowHeight { get; set; }
        public int LeftColumnWidth { get; set; }
        public int RightColumnWidth { get; set; }

        public Sizes(int rowHeight, int leftColumnWidth, int rightColumnWidth)
        {
            RowHeight = rowHeight;
            LeftColumnWidth = leftColumnWidth;
            RightColumnWidth = rightColumnWidth;
        }
    }
}

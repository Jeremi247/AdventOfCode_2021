using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode_2021
{
    class Line
    {
        public Line( Vector2<int> inStart, Vector2<int> inEnd )
        {
            start = inStart;
            end = inEnd;
        }

        public Vector2<int> start;
        public Vector2<int> end;
    }
}

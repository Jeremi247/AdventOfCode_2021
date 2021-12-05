using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode_2021
{
    class Vector2<T>
    {
        public Vector2( T x, T y )
        {
            X = x;
            Y = y;
        }

        public T X;
        public T Y;
    }
}

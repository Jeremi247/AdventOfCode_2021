using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode_2021
{
    class ListUtils
    {
        public static void ResizeList<T>( List<T> list, int newSize, T element = default( T ) )
        {
            int toAdd = newSize - list.Count;

            if( toAdd < 0 )
            {
                list.RemoveRange( list.Count - toAdd, toAdd );
            }
            else if( toAdd > 0 )
            {
                list.AddRange( Enumerable.Repeat( element, toAdd ) );
            }
        }
    }
}

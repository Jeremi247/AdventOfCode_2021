using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode_2021
{
    class Grid<T>
    {
        List<List<T>> grid = new List<List<T>>();

        public Grid( int sizeX, int sizeY )
        {
            ListUtils.ResizeList( grid, sizeY );

            for( int i = 0; i < grid.Count; i++ )
            {
                grid[i] = new List<T>();
                ListUtils.ResizeList( grid[i], sizeX );
            }
        }

        public List<T> this[int key]
        {
            get => grid[key];
            set => grid[key] = value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode_2021.Day5
{
    class Day5_Main : AbstractDay
    {
        public override string CalculateOutput( bool useAlternate = false )
        {
            string[] input = GetInput();

            if( !useAlternate )
            {
                List<Line> ventLines = ParseInput( input );
                Vector2<int> gridSize = GetGridSize( ventLines );
                return CountRepeats( ventLines, gridSize ).ToString();
            }
            else
            {
                List<Line> ventLines = ParseInput( input, true );
                Vector2<int> gridSize = GetGridSize( ventLines );
                return CountRepeats( ventLines, gridSize ).ToString();
            }
        }

        public override int GetDayNumber()
        {
            return 5;
        }

        private List<Line> ParseInput( string[] input, bool withDiagonals = false )
        {
            List<Line> lines = new List<Line>();

            foreach( string line in input )
            {
                string[] tempSplit = line.Split( " -> " );
                Vector2<int> lineStart = StringToVectorInt( tempSplit[0] );
                Vector2<int> lineEnd = StringToVectorInt( tempSplit[1] );

                if( lineStart.X == lineEnd.X || lineStart.Y == lineEnd.Y || withDiagonals )
                {
                    lines.Add( new Line( lineStart, lineEnd ) );
                }
            }

            return lines;
        }

        private Vector2<int> StringToVectorInt( string textVector )
        {
            string[] splitText = textVector.Split( ',' );

            int x = int.Parse( splitText[0] );
            int y = int.Parse( splitText[1] );

            return new Vector2<int>( x, y );
        }

        private int CountRepeats( List<Line> lines, Vector2<int> gridSize )
        {
            Grid<int> grid = new Grid<int>( gridSize.X, gridSize.Y );

            int foundRepeats = 0;

            foreach( Line line in lines )
            {
                int xDistance = Math.Abs( line.start.X - line.end.X );
                int yDistance = Math.Abs( line.start.Y - line.end.Y );
                int distance = Math.Max( xDistance, yDistance );

                int xDirection = Math.Clamp( line.end.X - line.start.X, -1, 1 );
                int yDirection = Math.Clamp( line.end.Y - line.start.Y, -1, 1 );

                for( int i = 0; i <= distance; ++i )
                {
                    int xLocation = line.start.X + i * xDirection;
                    int yLocation = line.start.Y + i * yDirection;

                    grid[yLocation][xLocation]++;
                    if( grid[yLocation][xLocation] == 2 )
                    {
                        foundRepeats++;
                    }
                }
            }

            return foundRepeats;
        }

        private Vector2<int> GetGridSize( List<Line> lines )
        {
            Vector2<int> size = new Vector2<int>( int.MinValue, int.MinValue );

            foreach( Line line in lines )
            {
                //lines use indexes
                size.X = Math.Max( Math.Max( line.start.X, line.end.X ) + 1, size.X );
                size.Y = Math.Max( Math.Max( line.start.Y, line.end.Y ) + 1, size.Y );
            }

            return size;
        }
    }
}

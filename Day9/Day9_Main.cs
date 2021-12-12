using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021.Day9
{
    class Day9_Main : AbstractDay
    {
        const char InvalidLocation = '9';

        public override string CalculateOutput( bool useAlternate = false )
        {
            string[] input = GetInput();

            int[] lowestPointsCounts = GetLowestPointsCount( input );
            List<int> basinSizes = GetAllBasinsSizes( input );
            basinSizes.Sort();

            if( !useAlternate )
            {
                return CountSummaryScore( lowestPointsCounts ).ToString();
            }
            else
            {
                int result = basinSizes[basinSizes.Count - 1] * basinSizes[basinSizes.Count - 2] * basinSizes[basinSizes.Count - 3];
                return result.ToString();
            }
        }

        public override int GetDayNumber()
        {
            return 9;
        }

        private int CountSummaryScore( int[] lowestPointsCounts )
        {
            int summaryValue = 0;

            for( int i = 0; i < lowestPointsCounts.Length; i++ )
            {
                summaryValue += lowestPointsCounts[i] * ( i + 1 );
            }

            return summaryValue;
        }

        private int[] GetLowestPointsCount( string[] input )
        {
            int[] results = new int[10];

            for( int i = 0; i < input.Length; ++i )
            {
                for( int j = 0; j < input[i].Length; j++ )
                {
                    char value = input[i][j];

                    //left side
                    if( i != 0 && input[i - 1][j] <= value )
                    {
                        continue;
                    }

                    //right side
                    if( i != input.Length - 1 && input[i + 1][j] <= value )
                    {
                        continue;
                    }

                    //top side
                    if( j != 0 && input[i][j - 1] <= value )
                    {
                        continue;
                    }

                    //bottom side
                    if( j != input[i].Length - 1 && input[i][j + 1] <= value )
                    {
                        continue;
                    }

                    int valueAsNum = value - '0';
                    results[valueAsNum]++;
                }
            }

            return results;
        }

        private List<int> GetAllBasinsSizes( string[] input )
        {
            List<List<char>> locations = new List<List<char>>();
            List<int> basinsSizes = new List<int>();

            foreach( string location in input )
            {
                locations.Add( new List<char>( location ) );
            }

            for( int i = 0; i < locations.Count; i++ )
            {
                for( int j = 0; j < locations[i].Count; j++ )
                {
                    if( locations[i][j] != InvalidLocation )
                    {
                        int basinSize = MeasureBasin( locations, new Vector2<int>( i, j ) );
                        basinsSizes.Add( basinSize );
                    }
                }
            }

            return basinsSizes;
        }

        private int MeasureBasin( List<List<char>> input, Vector2<int> startingLocation )
        {
            List<Vector2<int>> validLocations = new List<Vector2<int>>();
            validLocations.Add( startingLocation );

            int basinSize = 0;

            while( validLocations.Count != 0 )
            {
                if( input[validLocations[0].X][validLocations[0].Y] != InvalidLocation )
                {
                    basinSize++;
                    validLocations.AddRange( ProbeLocation( input, validLocations[0] ) );
                    input[validLocations[0].X][validLocations[0].Y] = InvalidLocation;
                }

                validLocations.RemoveAt( 0 );
            }

            return basinSize;
        }

        private List<Vector2<int>> ProbeLocation( List<List<char>> input, Vector2<int> location )
        {
            List<Vector2<int>> validLocations = new List<Vector2<int>>();

            //left side
            if( location.X != 0 && input[location.X - 1][location.Y] != InvalidLocation )
            {
                validLocations.Add( new Vector2<int>( location.X - 1, location.Y ) );
            }

            //right side
            if( location.X != input.Count - 1 && input[location.X + 1][location.Y] != InvalidLocation )
            {
                validLocations.Add( new Vector2<int>( location.X + 1, location.Y ) );
            }

            //top side
            if( location.Y != 0 && input[location.X][location.Y - 1] != InvalidLocation )
            {
                validLocations.Add( new Vector2<int>( location.X, location.Y - 1 ) );
            }

            //bottom side
            if( location.Y != input[location.X].Count - 1 && input[location.X][location.Y + 1] != InvalidLocation )
            {
                validLocations.Add( new Vector2<int>( location.X, location.Y + 1 ) );
            }

            return validLocations;
        }
    }
}

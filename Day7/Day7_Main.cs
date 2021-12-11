using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode_2021.Day7
{
    class Day7_Main : AbstractDay
    {
        public override string CalculateOutput( bool useAlternate = false )
        {
            string[] input = GetInput();
            int[] data = ParseInput( input );

            int highestPosition = GetHighestNumber( data );

            if( !useAlternate )
            {
                return GetCheapestPositionFuelCost( data, highestPosition + 1 ).ToString();
            }
            else
            {
                return GetCheapestPositionFuelCost( data, highestPosition + 1, true ).ToString();
            }
        }

        public override int GetDayNumber()
        {
            return 7;
        }

        private int[] ParseInput( string[] input )
        {
            string[] textNums = input[0].Split( ',' );
            int[] result = new int[textNums.Length];

            for( int i = 0; i < textNums.Length; ++i )
            {
                result[i] = int.Parse( textNums[i] );
            }

            return result;
        }

        private int GetHighestNumber( int[] data )
        {
            int result = int.MinValue;

            foreach( int number in data )
            {
                if(number> result)
                {
                    result = number;
                }
            }

            return result;
        }

        private int GetFuelUsageForAlignement( int[] positions, int targetPosition )
        {
            int fuelUsage = 0;

            foreach( int position in positions )
            {
                fuelUsage += Math.Abs( position - targetPosition );
            }

            return fuelUsage;
        }

        private int GetCheapestPositionFuelCost( int[] positions, int maxPosition, bool alternate = false )
        {
            int lowestFuelUsage = int.MaxValue;

            for( int i = 0; i < maxPosition; ++i )
            {
                int fuelUsage = 0;
                if( alternate )
                {
                    fuelUsage = GetFuelUsageForAlignement2( positions, i );
                }
                else
                {
                    fuelUsage = GetFuelUsageForAlignement( positions, i );
                }

                if( fuelUsage < lowestFuelUsage )
                {
                    lowestFuelUsage = fuelUsage;
                }
            }

            return lowestFuelUsage;
        }

        private int GetFuelUsageForAlignement2( int[] positions, int targetPosition )
        {
            int fuelUsage = 0;

            foreach( int position in positions )
            {
                int count = Math.Abs( position - targetPosition );
                fuelUsage = count * ( count + 1 ) / 2;
            }

            return fuelUsage;
        }
    }
}

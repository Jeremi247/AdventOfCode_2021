using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode_2021.Day1
{
    class Day1_Main : AbstractDay
    {
        public override string CalculateOutput( bool useAlternate = false )
        {
            string[] input = GetInput();

            int[] parsedInputs = ParseInput( input );

            if( !useAlternate )
            {
                return CountIncreases( parsedInputs ).ToString();
            }
            else
            {
                return CountMeasuredIncreases( parsedInputs ).ToString();
            }
        }

        public override int GetDayNumber()
        {
            return 1;
        }

        private int[] ParseInput( string[] input )
        {
            int[] result = new int[input.Length];

            for( int i = 0; i < input.Length; ++i )
            {
                result[i] = int.Parse( input[i] );
            }

            return result;
        }

        private int CountIncreases( int[] values )
        {
            int counter = 0;

            for( int i = 1; i < values.Length; ++i )
            {
                if( values[i] > values[i-1] )
                {
                    counter++;
                }
            }

            return counter;
        }

        private int CountMeasuredIncreases( int[] values )
        {
            int counter = 0;

            for( int i = 3; i < values.Length; ++i )
            {
                int LHS = Measure( values, i - 3, 3 );
                int RHS = Measure( values, i - 2, 3 );

                if( LHS < RHS )
                {
                    counter++;
                }
            }

            return counter;
        }

        private int Measure( int[] source, int startAt, int length )
        {
            int result = 0;

            for( int i = 0; i < length; ++i )
            {
                result += source[i + startAt];    
            }

            return result;
        }
    }
}

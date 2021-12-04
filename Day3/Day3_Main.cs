using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode_2021.Day3
{
    class Day3_Main : AbstractDay
    {
        public override string CalculateOutput( bool useAlternate = false )
        {
            string[] input = GetInput();

            //input = new string[]{
            //    "00100",
            //    "11110",
            //    "10110",
            //    "10111",
            //    "10101",
            //    "01111",
            //    "00111",
            //    "11100",
            //    "10000",
            //    "11001",
            //    "00010",
            //    "01010"
            //};

            List<List<int>> parsedInputs = ParseInput( input );
            List<int> commonBits = FindMostCommonBitsInColumn( parsedInputs );

            int gammaRate = BinaryToDecimal( commonBits );
            int epsilonRate = BinaryToDecimal( InvertBinary( commonBits ) );

            int oxygenRate = Decipher( parsedInputs, false );
            int co2 = Decipher( parsedInputs, true );

            if( !useAlternate )
            {
                return (gammaRate * epsilonRate).ToString();
            }
            else
            {
                return (oxygenRate * co2).ToString();
            }
        }

        public override int GetDayNumber()
        {
            return 3;
        }

        private List<List<int>> ParseInput( string[] input )
        {
            List<List<int>> result = new List<List<int>>();

            for( int i = 0; i < input.Length; ++i )
            {
                result.Add( new List<int>() );

                for( int j = 0; j < input[i].Length; ++j )
                {
                    result[i].Add( input[i][j] - '0' );
                }
            }

            return result;
        }

        private List<int> FindMostCommonBitsInColumn( List<List<int>> inputData )
        {
            int columnSize = inputData.Count;
            List<int> bitsCounter = new List<int>();

            foreach( List<int> inputRow in inputData )
            {
                for( int j = 0; j < inputRow.Count; ++j )
                {
                    if(bitsCounter.Count <= j)
                    {
                        bitsCounter.Add( 0 );
                    }

                    bitsCounter[j] += inputRow[j];
                }
            }

            List<int> result = new List<int>();
            for( int i = 0; i < bitsCounter.Count; ++i )
            {
                result.Add( Convert.ToInt32(bitsCounter[i] >= (float)columnSize / 2.0f) );
            }

            return result;
        }

        private List<int> InvertBinary( List<int> bits )
        {
            for( int i = 0; i < bits.Count; ++i )
            {
                bits[i] = Math.Abs( bits[i] - 1 );
            }

            return bits;
        }

        private int BinaryToDecimal( List<int> commonBits )
        {
            int result = 0;

            for( int i = commonBits.Count - 1; i >= 0; --i )
            {
                result += commonBits[i] << commonBits.Count - i - 1;
            }

            return result;
        }

        private int Decipher( List<List<int>> inputData, bool invertData )
        {
            List<List<int>> inputCopy = new List<List<int>>( inputData );

            for( int i = 0; i < inputCopy[0].Count; ++i )
            {
                List<int> commonBits = FindMostCommonBitsInColumn( inputCopy );

                if(invertData)
                {
                    commonBits = InvertBinary( commonBits );
                }

                for( int j = inputCopy.Count - 1; j >= 0; --j )
                {
                    if( inputCopy[j][i] != commonBits[i] )
                    {
                        inputCopy.RemoveAt( j );
                    }
                }

                if(inputCopy.Count == 1)
                {
                    break;
                }
            }

            return BinaryToDecimal( inputCopy[0] );
        }
    }
}

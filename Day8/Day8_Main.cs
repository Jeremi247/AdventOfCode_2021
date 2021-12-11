using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2021.Day8
{
    class InputEntry
    {
        public List<string> prePipeValues = new List<string>();
        public List<string> postPipeValues = new List<string>();

        public InputEntry( string inputLine )
        {
            string[] splitLine = inputLine.Split( '|' );

            string[] splitPrePipe = splitLine[0].Split( ' ' );
            string[] splitPostPipe = splitLine[1].Split( ' ' );

            foreach( string value in splitPrePipe )
            {
                if( value.Length != 0 )
                {
                    prePipeValues.Add( value );
                }
            }

            foreach( string value in splitPostPipe )
            {
                if( value.Length != 0 )
                {
                    postPipeValues.Add( value );
                }
            }
        }
    }

    class Day8_Main : AbstractDay
    {
        public override string CalculateOutput( bool useAlternate = false )
        {
            string[] input = GetInput();

            List<InputEntry> inputEntries = new List<InputEntry>();
            List<string> pipedValues = new List<string>();

            foreach( string line in input )
            {
                inputEntries.Add( new InputEntry( line ) );

                pipedValues.Add( line.Split( '|' )[1] );
            }

            List<int> acceptableValues = new List<int>() { 2, 3, 4, 7 };
            int counter = 0;

            foreach( string value in pipedValues )
            {
                string[] configs = value.Split( ' ' );

                foreach( string config in configs )
                {
                    if( acceptableValues.Contains( config.Length ) )
                    {
                        counter++;
                    }
                }
            }

            if( !useAlternate )
            {
                return counter.ToString();
            }
            else
            {
                return ResolveAlternate( inputEntries ).ToString();
            }
        }

        public override int GetDayNumber()
        {
            return 8;
        }

        public int ResolveAlternate( List<InputEntry> inputEntries )
        {
            int sum = 0;

            foreach( InputEntry entry in inputEntries )
            {
                sum += ResolveEntry( entry );
            }

            return sum;
        }

        public int ResolveEntry( InputEntry entry )
        {
            List<char> segmentOrder = FindProperSegmentOrder( entry );

            return GetResolvedNumber(segmentOrder, entry.postPipeValues);
        }

        private int GetResolvedNumber( List<char> segmentOrder, List<string> postPipeValues )
        {
            int result = 0;

            for( int i = postPipeValues.Count - 1; i >= 0; --i )
            {
                int value = DigitalNumberStringToNumber( segmentOrder, postPipeValues[ postPipeValues.Count - i - 1 ] );

                result += value * (int)Math.Pow( 10, i );    
            }

            return result;
        }

        private int DigitalNumberStringToNumber( List<char> segmentOrder, string value )
        {
            List<bool> mask = new List<bool>();

            for( int i = 0; i < segmentOrder.Count; i++ )
            {
                if( value.Contains( segmentOrder[i] ))
                {
                    mask.Add( true );
                }
                else
                {
                    mask.Add( false );
                }
            }

            return DigitalNumber.MatchMaskToNumber( mask );
        }

        private List<char> FindProperSegmentOrder( InputEntry entry )
        {
            List<DigitalNumber> digitalNumbers = new List<DigitalNumber>();

            for( int i = entry.prePipeValues.Count - 1; i >= 0; --i )
            {
                switch( entry.prePipeValues[i].Length )
                {
                case 2:
                    digitalNumbers.Add( DigitalNumber.BuildOne( new List<char>( entry.prePipeValues[i] ) ) );
                    entry.prePipeValues.RemoveAt( i );
                    break;
                case 3:
                    digitalNumbers.Add( DigitalNumber.BuildSeven( new List<char>( entry.prePipeValues[i] ) ) );
                    entry.prePipeValues.RemoveAt( i );
                    break;
                case 4:
                    digitalNumbers.Add( DigitalNumber.BuildFour( new List<char>( entry.prePipeValues[i] ) ) );
                    entry.prePipeValues.RemoveAt( i );
                    break;
                case 7:
                    digitalNumbers.Add( DigitalNumber.BuildEight( new List<char>( entry.prePipeValues[i] ) ) );
                    entry.prePipeValues.RemoveAt( i );
                    break;
                }
            }

            bool fiveAdded = false;
            bool zeroAdded = false;

            while( true )
            {
                for( int i = 0; i < digitalNumbers.Count; i++ )
                {
                    for( int j = 0; j < digitalNumbers.Count; j++ )
                    {
                        digitalNumbers[i].Ensure( digitalNumbers[j].segmentsProbabilities );

                        if( digitalNumbers[i].IsResolved() )
                        {
                            List<char> result = new List<char>();

                            foreach( List<char> probabilities in digitalNumbers[i].segmentsProbabilities )
                            {
                                result.Add( probabilities[0] );
                            }

                            return result;
                        }
                    }
                }

                if( !fiveAdded )
                {
                    DigitalNumber five = null;
                    bool success = DigitalNumber.TryBuildFive( digitalNumbers[0].segmentsProbabilities, entry.prePipeValues, ref five );
                    if( success )
                    {
                        fiveAdded = true;
                        digitalNumbers.Add( five );
                    }
                }

                if( !zeroAdded )
                {
                    DigitalNumber zero = null;
                    bool success = DigitalNumber.TryBuildZero( digitalNumbers[0].segmentsProbabilities, entry.prePipeValues, ref zero );
                    if( success )
                    {
                        zeroAdded = true;
                        digitalNumbers.Add( zero );
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode_2021.Day8
{
    class Modification
    {
        public List<char> ToAdd = new List<char>();
        public List<char> ToRemove = new List<char>();

        public void Add( char newChar )
        {
            if( !ToAdd.Contains( newChar ) )
            {
                ToAdd.Add( newChar );
            }

            ToRemove.Remove( newChar );
        }

        public void Remove( char newChar )
        {
            if( !ToAdd.Contains( newChar ) && !ToRemove.Contains( newChar ) )
            {
                ToRemove.Add( newChar );
            }
        }
    }

    class DigitalNumber
    {
        public List<List<char>> segmentsProbabilities = new List<List<char>>();

        private static List<List<bool>> Masks = new List<List<bool>> {
            new List<bool> { true, true, true, false, true, true, true },
            new List<bool> { false, false, true, false, false, true, false },
            new List<bool> { true, false, true, true, true, false, true },
            new List<bool> { true, false, true, true, false, true, true },
            new List<bool> { false, true, true, true, false, true, false },
            new List<bool> { true, true, false, true, false, true, true },
            new List<bool> { true, true, false, true, true, true, true },
            new List<bool> { true, false, true, false, false, true, false },
            new List<bool> { true, true, true, true, true, true, true },
            new List<bool> { true, true, true, true, false, true, true }
        };

        public static int MatchMaskToNumber( List<bool> mask )
        {
            for( int i = 0; i < Masks.Count; i++ )
            {
                bool matched = true;

                for( int j = 0; j < mask.Count; j++ )
                {
                    if( mask[j] != Masks[i][j] )
                    {
                        matched = false;
                        break;
                    }
                }

                if(matched)
                {
                    return i;
                }
            }

            return -1;
        }


        public DigitalNumber(
            List<char> inA = null,
            List<char> inB = null,
            List<char> inC = null,
            List<char> inD = null,
            List<char> inE = null,
            List<char> inF = null,
            List<char> inG = null
            )
        {
            segmentsProbabilities.Add( inA );
            segmentsProbabilities.Add( inB );
            segmentsProbabilities.Add( inC );
            segmentsProbabilities.Add( inD );
            segmentsProbabilities.Add( inE );
            segmentsProbabilities.Add( inF );
            segmentsProbabilities.Add( inG );
        }

        public static List<char> Inverse( List<char> activeSegments )
        {
            List<char> result = new List<char>();

            for( int i = 'a'; i <= 'g'; ++i )
            {
                if( !activeSegments.Contains( (char)i ) )
                {
                    result.Add( (char)i );
                }
            }

            return result;
        }

        public static DigitalNumber BuildOne( List<char> activeSegments )
        {
            List<char> inverted = Inverse( activeSegments );

            return new DigitalNumber(
                new List<char>( inverted ),
                new List<char>( inverted ),
                new List<char>( activeSegments ),
                new List<char>( inverted ),
                new List<char>( inverted ),
                new List<char>( activeSegments ),
                new List<char>( inverted ) );
        }

        public static DigitalNumber BuildFour( List<char> activeSegments )
        {
            List<char> inverted = Inverse( activeSegments );

            return new DigitalNumber(
                new List<char>( inverted ),
                new List<char>( activeSegments ),
                new List<char>( activeSegments ),
                new List<char>( activeSegments ),
                new List<char>( inverted ),
                new List<char>( activeSegments ),
                new List<char>( inverted ) );
        }

        public static DigitalNumber BuildSeven( List<char> activeSegments )
        {
            List<char> inverted = Inverse( activeSegments );

            return new DigitalNumber(
                new List<char>( activeSegments ),
                new List<char>( inverted ),
                new List<char>( activeSegments ),
                new List<char>( inverted ),
                new List<char>( inverted ),
                new List<char>( activeSegments ),
                new List<char>( inverted ) );
        }

        public static DigitalNumber BuildEight( List<char> activeSegments )
        {
            return new DigitalNumber(
                new List<char>( activeSegments ),
                new List<char>( activeSegments ),
                new List<char>( activeSegments ),
                new List<char>( activeSegments ),
                new List<char>( activeSegments ),
                new List<char>( activeSegments ),
                new List<char>( activeSegments ) );
        }

        public static bool TryBuildFive( List<List<char>> segmentsProbabilities, List<string> possibleCombinations, ref DigitalNumber outDigitalNumber )
        {
            List<char> cSegment = segmentsProbabilities[2];
            List<char> eSegment = segmentsProbabilities[4];

            if( cSegment.Count != 2 || eSegment.Count != 2 )
            {
                return false;
            }

            for( int i = 0; i < possibleCombinations.Count; i++ )
            {
                if( possibleCombinations[i].Length != 5 )
                {
                    continue;
                }

                bool xorCSegment = possibleCombinations[i].Contains( cSegment[0] ) ^ possibleCombinations[i].Contains( cSegment[1] );
                bool xorESegment = possibleCombinations[i].Contains( eSegment[0] ) ^ possibleCombinations[i].Contains( eSegment[1] );

                if( xorCSegment && xorESegment )
                {
                    List<char> activeSegments = new List<char>( possibleCombinations[i] );
                    List<char> inverted = Inverse( activeSegments );

                    outDigitalNumber = new DigitalNumber(
                        new List<char>( activeSegments ),
                        new List<char>( activeSegments ),
                        new List<char>( inverted ),
                        new List<char>( activeSegments ),
                        new List<char>( inverted ),
                        new List<char>( activeSegments ),
                        new List<char>( activeSegments ) );

                    possibleCombinations.RemoveAt( i );

                    return true;
                }
            }

            return false;
        }

        public static bool TryBuildZero( List<List<char>> segmentsProbabilities, List<string> possibleCombinations, ref DigitalNumber outDigitalNumber )
        {
            List<char> bSegment = segmentsProbabilities[1];

            if( bSegment.Count != 2 )
            {
                return false;
            }

            for( int i = 0; i < possibleCombinations.Count; i++ )
            {
                if( possibleCombinations[i].Length != 6 )
                {
                    continue;
                }

                bool xorBSegment = possibleCombinations[i].Contains( bSegment[0] ) ^ possibleCombinations[i].Contains( bSegment[1] );

                if( xorBSegment )
                {
                    List<char> activeSegments = new List<char>( possibleCombinations[i] );
                    List<char> inverted = Inverse( activeSegments );

                    outDigitalNumber = new DigitalNumber(
                        new List<char>( activeSegments ),
                        new List<char>( activeSegments ),
                        new List<char>( activeSegments ),
                        new List<char>( inverted ),
                        new List<char>( activeSegments ),
                        new List<char>( activeSegments ),
                        new List<char>( activeSegments ) );

                    possibleCombinations.RemoveAt( i );

                    return true;
                }
            }

            return false;
        }

        public void Ensure( List<List<char>> characters )
        {
            for( int i = 0; i < characters.Count; i++ )
            {
                if( segmentsProbabilities[i] == null )
                {
                    segmentsProbabilities[i] = characters[i];
                }
            }

            List<Modification> segmentModifications = new List<Modification>();
            ListUtils.ResizeListWithDefaults( segmentModifications, characters.Count );

            for( int i = 'a'; i <= 'g'; ++i )
            {
                for( int j = 0; j < characters.Count; j++ )
                {
                    //if characters[j] is null then segmentsProbabilities[j] is also null
                    if( characters[j] == null )
                    {
                        continue;
                    }

                    if( characters[j].Contains( (char)i ) )
                    {
                        segmentModifications[j].Add( (char)i );

                        foreach( Modification mod in segmentModifications )
                        {
                            mod.Remove( (char)i );
                        }
                    }
                }
            }

            for( int i = 0; i < segmentModifications.Count; i++ )
            {
                for( int j = 0; j < segmentModifications[i].ToRemove.Count; j++ )
                {
                    if( segmentsProbabilities[i] != null )
                    {
                        segmentsProbabilities[i].RemoveAll( x => x == segmentModifications[i].ToRemove[j] );
                    }
                }
            }
        }

        public bool IsResolved()
        {
            foreach( List<char> segment in segmentsProbabilities )
            {
                if( segment == null || segment.Count != 1 )
                {
                    return false;
                }
            }

            return true;
        }
    }
}

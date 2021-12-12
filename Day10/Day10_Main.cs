using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode_2021.Day10
{
    class Day10_Main : AbstractDay
    {
        public override string CalculateOutput( bool useAlternate = false )
        {
            string[] input = GetInput();

            List<List<char>> linesToComplete = new List<List<char>>();
            int corruptionValue = 0;
            foreach( string line in input )
            {
                int corruptionCost = 0;
                List<char> incompleteBrackets = GetIncompletenes( line, ref corruptionCost );
                if(incompleteBrackets.Count != 0)
                {
                    linesToComplete.Add( incompleteBrackets );
                }
                corruptionValue += corruptionCost;
            }

            List<ulong> allScores = new List<ulong>();
            foreach( List<char> line in linesToComplete )
            {
                allScores.Add( CompleteAndScoreLine( line ) );
            }

            allScores.Sort();
            ulong middleScore = allScores[(allScores.Count / 2)];

            if(!useAlternate)
            {
                return corruptionValue.ToString();
            }
            else
            {
                return middleScore.ToString();
            }
        }

        public override int GetDayNumber()
        {
            return 10;
        }

        private ulong CompleteAndScoreLine( List<char> line )
        {
            ulong score = 0;

            for( int i = line.Count - 1; i >= 0; i-- )
            {
                score *= 5;

                switch( line[i] )
                {
                case '(': score += 1; break;
                case '[': score += 2; break;
                case '{': score += 3; break;
                case '<': score += 4; break;
                }
            }

            return score;
        }

        private List<char> GetIncompletenes( string line, ref int outCorruptionValue )
        {
            List<char> openBrackets = new List<char>();

            foreach( char bracket in line )
            {
                switch( bracket )
                {
                case ')': 
                    if( openBrackets[openBrackets.Count-1] != '(' )
                    {
                        outCorruptionValue = 3;
                        return new List<char>();
                    }
                    openBrackets.RemoveAt( openBrackets.Count - 1 );
                    break;
                case ']':
                    if( openBrackets[openBrackets.Count - 1] != '[' )
                    {
                        outCorruptionValue = 57;
                        return new List<char>();
                    }
                    openBrackets.RemoveAt( openBrackets.Count - 1 );
                    break;
                case '}':
                    if( openBrackets[openBrackets.Count - 1] != '{' )
                    {
                        outCorruptionValue = 1197;
                        return new List<char>();
                    }
                    openBrackets.RemoveAt( openBrackets.Count - 1 );
                    break;
                case '>':
                    if( openBrackets[openBrackets.Count - 1] != '<' )
                    {
                        outCorruptionValue = 25137;
                        return new List<char>();
                    }
                    openBrackets.RemoveAt( openBrackets.Count - 1 );
                    break;
                default:
                    openBrackets.Add( bracket );
                    break;
                }
            }

            return openBrackets;
        }
    }
}

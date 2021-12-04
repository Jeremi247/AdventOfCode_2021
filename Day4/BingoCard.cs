using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode_2021.Day4
{
    enum BingoResult
    {
        InProgress,
        Success
    }

    class BingoCard
    {
        List<List<int>> cardNumbers = new List<List<int>>();
        bool[][] rowCounters;
        bool[][] columnCounters;

        public BingoCard( List<string> inputLines )
        {
            SetupCardNumbers( inputLines );
        }

        public void SetupCardNumbers( List<string> inputLines )
        {
            foreach( string line in inputLines )
            {
                List<int> row = new List<int>();

                string[] splitLine = line.Split( ' ' );

                foreach( string element in splitLine )
                {
                    if( element.Length != 0 )
                    {
                        row.Add( int.Parse( element ) );
                    }
                }

                cardNumbers.Add( row );
            }

            rowCounters = new bool[cardNumbers[0].Count][];
            columnCounters = new bool[cardNumbers.Count][];

            for( int i = 0; i < rowCounters.Length; i++ )
            {
                rowCounters[i] = new bool[cardNumbers.Count];
            }

            for( int i = 0; i < columnCounters.Length; i++ )
            {
                columnCounters[i] = new bool[cardNumbers[0].Count];
            }
        }

        public BingoResult MarkNumbers( int number )
        {
            BingoResult result = BingoResult.InProgress;

            for( int i = 0; i < cardNumbers.Count; i++ )
            {
                for( int j = 0; j < cardNumbers[i].Count; j++ )
                {
                    if( cardNumbers[i][j] == number )
                    {
                        rowCounters[i][j] = true;
                        columnCounters[j][i] = true;

                        //assumes numbers do not repeat
                        if( VerifyBoolEnumerable( rowCounters[i] ) || VerifyBoolEnumerable( columnCounters[j] ) )
                        {
                            result = BingoResult.Success;
                        }
                    }
                }
            }

            return result;
        }

        private bool VerifyBoolEnumerable( IEnumerable<bool> list )
        {
            foreach( bool value in list )
            {
                if( !value )
                {
                    return false;
                }    
            }

            return true;
        }

        public int ScoreBoard()
        {
            int score = 0;

            for( int i = 0; i < rowCounters.Length; i++ )
            {
                for( int j = 0; j < rowCounters[i].Length; j++ )
                {
                    if(rowCounters[i][j] == false)
                    {
                        score += cardNumbers[i][j];
                    }
                }
            }

            return score;
        }
    }
}

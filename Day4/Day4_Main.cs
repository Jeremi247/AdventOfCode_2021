using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode_2021.Day4
{
    class Day4_Main : AbstractDay
    {
        public override string CalculateOutput( bool useAlternate = false )
        {
            string[] input = GetInput();

            //input = new string[]
            //{
            //    "46,12,57,37,14,78,31,71,87,52,64,97,10,35,54,36,27,84,80,94,99,22,0,11,30,44,86,59,66,7,90,21,51,53,92,8,76,41,39,77,42,88,29,24,60,17,68,13,79,67,50,82,25,61,20,16,6,3,81,19,85,9,28,56,75,96,2,26,1,62,33,63,32,73,18,48,43,65,98,5,91,69,47,4,38,23,49,34,55,83,93,45,72,95,40,15,58,74,70,89",
            //    "",
            //    "30 66 93 24 92",
            //    "48 80 79 86 27",
            //    "89 13 62 94 81",
            //    "70 65 61  8 54",
            //    "96 97 20 90 34"
            //};

            List<List<string>> boards = SplitIntoBoards( input );
            List<int> numbersQueue = ExtractNumbersQueueFromBoards( boards );

            List<BingoCard> bingoCards = BuildBingoCards( boards );

            if( !useAlternate )
            {
                int winningScore = FindWinningScore( bingoCards, numbersQueue );
                return winningScore.ToString();
            }
            else
            {
                int lastWiningScore = FindLastWinningScore( bingoCards, numbersQueue );
                return lastWiningScore.ToString();
            }
        }

        public override int GetDayNumber()
        {
            return 4;
        }

        private List<List<string>> SplitIntoBoards( string[] input )
        {
            List<List<string>> result = new List<List<string>>();
            List<string> boardData = new List<string>();

            foreach( string line in input )
            {
                if( line.Length == 0 && boardData.Count != 0 )
                {
                    result.Add( boardData );
                    boardData = new List<string>();
                }
                else
                {
                    boardData.Add( line );
                }
            }

            result.Add( boardData );

            return result;
        }

        private List<int> ExtractNumbersQueueFromBoards( List<List<string>> boards )
        {
            List<int> result = new List<int>();
            List<string> numbersBoard = boards[0];
            boards.RemoveAt( 0 );

            string[] numbers = numbersBoard[0].Split( ',' );

            foreach( string number in numbers )
            {
                result.Add( int.Parse( number ) );
            }

            return result;
        }

        private List<BingoCard> BuildBingoCards( List<List<string>> boards )
        {
            List<BingoCard> cards = new List<BingoCard>();

            foreach( List<string> board in boards )
            {
                cards.Add( new BingoCard( board ) );
            }

            return cards;
        }

        private int FindLastWinningScore( List<BingoCard> bingoCards, List<int> numbersQueue )
        {
            BingoCard lastWiningCard = null;
            int lastWiningNumber = 0;

            foreach( int number in numbersQueue )
            {
                List<int> winningCards = ProcessBingoCards2( bingoCards, number );

                if( winningCards.Count > 0 )
                {
                    lastWiningCard = bingoCards[winningCards[winningCards.Count-1]];
                    lastWiningNumber = number;

                    for( int i = winningCards.Count - 1; i >= 0; --i )
                    {
                        bingoCards.RemoveAt( winningCards[i] );
                    }
                }
            }

            if( lastWiningCard != null )
            {
                int score = lastWiningCard.ScoreBoard();
                return score * lastWiningNumber;
            }

            return -1;
        }

        private int FindWinningScore( List<BingoCard> bingoCards, List<int> numbersQueue )
        {
            foreach( int number in numbersQueue )
            {
                int winningCard = ProcessBingoCards( bingoCards, number );

                if( winningCard != -1 )
                {
                    int score = bingoCards[winningCard].ScoreBoard();
                    return score * number;
                }
            }

            return -1;
        }

        private int ProcessBingoCards( List<BingoCard> bingoCards, int number )
        {
            int winningCard = -1;
            for( int i = 0; i < bingoCards.Count; i++ )
            {
                if( bingoCards[i].MarkNumbers( number ) == BingoResult.Success )
                {
                    if( winningCard == -1 )
                    {
                        winningCard = i;
                    }
                }
            }

            return winningCard;
        }

        private List<int> ProcessBingoCards2( List<BingoCard> bingoCards, int number )
        {
            List<int> winningCards = new List<int>();
            for( int i = 0; i < bingoCards.Count; i++ )
            {
                if( bingoCards[i].MarkNumbers( number ) == BingoResult.Success )
                {
                    winningCards.Add( i );
                }
            }

            return winningCards;
        }
    }
}

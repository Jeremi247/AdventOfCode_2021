using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode_2021.Day6
{
    class Day6_Main : AbstractDay
    {
        private const int MaxReproductionCycle = 8;
        private const int RepeatedReproductionCycle = 6;

        public override string CalculateOutput( bool useAlternate = false )
        {
            string[] input = GetInput();
            List<ulong> reproductionCyclesCounters = ParseInput( input );

            if( !useAlternate)
            {
                ulong fishesAfterTime = CountFishesAfterTime( reproductionCyclesCounters, 80 );
                return fishesAfterTime.ToString();
            }
            {
                ulong fishesAfterTime = CountFishesAfterTime( reproductionCyclesCounters, 256 );
                return fishesAfterTime.ToString();
            }
        }

        public override int GetDayNumber()
        {
            return 6;
        }

        private List<ulong> ParseInput( string[] input )
        {
            string[] numbers = input[0].Split( ',' );
            List<ulong> result = new List<ulong>();
            ListUtils.ResizeList( result, MaxReproductionCycle + 1, (ulong)0 );

            foreach( string number in numbers )
            {
                int parsedNumber = int.Parse( number );

                result[parsedNumber]++;
            }

            return result;
        }

        private void SimulateOneDay( List<ulong> reproductionCounter )
        {
            ulong temp = reproductionCounter[0];

            for( int i = 1; i < reproductionCounter.Count; ++i )
            {
                reproductionCounter[i - 1] = reproductionCounter[i];
            }

            reproductionCounter[MaxReproductionCycle] = temp;
            reproductionCounter[RepeatedReproductionCycle] += temp;
        }

        private ulong CountFishesAfterTime( List<ulong> initialFishes, int daysToPass )
        {
            ulong result = 0;

            for( int i = 0; i < daysToPass; ++i )
            {
                SimulateOneDay( initialFishes );
            }

            foreach( ulong fishGroup in initialFishes )
            {
                result += fishGroup;
            }

            return result;
        }
    }
}

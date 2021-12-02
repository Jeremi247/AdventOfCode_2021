using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2021
{
    class Program
    {
        static void Main( string[] args )
        {
            PrintResultsOfAllDays();

            Console.ReadKey();
        }

        static void PrintResultsOfAllDays()
        {
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany( assembly => assembly.GetTypes() )
                .Where( typeObject => typeObject.IsClass && !typeObject.IsAbstract && typeObject.IsSubclassOf( typeof( AbstractDay ) ) );

            AbstractDay[] allDays = new AbstractDay[ types.Count() ];

            foreach( Type type in types )
            {
                AbstractDay dayInstance = (AbstractDay)Activator.CreateInstance( type );

                allDays[dayInstance.GetDayNumber() - 1] = dayInstance;
            }

            foreach( AbstractDay dayInstance in allDays )
            {
                string result = dayInstance.CalculateOutput();

                Console.WriteLine( "Day{0} part1 result is {1}", dayInstance.GetDayNumber(), dayInstance.CalculateOutput() );
                Console.WriteLine( "Day{0} part2 result is {1}", dayInstance.GetDayNumber(), dayInstance.CalculateOutput( true ) );
                Console.WriteLine();
            }
        }
    }
}

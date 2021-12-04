using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode_2021
{
    abstract class AbstractDay
    {
        public abstract string CalculateOutput( bool useAlternate = false );
        public abstract int GetDayNumber();

        protected string[] GetInput()
        {
            return File.ReadAllLines( "InputFiles/Day" + GetDayNumber() + "_Input.txt" );
        }
    }
}

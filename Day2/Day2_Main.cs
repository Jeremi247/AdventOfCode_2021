using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode_2021.Day2
{
    enum Direction
    {
        Forward,
        Downwards,
        Upwards
    }

    struct Command
    {
        public Direction direction;
        public int value;
    }

    class Day2_Main : AbstractDay
    {
        public override string CalculateOutput( bool useAlternate = false )
        {
            string[] input = File.ReadAllLines( "InputFiles/Day2_Input.txt" );

            Command[] commands = ParseInput( input );

            if(!useAlternate)
            {
                return ResolveDepthAndDistance( commands ).ToString();
            }
            else
            {
                return ResolveDepthAndDistanceWithAim( commands ).ToString();
            }
        }

        public override int GetDayNumber()
        {
            return 2;
        }

        public Command[] ParseInput( string[] input )
        {
            Command[] commands = new Command[input.Length];

            for( int i = 0; i < input.Length; ++i )
            {
                string[] rawCommand = input[i].Split( ' ' );

                switch( rawCommand[0] )
                {
                case "forward": commands[i].direction = Direction.Forward; break;
                case "down": commands[i].direction = Direction.Downwards; break;
                case "up": commands[i].direction = Direction.Upwards; break;
                }

                commands[i].value = int.Parse( rawCommand[1] );
            }

            return commands;
        }

        public int ResolveDepthAndDistance( Command[] commands )
        {
            int horizontal = 0;
            int depth = 0;

            foreach( Command command in commands )
            {
                switch( command.direction )
                {
                case Direction.Forward: horizontal += command.value; break;
                case Direction.Downwards: depth += command.value; break;
                case Direction.Upwards: depth -= command.value; break;
                }
            }

            return horizontal * depth;
        }

        public int ResolveDepthAndDistanceWithAim( Command[] commands )
        {
            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            foreach( Command command in commands )
            {
                switch( command.direction )
                {
                case Direction.Forward:
                    horizontal += command.value;
                    depth += command.value * aim;
                    break;
                case Direction.Downwards:
                    aim += command.value;
                    break;
                case Direction.Upwards:
                    aim -= command.value;
                    break;
                }
            }

            return horizontal * depth;
        }
    }

    
}

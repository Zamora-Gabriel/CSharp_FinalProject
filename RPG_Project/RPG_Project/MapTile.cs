﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class MapTile : Map
    {
        int xCord;
        int yCord;
        string tile;

        public MapTiles mapType;

        public MapTile(int yCord, int xCord, MapTiles mapType)
        {
            this.yCord = yCord;
            this.xCord = xCord;

            this.mapType = mapType;

            //Console.WriteLine("{0},{1}", yCord, xCord);
        }

        public int YCord
        {
            get { return yCord; }
            set
            {
                yCord = value;
            }
        }
        public int XCord
        {
            get { return xCord; }
            set
            {
                xCord = value;
            }
        }

        public MapTiles MapType
        {
            get { return mapType; }
            private set
            {
                mapType = value;
            }
        }

        public void PrintSelf()
        {
            switch (mapType)
            {
                case MapTiles.GrassLand:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("G");
                    break;
                case MapTiles.Mountian:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("M");
                    break;
                case MapTiles.Forest:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("F");
                    break;
                case MapTiles.Water:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("W");
                    break;
                case MapTiles.Road:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("R");
                    break;
                case MapTiles.Dungeon:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("D");
                    break;
                case MapTiles.Shop:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("S");
                    break;
                case MapTiles.Player:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("P");
                    break;
            }
        }
    }
}

﻿using System;
using System.IO;

namespace RPG_Project
{

    enum MapTiles
    {
        Empty = 0,
        Forest = 70,
        GrassLand = 71,
        Mountian = 77,
        Road =82,
        Water = 87,
        Shop = 83,
        Dungeon = 68,
        Player = 100,
        Spawn = 66
    }
    class Map
    {
        const int MAP_HEIGHT = 20;
        const int MAP_WIDTH = 40;

        int height;
        int width;
        int characterNmb;

        int yPosition = 0;
        int xPosition = 0;

        char character;

        MapTile[,] mapData;

        MapTile playerTile;
        MapTile defaultTile;


        StreamReader reader;

        public Map()
        {
           
        }

        /*Getters and Setters*/

        public int Height{
            get { return height; }
            private set
            {
                height = value;
            }
        }

        public int Width
        {
            get { return width; }
            private set
            {
                width = value;
            }
        }

        public void GenerateMap()
        {
            Console.WriteLine(characterNmb = Convert.ToInt32(Char.ToUpper('B')));
            Console.ReadLine();
            //Generate special tiles
            playerTile = new MapTile(0, 0, MapTiles.Player);
            defaultTile = new MapTile(0, 0, MapTiles.Mountian);

            //TODO Change this to a relative path
            reader = File.OpenText("D:/0_School Work/GitHub/Intro_To_Html5/New folder/map/test.txt");

            //Finds maximum height and width of map
            Console.WriteLine("Generating map...");
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                if (line.Length > width)
                {
                    width = line.Length;
                }
                height++;
            }
            reader.Close();
            reader = File.OpenText("D:/0_School Work/GitHub/Intro_To_Html5/New folder/map/test.txt");
            Console.WriteLine("Width: {0} Height {1}",Width,Height);
            mapData = new MapTile[height,width+2];
            Console.WriteLine("Propgating data");
            //propgate array with data
            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width+2; x++)
                {
                    character = (char)reader.Read();
                    
                    try
                    {
                        //characterNmb = (int)Char.GetNumericValue(character);
                        characterNmb = Convert.ToInt32(Char.ToUpper(character));
                        //characterNmb = Convert.ToInt32(Char.GetNumericValue(char.ToLower(character)));
                        //characterNmb = Convert.ToInt32(new string (string.ToUpper(character)));
                    }
                    catch
                    {
                        Console.WriteLine("Error Converting map at {0}, {1}", y, x);
                    }
                    switch (characterNmb)
                    {
                        
                        case 66:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Spawn);

                            //Position player on spawn;
                            yPosition = y - MAP_HEIGHT/2;
                            xPosition = x - MAP_WIDTH/2;
                            break;
                        //Dungeon
                        case 68:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Dungeon);
                            break;
                        //Forest
                        case 70:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Forest);
                            break;
                        //GrassLand
                        case 71:
                            mapData[y, x] = new MapTile(y, x, MapTiles.GrassLand);
                            break;
                        //Mountain
                        case 77:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Mountian);
                            break;
                        //Road
                        case 82:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Road);
                            break;
                        //Shop
                        case 83:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Shop);
                            break;
                        //Water
                        case 87:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Water);
                            break;
                            //Catch for no char default to mountain
                        //case 65535:
                        //    mapData[x, y] = new MapTile(y, x, MapTiles.Mountian);
                        //    break;
                    }
                    //GenerateVisualMap();
                }
            }

            //reader = new StreamReader("~/map/test.txt");

            //do
            //{
            //    string character = (string)reader.ReadLine();
            //    Console.Write(character);

            //    

            //} while (!reader.EndOfStream);


            //FInd maximum width

            //Read character
            //Create object after that character
            //Set object to map position
            //GenerateVisualMap();
            UpdatePlayerMap();
        }

        void GenerateVisualMap()
        {
            //Go through map, printing each item.
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width-2 + 2; x++)
                {
                    mapData[y, x].PrintSelf();
                }
                Console.Write("\n");
            }
        }

        public void UpdatePlayerMap()
        {

            //Various printing variables, used for the centering and border of the map

            int winWidth = (Console.WindowWidth / 2);
            string border = new String('─', MAP_WIDTH+2);
            string topBorder = ($"┌{border}┐");
            string bottomBorder = ($"└{border}┘");
            string leftBorder = String.Format("{0," + ((winWidth) - (MAP_WIDTH / 2)) + "}", "│ ");
            string rightBoreder = String.Format("{0}", " │");

            //Clears screen
            Console.Clear();

            //Prints Top Border
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("{0," + ((winWidth+ 1) + ((border.Length / 2))) + "}", topBorder);

            //Starts loop to print the map data
            for (int y = 0; y < MAP_HEIGHT; y++)
            {
                //Prints left border and indents
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(leftBorder);

                //Loop for y axis
                if (y + yPosition >= mapData.GetLength(0) || y + yPosition < 0)
                {
                    //If out of bounds print default tile
                    for(int x = 0; x < MAP_WIDTH; x++)
                    {
                        defaultTile.PrintSelf();
                    }
                }
                else
                {
                    //Loop for x axis
                    for (int x = 0; x < MAP_WIDTH; x++)
                    {
                        //If out of bounds print default tile
                        if (x + xPosition >= width || x + xPosition < 0)
                        {
                            //MapTile tile = new MapTile(0, 0, MapTiles.Mountian);
                            defaultTile.PrintSelf();
                        }
                        else
                        {
                            //Find Center of Map and print player there
                            if(y == MAP_HEIGHT/2 && x == MAP_WIDTH / 2)
                            {
                                playerTile.YCord = y + yPosition;
                                playerTile.XCord = x + xPosition;
                                playerTile.PrintSelf();
                            }
                            else
                            {

                                mapData[y + yPosition, x + xPosition].PrintSelf();
                            }
                        }

                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(rightBoreder);
                Console.Write("\n");

            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("{0," + ((winWidth + 1) + ((border.Length / 2))) + "}", bottomBorder);
        }

        public void CheckSurroundings(int direction)
        {
            switch (direction)
            {
                case 1:
                    //While moving up
                    if (playerTile.YCord != 0)
                    {
                        if (mapData[playerTile.YCord -1, playerTile.XCord].mapType == MapTiles.Mountian)
                        {
                            break;
                        }
                        yPosition--;
                        //UpdatePlayerMap();
                    }
                    break;

                case 2:
                    //While moving down
                    if (playerTile.YCord != mapData.GetLength(0))
                    {
                        if (mapData[playerTile.YCord + 1, playerTile.XCord].mapType == MapTiles.Mountian)
                        {
                            break;
                        }
                        yPosition++;
                        //UpdatePlayerMap();
                    }
                    break;

                case 3:
                    //While moving left
                    if (playerTile.XCord != 0)
                    {
                        if (mapData[playerTile.YCord, playerTile.XCord - 1].mapType == MapTiles.Mountian)
                        {
                            break;
                        }
                        xPosition--;
                        //UpdatePlayerMap();
                    }
                    break;
                case 4:
                    //While moving right
                    if (playerTile.XCord != mapData.GetLength(1))
                    {
                        if (mapData[playerTile.YCord, playerTile.XCord + 1].mapType == MapTiles.Mountian)
                        {
                            break;
                        }
                        xPosition++;
                        //UpdatePlayerMap();
                    }
                    break;
            }
        }
    }
}

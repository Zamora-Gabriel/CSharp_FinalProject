﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class EnemyGenerator
    {
        Printer printer = new Printer();
        BattleManager battleManager;
        Random rand = new Random();

        Player player;

        //string[] template = new string[] {"HP: {0}\\{1} ","            ","{2}"};
        ///
        /// GRASSLAND ENEMIES ART Easy/Medium/Hard
        /// 
        string[] spearmanArt = new string[] { "HP: {0}\\{1} ", "    o ^     ", "   /|\\|     ", "    | |     ", "   / \\|     ", "{2}" };
        string[] swordmanArt = new string[] { "HP: {0}\\{1} ", "    o │     ", "   /|\\┼     ", "    |       ", "    / \\      ", "{2}" };
        string[] archerArt = new string[] { "HP: {0}\\{1}   ", "    o |\\    ", "   /|\\|─┼─> ", "    | |/    ", "    / \\      ", "{2}" };
        ///
        /// GRASSLAND ENEMIES ART Ultra Hard
        /// 
        string[] swordMasterArt = new string[] { "HP: {0}\\{1}   ","            ", "            ", "   │ o │    ", "   ┼/|\\┼    ", "     |      ", "    / \\     ", "{2}" };
        string[] giantArt = new string[] { "HP: {0}\\{1}   ", "    O [=]   ", "   /|\\ |    ", "  / | \\|    ", "    |  |    ", "   / \\      ", "  /   \\     ", "{2}" };
        string[] wizardArt = new string[] { "HP: {0}\\{1}   ", "            ", "     ^ *    ", "     o |    ", "    /|/|    ", "     |      ", "    / \\     ", "{2}" };


        /// <summary>
        /// FOREST ENEMIES ART Easy/Medium
        /// </summary>
        string[] wolfArt = new string[] { "HP: {0}\\{1} ", "   /\\_/\\    ", "  / 0  0\\   ", " |   []  | |", "  \\     / / ", "  /    \\/   ", " |_║__║_|   ", "{2}" };
        string[] jaguarArt = new string[] { "HP: {0}\\{1}   ", "            ", "    /\\/\\    ", "|  | o o|   ", "  \\ \\ =-=/   ", "  \\/    \\   ", "  |_║__║_|  ", "{2}" };
        string[] snakeArt = new string[] { "HP: {0}\\{1}   ", "            ", "            ", "      __    ", "     /  o>~ ", "___/ /    ", "/_______\\   ", "{2}" };
        /// <summary>
        /// FOREST ENEMIES ART Hard
        /// </summary>
        string[] birdArt = new string[] { "HP: {0}\\{1}   ", "    ___     ", "    | o\\    ", "    /   >   ", "   |/ | |   ", "   ||/ /    ", "   \\__/_    ", "{2}" };
        string[] shrubArt = new string[] { "HP: {0}\\{1}   ", "            ", "    ____ |  ", "   /o  o\\/  ", " _/\\_o _/   ", "     ||     ", "     \\\\     ", "{2}" };
        string[] treeguardArt = new string[] { "HP: {0}\\{1}   ", "    _/\\_    ", "  _/■  ■\\_  ", " /   ▄▄▄  \\ ", "/ _______  \\", "|_|  |  | |_|", "    |__|    ", "{2}" };

        /// <summary>
        /// WATER ENEMIES ART easy/medium
        /// </summary>
        string[] lobsterArt = new string[] { "HP: {0}\\{1}  ", "  |/ ___ \\| ","  \\ /o o\\ / ","   \\|   |/  ","    \\___/   ","  <_/||||\\_>","  <_/ || \\_>", "   <_/ \\_>  ", "{2}" };
        string[] fishArt = new string[] { "HP: {0}\\{1}   ", "            ", "            ", "            ", "            ", "|\\__/\\__    ", "| _____o>   ", "|/          ", "{2}" };
        string[] fatFrogArt = new string[] { "HP: {0}\\{1}   ", "            ", "            ", "            ", "            ", "    _       ", "  o~\\\\      ", " |_-__\\\\    ", "{2}" };
        /// <summary>
        /// WATER ENEMIES ART hard
        /// </summary>
        string[] squidArt = new string[] { "HP: {0}\\{1}   ", "    ___     ", "   /o o\\    ", "/\\ |   |  /\\", " | \\___/ |  ", " \\_/| |\\_/  ", " <_/   \\_>  ", "{2}" };
        string[] crabArt = new string[] { "HP: {0}\\{1}   ", " \\| ___ |/  ", "  \\/o o\\/   ", " /|     |\\  ", " |/\\___/\\|  ", "  |/   \\|   ", "   |   |    ", "{2}" };
        string[] waterbugArt = new string[] { "HP: {0}\\{1}   ", "   _    _   ", "    \\__/    ", "   _o  o_   ", " _/ |  | \\_ ", "    |  |    ", "  __/\\/\\__  ", "{2}" };

        //demon lord art
        string[] demonLordArt = new string[] { "HP: {0}\\{1}   ", "            ", "   \\ /   ^  ", " │  o   <0> ", " ┼\\/|\\/ /   ", "  |  /    ", "   / \\      ", "{2}" };

        public EnemyGenerator(Player player)
        {
            this.player = player;

        }

        BasicEnemy[] forestEnemy = new BasicEnemy[3];
        BasicEnemy[] grassLandEnemy = new BasicEnemy[3];
        BasicEnemy[] waterEnemy = new BasicEnemy[3];
        BasicEnemy boss;

        ///
        /// GRASSLAND ENEMIES
        ///
        void GrassLandEnemiesEasy()
        {
            grassLandEnemy[0] = new BasicEnemy("  Spearman   ", 13, 2, 1, 5, 10, 5, spearmanArt); //Weak Spearman
            grassLandEnemy[1] = new BasicEnemy("  Swordman   ", 10, 3, 1, 5, 10, 5, swordmanArt); //Weak Swordman;
            grassLandEnemy[2] = new BasicEnemy("   Archer    ", 5, 6, 0, 5, 5, 5, archerArt); //Weak Archer;

        }
        void GrassLandEnemiesMedium()
        {
            grassLandEnemy[0] = new BasicEnemy("  Spearman   ", 20, 8, 5, 15, 15, 10, spearmanArt); //Mid Spearman
            grassLandEnemy[1] = new BasicEnemy("  Swordman   ", 18, 10, 6, 10, 15, 10, swordmanArt); //Mid Swordman;
            grassLandEnemy[2] = new BasicEnemy("   Archer    ", 15, 12, 2, 15, 10, 10, archerArt); //Mid Archer;

        }
        void GrassLandEnemiesHard()
        {
            grassLandEnemy[0] = new BasicEnemy("Sword_Master ", 25, 18, 6, 20, 20, 20, swordMasterArt); //Hard Spearman
            grassLandEnemy[1] = new BasicEnemy("   Giant    ", 30, 14, 8, 20, 30, 15, giantArt); //Hard Swordman;
            grassLandEnemy[2] = new BasicEnemy("  Wizard    ", 30, 16, 5, 25, 20, 20, wizardArt); //Hard Archer;

        }
        void GrassLandEnemiesMax()
        {
            grassLandEnemy[0] = new BasicEnemy("Sword_Master ", 45, 22, 10, 25, 40, 25, swordMasterArt); //S.Hard Spearman
            grassLandEnemy[1] = new BasicEnemy("   Giant    ", 50, 18, 12, 25, 40, 25, giantArt); //S.Hard Swordman;
            grassLandEnemy[2] = new BasicEnemy("  Wizard    ", 40, 20, 8, 30, 40, 25, wizardArt); //S.Hard Archer;

        }

        ///
        /// FOREST ENEMIES
        ///
        void ForestEnemiesEasy()
        {
            forestEnemy[0] = new BasicEnemy("    Wolf     ", 10, 4, 1, 5, 10, 5, wolfArt); 
            forestEnemy[1] = new BasicEnemy("   Jaguar    ", 7, 3, 0, 10, 10, 5, jaguarArt);
            forestEnemy[2] = new BasicEnemy("    Snake    ", 5, 2, 0, 5, 5, 5, snakeArt); 
        }
        void ForestEnemiesMedium()
        {
            forestEnemy[0] = new BasicEnemy("    Wolf     ", 22, 10, 5, 15, 15, 10, wolfArt); 
            forestEnemy[1] = new BasicEnemy("   Jaguar    ", 19, 8, 2, 20, 15, 10, jaguarArt); 
            forestEnemy[2] = new BasicEnemy("    Snake    ", 16, 8, 2, 15, 10, 10, snakeArt); 

        }
        void ForestEnemiesHard()
        {
            forestEnemy[0] = new BasicEnemy("    Bird     ", 20, 18, 5, 25, 30, 15, birdArt);
            forestEnemy[1] = new BasicEnemy("   Shrub     ", 25, 16, 5, 25, 20, 20, shrubArt);
            forestEnemy[2] = new BasicEnemy("  Treeguard  ", 30, 16, 8, 15, 20, 20, treeguardArt); 
        }
        void ForestEnemiesMax()
        {
            forestEnemy[0] = new BasicEnemy("    Bird     ", 40, 22, 8, 25, 30, 25, birdArt);
            forestEnemy[1] = new BasicEnemy("   Shrub     ", 45, 18, 8, 25, 30, 25, shrubArt);
            forestEnemy[2] = new BasicEnemy("  Treeguard  ", 50, 18, 12, 30, 40, 25, treeguardArt);
        }
        ///
        /// WATER ENEMIES - EASY
        ///
        void WaterEnemiesEasy()
        {

            waterEnemy[0] = new BasicEnemy("  Fat Frog   ",10,6,0,5,5,5, fatFrogArt);
            waterEnemy[1] = new BasicEnemy("    Fish     ",5,3,0,10,10,5, fishArt);
            waterEnemy[2] = new BasicEnemy("   Lobster   ",15,2,4,5,10,5, lobsterArt); 
        }
        void WaterEnemiesMedium()
        {

            waterEnemy[0] = new BasicEnemy("  Fat Frog   ", 18, 12, 4, 15, 10, 10, fatFrogArt); 
            waterEnemy[1] = new BasicEnemy("    Fish     ", 15, 10, 4, 20, 15, 10, fishArt); 
            waterEnemy[2] = new BasicEnemy("   Lobster   ", 25, 8, 6, 15, 15, 10, lobsterArt); 
        }
        void WaterEnemiesHard()
        {

            waterEnemy[0] = new BasicEnemy("    Squid    ", 20, 16, 4, 25, 30, 15, squidArt); 
            waterEnemy[1] = new BasicEnemy("    Crab     ", 35, 4, 8, 20, 20, 20, crabArt); 
            waterEnemy[2] = new BasicEnemy("  Giant_Bug  ", 25, 2, 4, 25, 20, 20, waterbugArt); 
        }
        void WaterEnemiesMax()
        {

            waterEnemy[0] = new BasicEnemy("    Squid    ", 40, 20, 8, 30, 30, 25, squidArt);
            waterEnemy[1] = new BasicEnemy("    Crab     ", 60, 16, 14, 25, 40, 25, crabArt);
            waterEnemy[2] = new BasicEnemy("  Giant_Bug  ", 30, 22, 5, 30, 30, 25, waterbugArt);
        }

        //BOSS
        void Boss()
        {
            boss = new BasicEnemy("Demon Lord", 70, 25, 15, 35, 100, 1000, demonLordArt);
        }

        /// <summary>
        /// GRASS LANDS ENCOUNTER EASY
        /// </summary>
        public void GrassLandEncounter(int danger)
        {
            //Decide how many enemies
            int enemies = rand.Next(1, 3);
            //Choose enemy type
            BasicEnemy[] theEnemies = new BasicEnemy[enemies];
            switch (danger)
            {
                case 1:
                    for (int i = 0; i < enemies; i++)
                    {
                        GrassLandEnemiesEasy();
                        Console.WriteLine(grassLandEnemy[i].Name);
                        int rnd = rand.Next(0, grassLandEnemy.Length);
                        theEnemies[i] = grassLandEnemy[rnd];
                        
                    }
                    break;
                case 2:
                    for (int i = 0; i < enemies; i++)
                    {
                        GrassLandEnemiesMedium();
                        int rnd = rand.Next(0, grassLandEnemy.Length);
                        theEnemies[i] = grassLandEnemy[rnd];
                    }
                    break;
                case 3:
                    for (int i = 0; i < enemies; i++)
                    {
                        GrassLandEnemiesHard();
                        int rnd = rand.Next(0, grassLandEnemy.Length);
                        theEnemies[i] = grassLandEnemy[rnd];
                    }
                    break;
                case 4:
                    for (int i = 0; i < enemies; i++)
                    {
                        GrassLandEnemiesMax();
                        int rnd = rand.Next(0, grassLandEnemy.Length);
                        theEnemies[i] = grassLandEnemy[rnd];
                    }
                    break;
            }
            //Start Battle
            battleManager = new BattleManager(player, theEnemies, false);
            battleManager.BattleLoop();
        }

        ///
        /// FOREST ENCOUNTERS EASY
        ///
        public void ForestEncounter(int danger)
        {
            //Decide how many enemies
            int enemies = rand.Next(1, 3);
            //Choose enemy type
            BasicEnemy[] theEnemies = new BasicEnemy[enemies];
            switch (danger)
            {
                case 1:
                    for (int i = 0; i < enemies; i++)
                    {
                        ForestEnemiesEasy();
                        int rnd = rand.Next(0, forestEnemy.Length);
                        theEnemies[i] = forestEnemy[rnd];
                    }
                    break;
                case 2:
                    for (int i = 0; i < enemies; i++)
                    {
                        ForestEnemiesMedium();
                        int rnd = rand.Next(0, forestEnemy.Length);
                        theEnemies[i] = forestEnemy[rnd];
                    }
                    break;
                case 3:
                    for (int i = 0; i < enemies; i++)
                    {
                        ForestEnemiesHard();
                        int rnd = rand.Next(0, forestEnemy.Length);
                        theEnemies[i] = forestEnemy[rnd];
                    }
                    break;
                case 4:
                    for (int i = 0; i < enemies; i++)
                    {
                        ForestEnemiesMax();
                        int rnd = rand.Next(0, forestEnemy.Length);
                        theEnemies[i] = forestEnemy[rnd];
                    }
                    break;
            }
            //Start Battle
            battleManager = new BattleManager(player, theEnemies, false);
            battleManager.BattleLoop();
        }

        /// <summary>
        /// Water Encounter
        /// </summary>

        public void WaterEncounter(int danger)
        {
            //Decide how many enemies
            int enemies = rand.Next(1, 3);
            //Choose enemy type
            BasicEnemy[] theEnemies = new BasicEnemy[enemies];
            switch (danger)
            {
                case 1:
                    for (int i = 0; i < enemies; i++)
                    {
                        WaterEnemiesEasy();
                        int rnd = rand.Next(0, waterEnemy.Length);
                        theEnemies[i] = waterEnemy[rnd];
                    }
                    break;
                case 2:
                    for (int i = 0; i < enemies; i++)
                    {
                        WaterEnemiesMedium();
                        int rnd = rand.Next(0, waterEnemy.Length);
                        theEnemies[i] = waterEnemy[rnd];
                    }
                    break;
                case 3:
                    for (int i = 0; i < enemies; i++)
                    {
                        WaterEnemiesHard();
                        int rnd = rand.Next(0, waterEnemy.Length);
                        theEnemies[i] = waterEnemy[rnd];
                    }
                    break;
                case 4:
                    for (int i = 0; i < enemies; i++)
                    {
                        WaterEnemiesMax();
                        int rnd = rand.Next(0, waterEnemy.Length);
                        theEnemies[i] = waterEnemy[rnd];
                    }
                    break;
            }
            //Start Battle
            battleManager = new BattleManager(player, theEnemies, false);
            battleManager.BattleLoop();
        }

        public void BossBattle()
        {
            Boss();
            BasicEnemy[] theEnemies = new BasicEnemy[1];
            theEnemies[0] = boss;
            battleManager = new BattleManager(player, theEnemies, true);
            battleManager.BattleLoop();
        }


        //FOR TESTING ONLY
        public void forceEncounter(int enemies)
        {
            Console.WriteLine("Forest encounter");
            Console.ReadLine();
            BasicEnemy[] theEnemies = new BasicEnemy[enemies];
            for (int i = 0; i < enemies; i++)
            {
                ForestEnemiesHard();
                int rnd = rand.Next(0, forestEnemy.Length);
                theEnemies[i] = forestEnemy[rnd];
            }

            //Start Battle
            battleManager = new BattleManager(player, theEnemies, true);
            battleManager.BattleLoop();
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
namespace RPG_Project
{

    enum BattleState
    {
        PlayerTurn = 0,
        EnemyOneTurn,
        EnemyTwoTurn,
        EnemyThreeTurn,
        DeterminingState,
        End
    }


    class BattleManager
    {

        string[] bottomButtons = new string[] { "", "[ 1) Attack ]    [ 2) Abilites ]", "[ 3) Items  ]    [ 4) Run      ]" };
        readonly string[] bottumPotionList = new string[] {"[ 1) Normal-HP Potion: {0}] [ 2) Super-HP Potion: {1} ] [ 2) Mega-HP Potion: {2}]", "[ 1) Normal-PP Potion: {0}] [ 2) Super-PP Potion: {1} ] [ 2) Mega-PP Potion: {2}]","TmpBack" };
        string[] bottomFleeButtons = new string[] { "Are you sure you want to retreat?", "[1) Yes       2) No]" };
        string[] potionListCopy = new string[3];
        string[] bottomAttacks;
        string[] bottomAbilitiesList = new string[] {"[ 1){0}] [ 2){1} ] [ 3){2}]", "[ 4){0}] [ 5){1} ] [ 6){2}]","TmpBack"};

        static int SPEED_TO_MOVE = 50;


        Player player;
        BasicEnemy[] enemy;
        Printer printer = new Printer();

        int xpFromBattle;
        int moneyFromBattle;
        int deadEnemies;

        BattleState current = BattleState.DeterminingState;

        bool fleeing = false;
        bool bossBattle = false;

        int[] CombatentsSpeed = new int[4];

        public BattleManager(Player player, BasicEnemy[] newenemy, bool bossBattle)
        {
            this.player = player;
            this.bossBattle = bossBattle;
            enemy = new BasicEnemy[newenemy.Length];
            for (int i = 0; i < newenemy.Length; i++)
            {
                Console.WriteLine("Enemy {0} set", newenemy[i].Name);
                this.enemy[i] = newenemy[i];
            }
        }


        public void BattleLoop()
        {
            PlayMusic(true);
            while (true)
            {
                switch (current)
                {
                    case BattleState.PlayerTurn:
                        //TODO have player make choice
                        printer.PrintSingle("It's your move!");
                        PlayerChoice();
                        current = BattleState.DeterminingState;
                        break;
                    case BattleState.EnemyOneTurn:
                        //Determins if enemy attacks and sends damage to player
                        if(enemy[0].Health > 0)
                        {
                            player.RcvDamage(enemy[0].TakeAction(player));
                            Console.ReadLine();
                        }
                        current = BattleState.DeterminingState;
                        //TODO Print message that enemy attackes
                        break;
                    case BattleState.EnemyTwoTurn:
                        //Determins if enemy attacks and sends damage to player
                        if(enemy[1].Health > 0)
                        {
                            player.RcvDamage(enemy[1].TakeAction(player));
                            Console.ReadLine();
                        }
                        current = BattleState.DeterminingState;
                        //TODO Print message that enemy attackes
                        break;
                    case BattleState.EnemyThreeTurn:
                        //Determins if enemy attacks and sends damage to player
                        if(enemy[2].Health > 0)
                        {
                            player.RcvDamage(enemy[2].TakeAction(player));
                            Console.ReadLine();
                        }
                        current = BattleState.DeterminingState;
                        //TODO Print message that enemy attackes
                        break;
                    case BattleState.DeterminingState:
                        UpdateBoard();
                        SwitchState();
                        break;
                    case BattleState.End:
                        if (fleeing)
                        {
                            printer.PrintSingle("You ran away!", true, true);
                            Console.ReadLine();
                            return;
                        }
                        if (player.HasDied)
                        {
                            printer.PrintSingle("Oh no... you died", true, true);
                            Console.ReadLine();
                            return;
                        }
                        player.Exp += xpFromBattle;
                        player.Money += moneyFromBattle;
                        printer.PrintSingle("You win!", true, false);
                        string rewards = string.Format("You gained {0} money!", moneyFromBattle);
                        printer.PrintSingle(rewards, false, false);
                        rewards = string.Format("You gained {0} experience!", xpFromBattle);
                        printer.PrintSingle(rewards, false, true);
                        Console.ReadLine();
                        PlayMusic(false);
                        return;
                }
            }
        }

        void PlayMusic(bool play)
        {

            var soundPlayer = new SoundPlayer
            {
                SoundLocation = @"../../audio/combatMusic.wav"
            };
            var soundPlayer2 = new SoundPlayer
            {
                SoundLocation = @"../../audio/combatMusic.wav"
            };


            if (play)
            {
                try
                {
                    if (bossBattle)
                    {
                        soundPlayer2.PlayLooping();
                        return;
                    }
                    soundPlayer.PlayLooping();
                    return;
                }
                catch
                {

                }

            }
            soundPlayer.Stop();

        }

        void SwitchState()
        {
            //If all enemies or player is dead end combat
            deadEnemies = 0;
            foreach (Enemy allEnemies in enemy)
            {
                if (allEnemies.HasDied)
                {
                    deadEnemies++;
                }
                    
                if (deadEnemies == enemy.Length)
                {
                    current = BattleState.End;
                    return;
                }
            }

            //End battle if fleeing
            if (fleeing)
            {
                current = BattleState.End;
                return;
            }

            //Check if player has died
            if(player.Health <= 0)
            {
                player.HasDied = true;
                current = BattleState.End;
                return;
            }

            //Determine next move by speed. 
            //Currently the player or enemy can move multiple times in a row if they have high enough speed.
            while (true)
            {
                if (CombatentsSpeed[0] >= SPEED_TO_MOVE)
                {
                    current = BattleState.PlayerTurn;
                    CombatentsSpeed[0] -= SPEED_TO_MOVE;
                    return;
                }

                for (int i = 0; i < enemy.Length; i++)
                {
                    if (CombatentsSpeed[i + 1] >= SPEED_TO_MOVE)
                    {
                        current = (BattleState)i + 1;
                        CombatentsSpeed[i + 1] -= SPEED_TO_MOVE;
                        return;
                    }
                }

                //Increase cumulative speed
                CombatentsSpeed[0] += player.Speed;
                for (int i = 0; i < enemy.Length; i++)
                {
                    if (enemy[i].HasDied)
                    {
                        CombatentsSpeed[i + 1] = 0;
                    }
                    else
                    {
                        CombatentsSpeed[i + 1] += enemy[0].Speed;
                    }
                }
            }
        }

        void UpdateBoard()
        {
            Console.Clear();
            string[][] centeredObjects = new string[enemy.Length][];

            //Update and pack enemies
            for (int i = 0; i < enemy.Length; i++)
            {
                centeredObjects[i] = printer.PrepareString(enemy[i].EnemyArt, enemy[i].Health, enemy[i].MaxHealth, enemy[i].Name);
            }
            //Print top data
            printer.PrintTopScreen(centeredObjects);

            //Update and pack player
            printer.PrintMiddleScreen(printer.PrepareString(player.art, player.Health, player.MaxHealth, player.Name, player.Energy, player.MaxEnergy));

            //Print bottom interface
            printer.PrintBottomScreen(bottomButtons);
        }

        void UpdateBoard(int choice)
        {
            Console.Clear();
            string[][] centeredObjects = new string[enemy.Length][];

            //Update and pack enemies
            for (int i = 0; i < enemy.Length; i++)
            {
                centeredObjects[i] = printer.PrepareString(enemy[i].EnemyArt, enemy[i].Health, enemy[i].MaxHealth, enemy[i].Name);
            }
            //Print top data
            printer.PrintTopScreen(centeredObjects);

            //Update and pack player
            printer.PrintMiddleScreen(printer.PrepareString(player.art, player.Health, player.MaxHealth, player.Name, player.Energy, player.MaxEnergy));

            switch (choice)
            {
                case 1:

                    bottomAttacks = new string[3];
                    bottomAttacks[0] = "";
                    for(int i = 0; i < enemy.Length; i++)
                    {
                        bottomAttacks[1] += string.Format(" [ {0}): Attack {1} ] ", i+1, printer.RemoveWhitespace(enemy[i].Name));
                    }
                    bottomAttacks[2] = string.Format("[ {0}): Return to menu ]", enemy.Length+1);
                    printer.PrintBottomScreen(bottomAttacks);
                    break;

                case 2:
                    //TODO pull abilities list and print out options to use
                    string[,] abilitiesList = player.ReturnUnlockedAbilities();
                    for (int i = 0; i < bottomAbilitiesList.Length - 1; i++)
                    {
                        bottomAbilitiesList[i] = string.Format(bottomAbilitiesList[i], abilitiesList[i, 0], abilitiesList[i, 1], abilitiesList[i, 2]);
                    }
                    bottomAbilitiesList[2] = string.Format("[ 7): Return to menu ]");
                    printer.PrintBottomScreen(bottomAbilitiesList);
                    break;
                case 3:
                    //TODO pull list of items in inventory and options to use
                    int[,] potionList = player.ReturnPotions();
                    int count = 0;

                    //Copy string to standard string
                    foreach(string text in bottumPotionList)
                    {
                        potionListCopy[count] = text;
                        count++;
                    }

                    for (int i = 0; i < potionListCopy.Length-1; i++)
                    {
                        potionListCopy[i] = string.Format(potionListCopy[i], potionList[i,0], potionList[i, 1], potionList[i, 2]);
                    }
                    potionListCopy[2] = string.Format("[ 7): Return to menu ]");
                    printer.PrintBottomScreen(potionListCopy);
                    break;
                case 4:
                    printer.PrintBottomScreen(bottomFleeButtons);
                    break;
            }
        }

        //Waits for player input and then attacks
        //Depending on the number of enemies on screen selections have different effects
        void ChooseAttack()
        {
            bool choosing = true;
            while (choosing)
            {
                deadEnemies = 0;
                foreach (Enemy allEnemies in enemy)
                {
                    if (allEnemies.HasDied)
                    {
                        deadEnemies++;
                    }

                    if (deadEnemies == enemy.Length)
                    {
                        current = BattleState.End;
                        return;
                    }
                }

                int playerChoice = ReturnChoice();
                switch (playerChoice)
                {
                    case 1:
                        //Check if enemy is dead and prevent attacking it again.
                        if (enemy[0].HasDied)
                        {
                            PlayerChoice(1);
                        }
                        else
                        {
                            //Attack enemy
                            enemy[0].TakeDamage(player.AtkDamage(enemy[0]));
                            //if the enemy dies increase battle rewards
                            if (enemy[0].HasDied)
                            {
                                IncreaseBattleRewards(0);
                            }
                        }
                        choosing = false;
                        break;

                    case 2:
                        if (enemy.Length <= 1)
                        {
                            UpdateBoard();
                            PlayerChoice();
                            break;
                        }

                        if (enemy[1].HasDied)
                        {
                            PlayerChoice(1);
                        }
                        else
                        {
                            //Attack enemy
                            enemy[1].TakeDamage(player.AtkDamage(enemy[1]));
                            //if the enemy dies increase battle rewards
                            if (enemy[1].HasDied)
                            {
                                IncreaseBattleRewards(1);
                            }
                        }
                        choosing = false;
                        break;
                    case 3:
                        if (enemy.Length <= 2)
                        {
                            UpdateBoard();
                            PlayerChoice();
                            break;
                        }

                        if (enemy[2].HasDied)
                        {
                            PlayerChoice(1);
                        }
                        else
                        {
                            //Attack enemy
                            enemy[2].TakeDamage(player.AtkDamage(enemy[2]));
                            //if the enemy dies increase battle rewards
                            if (enemy[2].HasDied)
                            {
                                IncreaseBattleRewards(2);
                            }
                        }
                        choosing = false;
                        break;

                    case 4:
                        if(enemy.Length <= 3)
                        {
                            UpdateBoard();
                            PlayerChoice();
                            break;
                        }
                        break;

                    default:
                        UpdateBoard();
                        PlayerChoice();
                        break;
                }
            }
        }

        void ChooseAbility()
        {
            while (true)
            {
                string[,] abilities = player.ReturnUnlockedAbilities();
                int playerChoice = ReturnChoice();
                switch (playerChoice)
                {
                    //Self Heal
                    case 1:
                        if (abilities[0, 0] != "Locked" && player.Energy > 3)
                        {
                            printer.PrintSingle("Using " + (Abilities)2 + " ability");
                            player.UseAbility(1, enemy[0]);
                            player.Energy -= 3;
                            return;
                        }
                        PlayerChoice(2);
                        return;
                    //Shatter
                    case 2:
                        if (abilities[0, 1] != "Locked" && player.Energy > 4)
                        {
                            printer.PrintSingle("Using " + (Abilities)4 + " ability");
                            PlayerChoice(4);
                            ChooseAbilityTarget(playerChoice, 4);
                            return;
                        }
                        PlayerChoice(2);
                        return;

                    case 3:
                        if (abilities[0, 2] != "Locked")
                        {
                            printer.PrintSingle("Using " + (Abilities)6 + " ability");
                            PlayerChoice(4);
                            ChooseAbilityTarget(playerChoice, 4);
                            return;
                        }
                        PlayerChoice(2);
                        return;

                    case 4:
                        if (abilities[1, 0] != "Locked")
                        {
                            printer.PrintSingle("Using " + (Abilities)8 + " ability");
                            PlayerChoice(4);
                            ChooseAbilityTarget(playerChoice, 4);
                            return;
                        }
                        PlayerChoice(2);
                        return;

                    case 5:
                        if (abilities[1, 1] != "Locked")
                        {
                            PlayerChoice(4);
                            ChooseAbilityTarget(playerChoice, 10);
                            return;
                        }
                        PlayerChoice(2);
                        return;

                    case 6:
                        if (abilities[1, 2] != "Locked")
                        {
                            printer.PrintSingle("Using " + (Abilities)12 + " ability");
                            PlayerChoice(4);
                            ChooseAbilityTarget(playerChoice, 0);
                            return;
                        }
                        PlayerChoice(2);
                        return;

                    case 7:
                        UpdateBoard();
                        PlayerChoice();
                        return;

                    default:
                        //Exit on non valid number
                        UpdateBoard();
                        PlayerChoice();
                        return;
                }
            }
        }

        //TODO Something is not quite right here, often targets twice. need to look further into this.
        void ChooseAbilityTarget(int abilityChosen, int abilityCost)
        {
            bool choosing = true;
            while (choosing)
            {
                int playerChoice = ReturnChoice();
                switch (playerChoice)
                {

                    case 1:
                        //Check if enemy is dead and prevent attacking it again.
                        if (enemy[0].HasDied)
                        {
                            PlayerChoice(2);
                        }
                        else
                        {
                            //Attack enemy
                            enemy[0].TakeDamage(player.UseAbility(abilityChosen, enemy[0]));
                            player.Energy -= abilityCost;
                            //if the enemy dies increase battle rewards
                            if (enemy[0].HasDied)
                            {
                                IncreaseBattleRewards(0);
                            }
                        }
                        choosing = false;
                        break;
                    case 2:
                        if (enemy.Length <= 1)
                        {
                            UpdateBoard();
                            PlayerChoice();
                            break;
                        }
                        if (enemy[1].HasDied)
                        {
                            PlayerChoice(1);
                        }
                        else
                        {
                            enemy[1].TakeDamage(player.UseAbility(abilityChosen, enemy[1]));
                            player.Energy -= abilityCost;
                            if (enemy[1].HasDied)
                            {
                                IncreaseBattleRewards(1);
                            }
                        }
                        choosing = false;
                        break;

                    case 3:
                        if (enemy.Length <= 2)
                        {
                            UpdateBoard();
                            PlayerChoice();
                            break;
                        }
                        if (enemy[2].HasDied)
                        {
                            PlayerChoice(1);
                        }
                        else
                        {
                            enemy[2].TakeDamage(player.UseAbility(abilityChosen, enemy[2]));
                            player.Energy -= abilityCost;
                            if (enemy[2].HasDied)
                            {
                                IncreaseBattleRewards(2);
                            }
                        }
                        break;

                    case 4:
                        if (enemy.Length <= 3)
                        {
                            UpdateBoard();
                            PlayerChoice();
                            break;
                        }
                        break;

                    default:
                        UpdateBoard();
                        PlayerChoice();
                        break;
                }
            }
        }

        //Takes player input and uses potion if available.
        //NOT WORKING: Update the list shown on the battle screen
        //Fixed, it's an issue where it was overritting the formatting string. temp fix in place but it's ugly
        //If there is time will update
        void ChoosePotion()
        {
            while (true)
            {
                int[,] currentPotions = player.ReturnPotions();
                
                ////debugging, printInvent method is used to check list
                //player.PrintInvent();
                
                int playerChoice = ReturnChoice();
                switch (playerChoice)
                {
                    case 1:
                        if(currentPotions[0,0] != 0)
                        {
                            player.DrinkPotion(0);
                            return;
                        }
                        PlayerChoice(3);
                        return;

                    case 2:
                        if (currentPotions[0, 1] != 0)
                        {
                            player.DrinkPotion(1);
                            return;
                        }
                        PlayerChoice(3);
                        return;

                    case 3:
                        if (currentPotions[0, 2] != 0)
                        {
                            player.DrinkPotion(2);
                            return;
                        }
                        PlayerChoice(3);
                        return;

                    case 4:
                        if (currentPotions[1, 0] != 0)
                        {
                            player.DrinkPotion(3);
                            return;
                        }
                        PlayerChoice(3);
                        return;

                    case 5:
                        if (currentPotions[1, 1] != 0)
                        {
                            player.DrinkPotion(4);
                            return;
                        }
                        PlayerChoice(3);
                        return;

                    case 6:
                        if (currentPotions[1, 2] != 0)
                        {
                            player.DrinkPotion(5);
                            return;
                        }
                        PlayerChoice(3);
                        return;

                    case 7:
                        UpdateBoard();
                        PlayerChoice();
                        return;

                    default:
                        //Exit on non valid number
                        UpdateBoard();
                        PlayerChoice();
                        return;
                }
            }
        }

        void Flee()
        {
            while (true)
            {
                int playerChoice = ReturnChoice();
                switch (playerChoice)
                {
                    //Attack
                    case 1:
                        fleeing = true;
                        return;
                    //Abilitiy
                    case 2:
                        //ABILITIES
                        UpdateBoard();
                        PlayerChoice();
                        return;
                }
            }
        }

        void PlayerChoice()
        {
            while (true)
            {
                int playerChoice = ReturnChoice();
                switch (playerChoice)
                {
                    //Attack
                    case 1:
                        //Display enemies available to attack
                        UpdateBoard(playerChoice);
                        //Choose player to attack
                        ChooseAttack();
                        return;
                    //Abilitiy
                    case 2:
                        //TODO ADD ABILITIES
                        UpdateBoard(playerChoice);
                        //TODO: Let player use abilities
                        ChooseAbility();
                        return;
                    //Items
                    case 3:
                        UpdateBoard(playerChoice);
                        //TODO LET PLAYER USE ITEMS
                        ChoosePotion();
                        return;
                    //Run
                    case 4:
                        UpdateBoard(playerChoice);
                        Flee();
                        return;
                    //Catch for invalid input
                    default:
                        printer.PrintSingle("Please choose a valid selection");
                        break;
                }
            }
        }

        //Only to be called by internal functions. Used for resetting menus
        void PlayerChoice(int choice)
        {
            while (true)
            {
                switch (choice)
                {
                    //Attack
                    case 1:
                        //Display enemies available to attack
                        UpdateBoard(choice);
                        //Choose player to attack and notify player that their choice is dead
                        printer.PrintSingle("Choose a living target!");
                        ChooseAttack();
                        return;
                    //Abilitiy
                    case 2:
                        //Prompts player to choose a new ability
                        printer.PrintSingle("You can't use that ability");
                        ChooseAbility();
                        return;
                    //Items
                    case 3:
                        UpdateBoard(choice);
                        //Choose player to attack and notify player that their choice is dead
                        printer.PrintSingle("You don't have any of those potions!");
                        ChoosePotion();
                        return;
                    //Run
                    case 4:
                        UpdateBoard(1);
                        return;
                    //Catch for invalid input
                    default:
                        printer.PrintSingle("Please choose a valid selection");
                        break;
                }
            }
        }


        int ReturnChoice()
        {
            while (true)
            {
                
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch
                {
                    printer.PrintSingle("Invalid selection, please use a number!", true, true);
                }
            }
        }

        void IncreaseBattleRewards(int enemyPos)
        {
            xpFromBattle += enemy[enemyPos].ExpValue;
            moneyFromBattle += enemy[enemyPos].MoneyValue;
        }


    }
}

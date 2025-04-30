using System;
using System.Diagnostics;
using System.Media;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;
        private Random random;

        public Game()
        { 
            player = new Player("Player", 100); 
            currentRoom = new Room("This is an empty cell, only containing yourself.");
            random = new Random();
            Console.WriteLine("Welcome to Dungeon Explorer!");
            Console.WriteLine("You have been captured and thrown into a dungeon. Find a way to escape, fellow hero!");
        }
        // This is the startup message that the user will see when they start the game
        public object Start()
        {
            bool playing = true;

            while (playing)
            {
                Console.WriteLine("Choose an action");
                Console.WriteLine("");
                Console.WriteLine("1. Check room description");
                Console.WriteLine("2. Pick up an item");
                Console.WriteLine("3. Check inventory");
                Console.WriteLine("4. Check health");
                Console.WriteLine("5. Move to another room");
                Console.WriteLine("6. Exit game");
                // This is the UX section which allows the user to choose the action they want to take using a given integer
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine(currentRoom.GetDescription());
                        break;
                    //This option outputs to the user the description of the room they are in, code found in 'Room.cs'
                    case "2":
                        if (currentRoom.ItemPickedUp)
                        {
                            Console.WriteLine("There are no more items left in this room.");
                            break;
                        }
                        string[] items = {"armour", "cap", "sword", "axe", "key", "health potions"};
                        string randomItem = items[random.Next(items.Length)];
                        Console.WriteLine($"Looking around the room, you see a {randomItem}. Would you like to pick it up? (y/n)");
                        string pickUpChoice = Console.ReadLine();
                        if (pickUpChoice.ToLower() == "y")
                        {
                            player.PickUpItem(randomItem);
                            currentRoom.ItemPickedUp = true;
                        }
                        else
                        {
                            Console.WriteLine("You chose not to pick up the item.");
                        }
                        break;
                    //This option allows the user to pick up an item from the room, the item is then stored within the inventory which is found within 'Player.cs'
                    case "3":
                        Console.WriteLine("Inventory: ");
                        Console.WriteLine(player.InventoryContents());
                        break;
                    //This option allows the user to check their inventory, the inventory is stored within 'Player.cs'
                    case "4":
                        Console.WriteLine("Health: ");
                        Console.WriteLine(player.Health);
                        break;
                    //This option allows the user to check their health, the health is stored within 'Player.cs'
                    case "5":
                        MoveToNextRoom();
                        break;
                    //This option allows the user to move to the next room, the next room will be randomly generated
                    case "6":
                        playing = false;
                        Console.WriteLine("Exiting game...");
                        break;
                    //This option lets the player quit the game at any point
                    case "debug":
                        Testing.RunTests(player, currentRoom);
                        break;
                    //This hidden option does a quick debug to see if the game can function normally
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                    //This is a failsafe used to catch any invalid inputs
                }
            }
            return null;
        }

        private void MoveToNextRoom()
        {
            int nextRoomId = random.Next(1, 9);
            currentRoom = RoomList.CreateRoom(nextRoomId);

            Console.WriteLine("You move to the next room...");
            Console.WriteLine(currentRoom.GetDescription());
            //This is the logic that allows the player to move to the next room, the room is randomly generated and the description is outputted to the user, the user still has the option to check the room description again

            int roomCounter = RoomList.RoomCounter;

            if (nextRoomId == 5)
            {
                if (player.InventoryContents().Contains("key"))
                {
                    Console.WriteLine("You use the key to unlock the door and escape the dungeon!");
                    Console.WriteLine("Press Enter to Quit the game.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("The door is locked. You need a key to escape.");
                }
            }
            //This statement is in regard to the exit room, if the player has a key in their inventory then they can escape the dungeon. If not, then they cannot escape. This choice is made automatically in regards to the new game menu

            if (nextRoomId == 3)
            {
                Console.WriteLine("Prepare for battle...");
                CombatMenu("Zombie");
            }

            if (nextRoomId == 7)
            {
                Console.WriteLine("Its hollow eyes stare at you as it prepares for battle...");
                CombatMenu("Skeleton");
            }

            if (nextRoomId == 8)
            {
                Console.WriteLine("Its fiery gaze locks onto you as it prepares for battle...");
                CombatMenu("Dragon");
            }
            //Statements regarding specific rooms to start combat based on the RoomID

            if (roomCounter == 5)
            {
                Console.WriteLine("As you wander through these rooms, there is a slight suspicion that theres something up with this dungeon structure...");
            }
            else if (roomCounter == 10)
            {
                Console.WriteLine("You are definitely sure that there is something up with this dungeon, you swear that the rooms loop."); 
            }
            else if (roomCounter == 15)
            {
                Console.WriteLine("At this point, you can definitely confirm that the rooms are infact looping. You need to get out of here."); 
            }
            //Statements regarding the amount of rooms the player has been through, this is used to add more narrative to the game, emphesising the endlessness the game can have
        }
        
        public class Enemies : Entities
        {
            public Enemies(string name, int health, int minAttack, int maxAttack)
            {
                Name = name;
                Health = health;
                MinAttack = minAttack;
                MaxAttack = maxAttack;
            }

            public override void TakeDamage(int damage)
            {
                Health = Math.Max(0, Health - damage);
                Console.WriteLine($"{Name} takes {damage} damage! Remaining health: {Health}");
            }

            public int Attack()
            {
                return CalculateDamage();
            }
        }
        public void CombatMenu(string enemyName)
        {
            Enemies enemy = null;

            switch (enemyName.ToLower())
            {
                case "zombie":
                    enemy = new Enemies("Zombie", 20, 1, 8);
                    break;
                case "skeleton":
                    enemy = new Enemies("Skeleton", 30, 3, 6);
                    break;
                case "dragon":
                    enemy = new Enemies("Dragon", 100, 10, 20);
                    break;
                    //Stats for each possible enemy type
            }

            if (enemy == null)
            {
                Console.WriteLine("Error, enemy load error.");
                return;
            }

            bool inCombat = true;

            Console.WriteLine($"You are now in combat with {enemyName}.");
            //Instanciates starting logic to the player that the combat menu will show
            while (inCombat)
            {
                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Attack the enemy");
                Console.WriteLine("2. Use a health potion");

                string choice = Console.ReadLine();
                //Basic menu that is used only in combat

                switch (choice)
                {
                    case "1":
                        int playerDamage = player.CalculateDamage();
                        enemy.TakeDamage(playerDamage);
                        Console.WriteLine($"You attack the {enemyName} for {playerDamage} damage!");
                        if (enemy.Health <= 0)
                        {
                            Console.WriteLine($"You have defeated the {enemy.Name}!");
                            inCombat = false;
                        }
                        break;

                    case "2":
                        Console.WriteLine("You use a health potion.");
                        if (player.GetHealthPotionCount() > 0)
                        {
                            player.setHealth(player.Health + 30);
                            player.UseHealthPotion();
                            Console.WriteLine("You used a health potion and restored 30 health!");
                            Console.WriteLine($"You now have {player.Health} health and {player.GetHealthPotionCount()} health potions remaining.");
                        }
                        else
                        {
                            Console.WriteLine("You don't have any health potions left!");
                        }
                        break;
                    //Logic in regard to how health potions are handled. If the player has no health potions then the player cannot heal but their turn is registered. Health potions regenerate 30 health a time and is calculated before the enemy attacks

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                //The choices allows for the player to pick between using a health potion if they have any or just attacking the enemy 

                if (inCombat)
                {
                    if (enemy.Name.ToLower() == "skeleton")
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            int skeletonDamage = enemy.Attack();
                            player.TakeDamage(skeletonDamage);
                        }
                    }
                    else
                    {
                        int enemyDamage = enemy.Attack();
                        player.TakeDamage(enemyDamage);
                    }
                    //The enemy combat logic. The skeleton can attack twice on their turn, every other enemy can only attack once.

                    if (player.Health <= 0)
                    {
                        Console.WriteLine("You have been defeated! Game over.");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    //This checks if the player has been defeated, if so, the game will end

                    if (enemy.Health <= 0)
                    {
                        Console.WriteLine($"You have defeated the {enemy.Name}!");
                        inCombat = false;
                    }
                    //This checks if the enemy has been defeated, if so, the combat will end
                }
            }
        }
        public bool EndGame()
        {
            if (player.InventoryContents().Contains("key"))
            {
                Console.WriteLine("You have successfully escaped the dungeon!");
                return true;
            }
            else
            {
                return false;
            }
            //This is the ending clause which checks if the player has a key in their inventory which will allow them to get an ending
        }
    }
    internal static class Testing
    {
        public static void RunTests(Player player, Room room)
        {
            Debug.Assert(player != null, "Error: Player object should not be null");
            Debug.Assert(room != null, "Error: Room object should not be null");
            Debug.Assert(player.Health > 0, "Error: Player health should be greater than 0");
            Debug.Assert(player.Health < 101, "Error: Player health should not exceed its cap");
            Debug.Assert(!string.IsNullOrEmpty(room.GetDescription()), "Error: Room description should not be empty");
            Console.WriteLine("All tests passed.");
        }
    }
    // This is the testing class which will run the tests on the player and room objects
}
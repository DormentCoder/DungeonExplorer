using System;
using System.Diagnostics;
using System.Media;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        public Game()
        { 
            player = new Player("PlayerName", 100); 
            currentRoom = new Room("Starting Room"); 
            Console.WriteLine("Welcome to Dungeon Explorer!");
            Console.WriteLine("You have been captured and thrown into a dungeon. Instruct yourself on ways to escape, fellow hero!");
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
                Console.WriteLine("5. Attempt to leave the dungeon");
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
                        Console.WriteLine("Looking around the room, you see that you are in a room with limited spare goods which can help you on your journey. There is one sword, a singular key, and a rugged used cap on a stand. Which item do you pick up? (sword/cap/key): ");
                        string item = Console.ReadLine();
                        if (item == "sword" || item == "cap" || item == "key")
                        {
                            player.PickUpItem(item);
                            Console.WriteLine($"{item} has been added to your inventory.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid item. You can only pick up 'sword', 'cap', or 'key'.");
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
                        if (EndGame())
                        {
                            playing = false;
                            Console.WriteLine("You use the key to escape the dungeon.");
                        }
                        else
                        {
                            Console.WriteLine("You need a key to escape the dungeon.");
                        }
                        break;
                    //This option allows the user to attempt to leave the dungeon, if the user has the key in their inventory they will be able to leave the dungeon
                    case "6":
                        playing = false;
                        Console.WriteLine("Exiting game...");
                        break;
                    case "debug":
                        Testing.RunTests(player, currentRoom);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                        //This option allows the user to exit the game at any point
                }
            }
            return null;
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
}
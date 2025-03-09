using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();
        private string v;

        public Player(string name, int health) 
        {
            Name = name;
            Health = health;
        }
        //This object makes parameters based off variables to organise the player and their health

        public Player(string v)
        {
            this.v = v;
        }

        public void PickUpItem(string item)
        {
            inventory.Add(item);
        }
        //This object allows the player to pick up an item and store it within their inventory
        public string InventoryContents()
        {
            if (inventory.Count == 0)
            {
                return "Your inventory is empty.";
            }
            return string.Join(", ", inventory);
        }
        //This object store information relating to the players inventory
    }
}
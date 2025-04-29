using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; } 
        private List<string> inventory = new List<string>();
        public int MinAttack { get; private set; } = 0;
        public int MaxAttack { get; private set; } = 5;
        public int healthPotionCount = 0;
        public Player(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public void PickUpItem(string item)
        {
            inventory.Add(item);
            if (item.ToLower() == "health potions")
            {
                healthPotionCount += 3;
            }
            UpdateAttackValues();
        }
        //If the player picks up an item called 'health potions' then the player will receive 3 health potions and this will be added to this counter

        public string InventoryContents()
        {
            if (inventory.Count == 0)
            {
                return "Your inventory is empty.";
            }
            return string.Join(", ", inventory);
        }
        //Inventory Logic, If the player has items then it is displayed to the player in a list
        public int GetHealthPotionCount()
        {
            return healthPotionCount;
        }
        public void setHealth(int health)
        {
            Health = health;
        }
        public void UseHealthPotion()
        {
            if (healthPotionCount > 0)
            {
                healthPotionCount--;
            }
            //Logic for using health potions. If the player does use a health potion then the counter decreases by 1
        }
        private void UpdateAttackValues()
        {
            if (inventory.Contains("axe"))
            {
                MinAttack = 10;
                MaxAttack = 30;
            }
            else if (inventory.Contains("sword"))
            {
                MinAttack = 5;
                MaxAttack = 10;
            }
            else
            {
                MinAttack = 0;
                MaxAttack = 5;
            }
            //Item logics in regard to what item the player has equipped
        }
    }
}
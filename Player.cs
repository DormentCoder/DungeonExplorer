using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public abstract class Entities
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int MinAttack { get; protected set; }
        public int MaxAttack { get; protected set; }
        public abstract void TakeDamage(int damage);
        public int CalculateDamage()
        {
            Random random = new Random();
            return random.Next(MinAttack, MaxAttack + 1);
        }
    }
    public abstract class Item
    {
        public string Name { get; protected set; }
    }
    public class Potion : Item
    {
        public int HealAmount { get; private set; }

        public Potion(string name, int healAmount)
        {
            Name = name;
            HealAmount = healAmount;
        }
    }
    public class Equipment : Item
    {
        public int MinAttackBoost { get; private set; }
        public int MaxAttackBoost { get; private set; }

        public Equipment(string name, int minAttackBoost, int maxAttackBoost)
        {
            Name = name;
            MinAttackBoost = minAttackBoost;
            MaxAttackBoost = maxAttackBoost;
        }
    }
    public class Player : Entities
    {
        private List<string> inventory = new List<string>();
        public int healthPotionCount = 0;
        public Player(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public void PickUpItem(string item)
        {
            if (inventory.Contains(item))
            {
                // Refuse the action and display a message
                Console.WriteLine($"You already have {item} in your inventory. You cannot pick it up again.");
                return;
            }
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
        public override void TakeDamage(int damage)
        {
            Health = Math.Max(0, Health - damage);
            Console.WriteLine($"{Name} takes {damage} damage! Remaining health: {Health}");
        }
    }
}
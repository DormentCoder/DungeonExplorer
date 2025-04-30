using System;

namespace DungeonExplorer
{
    public class Room
    {
        private string description;
        public bool ItemPickedUp { get; set; } = false;

        public Room(string description)
        {
            this.description = description;
        }

        public string GetDescription()
        {
            return description;
        }
        //This command returns the room description to the player
    }

    public static class RoomList
    {
        private static int roomCounter = 0;
        public static int RoomCounter => roomCounter;
        public static Room CreateRoom(int roomId)
        {
        //This is a counter used to indicate how many rooms the player has navigated through. This is ran in the background for narration purposes
            roomCounter++;
            switch (roomId)
            {
                case 1:
                    return new Room("A dimly lit room with damp walls. You presume that this room might contain some items inside.");
                case 2:
                    return new Room("A room filled with old, broken furniture. Its quiet... very quiet...");
                case 3:
                    return new Room("A room with a chest inside. A zombie resides in this room.");
                case 4:
                    return new Room("A narrow hallway with flickering torches. The air feels cold.");
                case 5:
                    return new Room("An exit room with a locked door. You presume that you need a key to escape.");
                case 6:
                    return new Room("A hospital room with shelves of empth bottles, presumably health potions. You wonder if there is actually spare potions available.");
                case 7:
                    return new Room("A room with a skeleton standing in the corner, its hollow eyes stares at you as it walks towards you.");
                case 8:
                    return new Room("A grand chamber with a dragon resting in the center. Its eyes gaze upon you aggressively as you enter.");
                default:
                    return new Room("An empty, featureless room. It feels like this room is nothing of note to you, unless there is something hidden here, of course...");
                //The different room ID's which helps to make the game varied as the player progresses

            }
        }
    }
}
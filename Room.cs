namespace DungeonExplorer
{
    public class Room
    {
        private string description;

        public Room(string description)
        {
            this.description = description;
        }

        public string GetDescription()
        {
            description = "You are in a dark, dingey room underneath what appears to be the basement of a major castle network. You would wonder how you got here in the first place, however spending time on that would probably mean less time on getting out. There is a door infront of you which can allow for you to escape, however it is very clear that the door probably wont be unlocked for you.";
            return description;
        }
        //This file stores the information for the room that the player is in
    }
}
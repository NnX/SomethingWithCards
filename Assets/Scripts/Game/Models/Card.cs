namespace Game.Models
{
    public class Card
    {
        private int value;

        public Card(int value)
        {
            this.value = value;
        }

        public int GetValue()
        {
            return value;
        }
    }
}

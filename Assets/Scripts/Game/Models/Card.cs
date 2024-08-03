namespace Game.Models
{
    public class Card
    {
        public int Id { get; private set; }
        private readonly int _value;

        public Card(int value, int id)
        {
            Id = id;
            _value = value;
        }

        public int GetValue()
        {
            return _value;
        }
        
        public bool IsMatches(int value)
        {
            return _value == value;
        }
    }
}

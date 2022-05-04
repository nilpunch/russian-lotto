namespace RussianLotto.Networking
{
    public class Player : IPlayer
    {
        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}

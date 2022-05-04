namespace RussianLotto.Input
{
    public interface IInput
    {
        public IButtonElement ConnectToRandomRoom { get; }
        public IButtonElement LeaveRoom { get; }

        public ILobbyInput Lobby { get; }
    }
}

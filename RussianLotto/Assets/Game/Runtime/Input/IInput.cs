namespace RussianLotto.Input
{
    public interface IInput
    {
        public IMainMenuInput MainMenu { get; }
        public ISessionInput Session { get; }
    }
}

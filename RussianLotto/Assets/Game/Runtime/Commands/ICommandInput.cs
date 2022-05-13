namespace RussianLotto.Command
{
    public interface ICommandInput<out T>
    {
        public bool HasUnreadCommands { get; }

        public T ReadCommand();
    }
}

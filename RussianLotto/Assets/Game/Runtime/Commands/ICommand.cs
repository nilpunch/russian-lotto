namespace RussianLotto.Command
{
    public interface ICommand<in T>
    {
        public void Execute(T target);
    }
}

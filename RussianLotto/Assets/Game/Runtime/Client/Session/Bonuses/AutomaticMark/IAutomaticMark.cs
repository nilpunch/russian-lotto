namespace RussianLotto.Client
{
    public interface IAutomaticMark
    {
        public int MarksAvailable { get; }
        public void Mark(int amount);
    }
}

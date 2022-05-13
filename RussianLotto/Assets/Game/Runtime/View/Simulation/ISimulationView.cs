namespace RussianLotto.View
{
    public interface ISimulationView
    {
        public IBoardView Board { get; }
        public IAvailableNumbersView AvailableNumbers { get; }
    }
}

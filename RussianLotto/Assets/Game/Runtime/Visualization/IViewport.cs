using RussianLotto.Client;

namespace RussianLotto.View
{
    public interface IViewport
    {
        public IBoardView BoardView { get; }
        public IAvailableNumbersView AvailableNumbers { get; }
    }
}

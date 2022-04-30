using RussianLotto.Client;

namespace RussianLotto.View
{
    public interface IViewport
    {
        public IBoardView BoardView { get; }
        public IAvailableNumbersView AvailableNumbers { get; }
    }

    public interface ITextInput
    {
        public void HasInput();
        public string Read();
    }
}

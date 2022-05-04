namespace RussianLotto.Input
{
    public interface ITextInputElement : IInputElement
    {
        public void HasInput();
        public string Read();
    }
}

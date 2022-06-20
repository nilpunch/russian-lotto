using RussianLotto.Client;

namespace RussianLotto.Command
{
    public class ChangeCardCommand : ISessionCommand
    {
        private readonly int _card;

        public ChangeCardCommand(int card)
        {
            _card = card;
        }

        public void Execute(ISession target)
        {
            target.TryChangeCardToNewOne(_card);
        }
    }
}

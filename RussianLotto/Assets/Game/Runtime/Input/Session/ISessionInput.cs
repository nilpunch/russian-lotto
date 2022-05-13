using RussianLotto.Command;

namespace RussianLotto.Input
{
    public interface ISessionInput
    {
        public ICommandInput<ISessionCommand> Commands { get; }
    }
}

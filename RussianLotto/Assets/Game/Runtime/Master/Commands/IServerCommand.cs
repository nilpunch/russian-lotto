using RussianLotto.Client;
using RussianLotto.Command;

namespace RussianLotto.Master
{
    public interface IServerCommand : ICommand<MasterClient>
    {
    }
}

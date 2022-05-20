using RussianLotto.Command;
using RussianLotto.Master;

namespace RussianLotto.Networking
{
    public interface IMasterRoom : IRoom
    {
        ICommandInput<IServerCommand> MasterInput { get; }

        public void SendToClients(object command);

        public void OpenToJoin();

        public void CloseJoining();
    }
}

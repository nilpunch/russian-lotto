using System;
using RussianLotto.Client;
using RussianLotto.Command;
using RussianLotto.Master;

namespace RussianLotto.Networking
{
    public interface IRoom : IReadOnlyRoom, IDisposable
    {
        ICommandInput<ISessionCommand> SessionInput { get; }

        public void SendToServer(IServerCommand command);
        public void SendToClients(object command);

        public void OpenToJoin();
        public void CloseJoining();

        public void EnterRandom(GameType gameType, bool shuffled);
        public void Exit();
    }
}

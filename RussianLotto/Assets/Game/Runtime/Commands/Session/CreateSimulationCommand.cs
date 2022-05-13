using RussianLotto.Client;
using RussianLotto.Networking;

namespace RussianLotto.Command
{
    public class CreateSimulationCommand : ISessionCommand, ISerialization, IDeserialization
    {
        private int _seed;
        private GameType _gameType;
        private bool _shuffled;

        public CreateSimulationCommand()
        {
        }

        public CreateSimulationCommand(int seed, GameType gameType, bool shuffled)
        {
            _seed = seed;
            _gameType = gameType;
            _shuffled = shuffled;
        }

        public void Execute(ISession target)
        {
            target.GenerateSimulation(_seed, _gameType, _shuffled);
        }

        public void Serialize(IWriteHandle writeHandle)
        {
            writeHandle.WriteInt(_seed);
            writeHandle.WriteByte((byte)_gameType);
            writeHandle.WriteBool(_shuffled);
        }

        public void Deserialize(IReadHandle readHandle)
        {
            _seed = readHandle.ReadInt();
            _gameType = (GameType)readHandle.ReadByte();
            _shuffled = readHandle.ReadBool();
        }
    }
}

using RussianLotto.Save;

namespace RussianLotto.Master
{
    public class FinishMasterGameCommand : IServerCommand, ISerialization, IDeserialization
    {
        public void Execute(IMasterSimulation target)
        {
            if (target.MasterRoomState == MasterRoomState.GameSimulation)
                target.FinishGame();
        }

        public void Serialize(IWriteHandle writeHandle)
        {
        }

        public void Deserialize(IReadHandle readHandle)
        {
        }
    }
}

using RussianLotto.Client;
using RussianLotto.Networking;

namespace RussianLotto.Command
{
    public class DeleteSimulationCommand : ISessionCommand, ISerialization, IDeserialization
    {
        public DeleteSimulationCommand()
        {
        }

        public void Execute(ISession target)
        {
            if (target.HasSimulation)
                target.DeleteSimulation();
        }

        public void Serialize(IWriteHandle writeHandle)
        {
        }

        public void Deserialize(IReadHandle readHandle)
        {
        }
    }
}

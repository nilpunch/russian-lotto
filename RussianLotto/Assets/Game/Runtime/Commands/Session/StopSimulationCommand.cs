using RussianLotto.Client;
using RussianLotto.Networking;

namespace RussianLotto.Command
{
    public class StopSimulationCommand : ISessionCommand, ISerialization, IDeserialization
    {
        public StopSimulationCommand()
        {
        }

        public void Execute(ISession target)
        {
            if (target.HasSimulation && target.Simulation.State == SimulationState.Game)
                target.Simulation.FinishGame();
        }

        public void Serialize(IWriteHandle writeHandle)
        {
        }

        public void Deserialize(IReadHandle readHandle)
        {
        }
    }
}

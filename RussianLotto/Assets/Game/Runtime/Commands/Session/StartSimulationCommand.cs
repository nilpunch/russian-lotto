using RussianLotto.Client;
using RussianLotto.Networking;

namespace RussianLotto.Command
{
    public class StartSimulationCommand : ISessionCommand, ISerialization, IDeserialization
    {
        public StartSimulationCommand()
        {
        }

        public void Execute(ISession target)
        {
            if (target.HasSimulation && target.Simulation.State == SimulationState.Idle)
                target.Simulation.StartGame();
        }

        public void Serialize(IWriteHandle writeHandle)
        {
        }

        public void Deserialize(IReadHandle readHandle)
        {
        }
    }
}

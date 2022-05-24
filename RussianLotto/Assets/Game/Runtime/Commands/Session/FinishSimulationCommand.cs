using RussianLotto.Client;
using RussianLotto.Save;

namespace RussianLotto.Command
{
    public class FinishSimulationCommand : ISessionCommand, ISerialization, IDeserialization
    {
        public void Execute(ISession target)
        {
            if (target.HasSimulation && target.Simulation.State == SimulationState.Game)
            {
                target.Simulation.FinishGame();
            }
        }

        public void Serialize(IWriteHandle writeHandle)
        {
        }

        public void Deserialize(IReadHandle readHandle)
        {
        }
    }
}

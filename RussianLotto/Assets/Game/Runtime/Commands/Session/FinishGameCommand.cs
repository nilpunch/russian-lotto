using RussianLotto.Client;

namespace RussianLotto.Command
{
    public class FinishGameCommand : ISessionCommand
    {
        public FinishGameCommand()
        {
        }

        public void Execute(ISession target)
        {
            if (target.Simulation.State == SimulationState.Game)
                target.Simulation.FinishGame();
        }
    }
}

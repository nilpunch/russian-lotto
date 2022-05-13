using BananaParty.BehaviorTree;
using RussianLotto.Command;
using RussianLotto.Networking;

namespace RussianLotto.Master
{
    public class StartClientsSimulationNode : BehaviorNode
    {
        private readonly INetwork _network;

        public StartClientsSimulationNode(INetwork network)
        {
            _network = network;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (!Started)
            {
                _network.Room.SendToClients(new CreateSimulationCommand(
                    UnityEngine.Random.Range(int.MinValue, int.MaxValue), _network.Room.GameType,
                    _network.Room.ShuffledMode));
            }


            return BehaviorNodeStatus.Success;
        }
    }
}

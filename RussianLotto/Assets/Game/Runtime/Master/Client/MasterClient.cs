using BananaParty.BehaviorTree;
using RussianLotto.Behavior;
using RussianLotto.Client;
using RussianLotto.Networking;
using RussianLotto.View;

namespace RussianLotto.Master
{
    public class MasterClient : IClient, IVisualization<ITreeGraph<IReadOnlyBehaviorNode>>
    {
        private readonly BehaviorNode _behaviorTree;

        public MasterClient(IMasterNetwork masterNetwork)
        {
            _behaviorTree = new SequenceNode(new IBehaviorNode[]
            {
                new SequenceNode(new IBehaviorNode[]
                {
                    new IsConnectedToServer(masterNetwork),
                    new IsEnteredRoom(masterNetwork.Room),
                    new IsBecameMasterClientNode(masterNetwork),
                }, true, "MasterClientDetection"),

                new WaitNode(3000),
                new StartClientsSimulationNode(masterNetwork),
                new ConstantNode(BehaviorNodeStatus.Running)
            }, true, "MasterClient");
        }

        public void ExecuteFrame(long time)
        {
            if (_behaviorTree.Finished)
                _behaviorTree.Reset();

            _behaviorTree.Execute(time);
        }

        public void Visualize(ITreeGraph<IReadOnlyBehaviorNode> view)
        {
            _behaviorTree.WriteToGraph(view);
        }
    }
}

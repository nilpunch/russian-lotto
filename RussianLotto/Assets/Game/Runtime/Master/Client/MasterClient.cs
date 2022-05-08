using BananaParty.BehaviorTree;
using RussianLotto.Behavior;
using RussianLotto.Networking;
using RussianLotto.View;

namespace RussianLotto.Client
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
                    new IsConnectedToServer(masterNetwork.Socket),
                    new IsEnteredRoom(masterNetwork.Room),
                    new IsBecameMasterClientNode(masterNetwork.Master),
                }, true, "MasterClientDetection"),

                new ConstantNode(BehaviorNodeStatus.Running),

                // new RepeatNode
                // (
                //     new SelectorNode(new IBehaviorNode[]
                //     {
                //         new WaitNode(1000),
                //     }, true, "MasterClientLoop")
                // )
            }, true, "MasterClient");
        }

        public void ExecuteFrame(long time)
        {
            if (_behaviorTree.Finished)
                _behaviorTree.Reset();

            _behaviorTree.Execute(time);
        }

        public void Dispose()
        {
        }

        public void Visualize(ITreeGraph<IReadOnlyBehaviorNode> view)
        {
            _behaviorTree.WriteToGraph(view);
        }
    }
}

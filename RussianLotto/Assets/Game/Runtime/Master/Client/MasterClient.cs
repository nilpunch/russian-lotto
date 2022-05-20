using BananaParty.BehaviorTree;
using RussianLotto.Behavior;
using RussianLotto.Client;
using RussianLotto.Networking;
using RussianLotto.Tools;
using RussianLotto.View;

namespace RussianLotto.Master
{
    public class MasterClient : IClient, IVisualization<ITreeGraph<IReadOnlyBehaviorNode>>
    {
        private readonly BehaviorNode _behaviorTree;

        public MasterClient(IMasterNetwork masterNetwork, IReadOnlySession readOnlySession)
        {
            MasterSimulation masterSimulation = new MasterSimulation(masterNetwork, readOnlySession);

            _behaviorTree = new SequenceNode(new IBehaviorNode[]
            {
                new SequenceNode(new IBehaviorNode[]
                {
                    new IsConnectedToServer(masterNetwork),
                    new IsEnteredRoomNode(masterNetwork.Room),
                    new IsBecameMasterClientNode(masterNetwork),
                }, false, "MasterClientDetection"),

                new RestoreMasterDataNode(masterSimulation),

                new ParallelSequenceNode(new IBehaviorNode[]
                {
                    new SequenceNode(new IBehaviorNode[]
                    {
                        new IsConnectedToServer(masterNetwork),
                        new IsEnteredRoomNode(masterNetwork.Room),
                        new IsBecameMasterClientNode(masterNetwork),
                    }, true, "DisconnectDetection").RepeatUntil(BehaviorNodeStatus.Failure),

                    new SequenceNode(new IBehaviorNode[]
                    {
                        new SelectorNode(new IBehaviorNode[]
                        {
                            new IsMasterGamePreparationNode(masterSimulation),
                            new IsMasterGameStartedNode(masterSimulation),
                            new IsMasterGameFinishedNode(masterSimulation),
                            new RoomHasPlayersAmountNode(masterNetwork.Room, 2),
                        }, false, "RoomAwaiting").RepeatUntil(BehaviorNodeStatus.Success),

                        new SelectorNode(new IBehaviorNode[]
                        {
                            new IsMasterGamePreparationNode(masterSimulation),
                            new IsMasterGameStartedNode(masterSimulation),
                            new IsMasterGameFinishedNode(masterSimulation),

                            new WaitNode(3000).Invert(),
                            new PrepairMasterGameNode(masterSimulation),
                        }),

                        new SelectorNode(new IBehaviorNode[]
                        {
                            new IsMasterGameStartedNode(masterSimulation),
                            new IsMasterGameFinishedNode(masterSimulation),
                            new WaitNode(3000).Invert(),
                            new StartMasterGameNode(masterSimulation),
                        }),

                        new ParallelSelectorNode(new IBehaviorNode[]
                        {
                            new IsMasterGameFinishedNode(masterSimulation).RepeatUntil(BehaviorNodeStatus.Success),

                            new ExecuteCommandsNode<MasterSimulation>(masterNetwork.MasterRoom.MasterInput, masterSimulation,
                                "Network").Repeat(),
                        }),

                        new WaitNode(5000),

                        new ResetMasterGameNode(masterSimulation),
                    }, false, "MasterClientLoop").Repeat(),
                }),
            }, false, "MasterClient");
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

﻿using BananaParty.BehaviorTree;
using RussianLotto.Behavior;
using RussianLotto.Client;
using RussianLotto.Networking;
using RussianLotto.Tools;

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
                    }, false, "DisconnectDetection").RepeatUntil(BehaviorNodeStatus.Failure),

                    new SequenceNode(new IBehaviorNode[]
                    {
                        new SelectorNode(new IBehaviorNode[]
                        {
                            new IsMasterGamePreparationNode(masterSimulation),
                            new IsMasterGameStartedNode(masterSimulation),
                            new IsMasterGameFinishedNode(masterSimulation),
                            new RoomHasMinPlayersAmountToStartNode(masterNetwork.Room),
                        }, false, "RoomAwaiting").RepeatUntil(BehaviorNodeStatus.Success),

                        new SelectorNode(new IBehaviorNode[]
                        {
                            new IsMasterGamePreparationNode(masterSimulation),
                            new IsMasterGameStartedNode(masterSimulation),
                            new IsMasterGameFinishedNode(masterSimulation),

                            new WaitNode(5000).Invert(),
                            new RoomHasMinPlayersAmountToStartNode(masterNetwork.Room).Invert(),
                            new PrepairMasterGameNode(masterSimulation),
                        }),

                        new SelectorNode(new IBehaviorNode[]
                        {
                            new IsMasterGameStartedNode(masterSimulation),
                            new IsMasterGameFinishedNode(masterSimulation),
                            new WaitNode(5000).Invert(),
                            new StartMasterGameNode(masterSimulation),
                        }),

                        new ClearCommandsNode<MasterSimulation>(masterNetwork.MasterRoom.MasterInput),

                        new ParallelSelectorNode(new IBehaviorNode[]
                        {
                            new IsMasterGameFinishedNode(masterSimulation).RepeatUntil(BehaviorNodeStatus.Success),

                            new ExecuteCommandsNode<MasterSimulation>(masterNetwork.MasterRoom.MasterInput, masterSimulation,
                                "Network").Repeat(),
                        }),

                        new WaitNode(10000),

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

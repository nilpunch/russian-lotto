using System;
using BananaParty.BehaviorTree;
using RussianLotto.Behavior;
using RussianLotto.Input;
using RussianLotto.Networking;
using RussianLotto.Save;
using RussianLotto.Tools;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public class LocalClient : IClient, IVisualization<ITreeGraph<IReadOnlyBehaviorNode>>
    {
        private readonly BehaviorNode _behaviorTree;
        private readonly Session _session;

        public LocalClient(IOfflineMoneyEarn offlineMoneyEarn, INetwork network, IViewport viewport, IInput input)
        {
            _session = new Session(offlineMoneyEarn, new GameSaves());

            _behaviorTree = new SequenceNode(new IBehaviorNode[]
            {
                new SequenceNode(new IBehaviorNode[]
                {
                    new DeactivateInputNode(input.MainMenu.ConnectToRandomRoom),
                    new DeactivateInputNode(input.MainMenu.ShuffledSwitch),
                    new DeactivateInputNode(input.MainMenu.GameTypeSwitch),
                    new DeactivateInputNode(input.MainMenu.BetSwitch),
                }, false, "DeactivateAllInputs"),

                new SelectorNode(new IBehaviorNode[]
                {
                    new IsConnectedToServer(network),

                    new TimeoutNode
                    (
                        new ConnectToServer(network),
                        3000
                    ),
                }, false, "ConnectToServer"),

                new ParallelSequenceNode(new IBehaviorNode[]
                {
                    new SelectorNode(new IBehaviorNode[]
                    {
                        new IsConnectedToServer(network),
                    }).RepeatUntil(BehaviorNodeStatus.Failure),

                    new SelectorNode(new IBehaviorNode[]
                    {
                        new RenderNode<IWalletView, ISession>(viewport.WalletView, _session),
                    }).Repeat(),

                    new SequenceNode(new IBehaviorNode[]
                    {
                        new SwitchScreenToNode(viewport.ScreensPresentation, Screen.MainMenu),

                        new SelectorNode(new IBehaviorNode[]
                        {
                            new IsEnteredRoomNode(network.Room),

                            new ActivateInputNode(input.MainMenu.ShuffledSwitch).Invert(),
                            new ActivateInputNode(input.MainMenu.GameTypeSwitch).Invert(),
                            new ActivateInputNode(input.MainMenu.ConnectToRandomRoom).Invert(),
                            new ActivateInputNode(input.MainMenu.BetSwitch).Invert(),

                            new WaitButtonClickNode(input.MainMenu.ConnectToRandomRoom).Invert(),

                            new DeactivateInputNode(input.MainMenu.ConnectToRandomRoom).Invert(),
                            new DeactivateInputNode(input.MainMenu.ShuffledSwitch).Invert(),
                            new DeactivateInputNode(input.MainMenu.GameTypeSwitch).Invert(),
                            new DeactivateInputNode(input.MainMenu.BetSwitch).Invert(),

                            new HasMoneyToBetNode(_session, input.MainMenu.BetSwitch).Invert(),

                            new TimeoutNode
                            (
                                new EnterRandomRoomNode(network.Room, input.MainMenu.ShuffledSwitch, input.MainMenu.GameTypeSwitch),
                                3000
                            ),
                        }, true, "FindGame"),

                        new HasMoneyToBetNode(_session, input.MainMenu.BetSwitch),

                        new ParallelSequenceNode(new IBehaviorNode[]
                        {
                            new SelectorNode(new IBehaviorNode[]
                            {
                                new IsEnteredRoomNode(network.Room),

                                new SelectorNode(new IBehaviorNode[]
                                {
                                    new IsSessionHasSimulationNode(_session).Invert(),
                                    new DeleteSimulationNode(_session).Invert(),
                                    new LoseBankNode(_session),
                                }).Invert(),
                            }).RepeatUntil(BehaviorNodeStatus.Failure),

                            new SequenceNode(new IBehaviorNode[]
                            {
                                new ExecuteCommandsNode<ISession>(network.Room.SessionInput, _session, "Network"),
                            }, false, "ExecuteCommands").Repeat(),

                            new SelectorNode(new IBehaviorNode[]
                            {
                                new ActivateInputNode(input.MainMenu.LeaveRoom).Invert(),
                                new IsButtonPressedNode(input.MainMenu.LeaveRoom).Invert(),
                                new ExitRoomNode(network.Room).Invert(),
                            }, false, "ExitFromRoom").Repeat(),

                            new SequenceNode(new IBehaviorNode[]
                            {
                                new SwitchScreenToNode(viewport.ScreensPresentation, Screen.Room),

                                new IsSessionHasSimulationNode(_session),

                                new HasMoneyToBetNode(_session, input.MainMenu.BetSwitch),
                                new BetNode(_session, input.MainMenu.BetSwitch),
                                new MultiplyBetByPlayersAmountNode(_session, network.Room),

                                new ParallelSelectorNode(new IBehaviorNode[]
                                {
                                    new IsSessionHasSimulationNode(_session).Invert().RepeatUntil(BehaviorNodeStatus.Success),

                                    new IsSimulationFinishedNode(_session).RepeatUntil(BehaviorNodeStatus.Success),

                                    new ExecuteCommandsNode<ISession>(input.Session.Commands, _session, "Player").Repeat(),

                                    new SequenceNode(new IBehaviorNode[]
                                    {
                                        new SwitchScreenToNode(viewport.ScreensPresentation, Screen.Preparation),

                                        new IsSimulationPlayingNode(_session).RepeatUntil(BehaviorNodeStatus.Success),

                                        new SwitchScreenToNode(viewport.ScreensPresentation, Screen.Game),

                                        new ExecuteSimulationFrameNode(_session).Repeat(),
                                    }),
                                }, "Simulation"),

                                new SelectorNode(new IBehaviorNode[]
                                {
                                    new IsSessionHasSimulationNode(_session),
                                    new LoseBankNode(_session).Invert(),
                                    new ExitRoomNode(network.Room).Invert()
                                }),

                                new SwitchScreenToNode(viewport.ScreensPresentation, Screen.Results),

                                new RenderNode<IWinOrLoseView, ISession>(viewport.WinOrLoseView, _session),

                                new SelectorNode(new IBehaviorNode[]
                                {
                                    new IsPlayerWinNode(_session),
                                    new LoseBankNode(_session),
                                }),

                                new SelectorNode(new IBehaviorNode[]
                                {
                                    new IsPlayerWinNode(_session).Invert(),
                                    new CollectBankNode(_session).Invert(),
                                    new NotifyServerPlayerWinNode(network.Room)
                                }),

                                new ActivateInputNode(input.Session.Revenge),

                                new ParallelSelectorNode(new IBehaviorNode[]
                                {
                                    new IsSessionHasSimulationNode(_session).Invert().RepeatUntil(BehaviorNodeStatus.Success),

                                    new SequenceNode(new IBehaviorNode[]
                                    {
                                        new IsButtonPressedNode(input.Session.Revenge, true),
                                        new DeactivateInputNode(input.Session.Revenge)
                                    }).RepeatUntil(BehaviorNodeStatus.Success).Invert()
                                }),

                                new DeactivateInputNode(input.Session.Revenge),

                                new SelectorNode(new IBehaviorNode[]
                                {
                                    new IsButtonPressedNode(input.Session.Revenge),
                                    new ExitRoomNode(network.Room)
                                }),
                            }, false, "SessionLoop").Repeat(),

                            new ParallelSelectorNode(new IBehaviorNode[]
                            {
                                new SequenceNode(new IBehaviorNode[]
                                {
                                    new IsSessionHasSimulationNode(_session),
                                    new RenderNode<ISimulationView, ISession>(viewport.SimulationView, _session)
                                }, false, "SimulationRendering").Repeat(),

                                new SequenceNode(new IBehaviorNode[]
                                {
                                    new RenderNode<IPlayersView, IPlayers>(viewport.PlayersView, network.Room)
                                }, false, "PlayersRendering").Repeat(),

                            }, "Rendering"),
                        }, "RoomLoop"),
                    }).Repeat(),
                }, "ApplicationLoop"),
            }, false, "ApplicationPreparation");
        }

        /// <summary>
        /// Access for master only to restoring its state on master client switch.
        /// </summary>
        public IReadOnlySession Session => _session;

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

        public void Save()
        {
            _session.Save();
        }
    }
}

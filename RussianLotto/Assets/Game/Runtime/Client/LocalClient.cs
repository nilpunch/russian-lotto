using BananaParty.BehaviorTree;
using RussianLotto.Behavior;
using RussianLotto.Input;
using RussianLotto.Networking;
using RussianLotto.Tools;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public class LocalClient : IClient, IVisualization<ITreeGraph<IReadOnlyBehaviorNode>>
    {
        private readonly BehaviorNode _behaviorTree;
        private readonly Session _session;

        public LocalClient(INetwork network, IViewport viewport, IInput input)
        {
            _session = new Session();

            _behaviorTree = new SequenceNode(new IBehaviorNode[]
            {
                new SequenceNode(new IBehaviorNode[]
                {
                    new SequenceNode(new IBehaviorNode[]
                    {
                        new DeactivateInputNode(input.MainMenu.LeaveRoom),
                        new DeactivateInputNode(input.MainMenu.ConnectToRandomRoom),
                        new DeactivateInputNode(input.MainMenu.ShuffledSwitch),
                        new DeactivateInputNode(input.MainMenu.GameTypeSwitch),
                    }, false, "ResetAllInputs"),

                    new SelectorNode(new IBehaviorNode[]
                    {
                        new IsConnectedToServer(network),

                        new TimeoutNode
                        (
                            new ConnectToServer(network),
                            3000
                        ),
                    }, true, "ConnectToServer"),

                    new SwitchScreenToNode(viewport.ScreensPresentation, Screen.MainMenu),

                    new SelectorNode(new IBehaviorNode[]
                    {
                        new IsEnteredRoomNode(network.Room),

                        new SelectorNode(new IBehaviorNode[]
                        {
                            new IsSessionHasSimulationNode(_session).Invert(),
                            new DeleteSimulationNode(_session),
                        }).Invert(),

                        new ActivateInputNode(input.MainMenu.ShuffledSwitch).Invert(),
                        new ActivateInputNode(input.MainMenu.GameTypeSwitch).Invert(),
                        new ActivateInputNode(input.MainMenu.ConnectToRandomRoom).Invert(),

                        new WaitButtonClickNode(input.MainMenu.ConnectToRandomRoom).Invert(),

                        new DeactivateInputNode(input.MainMenu.ConnectToRandomRoom).Invert(),
                        new DeactivateInputNode(input.MainMenu.ShuffledSwitch).Invert(),
                        new DeactivateInputNode(input.MainMenu.GameTypeSwitch).Invert(),

                        new TimeoutNode
                        (
                            new EnterRandomRoomNode(network.Room, input.MainMenu.ShuffledSwitch, input.MainMenu.GameTypeSwitch),
                            3000
                        ),
                    }, true, "FindGame"),

                    new SequenceNode(new IBehaviorNode[]
                    {
                        new DeactivateInputNode(input.MainMenu.ConnectToRandomRoom),
                        new DeactivateInputNode(input.MainMenu.ShuffledSwitch),
                        new DeactivateInputNode(input.MainMenu.GameTypeSwitch),
                    }, false, "ResetAllInputs"),

                    new ParallelSelectorNode(new IBehaviorNode[]
                    {
                        new SequenceNode(new IBehaviorNode[]
                        {
                            new ExecuteCommandsNode<ISession>(network.Room.SessionInput, _session, "Network"),
                        }, false, "ExecuteCommands").Repeat(),

                        new SequenceNode(new IBehaviorNode[]
                        {
                            new ActivateInputNode(input.MainMenu.LeaveRoom),
                            new IsButtonPressedNode(input.MainMenu.LeaveRoom),
                            new ExitRoomNode(network.Room),
                        }, false, "ExitFromRoom").RepeatUntil(BehaviorNodeStatus.Success),

                        new SequenceNode(new IBehaviorNode[]
                        {
                            new SelectorNode(new IBehaviorNode[]
                            {
                                new IsSessionHasSimulationNode(_session),
                                new SwitchScreenToNode(viewport.ScreensPresentation, Screen.Room).Invert(),
                                new ConstantNode(BehaviorNodeStatus.Failure)
                            }, true),

                            new ParallelSelectorNode(new IBehaviorNode[]
                            {
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
                                new IsPlayerWinNode(_session).Invert(),
                                new NotifyServerPlayerWinNode(network.Room)
                            }),

                            new SwitchScreenToNode(viewport.ScreensPresentation, Screen.Results),

                            new ConstantNode(BehaviorNodeStatus.Running),
                        }, true, "SessionLoop").Repeat(),

                        new ParallelSelectorNode(new IBehaviorNode[]
                        {
                            new SequenceNode(new IBehaviorNode[]
                            {
                                new IsSessionHasSimulationNode(_session),
                                new RenderSimulationNode(viewport.SimulationView, _session)
                            }, false, "SimulationRendering").Repeat(),

                            new SequenceNode(new IBehaviorNode[]
                            {
                                new RenderPlayersNode(viewport.PlayersView, network.Room)
                            }, false, "PlayersRendering").Repeat(),

                        }, "Rendering"),
                    }, "RoomLoop"),
                }, true, "ApplicationLoop"),
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
    }
}

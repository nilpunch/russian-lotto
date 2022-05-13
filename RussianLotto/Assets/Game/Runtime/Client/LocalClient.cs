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

        public LocalClient(INetwork network, IViewport viewport, IInput input)
        {
            var session = new Session();

            // In progress
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

                        new SequenceNode(new IBehaviorNode[]
                        {
                            new DeactivateInputNode(input.MainMenu.LeaveRoom),
                            new DeactivateInputNode(input.MainMenu.ConnectToRandomRoom),
                            new DeactivateInputNode(input.MainMenu.ShuffledSwitch),
                            new DeactivateInputNode(input.MainMenu.GameTypeSwitch),
                        }, false, "ResetAllInputs").Invert(),

                        new TimeoutNode
                        (
                            new ConnectToServer(network),
                            3000
                        ),
                    }, true, "ConnectToServer"),

                    new SwitchScreenToNode(viewport.Presentation, Screen.MainMenu),

                    new SelectorNode(new IBehaviorNode[]
                    {
                        new IsEnteredRoom(network.Room),

                        new SelectorNode(new IBehaviorNode[]
                        {
                            new IsSessionHasSimulationNode(session).Invert(),
                            new DeleteSimulationNode(session),
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

                    new SwitchScreenToNode(viewport.Presentation, Screen.Room),

                    new ParallelSelectorNode(new IBehaviorNode[]
                    {
                        new SequenceNode(new IBehaviorNode[]
                        {
                            new ExecuteCommandsNode<ISession>(network.Room.SessionInput, session, "Network"),
                        }, false, "ExecuteCommands").Repeat(),

                        new SequenceNode(new IBehaviorNode[]
                        {
                            new ActivateInputNode(input.MainMenu.LeaveRoom),
                            new IsButtonPressedNode(input.MainMenu.LeaveRoom),
                            new ExitRoomNode(network.Room),
                        }, false, "ExitFromRoom").Repeat(BehaviorNodeStatus.Success),

                        new SequenceNode(new IBehaviorNode[]
                        {
                            new IsSessionHasSimulationNode(session),

                            new SwitchScreenToNode(viewport.Presentation, Screen.Preparation),
                            new WaitNode(5000),

                            new SwitchScreenToNode(viewport.Presentation, Screen.Game),
                            new StartSimulationNode(session),

                            new ParallelSelectorNode(new IBehaviorNode[]
                            {
                                new IsSimulationFinishedNode(session).Repeat(BehaviorNodeStatus.Success),
                                new ExecuteCommandsNode<ISession>(input.Session.Commands, session, "Player").Repeat(),
                                new ExecuteSimulationFrameNode(session).Repeat(),
                            }),

                            new SwitchScreenToNode(viewport.Presentation, Screen.Results),

                            new WaitNode(5000),

                            new ExitRoomNode(network.Room),
                        }, false, "SessionLoop").Repeat(BehaviorNodeStatus.Success),

                        new SelectorNode(new IBehaviorNode[]
                        {
                            new SequenceNode(new IBehaviorNode[]
                            {
                                new IsSessionHasSimulationNode(session),
                                new RenderSimulationNode(viewport.SimulationView, session)
                            }),

                            new ConstantNode(BehaviorNodeStatus.Success)
                        }, false, "Rendering").Repeat(),
                    }, "RoomLoop"),
                }, true, "ApplicationLoop"),
            }, false, "ApplicationPreparation");
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

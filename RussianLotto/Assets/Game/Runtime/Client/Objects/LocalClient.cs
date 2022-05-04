using BananaParty.BehaviorTree;
using RussianLotto.Application;
using RussianLotto.Behavior;
using RussianLotto.Input;
using RussianLotto.Networking;
using RussianLotto.Tools;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public class LocalClient : IClient, IClientContext, IVisualization<ITreeGraph<IReadOnlyBehaviorNode>>
    {
        private readonly BehaviorNode _behaviorTree;

        public LocalClient(INetwork network, IViewport viewport, IInput input)
        {
            var session = new Session();

            _behaviorTree = new SequenceNode(new IBehaviorNode[]
            {
                new SequenceNode(new IBehaviorNode[]
                {
                    new SelectorNode(new IBehaviorNode[]
                    {
                        new IsConnectedToServer(network.Socket),

                        new DeactivateInputNode(input.LeaveRoom).Invert(),
                        new DeactivateInputNode(input.ConnectToRandomRoom).Invert(),
                        new DeactivateInputNode(input.Lobby.ShuffledSwitch).Invert(),
                        new DeactivateInputNode(input.Lobby.GameTypeSwitch).Invert(),

                        new ConstantNode(BehaviorNodeStatus.Success)
                    }, true, "DisableInputs"),

                    new SelectorNode(new IBehaviorNode[]
                    {
                        new IsConnectedToServer(network.Socket),

                        new TimeoutNode
                        (
                            new ConnectToServer(network.Socket),
                            3000
                        ),
                    }, true, "ConnectToServer"),

                    new SelectorNode(new IBehaviorNode[]
                    {
                        new IsEnteredRoom(network.Room),

                        new ActivateInputNode(input.Lobby.ShuffledSwitch).Invert(),
                        new ActivateInputNode(input.Lobby.GameTypeSwitch).Invert(),

                        new WaitButtonClickNode(input.ConnectToRandomRoom).Invert(),

                        new DeactivateInputNode(input.Lobby.ShuffledSwitch).Invert(),
                        new DeactivateInputNode(input.Lobby.GameTypeSwitch).Invert(),

                        new TimeoutNode
                        (
                            new EnterRandomRoomNode(network.Room, input.Lobby.ShuffledSwitch, input.Lobby.GameTypeSwitch),
                            3000
                        ),
                    }, true, "FindGame"),

                    new DeactivateInputNode(input.Lobby.ShuffledSwitch),
                    new DeactivateInputNode(input.Lobby.GameTypeSwitch),

                    new WaitButtonClickNode(input.LeaveRoom),

                    new ExitRoomNode(network.Room),

                    new ConstantNode(BehaviorNodeStatus.Running),

                    // new SequenceNode(new IBehaviorNode[]
                                    // {
                                    //
                                    //     new RepeatNode
                                    //     (
                                    //         new SimulationRenderingNode(viewport.SimulationView, session.Simulation)
                                    //     ),
                                    //
                                    //     new WaitNode(1000),
                                    // }, true, "GameLoop")
                }, true, "ApplicationLoop"),
            }, false, "ApplicationPreparation");
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

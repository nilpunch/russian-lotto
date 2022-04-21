using BananaParty.BehaviorTree;
using Photon.Realtime;
using RussianLotto.Application;
using RussianLotto.Networking;

namespace RussianLotto.Client
{
    public class MasterClient : IClient
    {
        private readonly LoadBalancingClient _photonClient;
        private readonly BehaviorNode _behaviorTree;

        public MasterClient(LoadBalancingClient photonClient, AppSettings appSettings)
        {
            _photonClient = photonClient;

            var socket = new PhotonSocket(photonClient, appSettings);
            var roomNetwork = new RoomNetwork(socket);

            _behaviorTree = new SequenceNode(new IBehaviorNode[]
            {
                new SelectorNode(new IBehaviorNode[]
                {
                    new IsConnectedToServer(socket),
                    new TimeoutNode
                    (
                        new ConnectToServer(socket),
                        5000
                    ),
                    new WaitNode(1000),
                }, true, "ConnectToServer"),

                new SelectorNode(new IBehaviorNode[]
                {
                    new IsConnectedToRoom(roomNetwork),
                    new TimeoutNode
                    (
                        new ConnectToRandomRoom(roomNetwork),
                        5000
                    ),
                    new WaitNode(1000),
                }, true, "ConnectToRandomRoom"),

                new WaitNode(10000),
            }, true, "ApplicationLoop");
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
    }
}

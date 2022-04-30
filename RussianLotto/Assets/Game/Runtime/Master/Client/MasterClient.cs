using BananaParty.BehaviorTree;
using Photon.Realtime;
using RussianLotto.Application;
using RussianLotto.Networking;

namespace RussianLotto.Client
{
    public class MasterClient : IClient
    {
        private readonly BehaviorNode _behaviorTree;

        public MasterClient(IMasterNetwork masterNetwork)
        {
            _behaviorTree = new SequenceNode(new IBehaviorNode[]
            {
                new SequenceNode(new IBehaviorNode[]
                {
                    new IsConnectedToServer(masterNetwork.Socket),
                    new IsConnectedToRoom(masterNetwork.Room),
                    new IsBecameMasterClientNode(masterNetwork),
                }, true, "MasterClientRegistration"),

                new SelectorNode(new IBehaviorNode[]
                {
                    new WaitNode(1000),
                }, true, "MasterClientLoop"),
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
    }
}

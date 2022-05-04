using Photon.Realtime;

namespace RussianLotto.Networking
{
    public class PhotonMaster : IMaster
    {
        private readonly LoadBalancingClient _loadBalancingClient;

        public PhotonMaster(LoadBalancingClient loadBalancingClient)
        {
            _loadBalancingClient = loadBalancingClient;
        }

        public bool IsMasterClient => _loadBalancingClient.LocalPlayer.IsMasterClient;

        public void DispatchCommands()
        {
            _loadBalancingClient.Service();
        }
    }
}

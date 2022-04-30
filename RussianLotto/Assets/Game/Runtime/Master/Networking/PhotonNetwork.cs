using Photon.Realtime;

namespace RussianLotto.Networking
{
    public class PhotonNetwork : IMasterNetwork
    {
        private readonly LoadBalancingClient _loadBalancingClient;

        public PhotonNetwork(LoadBalancingClient loadBalancingClient, AppSettings appSettings)
        {
            _loadBalancingClient = loadBalancingClient;
            Socket = new PhotonSocket(loadBalancingClient, appSettings);
            Room = new PhotonRoom(Socket, loadBalancingClient);
        }

        public ISocket Socket { get; }
        public IRoom Room { get; }

        public bool IsMasterClient => _loadBalancingClient.LocalPlayer.IsMasterClient;
    }
}

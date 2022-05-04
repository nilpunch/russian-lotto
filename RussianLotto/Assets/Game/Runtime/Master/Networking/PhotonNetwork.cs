using Photon.Realtime;

namespace RussianLotto.Networking
{
    public class PhotonNetwork : IMasterNetwork
    {
        public PhotonNetwork(LoadBalancingClient loadBalancingClient, AppSettings appSettings)
        {
            Socket = new PhotonSocket(loadBalancingClient, appSettings);
            Room = new PhotonRoom(6, Socket, loadBalancingClient);
            Master = new PhotonMaster(loadBalancingClient);
        }

        public ISocket Socket { get; }
        public IRoom Room { get; }
        public IMaster Master { get; }
    }
}

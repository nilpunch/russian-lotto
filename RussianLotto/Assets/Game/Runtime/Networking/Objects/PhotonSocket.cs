using System;
using Photon.Realtime;

namespace RussianLotto.Networking
{
    public class PhotonSocket : ISocket
    {
        private readonly AppSettings _appSettings;
        private readonly LoadBalancingClient _photonClient;

        public PhotonSocket(LoadBalancingClient photonClient, AppSettings appSettings)
        {
            _appSettings = appSettings;
            _photonClient = photonClient;
        }

        public bool IsConnected => _photonClient.IsConnectedAndReady;

        public bool HasUnreadPayloadQueue { get; }

        public byte[] ReadPayloadQueue()
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            _photonClient.ConnectUsingSettings(_appSettings);
        }

        public void Send(byte[] payloadBytes)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            _photonClient.Disconnect();
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}

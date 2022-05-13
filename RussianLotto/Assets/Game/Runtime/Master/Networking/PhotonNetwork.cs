using System;
using System.Collections.Generic;
using Photon.Realtime;
using RussianLotto.Master;

namespace RussianLotto.Networking
{
    public class PhotonNetwork : IMasterNetwork, IConnectionCallbacks
    {
        private readonly LoadBalancingClient _loadBalancingClient;
        private readonly AppSettings _appSettings;

        public PhotonNetwork(LoadBalancingClient loadBalancingClient, AppSettings appSettings)
        {
            _loadBalancingClient = loadBalancingClient;
            _appSettings = appSettings;
            _loadBalancingClient.AddCallbackTarget(this);

            Room = new PhotonRoom(6, loadBalancingClient);
        }

        public IRoom Room { get; }

        public bool IsConnected { get; private set; }

        public bool IsMasterClient => _loadBalancingClient.LocalPlayer.IsMasterClient;

        public void Connect()
        {
            if (IsConnected)
                throw new InvalidOperationException();

            _loadBalancingClient.AppId = _appSettings.AppIdRealtime;
            _loadBalancingClient.AppVersion = _appSettings.AppVersion;
            _loadBalancingClient.ConnectToRegionMaster(_appSettings.FixedRegion);
        }

        public void Disconnect()
        {
            if (!IsConnected)
                throw new InvalidOperationException();

            _loadBalancingClient.Disconnect();
        }

        public void Dispose()
        {
            if (IsConnected)
                Disconnect();
            _loadBalancingClient.RemoveCallbackTarget(this);
        }

        public void DispatchCommands()
        {
            _loadBalancingClient.Service();
        }

        #region PhotonCallbacks

        public void OnConnected()
        {
            IsConnected = true;
        }

        public void OnConnectedToMaster()
        {
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            IsConnected = false;
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

namespace RussianLotto.Networking
{
    public class PhotonSocket : ISocket, IConnectionCallbacks
    {
        private readonly AppSettings _appSettings;
        private readonly LoadBalancingClient _photonClient;

        public PhotonSocket(LoadBalancingClient photonClient, AppSettings appSettings)
        {
            _appSettings = appSettings;
            _photonClient = photonClient;
            _photonClient.AddCallbackTarget(this);
            _photonClient.StateChanged += OnClientStateChanged;
        }

        public void Dispose()
        {
            Disconnect();
            _photonClient.RemoveCallbackTarget(this);
            _photonClient.StateChanged -= OnClientStateChanged;
        }

        private void OnClientStateChanged(ClientState arg1, ClientState arg2)
        {
            //Debug.Log(arg1 + " -> " + arg2);
        }

        public bool IsConnected { get; private set; }

        public bool HasUnreadPayloadQueue { get; }

        public byte[] ReadPayloadQueue()
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            // if (_photonClient.ReconnectAndRejoin())
            //     return;

            _photonClient.AppId = _appSettings.AppIdRealtime;
            _photonClient.AppVersion = _appSettings.AppVersion;
            _photonClient.ConnectToRegionMaster(_appSettings.FixedRegion);
        }

        public void Send(byte[] payloadBytes)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            if (_photonClient.IsConnected)
                _photonClient.Disconnect();
        }

        public void OnConnected()
        {
            IsConnected = true;
            Debug.Log("Connected!");


        }

        public void OnConnectedToMaster()
        {
            Debug.Log("Connected to Master!");
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            IsConnected = false;
            Debug.Log($"Disconnected due to: {cause.ToString()}");
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
            //throw new NotImplementedException();
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            //throw new NotImplementedException();
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
            //throw new NotImplementedException();
        }
    }
}

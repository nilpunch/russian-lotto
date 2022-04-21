using System;
using BananaParty.BehaviorTree;
using Photon.Realtime;
using RussianLotto.Client;
using RussianLotto.Networking;
using UnityEngine;

namespace RussianLotto.Application
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private ViewportRoot _viewport = null;
        [SerializeField] private AppSettings _photonSettings = null;

        private IClient _localClient;
        private IClient _masterClient;

        private void Awake()
        {
            var loadBalancingClient = new LoadBalancingClient();
            var socket = new PhotonSocket(loadBalancingClient, _photonSettings);
            _localClient = new LocalClient(socket, _viewport);

            _masterClient = new MasterClient(loadBalancingClient, _photonSettings);
        }

        private void Update()
        {
            long time = (long)(Time.time * 1000f);

            _masterClient.ExecuteFrame(time);
            _localClient.ExecuteFrame(time);
        }

        private void OnDestroy()
        {
            _masterClient.Dispose();
            _localClient.Dispose();
        }
    }
}

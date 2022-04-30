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
            var networking = new PhotonNetwork(new LoadBalancingClient(), _photonSettings);
            _localClient = new LocalClient(networking, _viewport);
            _masterClient = new MasterClient(networking);
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

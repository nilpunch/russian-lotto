using Photon.Realtime;
using RussianLotto.Client;
using RussianLotto.Networking;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Application
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private BehaviorTreeView _localClientTree = null;
        [SerializeField] private BehaviorTreeView _masterClientTree = null;

        [Space, SerializeField] private ViewportRoot _viewport = null;
        [SerializeField] private InputRoot _inputRoot = null;
        [SerializeField] private AppSettings _photonSettings = null;

        private LocalClient _localClient;
        private MasterClient _masterClient;
        private IMasterNetwork _network;

        private void Start()
        {
            _photonSettings.AppVersion = UnityEngine.Application.version;
            _network = new PhotonNetwork(new LoadBalancingClient(), _photonSettings);
            _localClient = new LocalClient(_network, _viewport, _inputRoot);
            _masterClient = new MasterClient(_network);
        }

        private void Update()
        {
            _network.Master.DispatchCommands();

            long time = (long)(Time.time * 1000f);

            _masterClient.ExecuteFrame(time);
            _localClient.ExecuteFrame(time);

            if (_localClientTree != null)
            {
                _localClient.Visualize(_localClientTree);
                _localClientTree.Project();
            }

            if (_masterClientTree != null)
            {
                _masterClient.Visualize(_masterClientTree);
                _masterClientTree.Project();
            }
        }

        private void OnDestroy()
        {
            _network.Room.Dispose();
            _network.Socket.Dispose();
            _masterClient.Dispose();
            _localClient.Dispose();
        }
    }
}

using ExitGames.Client.Photon;
using Photon.Realtime;
using RussianLotto.Client;
using RussianLotto.Command;
using RussianLotto.Input;
using RussianLotto.Master;
using RussianLotto.Networking;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Application
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private BehaviorTreeView _localClientTree;
        [SerializeField] private BehaviorTreeView _masterClientTree;

        [Space, SerializeField] private ViewportRoot _viewport;
        [SerializeField] private InputRoot _inputRoot;
        [SerializeField] private AppSettings _photonSettings;

        private LocalClient _localClient;
        private MasterClient _masterClient;
        private IMasterNetwork _network;

        private void Start()
        {
            SetupPhoton();

            _network = new PhotonNetwork(new LoadBalancingClient(), _photonSettings);
            _localClient = new LocalClient(_network, _viewport, _inputRoot);
            _masterClient = new MasterClient(_network);
        }

        private void Update()
        {
            _network.DispatchCommands();

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
            _network.Dispose();
        }

        private void OnApplicationQuit()
        {
            _network.Dispose();
        }

        private void SetupPhoton()
        {
            _photonSettings.AppVersion = UnityEngine.Application.version;

            PhotonPeer.RegisterType(typeof(CreateSimulationCommand), 0,
                PhotonMarshal.Serialize<CreateSimulationCommand>,
                PhotonMarshal.Deserialize<CreateSimulationCommand>);
        }
    }
}

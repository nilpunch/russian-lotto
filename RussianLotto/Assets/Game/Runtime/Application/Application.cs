using ExitGames.Client.Photon;
using Photon.Realtime;
using RussianLotto.Client;
using RussianLotto.Command;
using RussianLotto.Input;
using RussianLotto.Master;
using RussianLotto.Networking;
using RussianLotto.Save;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Application
{
    public class Application : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private BehaviorTreeView _localClientTree;
        [SerializeField] private BehaviorTreeView _masterClientTree;
#endif

        [Space, SerializeField] private ViewportRoot _viewport;
        [SerializeField] private InputRoot _inputRoot;
        [SerializeField] private OfflineMoneyEarn _offlineMoneyEarn;
        [SerializeField] private AppSettings _photonSettings;

        private LocalClient _localClient;
        private MasterClient _masterClient;
        private IMasterNetwork _network;

        private void Start()
        {
            UnityEngine.Application.targetFrameRate = 1000;
            UnityEngine.QualitySettings.vSyncCount = 0;

            SetupPhoton();

            _network = new PhotonNetwork(new LoadBalancingClient(), _photonSettings);
            _localClient = new LocalClient(_offlineMoneyEarn, _network, _viewport, _inputRoot);
            _masterClient = new MasterClient(_network, _localClient.Session);
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                UnityEngine.Application.Quit();
                return;
            }

            _network.DispatchCommands();

            long time = (long)(Time.realtimeSinceStartupAsDouble * 1000f);

            _masterClient.ExecuteFrame(time);
            _localClient.ExecuteFrame(time);

            #if UNITY_EDITOR

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

            #endif
        }

        private void OnDestroy()
        {
            OnApplicationQuit();
        }

        private void OnApplicationQuit()
        {
            _network.Dispose();
            _localClient.Save();
        }

        #region PhotonTrash

        private byte _registeredCommandsCounter = 0;

        private void SetupPhoton()
        {
            _photonSettings.AppVersion = UnityEngine.Application.version;

            // From server
            RegisterCommand<CreateSimulationCommand>();
            RegisterCommand<StartSimulationCommand>();
            RegisterCommand<StopSimulationCommand>();
            RegisterCommand<DeleteSimulationCommand>();

            // To server
            RegisterCommand<FinishMasterGameCommand>();
        }

        private void RegisterCommand<T>() where T : ISerialization, IDeserialization, new()
        {
            PhotonPeer.RegisterType(typeof(T), _registeredCommandsCounter, PhotonMarshal.Serialize<T>, PhotonMarshal.Deserialize<T>);
            _registeredCommandsCounter += 1;
        }
        #endregion

    }
}

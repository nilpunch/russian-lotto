using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Realtime;
using RussianLotto.Client;
using RussianLotto.Command;
using RussianLotto.Networking;
using UnityEngine;
using Player = RussianLotto.Networking.Player;

namespace RussianLotto.Master
{
    public class PhotonRoom : IMasterRoom, IMatchmakingCallbacks, IOnEventCallback
    {
        private const int ClientCommandsCode = 100;
        private const int ServerCommandsCode = 200;

        private const string GameTypeProperty = "gameType";
        private const string ShuffledProperty = "shuffled";

        private readonly LoadBalancingClient _loadBalancingClient;

        private readonly CommandsQueue<ISessionCommand> _localInputQueue;
        private readonly CommandsQueue<IServerCommand> _serverInputQueue;

        private string _roomName = string.Empty;
        private bool _tryingRejoining;
        private bool _rejoinShuffleMode;
        private GameType _rejoinGameType;

        public PhotonRoom(int maxPlayersAmount, LoadBalancingClient loadBalancingClient)
        {
            MaxPlayersAmount = maxPlayersAmount;
            _loadBalancingClient = loadBalancingClient;
            _loadBalancingClient.AddCallbackTarget(this);

            _localInputQueue = new ();
            _serverInputQueue = new ();
        }

        public void Dispose()
        {
            _loadBalancingClient.RemoveCallbackTarget(this);
        }

        public bool IsEntered => _loadBalancingClient.InRoom;
        public bool IsOpenToJoin => _loadBalancingClient.CurrentRoom.IsOpen;

        public int MaxPlayersAmount { get; }
        public GameType GameType { get; private set; }
        public bool ShuffledMode { get; private set; }

        public IReadOnlyCollection<IPlayer> ConnectedPlayers => IsEntered
            ? _loadBalancingClient.CurrentRoom.Players.Values.Select(player => new Player(player.UserId)).ToArray()
            : ArraySegment<IPlayer>.Empty;

        public bool CanSendCommands => _loadBalancingClient.IsConnectedAndReady;

        public ICommandInput<ISessionCommand> SessionInput => _localInputQueue;

        public ICommandInput<IServerCommand> MasterInput => _serverInputQueue;


        public void SendToServer(IServerCommand command)
        {
            if (CanSendCommands == false)
                throw new InvalidOperationException();

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient};

            _loadBalancingClient.OpRaiseEvent(ServerCommandsCode, command, raiseEventOptions, SendOptions.SendReliable);
        }

        public void SendToClients(object command)
        {
            if (CanSendCommands == false)
                throw new InvalidOperationException();

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};

            _loadBalancingClient.OpRaiseEvent(ClientCommandsCode, command, raiseEventOptions, SendOptions.SendReliable);
        }

        public void OpenToJoin()
        {
            if (!IsEntered)
                throw new InvalidOperationException();

            _loadBalancingClient.CurrentRoom.IsOpen = true;
        }

        public void CloseJoining()
        {
            if (!IsEntered)
                throw new InvalidOperationException();

            _loadBalancingClient.CurrentRoom.IsOpen = false;
        }

        public void EnterRandom(GameType gameType, bool shuffled)
        {
            if (CanSendCommands == false)
                throw new InvalidOperationException();

            // if (_loadBalancingClient.OpRejoinRoom(_roomName))
            // {
            //     _tryingRejoining = true;
            //     _rejoinGameType = gameType;
            //     _rejoinShuffleMode = shuffled;
            //     return;
            // }

            JoinOrCreateRoom(gameType, shuffled);
        }

        public void Exit()
        {
            if (!IsEntered)
                throw new InvalidOperationException();

            if (CanSendCommands == false)
                throw new InvalidOperationException();

            _loadBalancingClient.OpLeaveRoom(false);
        }

        #region PhotonCallbacks

        public void OnEvent(EventData photonEvent)
        {
            switch (photonEvent.CustomData)
            {
                case ISessionCommand command:
                    _localInputQueue.PushCommand(command);
                    break;

                case IServerCommand command:
                    _serverInputQueue.PushCommand(command);
                    break;
            }
        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
        }

        public void OnCreatedRoom()
        {
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
        }

        public void OnJoinedRoom()
        {
            Debug.Log("Room Joined! Name: " + _loadBalancingClient.CurrentRoom.Name);

            _roomName = _loadBalancingClient.CurrentRoom.Name;
            GameType = (GameType)_loadBalancingClient.CurrentRoom.CustomProperties[GameTypeProperty];
            ShuffledMode = (bool)_loadBalancingClient.CurrentRoom.CustomProperties[ShuffledProperty];
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            if (_tryingRejoining)
            {
                Debug.Log("Room Failed Rejoining! " + message);
                _tryingRejoining = false;
                JoinOrCreateRoom(_rejoinGameType, _rejoinShuffleMode);
            }
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
        }

        public void OnLeftRoom()
        {
            Debug.Log("Room Left!");
        }

        #endregion

        private void JoinOrCreateRoom(GameType gameType, bool shuffled)
        {
            Hashtable roomProperties = new() {{GameTypeProperty, gameType}, {ShuffledProperty, shuffled}};

            OpJoinRandomRoomParams joinRandomRoomParams = new()
            {
                ExpectedCustomRoomProperties = roomProperties,
                ExpectedMaxPlayers = (byte)MaxPlayersAmount
            };

            EnterRoomParams enterRoomParams = new()
            {
                RoomOptions = new RoomOptions()
                {
                    MaxPlayers = (byte)MaxPlayersAmount,
                    CustomRoomProperties = roomProperties,
                    CustomRoomPropertiesForLobby = new []{GameTypeProperty, ShuffledProperty}
                    // PlayerTtl = 60000,
                    // EmptyRoomTtl = 60000,
                },
            };

            _loadBalancingClient.OpJoinRandomOrCreateRoom(joinRandomRoomParams, enterRoomParams);
        }
    }
}

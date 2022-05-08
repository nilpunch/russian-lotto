using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Realtime;
using RussianLotto.Client;
using UnityEngine;

namespace RussianLotto.Networking
{
    public class PhotonRoom : IRoom, IMatchmakingCallbacks
    {
        private const string GameTypeProperty = "gameType";
        private const string ShuffledProperty = "shuffled";

        private readonly ISocket _socket;
        private readonly LoadBalancingClient _loadBalancingClient;

        private string _roomName = string.Empty;
        private bool _tryingRejoining;
        private bool _rejoinShuffleMode;
        private GameType _rejoinGameType;

        public PhotonRoom(int maxPlayersAmount, ISocket socket, LoadBalancingClient loadBalancingClient)
        {
            MaxPlayersAmount = maxPlayersAmount;
            _socket = socket;
            _loadBalancingClient = loadBalancingClient;
            _loadBalancingClient.AddCallbackTarget(this);
        }

        public void Dispose()
        {
            _socket.Dispose();
            _loadBalancingClient.RemoveCallbackTarget(this);
        }

        public bool IsEntered => _loadBalancingClient.InRoom;
        public bool IsOpenToJoin => _loadBalancingClient.CurrentRoom.IsOpen;

        public int MaxPlayersAmount { get; }
        public SimulationState SimulationState { get; private set; }
        public GameType GameType { get; private set; }
        public bool ShuffledMode { get; private set; }
        public IReadOnlyCollection<IPlayer> ConnectedPlayers => IsEntered
                ? _loadBalancingClient.CurrentRoom.Players.Values.Select(player => new Player(player.NickName)).ToArray()
                : ArraySegment<IPlayer>.Empty;

        public bool CanSendCommands => _loadBalancingClient.IsConnectedAndReady;

        public void OpenToJoin()
        {
            if (!IsEntered)
                throw new InvalidOperationException();

            _loadBalancingClient.CurrentRoom.IsOpen = true;
        }

        public void CloseJoining()
        {
            throw new NotImplementedException();
        }

        public void EnterRandom(GameType gameType, bool shuffled)
        {
            if (_socket.IsConnected == false)
                throw new InvalidOperationException();

            if (CanSendCommands == false)
                throw new InvalidOperationException();

            if (_loadBalancingClient.OpRejoinRoom(_roomName))
            {
                _tryingRejoining = true;
                _rejoinGameType = gameType;
                _rejoinShuffleMode = shuffled;
                return;
            }

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
            Hashtable roomProperties = new()
            {
                { GameTypeProperty, gameType },
                { ShuffledProperty, shuffled }
            };

            OpJoinRandomRoomParams joinRandomRoomParams = new()
            {
                ExpectedCustomRoomProperties = roomProperties,
            };

            EnterRoomParams enterRoomParams = new()
            {
                RoomOptions = new RoomOptions()
                {
                    MaxPlayers = (byte)MaxPlayersAmount,
                    CustomRoomProperties = roomProperties,
                    PlayerTtl = 60000,
                    EmptyRoomTtl = 60000,
                },
            };

            _loadBalancingClient.OpJoinRandomOrCreateRoom(joinRandomRoomParams, enterRoomParams);
        }
    }
}

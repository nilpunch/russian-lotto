using System;
using RussianLotto.Client;
using RussianLotto.Command;
using RussianLotto.Networking;

namespace RussianLotto.Master
{
    public class MasterSimulation : IMasterSimulation
    {
        private readonly IMasterNetwork _masterNetwork;
        private readonly IReadOnlySession _session;

        public MasterSimulation(IMasterNetwork masterNetwork, IReadOnlySession session)
        {
            _masterNetwork = masterNetwork;
            _session = session;
        }

        public MasterRoomState MasterRoomState { get; private set; }

        public void PrepareSimulation()
        {
            if (MasterRoomState != MasterRoomState.WaitingPlayers)
                throw new InvalidOperationException();

            _masterNetwork.MasterRoom.CloseJoining();

            int gameSeed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            _masterNetwork.MasterRoom.SendToClients(new CreateSimulationCommand(gameSeed, _masterNetwork.Room.GameType,
                _masterNetwork.Room.ShuffledMode));

            MasterRoomState = MasterRoomState.GamePreparation;
        }

        public void StartSimulation()
        {
            if (MasterRoomState != MasterRoomState.GamePreparation)
                throw new InvalidOperationException();

            _masterNetwork.MasterRoom.SendToClients(new StartSimulationCommand());

            MasterRoomState = MasterRoomState.GameSimulation;
        }

        public void FinishGame()
        {
            if (MasterRoomState != MasterRoomState.GameSimulation)
                throw new InvalidOperationException();

            _masterNetwork.MasterRoom.SendToClients(new StopSimulationCommand());

            MasterRoomState = MasterRoomState.GameFinished;
        }

        public void ResetSimulation()
        {
            _masterNetwork.MasterRoom.SendToClients(new DeleteSimulationCommand());
            _masterNetwork.MasterRoom.OpenToJoin();
            MasterRoomState = MasterRoomState.WaitingPlayers;
        }

        public void RestoreStateFromLocal()
        {
            MasterRoomState = !_session.HasSimulation
                ? MasterRoomState.WaitingPlayers
                : _session.ReadOnlySimulation.State switch
                {
                    SimulationState.Idle => MasterRoomState.GamePreparation,
                    SimulationState.Game => MasterRoomState.GameSimulation,
                    SimulationState.Finished => MasterRoomState.GameFinished,
                    _ => throw new ArgumentOutOfRangeException()
                };

            if (MasterRoomState == MasterRoomState.WaitingPlayers)
                _masterNetwork.MasterRoom.OpenToJoin();
        }
    }
}

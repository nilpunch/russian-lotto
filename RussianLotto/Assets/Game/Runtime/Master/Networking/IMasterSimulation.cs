namespace RussianLotto.Master
{
    public interface IMasterSimulation
    {
        MasterRoomState MasterRoomState { get; }
        void PrepareSimulation();
        void StartSimulation();
        void FinishGame();
        void ResetSimulation();

        void RestoreStateFromLocal();
    }
}

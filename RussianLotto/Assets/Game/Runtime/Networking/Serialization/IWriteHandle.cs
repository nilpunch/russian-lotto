namespace RussianLotto.Networking
{
    public interface IWriteHandle
    {
        int CurrentIndex { get; }

        void WriteInt(int value);
        void WriteBool(bool value);
        void WriteByte(byte value);
    }
}

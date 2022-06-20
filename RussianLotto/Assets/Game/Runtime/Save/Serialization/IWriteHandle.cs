namespace RussianLotto.Save
{
    public interface IWriteHandle
    {
        void WriteInt(int value);
        void WriteBool(bool value);
        void WriteByte(byte value);
    }
}

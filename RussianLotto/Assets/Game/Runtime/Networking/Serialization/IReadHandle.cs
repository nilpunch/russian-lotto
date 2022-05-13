namespace RussianLotto.Networking
{
    public interface IReadHandle
    {
        int CurrentIndex { get; }

        int ReadInt();
        bool ReadBool();
        byte ReadByte();
    }
}

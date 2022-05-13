namespace RussianLotto.Networking
{
    public class WriteHandle : IWriteHandle
    {
        private readonly byte[] _data;

        public WriteHandle(byte[] data)
        {
            _data = data;
            CurrentIndex = 0;
        }

        public int CurrentIndex { get; private set; }

        public void WriteInt(int value)
        {
            for (int i = 0; i < sizeof(int); ++i)
            {
                _data[CurrentIndex] = (byte)(value >> (i * 8));
                CurrentIndex += 1;
            }
        }

        public void WriteBool(bool value)
        {
            _data[CurrentIndex] = (byte)(value == true ? 1 : 0);
            CurrentIndex += 1;
        }

        public void WriteByte(byte value)
        {
            _data[CurrentIndex] = value;
            CurrentIndex += 1;
        }
    }
}

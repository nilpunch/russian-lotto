namespace RussianLotto.Save
{
    public class ReadHandle : IReadHandle
    {
        private readonly byte[] _data;

        public ReadHandle(byte[] data)
        {
            _data = data;
            CurrentIndex = 0;
        }

        public int CurrentIndex { get; private set; }

        public int ReadInt()
        {
            int result = 0;

            for (int i = 0; i < sizeof(int); ++i)
            {
                result |= _data[CurrentIndex] << (i * 8);
                CurrentIndex += 1;
            }

            return result;
        }

        public bool ReadBool()
        {
            bool result = _data[CurrentIndex] == 1;
            CurrentIndex += 1;

            return result;
        }

        public byte ReadByte()
        {
            byte result = _data[CurrentIndex];
            CurrentIndex += 1;

            return result;
        }
    }
}

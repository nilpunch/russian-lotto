using ExitGames.Client.Photon;
using RussianLotto.Save;

namespace RussianLotto.Networking
{
    public static class PhotonMarshal
    {
        private static readonly byte[] s_buffer = new byte[1000];

        public static object Deserialize<T>(StreamBuffer inStream, short length) where T : IDeserialization, new()
        {
            T instance = new T();

            lock (s_buffer)
            {
                inStream.Read(s_buffer, 0, length);
                IReadHandle readHandle = new ReadHandle(s_buffer);
                instance.Deserialize(readHandle);
            }

            return instance;
        }

        public static short Serialize<T>(StreamBuffer outStream, object instance) where T : ISerialization
        {
            T typedInstance = (T)instance;
            int length;

            lock (s_buffer)
            {
                WriteHandle writeHandle = new WriteHandle(s_buffer);
                typedInstance.Serialize(writeHandle);
                length = writeHandle.CurrentIndex;
                outStream.Write(s_buffer, 0, length);
            }

            return (short)length;
        }
    }
}

using System;
using RussianLotto.Save;

namespace RussianLotto.Client
{
    public struct SerializableTimeSpan : ISerialization, IDeserialization
    {
        public int Seconds;
        public int Minutes;
        public int Hours;
        public int Days;

        public static implicit operator SerializableTimeSpan(TimeSpan timeSpan)
        {
            return new SerializableTimeSpan
            {
                Seconds = timeSpan.Seconds,
                Minutes = timeSpan.Minutes,
                Hours = timeSpan.Hours,
                Days = timeSpan.Days,
            };
        }

        public static implicit operator TimeSpan(SerializableTimeSpan jsonTimeSpan)
        {
            return new TimeSpan(
                jsonTimeSpan.Days, jsonTimeSpan.Hours,
                jsonTimeSpan.Minutes, jsonTimeSpan.Seconds);
        }

        public void Serialize(IWriteHandle writeHandle)
        {
            writeHandle.WriteInt(Seconds);
            writeHandle.WriteInt(Minutes);
            writeHandle.WriteInt(Hours);

            writeHandle.WriteInt(Days);
        }

        public void Deserialize(IReadHandle readHandle)
        {
            Seconds = readHandle.ReadInt();
            Minutes = readHandle.ReadInt();
            Hours = readHandle.ReadInt();
            
            Days = readHandle.ReadInt();
        }
    }
}
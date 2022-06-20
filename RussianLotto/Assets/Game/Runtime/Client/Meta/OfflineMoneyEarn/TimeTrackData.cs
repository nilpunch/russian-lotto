using System;
using RussianLotto.Save;

namespace RussianLotto.Client
{
    public class TimeTrackData : ISerialization, IDeserialization
    {
        public TimeSpan TimeInAfk { get; set; }
        public DateTime ExitTime { get; set; }

        public void Serialize(IWriteHandle writeHandle)
        {
            SerializableTimeSpan timeSpan = TimeInAfk;
            SerializableDateTime dateTime = ExitTime;
            timeSpan.Serialize(writeHandle);
            dateTime.Serialize(writeHandle);
        }

        public void Deserialize(IReadHandle readHandle)
        {
            SerializableTimeSpan timeSpan = new SerializableTimeSpan();
            SerializableDateTime dateTime = new SerializableDateTime();
            timeSpan.Deserialize(readHandle);
            dateTime.Deserialize(readHandle);

            TimeInAfk = timeSpan;
            ExitTime = dateTime;
        }
    }
}

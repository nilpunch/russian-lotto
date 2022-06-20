using System;
using RussianLotto.Save;

namespace RussianLotto.Client
{
    public struct SerializableDateTime : ISerialization, IDeserialization
    {
        public int Second;
        public int Minute;
        public int Hour;

        public int Day;
        public int Month;
        public int Year;

        public static implicit operator SerializableDateTime(DateTime dateTime)
        {
            return new SerializableDateTime
            {
                Second = dateTime.Second,
                Minute = dateTime.Minute,
                Hour = dateTime.Hour,
                Day = dateTime.Day,
                Month = dateTime.Month,
                Year = dateTime.Year
            };
        }

        public static implicit operator DateTime(SerializableDateTime jsonDateTime)
        {
            return new DateTime(
                jsonDateTime.Year, jsonDateTime.Month,
                jsonDateTime.Day, jsonDateTime.Hour,
                jsonDateTime.Minute, jsonDateTime.Second);
        }

        public void Serialize(IWriteHandle writeHandle)
        {
            writeHandle.WriteInt(Second);
            writeHandle.WriteInt(Minute);
            writeHandle.WriteInt(Hour);

            writeHandle.WriteInt(Day);
            writeHandle.WriteInt(Month);
            writeHandle.WriteInt(Year);
        }

        public void Deserialize(IReadHandle readHandle)
        {
            Second = readHandle.ReadInt();
            Minute = readHandle.ReadInt();
            Hour = readHandle.ReadInt();

            Day = readHandle.ReadInt();
            Month = readHandle.ReadInt();
            Year = readHandle.ReadInt();
        }
    }
}
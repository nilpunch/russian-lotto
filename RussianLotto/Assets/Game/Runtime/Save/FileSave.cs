using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RussianLotto.Save
{
    public class FileSave<TAbstract, TConcrete> : ISave<TAbstract>
        where TAbstract : ISerialization, IDeserialization
        where TConcrete : TAbstract, new()
    {
        private const int BufferSize = 1000;

        private readonly string FileName;
        private readonly byte[] _writeBuffer;

        public FileSave()
        {
            FileName = UnityEngine.Application.persistentDataPath + "/Save" + typeof(TConcrete).Name + ".save";

            Debug.Log(FileName);

            _writeBuffer = new byte[BufferSize];
        }

        public TAbstract Load()
        {
            if (File.Exists(FileName) == false)
                return new TConcrete();

            using (var fileStream = File.OpenRead(FileName))
            {
                fileStream.Read(_writeBuffer);
            }

            IReadHandle readHandle = new ReadHandle(_writeBuffer);
            TConcrete instance = new TConcrete();
            instance.Deserialize(readHandle);
            return instance;
        }

        public void Save(TAbstract instance)
        {
            WriteHandle writeHandle = new WriteHandle(_writeBuffer);
            instance.Serialize(writeHandle);

            using var fileStream = File.Exists(FileName) ? File.OpenWrite(FileName) : File.Create(FileName);

            fileStream.Write(_writeBuffer, 0, writeHandle.CurrentIndex);
        }
    }
}

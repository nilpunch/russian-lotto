using System;
using RussianLotto.Save;
using UnityEngine;

namespace RussianLotto.Client
{
    public class OfflineMoneyEarn : MonoBehaviour, IOfflineMoneyEarn
    {
        [SerializeField] private int _minimumHoursForEarning = 2;
        [SerializeField] private int _earningPerHour = 100;
        [SerializeField] private int _maxEarning = 1000;

        private DateTime _afkStartTime;
        private TimeSpan _timeInAfk;
        private DateTime _exitTime;

        private ISave<TimeTrackData> _timeTrackSave;


        public bool HasEarn => Mathf.FloorToInt((float)ElapsedTime.TotalHours) >= _minimumHoursForEarning;

        private TimeSpan ElapsedTime => _timeInAfk;

        private int MoneyEarn => (int)ElapsedTime.TotalHours * _earningPerHour;

        private bool InAfk { get; set; }

        private void Awake()
        {
            _timeTrackSave = new FileSave<TimeTrackData, TimeTrackData>();

            TimeTrackData data = _timeTrackSave.Load();

            if (data.ExitTime == default)
            {
                _timeInAfk = TimeSpan.Zero;
                return;
            }

            _timeInAfk = data.TimeInAfk + (DateTime.Now - data.ExitTime);
        }

        private void OnApplicationFocus(bool focused)
        {
            if (focused)
            {
                if (InAfk)
                {
                    StopAfk();
                }
            }
            else
            {
                if (!InAfk)
                {
                    StartAfk();
                    Save();
                }
            }
        }

        private void OnApplicationPause(bool paused)
        {
            if (!paused)
            {
                if (InAfk)
                {
                    StopAfk();
                }
            }
            else
            {
                if (!InAfk)
                {
                    StartAfk();
                    Save();
                }
            }
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        public void Collect(IWallet wallet)
        {
            if (HasEarn == false)
                throw new InvalidOperationException();

            wallet.Add(Mathf.Min(_maxEarning, MoneyEarn));
            _timeInAfk = TimeSpan.Zero;
            Save();
        }

        private void Save()
        {
            _timeTrackSave.Save(new TimeTrackData(){ ExitTime = DateTime.Now, TimeInAfk = _timeInAfk });
        }

        private void StartAfk()
        {
            if (InAfk)
                throw new InvalidOperationException();

            _afkStartTime = DateTime.Now;
        }

        private void StopAfk()
        {
            if (!InAfk)
                throw new InvalidOperationException();

            _timeInAfk += DateTime.Now - _afkStartTime;
        }
    }
}

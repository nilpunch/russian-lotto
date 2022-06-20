using RussianLotto.Client;

namespace RussianLotto.Input
{
    public class BoolSwitchToGameTypeSwitchAdapter : ISwitchElement<GameType>
    {
        private readonly ISwitchElement<bool> _boolSwitch;

        public BoolSwitchToGameTypeSwitchAdapter(ISwitchElement<bool> boolSwitch)
        {
            _boolSwitch = boolSwitch;
        }

        public bool Active
        {
            get => _boolSwitch.Active;
            set => _boolSwitch.Active = value;
        }

        public GameType State => _boolSwitch.State ? GameType.Fast : GameType.Classic;
    }
}

using UnityEngine;

namespace RussianLotto.View
{
    public class ScreensPresentation : MonoBehaviour, IScreensPresentation
    {
        [SerializeField] private VisibilityRoot[] _screenRoots;

        private void Start()
        {
            SwitchTo(Screen.MainMenu);
        }

        public void SwitchTo(Screen screen)
        {
            foreach (var root in _screenRoots)
            {
                if (root.Screen.HasFlag(screen))
                {
                    root.Show();
                }
                else
                {
                    root.Hide();
                }
            }
        }
    }
}

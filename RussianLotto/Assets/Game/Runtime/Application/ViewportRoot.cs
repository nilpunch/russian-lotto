using System;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Application
{
    public class ViewportRoot : MonoBehaviour, IViewport
    {
        [SerializeField] private BoardView _boardView = null;

        public IBoardView BoardView => _boardView;
        public IAvailableNumbersView AvailableNumbers => throw new NotImplementedException();
    }
}

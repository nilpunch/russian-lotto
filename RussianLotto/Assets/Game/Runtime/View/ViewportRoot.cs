﻿using UnityEngine;

namespace RussianLotto.View
{
    public class ViewportRoot : MonoBehaviour, IViewport
    {
        [SerializeField] private SimulationView _simulationView;
        [SerializeField] private ScreensPresentation _screensPresentation;
        [SerializeField] private PlayersView _playersView;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private WinOrLoseView _winOrLoseView;

        public IWinOrLoseView WinOrLoseView => _winOrLoseView;
        public IScreensPresentation ScreensPresentation => _screensPresentation;
        public IWalletView WalletView => _walletView;
        public ISimulationView SimulationView => _simulationView;
        public IPlayersView PlayersView => _playersView;
    }
}

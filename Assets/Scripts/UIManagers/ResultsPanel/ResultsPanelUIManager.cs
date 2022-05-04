using Context;
using Game;
using UIManagers.BottomPanel;
using UIManagers.ScreenStats;
using UnityEngine;

namespace UIManagers.ResultsPanel
{
    [Singleton]
    public class ResultsPanelUIManager : MonoBehaviour, IResultsPanelUIManager
    {
        [SerializeField] private GameObject gameObjectPanel;
        
        [Inject] private IGameManager gameManager;

        public void OpenPanel()
        {
            gameObjectPanel.SetActive(true);
        }

        public void ClosePanel()
        {
            gameObjectPanel.SetActive(false);
        }

        public void ReturnToMainScreen()
        {
            gameManager.FinishGame();
        }
    }
}
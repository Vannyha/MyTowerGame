using Context;
using Game;
using Tower;
using UnityEngine;
using UnityEngine.UI;

namespace UIManagers.ScreenStats
{
    [Singleton]
    public class ScreenStatsUIManager : MonoBehaviour, IScreenStatsUIManager
    {
        [SerializeField] private GameObject gameObjectPanel;
        [SerializeField] private Text hpText;
        
        [Inject] private ITowerManager towerManager;
        [Inject] private IGameManager gameManager;

        public void OpenPanel()
        {
            gameObjectPanel.SetActive(true);
        }

        public void ClosePanel()
        {
            gameObjectPanel.SetActive(false);
        }

        private void Update()
        {
            if (!gameManager.IsGameStarted)
            {
                return;
            }
            
            hpText.text = towerManager.CurrentTowerHp.ToString();
        }
    }
}
using Context;
using Game;
using UnityEngine;

namespace UIManagers.MainScreen
{
    public class MainScreenUIManager : MonoBehaviour, IMainScreenUIManager
    {
        [SerializeField] private GameObject gameObjectPanel;

        private IGameManager gameManager;
        public void SetupBeans(GameContext context)
        {
            gameManager = context.GameManagerInstance;
        }
        
        public void OpenPanel()
        {
            gameObjectPanel.SetActive(true);
        }

        public void ClosePanel()
        {
            gameObjectPanel.SetActive(false);
        }
        
        public void StartGame()
        {
            gameManager.StartGame();
        }
    }
}
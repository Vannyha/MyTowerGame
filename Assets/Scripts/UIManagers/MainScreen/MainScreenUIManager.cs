using Context;
using Game;
using UnityEngine;

namespace UIManagers.MainScreen
{
    [Singleton]
    public class MainScreenUIManager : MonoBehaviour, IMainScreenUIManager
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
        
        public void StartGame()
        {
            gameManager.StartGame();
        }
    }
}
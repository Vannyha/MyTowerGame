using Context;
using Tower;
using UnityEngine;

namespace UIManagers.ShopScreen
{
    public partial class ShopScreenUIManager : MonoBehaviour, IShopScreenUIManager
    {
        [SerializeField] private GameObject gameObjectPanel;

        private ITowerManager towerManager;

        public void OpenPanel()
        {
            gameObjectPanel.SetActive(true);
        }

        public void ClosePanel()
        {
            gameObjectPanel.SetActive(false);
        }

        public void SetupBeans(GameContext context)
        {
            towerManager = context.TowerManagerInstance;
        }
    }
}
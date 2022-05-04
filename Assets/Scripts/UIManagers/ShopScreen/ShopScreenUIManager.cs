using Context;
using Tower;
using UnityEngine;

namespace UIManagers.ShopScreen
{
    [Singleton]
    public partial class ShopScreenUIManager : MonoBehaviour, IShopScreenUIManager
    {
        [SerializeField] private GameObject gameObjectPanel;

        [Inject] private ITowerManager towerManager;

        public void OpenPanel()
        {
            gameObjectPanel.SetActive(true);
        }

        public void ClosePanel()
        {
            gameObjectPanel.SetActive(false);
        }
    }
}
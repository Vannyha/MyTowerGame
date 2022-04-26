using Context;
using UnityEngine;

namespace UIManagers.ShopScreen
{
    public class ShopScreenUIManager : MonoBehaviour, IShopScreenUIManager
    {
        [SerializeField] private GameObject gameObjectPanel;

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
            
        }
    }
}
using Context;
using UIManagers.LaboratoryScreen;
using UIManagers.MainScreen;
using UIManagers.ShopScreen;
using UIManagers.WorkshopScreen;
using UnityEngine;

namespace UIManagers.BottomPanel
{
    public class BottomPanelUIManager : MonoBehaviour, IBottomPanelUIManager
    {
        [SerializeField] private GameObject gameObjectPanel;

        private ILaboratoryScreenUIManager laboratoryScreenUIManager;
        private IMainScreenUIManager mainScreenUIManager;
        private IShopScreenUIManager shopScreenUIManager;
        private IWorkshopScreenUIManager workshopScreenUIManager;

        public void SetupBeans(GameContext context)
        {
            laboratoryScreenUIManager = context.LaboratoryScreenUIManagerInstance;
            mainScreenUIManager = context.MainScreenUIManagerInstance;
            shopScreenUIManager = context.ShopScreenUIManagerInstance;
            workshopScreenUIManager = context.WorkshopScreenUIManagerInstance;
            OpenMainPreset();
        }
        
        public void OpenPanel()
        {
            gameObjectPanel.SetActive(true);
        }

        public void ClosePanel()
        {
            gameObjectPanel.SetActive(false);
        }

        public void OpenMainPreset()
        {
            CloseAllPresets();
            mainScreenUIManager.OpenPanel();
        }

        public void OpenShopPreset()
        {
            CloseAllPresets();
            shopScreenUIManager.OpenPanel();
        }

        public void OpenWorkshopPreset()
        {
            CloseAllPresets();
            workshopScreenUIManager.OpenPanel();
        }

        public void OpenLaboratoryPreset()
        {
            CloseAllPresets();
            laboratoryScreenUIManager.OpenPanel();
        }

        private void CloseAllPresets()
        {
            laboratoryScreenUIManager.ClosePanel();
            mainScreenUIManager.ClosePanel();
            shopScreenUIManager.ClosePanel();
            workshopScreenUIManager.ClosePanel();
        }
    }
}
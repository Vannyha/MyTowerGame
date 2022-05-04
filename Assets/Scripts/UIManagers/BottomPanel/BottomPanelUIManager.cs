using Context;
using UIManagers.LaboratoryScreen;
using UIManagers.MainScreen;
using UIManagers.ShopScreen;
using UIManagers.WorkshopScreen;
using UnityEngine;

namespace UIManagers.BottomPanel
{
    [Singleton]
    public class BottomPanelUIManager : MonoBehaviour, IBottomPanelUIManager
    {
        [SerializeField] private GameObject gameObjectPanel;

        [Inject] private ILaboratoryScreenUIManager laboratoryScreenUIManager;
        [Inject] private IMainScreenUIManager mainScreenUIManager;
        [Inject] private IShopScreenUIManager shopScreenUIManager;
        [Inject] private IWorkshopScreenUIManager workshopScreenUIManager;

        public void Init()
        {
            OpenMainPreset();
            OpenPanel();
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
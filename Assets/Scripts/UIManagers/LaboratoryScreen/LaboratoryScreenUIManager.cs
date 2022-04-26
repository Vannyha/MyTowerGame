using Context;
using UnityEngine;

namespace UIManagers.LaboratoryScreen
{
    public class LaboratoryScreenUIManager : MonoBehaviour, ILaboratoryScreenUIManager
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
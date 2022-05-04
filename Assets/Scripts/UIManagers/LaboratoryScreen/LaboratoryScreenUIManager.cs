using Context;
using UnityEngine;

namespace UIManagers.LaboratoryScreen
{
    [Singleton]
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
    }
}
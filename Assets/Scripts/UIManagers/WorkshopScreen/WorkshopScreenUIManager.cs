using Context;
using UnityEngine;

namespace UIManagers.WorkshopScreen
{
    [Singleton]
    public class WorkshopScreenUIManager : MonoBehaviour, IWorkshopScreenUIManager
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
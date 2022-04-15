using Context;
using Tower;
using UnityEngine;
using UnityEngine.UI;

namespace UIManagers.ScreenStats
{
    public class ScreenStatsUIManager : MonoBehaviour, IScreenStatsUIManager
    {
        [SerializeField] private Text hpText;
        
        private ITowerManager towerManager;
        public void SetupBeans(GameContext context)
        {
            towerManager = context.TowerManagerInstance;
        }

        private void Update()
        {
            hpText.text = towerManager.CurrentTowerHp.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using Tower;
using UIManagers.UIHelpers;
using UnityEngine;

namespace UIManagers.ShopScreen
{
    public partial class ShopScreenUIManager
    {
        private readonly List<PicNameSlotUI> itemButtons = new List<PicNameSlotUI>();

        [SerializeField] private TowerDatabase database;
        [SerializeField] private PicNameSlotUI slotForShowInfo;
        [SerializeField] private Transform gridForTowers;
        
        private void InitTowersPics()
        {
            foreach (TowerType type in Enum.GetValues(typeof(TowerType)))
            {
                if (type == TowerType.None) continue;
                PicNameSlotUI item = Instantiate(slotForShowInfo);
                item.SetParentAndResetTransform(gridForTowers);
                item.SetImage(database.GetTowerSpriteByType(type));
            }
        }
    }
}
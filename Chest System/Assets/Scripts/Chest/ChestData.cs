using System;
using UnityEngine;

namespace ChestSystem.Chest
{
    [Serializable]
    public struct ChestData
    {
        public int minCoinsRewarded;
        public int maxCoinsRewarded;
        public int minGemsRewarded;
        public int maxGemsRewarded;
        public int openDurationInMinutes;
        public EChestType chestRarity;
        public Sprite chestLockedIcon;
        public Sprite chestUnlockingIcon;
        public Sprite chestUnlockedIcon;
    }
}

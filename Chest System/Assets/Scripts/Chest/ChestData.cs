using System;
using UnityEngine;

namespace ChestSystem.Chest
{
    [Serializable]
    public struct ChestData
    {
        [Header("Icon")]
        public Sprite chestIcon;

        [Header("Rarity")]
        public EChestType chestRarity;

        [Header("Coins Data")]
        public int minCoinsRewarded;
        public int maxCoinsRewarded;

        [Header("Gems Data")]
        public int minGemsRewarded;
        public int maxGemsRewarded;

        [Header("Duration (In minutes)")]
        public int openDuration;
    }
}

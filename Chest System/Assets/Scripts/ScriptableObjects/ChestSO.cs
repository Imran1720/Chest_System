using UnityEngine;

namespace ChestSystem.Chest
{
    [CreateAssetMenu(fileName = "ChestTypeSO", menuName = "ScriptableObjects/ChestType")]
    public class ChestSO : ScriptableObject
    {
        public ChestData[] ChestTypeList;
    }
}

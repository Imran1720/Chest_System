using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    [CreateAssetMenu(fileName = "PopUpMessages", menuName = "ScriptableObjects/PopUpMessages")]
    public class PopUpMessagesSO : ScriptableObject
    {
        public string slotFullMessage;
        public string noFundsMessage;
        public string chestOpeningMessage;
    }
}

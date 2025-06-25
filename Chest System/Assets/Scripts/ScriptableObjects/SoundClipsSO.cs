using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Sound
{
    [CreateAssetMenu(fileName = "SoundListSO", menuName = "ScriptableObjects/SoundClipSO")]
    public class SoundClipsSO : ScriptableObject
    {
        public List<Sounds> audioList;
    }
}

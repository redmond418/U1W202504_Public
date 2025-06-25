using System;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class WordInfoTriggerCondition : ITriggerCondition
    {
        [SerializeField] private Word word;

        public bool ValidateCondition(params ITriggerInfo[] infos)
        {
            if(!infos.TryFirstOfType<WordTriggerInfo>(out var wordTriggerInfo)) return false;
            return wordTriggerInfo.Word == word;
        }
    }
}

using System;
using System.Linq;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class ActiveWordTriggerCondition : ITriggerCondition
    {
        [SerializeField] private Word word;

        public bool ValidateCondition(params ITriggerInfo[] _)
        {
            return ActiveWordsHolder.GetActiveWords().Contains(word);
        }
    }
}

using System;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class IndexInfoTriggerCondition : ITriggerCondition
    {
        [SerializeField] private int index;

        public bool ValidateCondition(params ITriggerInfo[] infos)
        {
            if(!infos.TryFirstOfType<IndexTriggerInfo>(out var indexTriggerInfo)) return false;
            return indexTriggerInfo.Index == index;
        }
    }
}

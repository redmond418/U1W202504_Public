using System;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class OrTriggerCondition : ITriggerCondition
    { 
        [SerializeField, SerializeReference] private ITriggerCondition[] conditions;

        public bool ValidateCondition(params ITriggerInfo[] infos)
        {
            bool orFlag = false;
            foreach (var condition in conditions)
            {
                orFlag |= condition.ValidateCondition(infos);
            }
            return orFlag;
        }
    }
}

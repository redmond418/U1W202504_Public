using System;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class AndTriggerCondition : ITriggerCondition
    {
        [SerializeField, SerializeReference] private ITriggerCondition[] conditions;

        public bool ValidateCondition(params ITriggerInfo[] infos)
        {
            bool andFlag = true;
            foreach (var condition in conditions)
            {
                andFlag &= condition.ValidateCondition(infos);
            }
            return andFlag;
        }
    }
}

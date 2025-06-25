using System;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class NotTriggerCondition : ITriggerCondition
    {
        [SerializeField, SerializeReference] private ITriggerCondition condition;

        public bool ValidateCondition(params ITriggerInfo[] infos)
        {
            return !condition.ValidateCondition(infos);
        }
    }
}

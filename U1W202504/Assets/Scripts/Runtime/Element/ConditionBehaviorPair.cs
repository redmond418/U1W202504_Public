using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class ConditionBehaviorPair
    {
        [SerializeReference] public ITriggerCondition condition;
        [SerializeReference] public IElementBehavior behavior;

        public ITriggerCondition Condition => condition;
        public IElementBehavior Behavior => behavior;

        public async UniTask<bool> TryInvoke(CancellationToken cancellationToken, params ITriggerInfo[] infos)
        {
            if(condition.ValidateCondition(infos))
            {
                await behavior.Invoke(cancellationToken, infos);
                return true;
            }
            return false;
        }
    }
}

using System;
using System.Threading;
using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace U1W
{
    [Serializable, BoxGroup]
    public class ConditionBehaviorPairsSequence : IElementBehavior
    {
        [SerializeField] private bool uncontrollable = true;
        [SerializeField] private float delay = 1;
        [SerializeField] private ConditionBehaviorPair[] pairs;
        [SerializeField, SerializeReference] private IElementBehavior defaultBehavior;

        public async UniTask Invoke(CancellationToken cancellationToken, params ITriggerInfo[] infos)
        {
            UncontrollableOrder uncontrollableOrder = null;
            if(uncontrollable)
            {
                uncontrollableOrder = new();
                UncontrollableManager.AddOrder(uncontrollableOrder);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(delay), false, PlayerLoopTiming.Update, cancellationToken);
            foreach (var pair in pairs)
            {
                if(await pair.TryInvoke(cancellationToken, infos)) 
                {
                    if(uncontrollable) UncontrollableManager.RemoveOrder(uncontrollableOrder);
                    return;
                }
            }
            if(defaultBehavior == null) 
            {
                if(uncontrollable) UncontrollableManager.RemoveOrder(uncontrollableOrder);
                return;
            }
            await defaultBehavior.Invoke(cancellationToken);

            if(uncontrollable) UncontrollableManager.RemoveOrder(uncontrollableOrder);
        }
    }
}

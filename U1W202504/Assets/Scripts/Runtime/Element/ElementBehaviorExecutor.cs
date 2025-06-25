using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace U1W
{
    public class ElementBehaviorExecutor : MonoBehaviour
    {
        [SerializeField, SerializeReference] private IBehaviorTrigger trigger;
        [SerializeField] private ConditionBehaviorPairsSequence conditionBehaviorPairsSequence;

        private void Awake()
        {
            trigger.OnTrigger
                .SubscribeAwait(async (info, cancellationToken) => await InvokeSequence(info, cancellationToken))
                .AddTo(this);
        }

        private async UniTask InvokeSequence(ITriggerInfo info, CancellationToken cancellationToken)
        {
            await conditionBehaviorPairsSequence.Invoke(cancellationToken, info);
        }
    }
}

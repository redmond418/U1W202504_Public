using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace U1W
{
    public class OnEnabledExecutor : MonoBehaviour
    {
        [SerializeField] private ConditionBehaviorPairsSequence conditionBehaviorPairsSequence;

        private void OnEnable()
        {
            InvokeSequence(destroyCancellationToken).Forget();
        }

        private async UniTask InvokeSequence(CancellationToken cancellationToken)
        {
            await conditionBehaviorPairsSequence.Invoke(cancellationToken, null);
        }
    }
}

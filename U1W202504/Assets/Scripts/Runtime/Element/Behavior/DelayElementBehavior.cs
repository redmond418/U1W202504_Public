using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class DelayElementBehavior : IElementBehavior
    {
        [SerializeField] private float delay = 1;

        public async UniTask Invoke(CancellationToken cancellationToken, params ITriggerInfo[] infos)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay), false, PlayerLoopTiming.Update, cancellationToken);
        }
    }
}

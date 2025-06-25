using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion.Animation;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class LitMotionAnimationStopElementBehavior : IElementBehavior
    {
        [SerializeField] private LitMotionAnimation animation;

        #pragma warning disable CS1998
        public async UniTask Invoke(CancellationToken cancellationToken, params ITriggerInfo[] infos)
        {
            if(!animation.IsActive) return;
            animation.Stop();
        }
    }
}

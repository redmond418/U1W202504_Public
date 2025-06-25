using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion.Animation;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class LitMotionAnimationElementBehavior : IElementBehavior
    {
        [SerializeField] private bool waitForAnimation;
        [SerializeField] private LitMotionAnimation animation;

        public async UniTask Invoke(CancellationToken cancellationToken, params ITriggerInfo[] infos)
        {
            if(!animation.IsActive) animation.Play();
            else animation.Restart();
            if(!waitForAnimation) return;
            await UniTask.NextFrame(cancellationToken);
            await UniTask.WaitWhile(() => animation.IsPlaying, PlayerLoopTiming.Update, cancellationToken);
        }
    }
}

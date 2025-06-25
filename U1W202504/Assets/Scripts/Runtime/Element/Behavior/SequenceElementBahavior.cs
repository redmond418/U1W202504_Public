using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class SequenceElementBahavior : IElementBehavior
    {
        [SerializeField, SerializeReference] private IElementBehavior[] behaviors;

        public async UniTask Invoke(CancellationToken cancellationToken, params ITriggerInfo[] infos)
        {
            foreach (var behavior in behaviors)
            {
                await behavior.Invoke(cancellationToken, infos);
            }
        }
    }
}

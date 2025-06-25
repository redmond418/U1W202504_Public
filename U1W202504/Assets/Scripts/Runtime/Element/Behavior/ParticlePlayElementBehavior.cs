using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class ParticlePlayElementBehavior : IElementBehavior
    {
        [SerializeField] private ParticleSystem particleSystem;

        #pragma warning disable CS1998
        public async UniTask Invoke(CancellationToken cancellationToken, params ITriggerInfo[] infos)
        {
            particleSystem.Play();
        }
    }
}

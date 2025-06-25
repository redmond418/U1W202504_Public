using System;
using System.Threading;
using AnnulusGames.LucidTools.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class SEPlayElementBehavior : IElementBehavior
    {
        [SerializeField] private AudioClip audioClip;
        [SerializeField, Range(0f, 1f)] private float volume = 1;

        #pragma warning disable CS1998
        public async UniTask Invoke(CancellationToken cancellationToken, params ITriggerInfo[] infos)
        {
            LucidAudio.PlaySE(audioClip)
                .SetVolume(volume);
        }
    }
}

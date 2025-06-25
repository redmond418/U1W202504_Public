using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class DebugElementBehavior : IElementBehavior
    {
        [SerializeField] private string text;

        #pragma warning disable CS1998
        public async UniTask Invoke(CancellationToken cancellationToken, params ITriggerInfo[] infos)
        {
            Debug.Log(text);
        }
    }
}

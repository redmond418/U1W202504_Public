using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace U1W
{
    // もはや全部めんどいのでUnityEventにしちゃうクラス
    [Serializable]
    public class UnityEventElementBehavior : IElementBehavior
    {
        [SerializeField] private UnityEvent onInvoke;

        #pragma warning disable CS1998
        public async UniTask Invoke(CancellationToken cancellationToken, params ITriggerInfo[] infos)
        {
            onInvoke.Invoke();
        }

        public void AddListener(UnityAction action)
        {
            onInvoke.AddListener(action);
        }
    }
}

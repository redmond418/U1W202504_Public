using System;
using System.Threading;
using AnnulusGames.SceneSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class SceneLoadElementBehavior : IElementBehavior
    {
        [SerializeField] private SceneReference scene;

        public async UniTask Invoke(CancellationToken cancellationToken, params ITriggerInfo[] infos)
        {
            await Scenes.LoadSceneAsync(scene).ToUniTask(cancellationToken);
        }
    }
}

using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace U1W
{
    /// <summary>
    /// 何もしないことを明示するためのもの
    /// </summary>
    [Serializable]
    public class NothingElementBehavior : IElementBehavior
    {
        #pragma warning disable CS1998
        public async UniTask Invoke(CancellationToken cancellationToken, params ITriggerInfo[] infos)
        {
            // Nothing to do
        }
    }
}

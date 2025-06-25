using System.Threading;
using Cysharp.Threading.Tasks;

namespace U1W
{
    public interface IElementBehavior
    {
        public UniTask Invoke(CancellationToken cancellationToken, params ITriggerInfo[] infos);
    }
}

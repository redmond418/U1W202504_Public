using R3;

namespace U1W
{
    public interface IBehaviorTrigger
    {
        public Observable<ITriggerInfo> OnTrigger { get; }
    }
}

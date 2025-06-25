using System;
using ObservableCollections;
using R3;

namespace U1W
{
    [Serializable]
    public class OnWordRemoveBehaviorTrigger : IBehaviorTrigger
    {
        public Observable<ITriggerInfo> OnTrigger => 
            ActiveWordsHolder.GetActiveWords()
            .ObserveRemove()
            .Select(wordEvent => new WordTriggerInfo(wordEvent.Value) as ITriggerInfo);
    }
}

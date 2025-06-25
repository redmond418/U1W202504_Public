using System;
using ObservableCollections;
using R3;

namespace U1W
{
    [Serializable]
    public class OnWordAppendBehaviorTrigger : IBehaviorTrigger
    {
        public Observable<ITriggerInfo> OnTrigger => 
            ActiveWordsHolder.GetActiveWords()
            .ObserveAdd()
            .Select(wordEvent => new WordTriggerInfo(wordEvent.Value) as ITriggerInfo);
    }
}

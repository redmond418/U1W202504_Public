using System;
using R3;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class OnFlipBehaviorTrigger : IBehaviorTrigger
    {
        [SerializeField] private WordCharactersViewer wordCharactersViewer;

        public Observable<ITriggerInfo> OnTrigger => wordCharactersViewer.OnClick.Select(index => new IndexTriggerInfo(index) as ITriggerInfo);
    }
}

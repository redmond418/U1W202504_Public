using System;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class IndexTriggerInfo : ITriggerInfo
    {
        [SerializeField] private int index;

        public int Index => index;

        public IndexTriggerInfo(int index)
        {
            this.index = index;
        }
    }
}

using System;
using UnityEngine;

namespace U1W
{
    [Serializable]
    public class WordTriggerInfo : ITriggerInfo
    {
        [SerializeField] private Word word;

        public Word Word => word;

        public WordTriggerInfo(Word word)
        {
            this.word = word;
        }
    }
}

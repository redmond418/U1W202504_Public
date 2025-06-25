using System;
using System.Linq;

namespace U1W
{
    [Serializable]
    public record KnownWord
    {
        public string wordName;
        public KnownWordTag tag;

        public KnownWord(string wordName, KnownWordTag tag)
        {
            this.wordName = wordName;
            this.tag = tag;
        }

        public static KnownWord Create(Word word)
        {
            KnownWordTag tag = KnownWordTag.None;
            if(word.Tags.Any(tag => tag is RareWordTag)) tag = KnownWordTag.Rare;
            return new KnownWord(word.WordName, tag);
        }
    }

    public enum KnownWordTag
    {
        None = 0, 
        Rare = 1,
    }
}

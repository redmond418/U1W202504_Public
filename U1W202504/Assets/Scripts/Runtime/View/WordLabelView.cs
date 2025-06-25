using System.Linq;
using TMPro;
using UnityEngine;

namespace U1W
{
    public class WordLabelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private char unknownFill = '?';

        public void SetUnknownWord(KnownWord knownWord)
        {
            text.text = new string(unknownFill, knownWord.wordName.Length - knownWord.wordName.Count(character => character == ' '));
        }

        public void SetWord(KnownWord knownWord)
        {
            text.text = knownWord.wordName;
        }
    }
}

using UnityEngine;

namespace U1W
{
    [CreateAssetMenu(fileName = "All Words", menuName = "ScriptableObject/All Words List")]
    public class AllWordList : ScriptableObject
    {
        [SerializeField] private Word[] words;

        public Word[] Words => words;
    }
}

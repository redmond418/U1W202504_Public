using UnityEngine;

namespace U1W
{
    public class EnabledActiveRegister : MonoBehaviour
    {
        [SerializeField] private Word word;

        void OnEnable()
        {
            ActiveWordsHolder.AppendWord(word);
        }

        void OnDisable()
        {
            ActiveWordsHolder.RemoveWord(word);
        }
    }
}

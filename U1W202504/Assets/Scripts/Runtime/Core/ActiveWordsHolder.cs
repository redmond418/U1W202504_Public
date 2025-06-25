using ObservableCollections;
using UnityEngine;

namespace U1W
{
    [DefaultExecutionOrder(-10)]
    public class ActiveWordsHolder : MonoBehaviour
    {
        private static ActiveWordsHolder instance;
        private static ActiveWordsHolder Instance
        {
            get
            {
                if(instance == null) instance = FindAnyObjectByType<ActiveWordsHolder>();
                return instance;
            }
        }

        private ObservableList<Word> activeWords = new();
        public IReadOnlyObservableList<Word> ActiveWords => activeWords;
        private ObservableList<Word> deactivatedWords = new();
        public IReadOnlyObservableList<Word> DeactivatedWords => deactivatedWords;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(gameObject);
                instance.ResetAll();
                return;
            }
        }

        public void Append(Word word)
        {
            activeWords.Add(word);
            if(deactivatedWords.Contains(word)) deactivatedWords.Remove(word);
        }
        public void Remove(Word word) 
        {
            activeWords.Remove(word);
            deactivatedWords.Add(word);
        }
        public void ResetAll()
        {
            activeWords.Clear();
            deactivatedWords.Clear();
        }

        public static void AppendWord(Word word)
        {
            if(Instance == null) return;
            Instance.Append(word);
        }
        public static void RemoveWord(Word word) 
        {
            if(Instance == null) return;
            Instance.Remove(word);
        }
        public static IReadOnlyObservableList<Word> GetActiveWords() => Instance.ActiveWords;
        public static IReadOnlyObservableList<Word> GetDeactivatedWords() => Instance.DeactivatedWords;
    }
}

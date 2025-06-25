using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ObservableCollections;
using R3;
using TMPro;
using UnityEngine;

namespace U1W
{
    public class KnownWordsListPresenter : MonoBehaviour
    {
        [SerializeField] private AllWordList allWordList;
        [SerializeField] private WordLabelView wordLabelViewPrefab;
        [SerializeField] private RectTransform contentParent;
        [SerializeField] private TMP_Text countLabel;

        private List<WordLabelView> wordLabels = new();

        private void Awake()
        {
            foreach (var word in allWordList.Words)
            {
                var instance = Instantiate(wordLabelViewPrefab, contentParent);
                wordLabels.Add(instance);
                instance.SetUnknownWord(KnownWord.Create(word));
            }
            foreach (var knownWord in KnownWordsHolder.List)
            {
                SetWord(knownWord);
            }
            KnownWordsHolder.List
                .ObserveAdd(destroyCancellationToken)
                .Select(e => e.Value)
                .Subscribe(SetWord).AddTo(this);
            SetCountLabel(KnownWordsHolder.List.Count, allWordList.Words.Length);
        }

        private void SetWord(KnownWord knownWord)
        {
            int index = 0;
            while (index < allWordList.Words.Length && allWordList.Words[index].WordName != knownWord.wordName) index++;
            wordLabels[index].SetWord(knownWord);
            
            SetCountLabel(KnownWordsHolder.List.Count, allWordList.Words.Length);
        }

        private void SetCountLabel(int knownCount, int maxCount)
        {
            countLabel.text = knownCount + "/" + maxCount;
        }
    }
}

using System;
using ObservableCollections;
using R3;
using UnityEngine;
using ZeroMessenger;

namespace U1W
{
    public class CenterTextPresenter : MonoBehaviour
    {
        [SerializeField] private AllWordList allWordList;
        [SerializeField] private GameObject[] tutorialTexts;
        [SerializeField] private GameObject clearText;
        [SerializeField] private float clearDelay;

        private ReactiveProperty<int> tutorialProgress = new();

        private void Awake()
        {
            if(KnownWordsHolder.List.Count >= allWordList.Words.Length) 
            {
                clearText.SetActive(true);
                return;
            }
            KnownWordsHolder.List.ObserveAdd(destroyCancellationToken)
                .Where(e => e.Index >= allWordList.Words.Length - 1)
                .Delay(TimeSpan.FromSeconds(clearDelay))
                .Subscribe(clearText, (_, clearText) => clearText.SetActive(true))
                .AddTo(this);
            if(PlayerPrefs.GetInt("TutorialDone") != 0) return;
            for (int i = 0; i < tutorialTexts.Length; i++)
            {
                tutorialTexts[i].SetActive(i == tutorialProgress.Value);
            }
            MessageBroker<TutorialProgressMessage>.Default
                .Subscribe(message => TutorialProgress(message.progress))
                .AddTo(this);
        }

        private void TutorialProgress(int progress)
        {
            if(tutorialProgress.Value != progress) return;
            tutorialProgress.Value = progress + 1;
            if(tutorialProgress.Value >= tutorialTexts.Length) PlayerPrefs.SetInt("TutorialDone", 1);
            for (int i = 0; i < tutorialTexts.Length; i++)
            {
                tutorialTexts[i].SetActive(i == tutorialProgress.Value);
            }
        }
    }
}

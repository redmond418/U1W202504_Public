using System;
using System.Collections.Generic;
using System.Linq;
using ObservableCollections;
using R3;
using UnityEngine;
using Unityroom.Client;

namespace U1W
{
    public class KnownWordsHolder : MonoBehaviour
    {
        private static KnownWordsHolder instance;
        private static KnownWordsHolder Instance
        {
            get
            {
                if(instance == null) 
                {
                    instance = FindAnyObjectByType<KnownWordsHolder>();
                    instance.Initialize();
                    DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }

        [SerializeField] private string hmacKey;
        
        private bool isInitialized;

        private void Awake()
        {
            if(instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        public void Initialize()
        {
            if(isInitialized) return;
            isInitialized = true;
            string savedText = PlayerPrefs.GetString("KnownWords");
            var list = JsonUtility.FromJson<SerializationWrapper<List<KnownWord>>>(savedText)?.value;
            if(list == null) list = new();
            knownWords = new ObservableList<KnownWord>(list);

            knownWords.ObserveChanged()
                .Subscribe(knownWords,  (_, knownWords) =>
                {
                    string savedText = JsonUtility.ToJson(new SerializationWrapper<List<KnownWord>>(knownWords.ToList()));
                    PlayerPrefs.SetString("KnownWords", savedText);
                }).AddTo(this);
            knownWords.ObserveAdd()
                .Select(knownWords, (_, knownWords) => knownWords.Count)
                .SubscribeAwait(async (count, cancellationToken) =>
                {
                    UnityroomClient client = new UnityroomClient()
                    {
                        HmacKey = hmacKey,
                    };
                    try
                    {
                        await client.Scoreboards.SendAsync(new SendScoreRequest()
                        {
                            ScoreboardId = 1,
                            Score = count
                        }, cancellationToken);
                    }
                    catch (UnityroomApiException)
                    {
                        
                    }
                    catch(InvalidOperationException e)
                    {
                        Debug.LogWarning(e.Message);
                    }
                }, AwaitOperation.ThrottleFirstLast).AddTo(this);
        }

        private ObservableList<KnownWord> knownWords;

        public static IReadOnlyObservableList<KnownWord> List => Instance.knownWords;
        public static void AddWord(Word word)
        {
            KnownWord knownWord = KnownWord.Create(word);
            var knownWords = Instance.knownWords;
            if(knownWords.Contains(knownWord)) return;
            knownWords.Add(knownWord);
        }
    }
}

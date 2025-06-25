using System.Collections.Generic;
using System.Linq;
using Alchemy.Inspector;
using AnnulusGames.LucidTools.Audio;
using R3;
using UnityEngine;

namespace U1W
{
    public class FocusManager : MonoBehaviour
    {
        [SerializeField] private bool autoFind = true;
        [SerializeField] private List<Focusable> focusables;
        [SerializeField] private AudioClip focusSE;

        private Focusable currentInFocus;

        private void Awake()
        {
            if(autoFind) FindFocusables();
            foreach (var element in focusables)
            {
                SetupFocusable(element);
            }
        }

        private void SetupFocusable(Focusable focusable)
        {
            focusable.IsFocused
                .Where(focused => focused)
                .Subscribe(_ =>
                {
                    if(currentInFocus != null) currentInFocus.SetFocus(false);
                    currentInFocus = focusable;

                    LucidAudio.PlaySE(focusSE);
                }).AddTo(focusable);
        }

        public void AddFocusable(Focusable focusable) => SetupFocusable(focusable);

        [Button, DisableInPlayMode]
        private void FindFocusables()
        {
            focusables = FindObjectsByType<Focusable>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        }
    }
}

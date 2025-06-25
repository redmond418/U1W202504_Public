using Alchemy.Inspector;
using AnnulusGames.LucidTools.Audio;
using LitMotion.Animation;
using R3;
using UnityEngine;
using UnityEngine.UI;
using ZeroMessenger;

namespace U1W
{
    public class MenuPresenter : MonoBehaviour
    {
        [Title("Menu")]
        [SerializeField] private Button openToggleButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button reloadButton;
        [SerializeField] private Button creditOpenButton;
        [SerializeField] private CanvasGroup menuGroup;
        [SerializeField] private LitMotionAnimation menuOpenAnimation;
        [Title("Credit")]
        [SerializeField] private Button creditCloseButton;
        [SerializeField] private CanvasGroup creditGroup;
        [SerializeField] private LitMotionAnimation creditOpenAnimation;
        [Space(20)]
        [SerializeField] private Reloader reloader;
        [Space(20)]
        [SerializeField] private AudioClip clickSE;

        private UncontrollableOrder uncontrollableOrder;

        private void Awake()
        {
            openToggleButton.OnClickAsObservable()
                .Select(menuGroup, (_, menuGroup) => !menuGroup.blocksRaycasts)
                .Subscribe(open => 
                {
                    SetOpenCredit(false);
                    SetOpen(open);
                    LucidAudio.PlaySE(clickSE);
                })
                .AddTo(this);
            closeButton.OnClickAsObservable()
                .Subscribe(_ => 
                {
                    SetOpen(false);
                    SetOpenCredit(false);
                    LucidAudio.PlaySE(clickSE);
                })
                .AddTo(this);
            reloadButton.OnClickAsObservable()
                .Subscribe(reloader, (_, reloader) => 
                {
                    Time.timeScale = 1;
                    reloader.Reload();
                    LucidAudio.PlaySE(clickSE);
                })
                .AddTo(this);
            creditOpenButton.OnClickAsObservable()
                .Subscribe(_ => 
                {
                    SetOpen(false);
                    SetOpenCredit(true);
                    LucidAudio.PlaySE(clickSE);
                })
                .AddTo(this);
            creditCloseButton.OnClickAsObservable()
                .Subscribe(_ => 
                {
                    SetOpenCredit(false);
                    SetOpen(true);
                    LucidAudio.PlaySE(clickSE);
                })
                .AddTo(this);
        }

        private void SetOpen(bool opened)
        {
            if(opened == menuGroup.blocksRaycasts) return;

            Time.timeScale = opened ? 0 : 1;
            if(opened) UncontrollableManager.AddOrder(uncontrollableOrder);
            else UncontrollableManager.RemoveOrder(uncontrollableOrder);

            menuGroup.alpha = opened ? 1 : 0;
            menuGroup.blocksRaycasts = opened;
            if(opened) 
            {
                MessageBroker<TutorialProgressMessage>.Default.Publish(new(2));
                if(!menuOpenAnimation.IsActive) menuOpenAnimation.Play();
                else menuOpenAnimation.Restart();
            }
        }

        private void SetOpenCredit(bool opened)
        {
            if(opened == creditGroup.blocksRaycasts) return;

            Time.timeScale = opened ? 0 : 1;
            if(opened) UncontrollableManager.AddOrder(uncontrollableOrder);
            else UncontrollableManager.RemoveOrder(uncontrollableOrder);

            creditGroup.alpha = opened ? 1 : 0;
            creditGroup.blocksRaycasts = opened;
            if(opened) 
            {
                if(!creditOpenAnimation.IsActive) creditOpenAnimation.Play();
                else creditOpenAnimation.Restart();
            }
        }
    }
}

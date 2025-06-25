using AnnulusGames.SceneSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace U1W
{
    public class Reloader : MonoBehaviour
    {
        public void Reload()
        {
            if(!enabled) return;
            enabled = false;
            Scenes.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

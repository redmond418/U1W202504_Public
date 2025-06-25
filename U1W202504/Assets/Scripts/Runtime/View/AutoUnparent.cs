using UnityEngine;

namespace U1W
{
    public class AutoUnparent : MonoBehaviour
    {
        private void Awake()
        {
            transform.parent = null;
        }
    }
}

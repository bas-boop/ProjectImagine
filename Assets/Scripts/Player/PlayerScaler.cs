using Framework.Extensions;
using UnityEngine;

namespace Player
{
    public sealed class PlayerScaler : MonoBehaviour
    {
        [SerializeField] private bool isInstant;
        [SerializeField] private float smallerScale = 0.75f;

        public void Scale()
        {
            if (isInstant)
            {
                Vector3 newScale = transform.localScale;
                newScale.SetY(transform.localScale.y == 1 ? smallerScale : 1);
                transform.localScale = newScale;
                return;
            }
            
            
        }
    }
}
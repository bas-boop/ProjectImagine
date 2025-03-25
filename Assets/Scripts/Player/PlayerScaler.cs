using Framework.Extensions;
using UnityEngine;

namespace Player
{
    public sealed class PlayerScaler : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 1)] private float smallerScale = 0.75f;

        public void ToggleScale()
        {
            Vector3 newScale = transform.localScale;
            newScale.SetY(transform.localScale.y == 1 ? smallerScale : 1);
            transform.localScale = newScale;
        }
    }
}
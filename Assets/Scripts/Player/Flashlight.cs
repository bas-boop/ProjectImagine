using System.Collections;
using Framework.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    [RequireComponent(typeof(Light))]
    public sealed class Flashlight : MonoBehaviour
    {
        [SerializeField] private new Light light;
        [SerializeField, RangeVector2(0, 1, 0, 1)] private Vector2 offDurationRange;
        [SerializeField, RangeVector2(0, 1, 0, 1)] private Vector2 onDurationRange;
        [SerializeField, RangeVector2(0, 1, 0, 1)] private Vector2 weakLightRange;

        private float _intensity;

        private void Awake()
        {
            if (!light)
                light = GetComponent<Light>();
        }

        public void SetOn(bool target) => light.intensity = target ? _intensity : 0;

        public void DoFlicker(float duration) => StartCoroutine(Flicker(duration));

        /// <summary>
        /// Will turn off and on the flashlight for the given duration.
        /// </summary>
        /// <param name="duration">Time in secondes</param>
        private IEnumerator Flicker(float duration)
        {
            _intensity = light.intensity;
            duration /= 100;
            float elapsed = 0;

            while (elapsed < duration)
            {
                light.intensity = 0f;
                yield return new WaitForSeconds(Random.Range(offDurationRange.x, offDurationRange.y));

                light.intensity = _intensity * Random.Range(weakLightRange.x, weakLightRange.y);
                yield return new WaitForSeconds(Random.Range(onDurationRange.x, onDurationRange.y));

                elapsed += Time.deltaTime;
                
                if (elapsed >= duration)
                    break;
            }

            light.intensity = _intensity;
            StopAllCoroutines();
        }
    }
}
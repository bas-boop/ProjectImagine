using UnityEngine;

namespace Framework.AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class DisposeAudio : MonoBehaviour
    {
        [SerializeField] private float marginDisposeTime = 1;
        
        private AudioSource _source;

        private void Awake() => _source = GetComponent<AudioSource>();

        private void Start()
        {
            if (!_source.loop)
                Invoke(nameof(Des), _source.clip.length + marginDisposeTime);
        }

        private void Des() => Destroy(gameObject);
    }
}
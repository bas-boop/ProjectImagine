using UnityEngine;
using Random = UnityEngine.Random;

namespace Framework.AudioSystem
{
    public sealed class AudioManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private FirstPersonController firstPersonController;
        [SerializeField] private AudioSource audioPlayer2D;
        [SerializeField] private AudioSource audioPlayer3D;
        [SerializeField] private Transform[] audioPoints;
        
        [Header("Audio")]
        [SerializeField] private AudioClip breathSound;
        [SerializeField] private AudioClip[] footstepsSound;
        [SerializeField] private float footstepInterval = 0.5f;

        private float _footstepTimer;

        private void Update()
        {
            if (!firstPersonController.IsWalking())
                return;

            _footstepTimer += Time.deltaTime;
            
            if (_footstepTimer >= footstepInterval)
            {
                _footstepTimer = 0f;
                PlayFootStep();
            }
        }

        public void Play() => Instantiate(audioPlayer2D, transform.position, transform.rotation, transform);
        
        public void PlayDirection(int dir)
            => Instantiate(audioPlayer3D, audioPoints[dir].position, audioPoints[dir].rotation);

        public void PlayBreathingSound()
        {
            AudioSource newAudio = Instantiate(audioPlayer2D, transform.position, transform.rotation, transform);
            newAudio.clip = breathSound;
        }

        private void PlayFootStep()
        {
            AudioSource newAudio = Instantiate(audioPlayer2D, transform.position, transform.rotation, transform);
            int random = Random.Range(0, footstepsSound.Length);
            newAudio.clip = footstepsSound[random];
        }
    }
}
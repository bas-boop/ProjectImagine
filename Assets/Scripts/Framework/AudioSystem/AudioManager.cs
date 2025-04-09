using UnityEngine;
using Random = UnityEngine.Random;

namespace Framework.AudioSystem
{
    public sealed class AudioManager : MonoBehaviour
    {
        private const string BREATH_GAMEOBJECT_NAME = "BreathAudioPlayer";
        
        [Header("References")]
        [SerializeField] private FirstPersonController firstPersonController;
        [SerializeField] private AudioSource audioPlayer2D;
        [SerializeField] private AudioSource audioPlayer3D;
        [SerializeField] private Transform[] audioPoints;
        
        [Header("Audio")]
        [SerializeField] private AudioClip breathSound;
        [SerializeField] private AudioClip[] footstepsSound;
        [SerializeField] private float footstepInterval = 0.5f;
        [SerializeField] private float footstepVolume = 1.5f;
        
        // No header needed, there is a collapse for them 
        [SerializeField] private bool canPlayBreath = true;
        [SerializeField] private bool canPlayFootsteps = true;

        private bool _isPlayingBreath;
        private float _footstepTimer;
        private AudioSource _breathAudioPlayer;

        private void Awake()
        {
            if (!_breathAudioPlayer)
                InitBreathAudioPlayer();
        }

        private void Update()
        {
            if (!firstPersonController.IsWalking())
                return;

            _footstepTimer += Time.deltaTime;
            
            if (_footstepTimer >= footstepInterval)
            {
                _footstepTimer = 0;
                PlayFootStep();
            }
        }
        
        public void Play(AudioClip clip)
        {
            AudioSource newAudio = Instantiate(audioPlayer2D, transform.position, transform.rotation, transform);
            newAudio.clip = clip;
            newAudio.Play();
        }

        public void PlayDirection(int dir)
            => Instantiate(audioPlayer3D, audioPoints[dir].position, audioPoints[dir].rotation);

        public void ToggleBreathPlaying()
        {
            if (!canPlayBreath)
                return;
            
            _isPlayingBreath = !_isPlayingBreath;
            
            if (_isPlayingBreath)
                _breathAudioPlayer.Play();
            else
                _breathAudioPlayer.Stop();
        }

        private void InitBreathAudioPlayer()
        {
            _breathAudioPlayer = Instantiate(audioPlayer2D, transform.position, transform.rotation, transform);
            
            _breathAudioPlayer.name = BREATH_GAMEOBJECT_NAME;
            _breathAudioPlayer.playOnAwake = false;
            _breathAudioPlayer.clip = breathSound;
            _breathAudioPlayer.loop = true;
            
            _breathAudioPlayer.Stop();
        }

        private void PlayFootStep()
        {
            if (!canPlayFootsteps)
                return;
            
            AudioSource newAudio = Instantiate(audioPlayer2D, transform.position, transform.rotation, transform);
            int random = Random.Range(0, footstepsSound.Length);
            newAudio.clip = footstepsSound[random];
            newAudio.volume *= footstepVolume;
            newAudio.Play();
        }
    }
}
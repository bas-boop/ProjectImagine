using UnityEngine;

public class SimpleBackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.Play();
    }

    void OnDestroy()
    {
        audioSource.Stop();
    }
}
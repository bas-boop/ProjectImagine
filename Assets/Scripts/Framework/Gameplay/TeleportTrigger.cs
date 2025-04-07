using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TeleportTrigger : MonoBehaviour
{
    public Transform player;
    public Vector3 teleportDestination;
    public TextMeshProUGUI interactText;
    public Image fadeImage;
    public float fadeDuration = 0.5f;
    public AudioClip teleportSound;
    public AudioSource audioSource;

    private bool playerInRange = false;
    private bool isTeleporting = false;

    private void Start()
    {
        interactText.gameObject.SetActive(false);
        fadeImage.color = new Color(0, 0, 0, 0); // fully transparent
    }

    private void Update()
    {
        if (playerInRange && !isTeleporting && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactText.gameObject.SetActive(false);
        }
    }

    IEnumerator TeleportPlayer()
    {
        isTeleporting = true;
        interactText.gameObject.SetActive(false);

        // Play sound
        if (audioSource && teleportSound)
        {
            audioSource.PlayOneShot(teleportSound);
        }

        // Fade to black
        yield return StartCoroutine(Fade(0, 1));

        // Move player
        player.position = teleportDestination;

        // Fade back in
        yield return StartCoroutine(Fade(1, 0));

        isTeleporting = false;
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
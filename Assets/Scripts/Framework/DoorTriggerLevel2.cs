using UnityEngine;
using System.Collections;

public class DoorTriggerLevel2 : MonoBehaviour
{
    public Transform door1; // Assign first door prefab
    public Transform door2; // Assign second door prefab
    public float openAngle1 = 135f;
    public float openAngle2 = -135f;
    public float openSpeed = 2f;
    private bool hasActivated = false;
    private Quaternion initialRotation1;
    private Quaternion initialRotation2;
    private Quaternion targetRotation1;
    private Quaternion targetRotation2;

    private void Start()
    {
        initialRotation1 = door1.rotation;
        initialRotation2 = door2.rotation;
        targetRotation1 = initialRotation1 * Quaternion.Euler(0, openAngle1, 0);
        targetRotation2 = initialRotation2 * Quaternion.Euler(0, openAngle2, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasActivated)
        {
            hasActivated = true;
            StartCoroutine(OpenDoors());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && hasActivated)
        {
            StartCoroutine(CloseDoorsAndDestroy());
        }
    }

    private IEnumerator OpenDoors()
    {
        float elapsedTime = 0f;
        float duration = openSpeed;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            door1.rotation = Quaternion.Slerp(initialRotation1, targetRotation1, t);
            door2.rotation = Quaternion.Slerp(initialRotation2, targetRotation2, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door1.rotation = targetRotation1;
        door2.rotation = targetRotation2;
    }

    private IEnumerator CloseDoorsAndDestroy()
    {
        float elapsedTime = 0f;
        float duration = openSpeed;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            door1.rotation = Quaternion.Slerp(targetRotation1, initialRotation1, t);
            door2.rotation = Quaternion.Slerp(targetRotation2, initialRotation2, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door1.rotation = initialRotation1;
        door2.rotation = initialRotation2;
        Destroy(gameObject); // Remove the trigger box permanently after doors close
    }
}
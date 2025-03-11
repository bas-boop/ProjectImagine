using System.Collections;
using UnityEngine;

namespace Framework.Gameplay.DoorSystem
{
    public class DoorTriggerLevel2 : MonoBehaviour
    {
        [SerializeField] private Door leftDoor;
        [SerializeField] private Door rightDoor;
        [SerializeField] private float openSpeed = 2f;

        private bool _hasActivated;

        private void Start()
        {
            leftDoor.Initialize();
            rightDoor.Initialize();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_hasActivated)
            {
                _hasActivated = true;
                StartCoroutine(MoveDoor(leftDoor, true));
                StartCoroutine(MoveDoor(rightDoor, true));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && _hasActivated)
            {
                StartCoroutine(MoveDoor(leftDoor, false));
                StartCoroutine(MoveDoor(rightDoor, false));
                Destroy(gameObject);
            }
        }

        private IEnumerator MoveDoor(Door door, bool opening)
        {
            float elapsedTime = 0f;
            float duration = openSpeed;
            Quaternion startRot = door.transform.rotation;
            Quaternion targetRot = opening ? door.TargetRotation : door.InitialRotation;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                door.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            door.transform.rotation = targetRot;
        }
    }
}
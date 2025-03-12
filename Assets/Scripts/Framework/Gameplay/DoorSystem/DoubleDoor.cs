using System.Collections;
using UnityEngine;

namespace Framework.Gameplay.DoorSystem
{
    public sealed class DoubleDoor : MonoBehaviour
    {
        [SerializeField] private Door leftDoor;
        [SerializeField] private Door rightDoor;
        [SerializeField] private float openSpeed = 2f;

        [Header("Gizmos")]
        [SerializeField] private bool useGizmos;
        [SerializeField] private Color startLineColor = Color.red;
        [SerializeField] private Color endLineColor = Color.green;
        [SerializeField] private float lineLenght = 5;

        private void Awake()
        {
            leftDoor.Init();
            rightDoor.Init();
        }

        public void Open()
        {
            StartCoroutine(MoveDoor(leftDoor, true));
            StartCoroutine(MoveDoor(rightDoor, true));
        }

        public void Close()
        {
            StartCoroutine(MoveDoor(leftDoor, false));
            StartCoroutine(MoveDoor(rightDoor, false));
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

        private void OnDrawGizmos()
        {
            if (!useGizmos)
                return;
            
            DrawDoorGizmo(leftDoor, Vector3.forward);
            DrawDoorGizmo(rightDoor, Vector3.back);
        }

        private void DrawDoorGizmo(Door door, Vector3 dir)
        {
            if (door.transform == null)
                return;

            Vector3 startDirection = door.transform.rotation * dir;
            Vector3 endDirection = Quaternion.Euler(0, door.openAngle, 0) * startDirection;

            Gizmos.color = startLineColor;
            Gizmos.DrawLine(door.transform.position, door.transform.position + startDirection * lineLenght);

            Gizmos.color = endLineColor;
            Gizmos.DrawLine(door.transform.position, door.transform.position + endDirection * lineLenght);
        }
    }
}
using UnityEngine;

namespace Framework.Gameplay.DoorSystem
{
    [System.Serializable]
    public struct Door
    {
        public Transform transform;
        public float openAngle;
        public Quaternion InitialRotation { get; private set; }
        public Quaternion TargetRotation { get; private set; }

        public void Initialize()
        {
            InitialRotation = transform.rotation;
            TargetRotation = InitialRotation * Quaternion.Euler(0, openAngle, 0);
        }
    }
}
using System;
using Framework.Attributes;
using Framework.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.TriggerArea
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(MeshFilter))]
    public sealed class TriggerArea : MonoBehaviour
    {
        private const string FBX_SUFFIX = ".fbx";
        
        [SerializeField] private StandardMeshes shapeToUse;
        [SerializeField, Tag] private string tagToTriggerWith = "Player";
        [SerializeField] private TriggerBehaviour behaviour;
        [SerializeField] private bool isOneTimeUse;
        
        [Space(20)]
        [SerializeField] private UnityEvent onEnter = new();
        [SerializeField] private UnityEvent onExit = new();

        private MeshFilter _meshFilter;
        private BoxCollider _boxCollider;
        private SphereCollider _sphereCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _sphereCollider = GetComponent<SphereCollider>();
            _capsuleCollider = GetComponent<CapsuleCollider>();

            _boxCollider.isTrigger = true;
            _sphereCollider.isTrigger = true;
            _capsuleCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (behaviour == TriggerBehaviour.EXIT_ONLY
                || !other.CompareTag(tagToTriggerWith))
                return;
            
            onEnter?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (behaviour == TriggerBehaviour.ENTER_ONLY
                || !other.CompareTag(tagToTriggerWith))
                return;
            
            onExit?.Invoke();
        }

        private void OnValidate() => UpdateMesh();

        private void UpdateMesh()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.mesh = Resources.GetBuiltinResource<Mesh>(shapeToUse.GetStringValue() + FBX_SUFFIX);

            _boxCollider = GetComponent<BoxCollider>();
            _sphereCollider = GetComponent<SphereCollider>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            
            switch (shapeToUse)
            {
                case StandardMeshes.CUBE:
                    _boxCollider.enabled = true;
                    _sphereCollider.enabled = false;
                    _capsuleCollider.enabled = false;
                    break;
                
                case StandardMeshes.SPHERE:
                    _boxCollider.enabled = false;
                    _sphereCollider.enabled = true;
                    _capsuleCollider.enabled = false;
                    break;
                
                case StandardMeshes.CAPSULE:
                    _boxCollider.enabled = false;
                    _sphereCollider.enabled = false;
                    _capsuleCollider.enabled = true;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void TestTrigger()
        {
            switch (shapeToUse)
            {
                case StandardMeshes.CUBE:
                    Debug.Log("Cube");
                    break;
                case StandardMeshes.SPHERE:
                    Debug.Log("Sphere");
                    break;
                case StandardMeshes.CAPSULE:
                    Debug.Log("Capsule");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
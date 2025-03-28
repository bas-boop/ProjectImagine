using UI.Canvas;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class InputParser : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private InputActionAsset _inputActionAsset;
        
        [SerializeField] private TodoPaper todoPaper;
    
        private void Awake()
        {
            GetReferences();
            Init();
        }

        private void OnEnable() => AddListeners();

        private void OnDisable() => RemoveListeners();

        private void GetReferences()
        {
            _playerInput = GetComponent<PlayerInput>();

            if (!todoPaper)
                todoPaper = FindFirstObjectByType<TodoPaper>();
        }

        private void Init() => _inputActionAsset = _playerInput.actions;

        private void AddListeners()
        {
            _inputActionAsset["TogglePaper"].performed += TogglePaper;
        }

        private void RemoveListeners()
        {
            _inputActionAsset["TogglePaper"].performed -= TogglePaper;
        }
        
        #region Context
        
        private void TogglePaper(InputAction.CallbackContext context) => todoPaper.TogglePosition();

        #endregion
    }
}
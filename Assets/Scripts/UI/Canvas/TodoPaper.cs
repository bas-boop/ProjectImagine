using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Canvas
{
    public sealed class TodoPaper : MonoBehaviour
    {
        private const string CREATE_TODOS_ERROR = "TodoPrefab or TodoParent is not assigned in the Inspector.";
        private const float EASE_IN = 2;
        private const float EASE_OUT = 3;
        
        [Header("Settings todos")]
        [SerializeField] private Transform todoParent;
        [SerializeField] private TodoPair todoPrefab;
        [SerializeField] private List<string> todos;
        [SerializeField] private string secretTodo;
        
        [Header("Animation")]
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Vector2 hiddenPosition;
        [SerializeField] private Vector2 visiblePosition = Vector2.zero;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private float rotationAngle = 15f;
        [SerializeField] private UnityEvent onPaper = new();
        
        private Dictionary<string, TodoPair> _todoPairs = new ();
        private int _currentTodo;
        
        private Coroutine _currentAnimation;
        private bool _isShowingPaper;

        private void Start()
        {
            rectTransform.localPosition = hiddenPosition;
            
            TodoPair[] childTodos = todoParent.GetComponentsInChildren<TodoPair>();

            if (childTodos.Length > 0)
            {
                foreach (TodoPair pair in childTodos)
                {
                    _todoPairs.Add(pair.GetTask(), pair);
                }
            }
        }

        [ContextMenu("Create Todos")]
        public void CreatTodos()
        {
            for (int i = todoParent.childCount - 1; i >= 0; i--)
            {
                Transform child = todoParent.GetChild(i);
                DestroyImmediate(child.gameObject);
            }

            _todoPairs.Clear();

            if (todoPrefab == null 
                || todoParent == null)
                throw new Exception(CREATE_TODOS_ERROR);

            int lenght = todos.Count;
            
            for (int i = 0; i < lenght; i++)
            {
                TodoPair todo = Instantiate(todoPrefab, todoParent);
                todo.SetText(todos[i]);
                _todoPairs.Add(todos[i], todo);
            }
        }

        public void MarkToDone(string todo)
        {
            if (_todoPairs.ContainsKey(todo))
                _todoPairs[todo].SetTodoDone();
        }
        
        public void AddSecretTodo()
        {
            TodoPair todo = Instantiate(todoPrefab, todoParent);
            todo.SetText(secretTodo);
            _todoPairs.Add(secretTodo, todo);
        }

        public void TogglePosition()
        {
            if (_currentAnimation != null)
                StopCoroutine(_currentAnimation);
            
            Vector2 targetPosition = _isShowingPaper ? hiddenPosition : visiblePosition;
            float targetRotation = _isShowingPaper ? 0f : rotationAngle;

            onPaper?.Invoke();
            _currentAnimation = StartCoroutine(SmoothTransition(targetPosition, targetRotation));
            _isShowingPaper = !_isShowingPaper;
        }

        private IEnumerator SmoothTransition(Vector2 targetPosition, float targetRotation)
        {
            Vector2 startPosition = rectTransform.anchoredPosition;
            Quaternion startRotation = rectTransform.rotation;
            Quaternion targetRotationQuaternion = Quaternion.Euler(0, 0, targetRotation);

            float elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                float t = elapsedTime / animationDuration;

                // An ease-in-out effect formula I wanted to try out
                t = t * t * (EASE_OUT - EASE_IN * t);

                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
                rectTransform.rotation = Quaternion.Slerp(startRotation, targetRotationQuaternion, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.anchoredPosition = targetPosition;
            rectTransform.rotation = targetRotationQuaternion;
        }
    }
}

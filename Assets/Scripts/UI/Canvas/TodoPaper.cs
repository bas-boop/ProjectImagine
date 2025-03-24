using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Canvas
{
    public sealed class TodoPaper : MonoBehaviour
    {
        private const string CREATE_TODOS_ERROR = "TodoPrefab or TodoParent is not assigned in the Inspector.";
        
        [SerializeField] private Transform todoParent;
        [SerializeField] private TodoPair todoPrefab;
        [SerializeField] private List<string> todos;
        
        private List<TodoPair> _todoObjects = new ();
        private int _currentTodo;

        private void Start()
        {
            TodoPair[] childTodos = todoParent.GetComponentsInChildren<TodoPair>();
            
            if (childTodos.Length > 0)
                _todoObjects.AddRange(childTodos);
        }

        [ContextMenu("Create Todos")]
        public void CreatTodos()
        {
            for (int i = todoParent.childCount - 1; i >= 0; i--)
            {
                Transform child = todoParent.GetChild(i);
                Debug.Log(i);
                DestroyImmediate(child.gameObject);
            }

            _todoObjects.Clear();

            if (todoPrefab == null 
                || todoParent == null)
                throw new Exception(CREATE_TODOS_ERROR);

            int lenght = todos.Count;
            
            for (int i = 0; i < lenght; i++)
            {
                TodoPair todo = Instantiate(todoPrefab, todoParent);
                todo.SetText(todos[i]);
                _todoObjects.Add(todo);
            }
        }

        public void MarkNextTodoDone()
        {
            if (_currentTodo == _todoObjects.Count)
                return;
            
            _todoObjects[_currentTodo].SetTodoDone();
            _currentTodo++;
        }
    }
}

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
        
        private List<TodoPair> _todoObjects;

        [ContextMenu("Create Todos")]
        public void CreatTodos()
        {
            if (_todoObjects is { Count: > 0 })
                _todoObjects.Clear();

            if (todoPrefab == null || todoParent == null)
                throw new Exception(CREATE_TODOS_ERROR);

            int lenght = todos.Count;
            
            for (int i = 0; i < lenght; i++)
            {
                TodoPair todo = Instantiate(todoPrefab, todoParent);
                todo.SetText(todos[i]);
                _todoObjects.Add(todo);
            }
        }
    }
}

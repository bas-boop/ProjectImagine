using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Canvas
{
    public sealed class Dialogue : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private float characterDelay = 0.05f;
        [SerializeField] private float readTime = 3;
        [SerializeField] private UnityEvent onCharacterTyped;
        [SerializeField] private UnityEvent onDoneShowing;

        private Coroutine _typingCoroutine;
        private List<string> _shownDialogue = new ();

        private void Awake() => text.text = string.Empty;

        public void ShowNewDialogue(string target)
        {
            if (_shownDialogue.Count > 0
                && _shownDialogue.Contains(target))
                return;
            
            if (_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);
            
            _shownDialogue.Add(target);
            _typingCoroutine = StartCoroutine(TypeText(target));
        }

        private IEnumerator TypeText(string target)
        {
            text.text = string.Empty;

            foreach (char c in target)
            {
                text.text += c;
                onCharacterTyped?.Invoke();
                yield return new WaitForSeconds(characterDelay);
            }

            yield return new WaitForSeconds(readTime);
            
            text.text = string.Empty;
            _typingCoroutine = null;
            onDoneShowing?.Invoke();
        }
    }
}
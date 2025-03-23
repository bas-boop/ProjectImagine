using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvas
{
    public class TodoPair : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image scrapedOutImage;

        private void Start() => scrapedOutImage.gameObject.SetActive(false);

        public void SetText(string target) => text.text = target;
        
        public void SetTodoDone() => scrapedOutImage.gameObject.SetActive(true);
    }
}
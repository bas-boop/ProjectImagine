using UnityEngine;

namespace Framework
{
    public sealed class GameCloser : MonoBehaviour
    {
        public void CloseGame() => Application.Quit();
    }
}
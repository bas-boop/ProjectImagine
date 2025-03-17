using UnityEngine;

namespace Environment
{
    public sealed class FogChanger : MonoBehaviour
    {
        private const string HASH = "#";
        
        public void ChangeFogHexColor(string targetHexColor)
        {
            if (ColorUtility.TryParseHtmlString(HASH + targetHexColor, out Color hexColor))
                RenderSettings.fogColor = hexColor;
        }
        
        public void ChangeFogColor(string targetColor)
        {
            if (ColorUtility.TryParseHtmlString(targetColor, out Color hexColor))
                RenderSettings.fogColor = hexColor;
        }

        public void ChangeFogDensity(float target) => RenderSettings.fogDensity = Mathf.Clamp01(target);
    }
}

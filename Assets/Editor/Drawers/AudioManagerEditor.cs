using UnityEngine;
using UnityEditor;

using Framework.AudioSystem;

namespace Editor.Drawers
{
    [CustomEditor(typeof(AudioManager))]
    public sealed class AudioManagerEditor : UnityEditor.Editor
    {
        private const string BREATH_PROPERTY_NAME = "canPlayBreath";
        private const string FOOTSTEPS_PROPERTY_NAME = "canPlayFootsteps";
        private const string DEBUG_OPTIONS = "Debug Options";
        private const string BREATH_INSPECTOR_NAME = "Can Play Breath";
        private const string FOOTSTEPS_INSPECTOR_NAME = "Can Play Footsteps";
        
        private SerializedProperty _canPlayBreath;
        private SerializedProperty _canPlayFootsteps;

        private bool ShowDebugOptions
        {
            get => EditorPrefs.GetBool($"{target.GetInstanceID()}_showDebugOptions", false);
            set => EditorPrefs.SetBool($"{target.GetInstanceID()}_showDebugOptions", value);
        }

        private SerializedProperty _iterator;

        private void OnEnable()
        {
            _canPlayBreath = serializedObject.FindProperty(BREATH_PROPERTY_NAME);
            _canPlayFootsteps = serializedObject.FindProperty(FOOTSTEPS_PROPERTY_NAME);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            _iterator = serializedObject.GetIterator();

            bool enterChildren = true;

            while (_iterator.NextVisible(enterChildren))
            {
                enterChildren = false;

                if (_iterator.propertyPath is BREATH_PROPERTY_NAME or FOOTSTEPS_PROPERTY_NAME)
                    continue;

                EditorGUILayout.PropertyField(_iterator, true);
            }

            EditorGUILayout.Space();
            ShowDebugOptions = EditorGUILayout.Foldout(ShowDebugOptions, DEBUG_OPTIONS,
                                            true, EditorStyles.foldoutHeader);

            if (ShowDebugOptions)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_canPlayBreath, new GUIContent(BREATH_INSPECTOR_NAME));
                EditorGUILayout.PropertyField(_canPlayFootsteps, new GUIContent(FOOTSTEPS_INSPECTOR_NAME));
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
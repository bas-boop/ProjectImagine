using UnityEngine;
using UnityEditor;

using Framework.AudioSystem;

namespace Editor.Drawers
{
    [CustomEditor(typeof(AudioManager))]
    public sealed class AudioManagerEditor : UnityEditor.Editor
    {
        private SerializedProperty canPlayBreath;
        private SerializedProperty canPlayFootsteps;

        // Persistent state for the foldout
        private bool showDebugOptions
        {
            get => EditorPrefs.GetBool($"{target.GetInstanceID()}_showDebugOptions", false);
            set => EditorPrefs.SetBool($"{target.GetInstanceID()}_showDebugOptions", value);
        }

        private SerializedProperty iterator; // For iterating through serialized properties

        private void OnEnable()
        {
            // Link serialized properties
            canPlayBreath = serializedObject.FindProperty("canPlayBreath");
            canPlayFootsteps = serializedObject.FindProperty("canPlayFootsteps");
        }

        public override void OnInspectorGUI()
        {
            // Start iterating over all serialized properties
            serializedObject.Update();
            iterator = serializedObject.GetIterator();

            bool enterChildren = true;

            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;

                // Skip drawing debug-related properties
                if (iterator.propertyPath == "canPlayBreath" || iterator.propertyPath == "canPlayFootsteps")
                    continue;

                // Draw all other properties
                EditorGUILayout.PropertyField(iterator, true);
            }

            // Add a foldout section for Debug Options
            EditorGUILayout.Space();
            showDebugOptions = EditorGUILayout.Foldout(showDebugOptions, "Debug Options", true, EditorStyles.foldoutHeader);

            // If the foldout is expanded, display the debug fields
            if (showDebugOptions)
            {
                EditorGUI.indentLevel++; // Indent for better styling
                EditorGUILayout.PropertyField(canPlayBreath, new GUIContent("Can Play Breath"));
                EditorGUILayout.PropertyField(canPlayFootsteps, new GUIContent("Can Play Footsteps"));
                EditorGUI.indentLevel--; // Reset indent level
            }

            // Apply any property changes
            serializedObject.ApplyModifiedProperties();
        }
    }
}
using UnityEditor;
using UnityEngine;

namespace Sibz.EditorList.Editor
{
    /// <summary>
    /// Base class to derive from when making nice editor lists.
    /// </summary>
    /// <typeparam name="T">Type of item in the list</typeparam>
    public abstract partial class ListDrawer<T> : PropertyDrawer
    {
        /// <summary>
        /// Main function responsible for structuring the property.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Property = property;

            EditorGUI.BeginProperty(position, label, property);
            if (AddColonToLabel)
            {
                label.text = label.text + ":";
            }

            if (UseFoldout && (IsFoldedOut = Foldout(IsFoldedOut, label)) || !UseFoldout)
            {
                if (!UseFoldout)
                {
                    GUILayout.Label(label, NonFoldedHeadingLabelStyle);
                }

                EditorGUI.indentLevel += IndentLevelChange;
                {
                    if (IndentContent)
                    {
                        EditorGUI.indentLevel++;
                    }

                    ContentSection(label);

                    if (IndentContent)
                    {
                        EditorGUI.indentLevel--;
                    }
                }
                EditorGUI.indentLevel -= IndentLevelChange;
            }
            EditorGUI.EndProperty();
        }

        // Gets rid of space atop the list.
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) { return 0f; }
    }
}

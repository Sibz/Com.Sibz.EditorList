using UnityEditor;
using UnityEngine;

namespace Sibz.EditorList
{
    /// <summary>
    /// Base class to derive from when making nice editor lists.
    /// </summary>
    /// <typeparam name="T">Type of item in the list</typeparam>
    public abstract partial class ListDrawer<T> : PropertyDrawer
    {
        protected virtual void ContentSection(GUIContent label)
        {

            var listProperty = Property.FindPropertyRelative(nameof(EditorList<T>.List));
            if (listProperty != null)
            {
                Header(listProperty, label);

                ListAreaSection(listProperty);

                Footer(listProperty);
            }
            else
            {
                Debug.LogWarning($"{nameof(ListDrawer<T>)}: Unable to get list property. Be sure your property extends EditorList<T>.");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="listProperty"></param>
        protected virtual void ListAreaSection(SerializedProperty listProperty)
        {
            for (int i = 0; i < listProperty.arraySize; i++)
            {
                var listItemProperty = listProperty.GetArrayElementAtIndex(i);
                ListItemAreaDrawer(listProperty, listItemProperty, i);
            }
        }

        /// <summary>
        /// Header section. Can be overriden to change what content appears above the list.
        /// </summary>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        /// <param name="label">Label provided to the main PropertyField</param>
        protected virtual void Header(SerializedProperty listProperty, GUIContent label) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="listProperty"></param>
        /// <param name="listItemProperty"></param>
        /// <param name="index"></param>
        protected virtual void ListItemAreaDrawer(SerializedProperty listProperty, SerializedProperty listItemProperty, int index)
        {
            EditorGUILayout.BeginHorizontal();
            {
                ListItemDrawer(listProperty, listItemProperty, index);

                ListItemButtonsDrawer(listProperty, listItemProperty, index);

            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Item section. Defaults to a Property Field. Override if required. Is drawn inside Horizontal Section with buttons.
        /// </summary>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        /// <param name="listItemProperty">The listItem SerializedProperty</param>
        /// <param name="index">Index of item</param>
        protected virtual void ListItemDrawer(SerializedProperty listProperty, SerializedProperty listItemProperty, int index)
        {
            EditorGUILayout.PropertyField(listItemProperty, GUIContent.none);
        }

        /// <summary>
        /// Item Buttons Section. Defaults to Delete button and if Ordered=true, MoveUp and MoveDown  buttons.
        /// Is drawn inside Horizontal Section with list item.
        /// </summary>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        /// <param name="listItemProperty">The listItem SerializedProperty</param>
        /// <param name="index">Index of item</param>
        protected virtual void ListItemButtonsDrawer(SerializedProperty listProperty, SerializedProperty listItemProperty, int index)
        {
            if (DeleteButton)
            {
                DeleteItem(listProperty, listItemProperty, index);
            }
            if (Ordered)
            {
                if (MoveUpButton)
                {
                    MoveUp(listProperty, listItemProperty, index);
                }

                if (MoveDownButton)
                {
                    MoveDown(listProperty, listItemProperty, index);
                }
            }
        }

        /// <summary>
        /// Footer section. Defaults to a Delete All Button.
        /// Can be overridden to display other content, or remove the button.
        /// </summary>
        /// <param name="property">The main SerializedProperty the list belongs to</param>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        protected virtual void Footer(SerializedProperty listProperty)
        {
            EditorGUILayout.BeginHorizontal();
            {
                int indent = IndentLevelChange + (IndentContent ? 1 : 0);
                GUILayout.Label("", GUILayout.MaxWidth(indent * 10));
                if (DeleteAllButton)
                {
                    ClearList(listProperty);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}

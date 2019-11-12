using System;
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
                HeaderSection(listProperty, label);

                ListAreaSection(listProperty);

                FooterSection(listProperty);
            }
            else
            {
                Debug.LogWarning($"{nameof(ListDrawer<T>)}: Unable to get list property. Be sure your property extends EditorList<T> and that the property class & T are Serializable");
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
                ListItemAreaSection(listProperty, listItemProperty, i);
            }
        }
        [Obsolete("Use ListAreaSection instead")]
        protected virtual void ListAreaDrawer(SerializedProperty listProperty) => ListAreaSection(listProperty);

        /// <summary>
        /// Header section. Can be overriden to change what content appears above the list.
        /// </summary>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        /// <param name="label">Label provided to the main PropertyField</param>
        protected virtual void HeaderSection(SerializedProperty listProperty, GUIContent label) { }
        [Obsolete("Use HeaderSection instead")]
        protected virtual void Header(SerializedProperty listProperty, GUIContent label) => HeaderSection(listProperty, label);

        /// <summary>
        ///
        /// </summary>
        /// <param name="listProperty"></param>
        /// <param name="listItemProperty"></param>
        /// <param name="index"></param>
        protected virtual void ListItemAreaSection(SerializedProperty listProperty, SerializedProperty listItemProperty, int index)
        {
            EditorGUILayout.BeginHorizontal();
            {
                ListItemSection(listProperty, listItemProperty, index);

                ListItemButtonsSection(listProperty, listItemProperty, index);

            }
            EditorGUILayout.EndHorizontal();
        }
        [Obsolete("Use ListItemAreaSection instead")]
        protected virtual void ListItemAreaDrawer(SerializedProperty listProperty, SerializedProperty listItemProperty, int index) => ListItemAreaSection(listProperty, listItemProperty, index);

        /// <summary>
        /// Item section. Defaults to a Property Field. Override if required. Is drawn inside Horizontal Section with buttons.
        /// </summary>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        /// <param name="listItemProperty">The listItem SerializedProperty</param>
        /// <param name="index">Index of item</param>
        protected virtual void ListItemSection(SerializedProperty listProperty, SerializedProperty listItemProperty, int index)
        {
            EditorGUILayout.PropertyField(listItemProperty, GUIContent.none);
        }

        [Obsolete("Use ListItemSection instead")]
        protected virtual void ListItemDrawer(SerializedProperty listProperty, SerializedProperty listItemProperty, int index) => ListItemSection(listProperty, listItemProperty, index);
        /// <summary>
        /// Item Buttons Section. Defaults to Delete button and if Ordered=true, MoveUp and MoveDown  buttons.
        /// Is drawn inside Horizontal Section with list item.
        /// </summary>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        /// <param name="listItemProperty">The listItem SerializedProperty</param>
        /// <param name="index">Index of item</param>
        protected virtual void ListItemButtonsSection(SerializedProperty listProperty, SerializedProperty listItemProperty, int index)
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
        [Obsolete("Use ListItemButtonsSection instead")]
        protected virtual void ListItemButtonsDrawer(SerializedProperty listProperty, SerializedProperty listItemProperty, int index) => ListItemButtonsSection(listProperty, listItemProperty, index);

        /// <summary>
        /// Footer section. Defaults to a Delete All Button.
        /// Can be overridden to display other content, or remove the button.
        /// </summary>
        /// <param name="property">The main SerializedProperty the list belongs to</param>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        protected virtual void FooterSection(SerializedProperty listProperty)
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
        [Obsolete("Use FooterSection instead")]
        protected virtual void Footer(SerializedProperty listProperty) => FooterSection(listProperty);
    }
}

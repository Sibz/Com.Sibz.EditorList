﻿using UnityEditor;
using UnityEngine;

namespace Sibz.EditorList.Editor
{
    /// <summary>
    /// Base class to derive from when making nice editor lists.
    /// </summary>
    /// <typeparam name="T">Type of item in the list</typeparam>
    public abstract class ListDrawer<T> : PropertyDrawer
    {
        /// <summary>
        /// Stores state of foldout
        /// This can be overridden to allow implementor to save the state
        /// </summary>
        public virtual bool IsFoldedOut { get; set; } = true;

        protected SerializedProperty Property { get; private set; }

        /// <summary>
        /// Indicates if list is ordered
        /// If ordered full set of delete, moveup and movedown buttons
        /// Otherwise just the delete button
        /// </summary>
        protected virtual bool Ordered => true;

        /// <summary>
        /// Indicates whether list should be in a foldout section
        /// If not if will have a heading
        /// </summary>
        protected virtual bool UseFoldout => true;

        /// <summary>
        /// Used to add colen to end of property label text
        /// Defaults to true if not a foldout, otherwise false
        /// </summary>
        protected virtual bool AddColonToLabel => !UseFoldout;

        /// <summary>
        /// Used to indent content under heading / foldout
        /// Defaults to true
        /// </summary>
        protected virtual bool IndentContent => true;

        /// <summary>
        /// Level of indent change the drawer to apply
        /// </summary>
        protected virtual int IndentLevelChange => 0;

        /// <summary>
        /// Default button to use for delete all.
        /// If overridden, property must display a button (or similar control) and return true to trigger action
        /// </summary>
        protected virtual bool DeleteAllButton => GUILayout.Button("Delete All");

        /// <summary>
        /// Default delete button on each item.
        /// If overridden, property must display a button (or similar control) and return true to trigger action
        /// </summary>
        protected virtual bool DeleteButton => Ordered ? GUILayout.Button("x", ItemButtonsOptions) : GUILayout.Button("Delete", ItemButtonsOptions);

        /// <summary>
        /// Default move up button on each item.
        /// If overridden, property must display a button (or similar control) and return true to trigger action
        /// </summary>
        protected virtual bool MoveUpButton => GUILayout.Button("↑", ItemButtonsOptions);

        /// <summary>
        /// Default move down button on each item.
        /// If overridden, property must display a button (or similar control) and return true to trigger action
        /// </summary>
        protected virtual bool MoveDownButton => GUILayout.Button("↓", ItemButtonsOptions);

        /// <summary>
        /// Default foldout function
        /// If overridden, function must display a foldout (or similar control) and return true to show content
        /// </summary>
        protected virtual bool Foldout(bool foldedOut, GUIContent label) => EditorGUILayout.Foldout(foldedOut, label);

        /// <summary>
        /// Style of the header label when fold out is not selected
        /// </summary>
        protected virtual GUIStyle NonFoldedHeadingLabelStyle => new GUIStyle { fontStyle = FontStyle.Bold };

        /// <summary>
        /// Options to use for list item buttons (delete move up etc).
        /// <para>Use 'Ordered' property to distinguish buttons between ordered/unordered lists</para>
        /// </summary>
        protected virtual GUILayoutOption[] ItemButtonsOptions => Ordered ? new GUILayoutOption[] { GUILayout.MaxWidth(20) } : new GUILayoutOption[0];


        protected virtual void ContentSection(GUIContent label)
        {

            var listProperty = Property.FindPropertyRelative(nameof(EditorList<T>.List));
            if (listProperty != null)
            {
                Header(listProperty, label);

                for (int i = 0; i < listProperty.arraySize; i++)
                {
                    var listItemProperty = listProperty.GetArrayElementAtIndex(i);
                    ListItemAreaDrawer(listProperty, listItemProperty, i);
                }

                Footer(listProperty);
            }
            else
            {
                Debug.LogWarning($"{nameof(ListDrawer<T>)}: Unable to get list property. Be sure your property extends EditorList<T>.");
            }
        }

        /// <summary>
        /// Header section. Can be overriden to change what content appears above the list.
        /// </summary>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        /// <param name="label">Label provided to the main PropertyField</param>
        protected virtual void Header(SerializedProperty listProperty, GUIContent label) { }


        protected virtual void ListItemAreaDrawer(SerializedProperty listProperty, SerializedProperty listItemProperty, int index)
        {
            GUILayout.BeginHorizontal();
            {
                ListItemDrawer(listProperty, listItemProperty, index);

                ListItemButtonsDrawer(listProperty, listItemProperty, index);

            }
            GUILayout.EndHorizontal();
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
            if (DeleteAllButton)
            {
                ClearList(listProperty);
            }
        }

        /// <summary>
        /// Clears the list property.
        /// <para>Can be overridden if not altering property directly or other logic is required.</para>
        /// </summary>
        /// <param name="property">The main SerializedProperty the list belongs to</param>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        protected virtual void ClearList(SerializedProperty listProperty) => listProperty.ClearArray();

        /// <summary>
        /// Deletes an item from the list property.
        /// <para>Can be overridden if not altering property directly or other logic is required.</para>
        /// </summary>
        /// <param name="listProperty"></param>
        /// <param name="listItemProperty"></param>
        /// <param name="index"></param>
        protected virtual void DeleteItem(SerializedProperty listProperty, SerializedProperty listItemProperty, int index)
        {
            listProperty.DeleteArrayElementAtIndex(index);
        }

        /// <summary>
        /// Moves an item within the list property. Is only called if toIndex is valid (I.e. not outside the list).
        /// <para>Can be overridden if not altering property directly or other logic is required.</para>
        /// </summary>
        /// <param name="listProperty"></param>
        /// <param name="listItemProperty"></param>
        /// <param name="fromIndex"></param>
        /// <param name="toIndex"></param>
        protected virtual void MoveTo(SerializedProperty listProperty, SerializedProperty listItemProperty, int fromIndex, int toIndex)
        {
            listProperty.MoveArrayElement(fromIndex, toIndex);
        }

        /// <summary>
        /// Checks item index and calls MoveTo
        /// </summary>
        /// <param name="listProperty"></param>
        /// <param name="listItemProperty"></param>
        /// <param name="fromIndex"></param>
        private void MoveUp(SerializedProperty listProperty, SerializedProperty listItemProperty, int fromIndex)
        {
            if (fromIndex > 0)
            {
                MoveTo(listProperty, listItemProperty, fromIndex, fromIndex - 1);
            }
        }

        /// <summary>
        /// Checks item index and calls MoveTo
        /// </summary>
        /// <param name="listProperty"></param>
        /// <param name="listItemProperty"></param>
        /// <param name="fromIndex"></param>
        private void MoveDown(SerializedProperty listProperty, SerializedProperty listItemProperty, int fromIndex)
        {
            if (fromIndex < listProperty.arraySize)
            {
                MoveTo(listProperty, listItemProperty, fromIndex, fromIndex + 1);
            }
        }

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

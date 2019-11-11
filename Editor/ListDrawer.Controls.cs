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
    }
}

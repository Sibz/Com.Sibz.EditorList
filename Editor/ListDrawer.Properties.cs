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
        /// Style of the header label when fold out is not selected
        /// </summary>
        protected virtual GUIStyle NonFoldedHeadingLabelStyle => new GUIStyle { fontStyle = FontStyle.Bold };

        /// <summary>
        /// Options to use for list item buttons (delete move up etc).
        /// <para>Use 'Ordered' property to distinguish buttons between ordered/unordered lists</para>
        /// </summary>
        protected virtual GUILayoutOption[] ItemButtonsOptions => Ordered ? new GUILayoutOption[] { GUILayout.MaxWidth(20) } : new GUILayoutOption[0];
    }
}

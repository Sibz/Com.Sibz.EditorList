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
        /// <summary>
        /// Clears the list property.
        /// <para>Can be overridden if not altering property directly or other logic is required.</para>
        /// </summary>
        /// <param name="property">The main SerializedProperty the list belongs to</param>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        protected virtual void ClearList() => ListProperty.ClearArray();
        [Obsolete("Use ClearList() instead")]
        protected virtual void ClearList(SerializedProperty listProperty) => listProperty.ClearArray();

        /// <summary>
        /// Deletes an item from the list property.
        /// <para>Can be overridden if not altering property directly or other logic is required.</para>
        /// </summary>
        /// <param name="listProperty"></param>
        /// <param name="listItemProperty"></param>
        /// <param name="index"></param>
        protected virtual void DeleteItem(SerializedProperty listItemProperty, int index)
        {
            ListProperty.DeleteArrayElementAtIndex(index);
        }
        [Obsolete("Use DeleteItem(SerializedProperty listItemProperty, int index) instead")]
        protected virtual void DeleteItem(SerializedProperty listProperty, SerializedProperty listItemProperty, int index) => DeleteItem(listItemProperty, index);
        /// <summary>
        /// Moves an item within the list property. Is only called if toIndex is valid (I.e. not outside the list).
        /// <para>Can be overridden if not altering property directly or other logic is required.</para>
        /// </summary>
        /// <param name="listProperty"></param>
        /// <param name="listItemProperty"></param>
        /// <param name="fromIndex"></param>
        /// <param name="toIndex"></param>
        protected virtual void MoveTo(SerializedProperty listItemProperty, int fromIndex, int toIndex)
        {
            ListProperty.MoveArrayElement(fromIndex, toIndex);
        }
        [Obsolete("Use MoveTo(SerializedProperty listItemProperty, int index) instead")]
        protected virtual void MoveTo(SerializedProperty listProperty, SerializedProperty listItemProperty, int fromIndex, int toIndex) => MoveTo(listItemProperty, fromIndex, toIndex);

        /// <summary>
        /// Checks item index and calls MoveTo
        /// </summary>
        /// <param name="listProperty"></param>
        /// <param name="listItemProperty"></param>
        /// <param name="fromIndex"></param>
        protected void MoveUp(SerializedProperty listItemProperty, int fromIndex)
        {
            if (fromIndex > 0)
            {
                MoveTo(listItemProperty, fromIndex, fromIndex - 1);
            }
        }

        /// <summary>
        /// Checks item index and calls MoveTo
        /// </summary>
        /// <param name="listProperty"></param>
        /// <param name="listItemProperty"></param>
        /// <param name="fromIndex"></param>
        protected void MoveDown(SerializedProperty listItemProperty, int fromIndex)
        {
            if (fromIndex < ListProperty.arraySize)
            {
                MoveTo(listItemProperty, fromIndex, fromIndex + 1);
            }
        }
    }
}

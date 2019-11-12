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
    }
}

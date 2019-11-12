using UnityEditor;
using UnityEngine;

namespace Sibz.EditorList
{
    /// <summary>
    /// Base class for nicer UnityEngine.Object lists in the editor
    /// </summary>
    /// <typeparam name="T">Object type, must derive from UnityEngine.Object</typeparam>
    public abstract class ObjectListDrawer<T> : ListDrawer<T> where T : UnityEngine.Object
    {
        /// <summary>
        /// Allow items on list to be null.
        /// Defaults to true. If overriden to false, items that are null will be deleted.
        /// </summary>
        protected virtual bool AllowNullObjects => true;

        /// <summary>
        /// Default clear object button on each item.
        /// <para>If overridden, property must display a button (or similar control) and return true to trigger action</para>
        /// </summary>
        protected virtual bool ClearButton => Ordered ? GUILayout.Button("c", ItemButtonsOptions) : GUILayout.Button("Clear", ItemButtonsOptions);

        /// <summary>
        /// Function used to display the object. Defaults to a EditorGUILayout.ObjectField.
        /// </summary>
        /// <param name="obj">Object to use in object field</param>
        /// <returns>Current Object in the object field.</returns>
        protected virtual T AddObjectField(T obj) => (T)EditorGUILayout.ObjectField("Drop to add to list:", obj, typeof(T), true);

        /// <summary>
        /// Clears an object in the array, item will be null but still exist in the array.
        /// </summary>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        /// <param name="listItemProperty">The listItem SerializedProperty</param>
        /// <param name="index">Index of item</param>
        protected virtual void ClearItem(SerializedProperty listItemProperty, int index)
        {
            // Check value is not null, otherwise we will unset it
            if (listItemProperty.objectReferenceValue != null)
            {
                ListProperty.DeleteArrayElementAtIndex(index);
            }
        }

        /// <summary>
        /// Deletes an object in the array. Calls a second time incase first time only cleared the item.
        /// </summary>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        /// <param name="listItemProperty">The listItem SerializedProperty</param>
        /// <param name="index">Index of item</param>
        protected override void DeleteItem(SerializedProperty listItemProperty, int index)
        {
            int count = ListProperty.arraySize;

            base.DeleteItem(listItemProperty, index);

            // If deleting a object field, the first time will only clear the GO
            // We need a second attempt to delete it
            if (ListProperty.arraySize == count)
            {
                base.DeleteItem(listItemProperty, index);
            }
        }

        /// <summary>
        /// Header Section. This overridden version displays the Add Object Field.
        /// </summary>
        /// <param name="property">The main SerializedProperty the list belongs to</param>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        /// <param name="label">Label provided to the main PropertyField</param>
        protected override void HeaderSection(GUIContent label)
        {
            T go = null;
            if ((go = AddObjectField(go)) != null)
            {
                int newItemIndex = ListProperty.arraySize;
                ListProperty.InsertArrayElementAtIndex(newItemIndex);
                ListProperty.GetArrayElementAtIndex(newItemIndex).objectReferenceValue = go;
            }
        }

        /// <summary>
        /// List items buttons drawer. This overriden version adds a clear button if AllowNullObjects = true.
        /// </summary>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        /// <param name="listItemProperty">The listItem SerializedProperty</param>
        /// <param name="index">Index of item</param>
        protected override void ListItemButtonsSection(SerializedProperty listItemProperty, int index)
        {
            if (AllowNullObjects)
            {
                if (ClearButton)
                {
                    ClearItem(listItemProperty, index);
                }
            }
            base.ListItemButtonsSection(listItemProperty, index);
        }

        /// <summary>
        /// List item drawer. This overridden version will remove items if they are null and AllowNullObjects = false
        /// </summary>
        /// <param name="listProperty">The list SerializedProperty that has the associated array attached.</param>
        /// <param name="listItemProperty">The listItem SerializedProperty</param>
        /// <param name="index">Index of item</param>
        protected override void ListItemSection(SerializedProperty listItemProperty, int index)
        {
            if (!AllowNullObjects && listItemProperty.objectReferenceValue == null)
            {
                DeleteItem(listItemProperty, index);
            }
            else
            {
                base.ListItemSection(listItemProperty, index);
            }
        }
    }
}

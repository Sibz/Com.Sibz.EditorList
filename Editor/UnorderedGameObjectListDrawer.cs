using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(UnorderedGameObjectList))]
public class UnorderedGameObjectListDrawer : Sibz.EditorList.Editor.ObjectListDrawer<GameObject>
{
    protected override bool Ordered => false;
}

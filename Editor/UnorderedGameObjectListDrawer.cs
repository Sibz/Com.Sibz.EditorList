using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(UnorderedGameObjectList))]
public class UnorderedGameObjectListDrawer : Sibz.EditorList.ObjectListDrawer<GameObject>
{
    protected override bool Ordered => false;
}

using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(OrderedGameObjectList))]
public class OrderedGameObjectListDrawer : Sibz.EditorList.Editor.ObjectListDrawer<GameObject>
{
    protected override bool Ordered => true;
}



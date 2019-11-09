using UnityEditor;
using UnityEngine;
using Sibz.EditorList.Editor;

[CustomPropertyDrawer(typeof(OrderedGameObjectList))]
public class OrderedGameObjectListDrawer : ObjectListDrawer<GameObject>
{
    protected override bool Ordered => true;
}



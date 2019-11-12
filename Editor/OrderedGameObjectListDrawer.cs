using UnityEditor;
using UnityEngine;
using Sibz.EditorList;

[CustomPropertyDrawer(typeof(OrderedGameObjectList))]
public class OrderedGameObjectListDrawer : ObjectListDrawer<GameObject>
{
    protected override bool Ordered => true;
}



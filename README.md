## Com.Sibz.EditorList
This packages enables lists that can be nicely displayed in the editor. It's fully extensible.

Contains two implementations
 - UnorderedGameObjectList
 - OrderedGameObjectList
 
 Both are very similar, they display a list of game objects, however the ordered one has ordering buttons.
 
 ### Usage 
 
 ```csharp
using UnityEditor;
using UnityEngine;

public class MyTestMono : MonoBehaviour
{
    public UnorderedGameObjectList GameObjectsList1;
    public OrderedGameObjectList GameObjectsList2;
}

// This is required to enable the use of GUILayout. Can also be put in own file.
#if UNITY_EDITOR
[CustomEditor(typeof(MyTestMono)), CanEditMultipleObjects]
public class MyTestMonoCI : Editor{}
#endif
 ```
This should give you something similar to:

<a href="https://imgbb.com/"><img src="https://i.ibb.co/Cbn9DSj/sample.png" alt="sample" border="0"></a>

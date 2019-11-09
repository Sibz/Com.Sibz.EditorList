## Com.Sibz.EditorList
This packages enables lists that can be nicely displayed in the editor. It's fully extensible.

Contains two implementations
 - UnorderedGameObjectList
 - OrderedGameObjectList
 
 Both are very similar, they display a list of game objects, however the ordered one has ordering buttons.
 
 ### Usage 
 
 ```csharp
public class MyTestMono : MonoBehaviour
{
    public UnorderedGameObjectList GameObjectsList1;
    public OrderedGameObjectList GameObjectsList2;
}

// This is required to enable the use of GUILayout. Can also be put in own file.
#if UNITY_EDITOR
[CustomEditor(typeof(MyTestMono))]
public class MyTestMonoCI : Editor{}
#endif
 ```

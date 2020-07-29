using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "Ship Equipment", order = 1)]
public class sItem : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }

    public string itemName;
    public Sprite itemSprite;

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
}

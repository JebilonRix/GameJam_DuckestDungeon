using UnityEngine;


[CreateAssetMenu(fileName = "Item Data", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Npc Data", menuName = "ScriptableObjects/Npc Data", order = 1)]
public class NpcData : ScriptableObject
{
    [FormerlySerializedAs("_idleAnim")] public RuntimeAnimatorController IdleAnim;

    public List<ItemPairs> Pairs;
    public List<Sprite> TakeSprites;
    
}

[System.Serializable]
public class ItemPairs
{
    public List<Item> ItemsWeNeed = new List<Item>();
    public List<Item> ItemsWeGive = new List<Item>();
}

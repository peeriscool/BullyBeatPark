using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract base class for items colleactiable
//Unity3D - Scriptable Object Inventory System | Part 1  https://www.youtube.com/watch?v=_IqTeruf3-s
public enum ItemType
{
    toy,
    buff,
    essential,
}
public abstract class ItemObject : ScriptableObject
{
  //  public GameObject prefab;
    public GameObject UI;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
}
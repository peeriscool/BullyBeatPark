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

public enum attribute
{
    extrastep, //1 free extra walking step
    freepunch, //1 free punch
    obstaclediscount, //1 free movement for obstacles
}
public abstract class ItemObject : ScriptableObject
{
    //  public GameObject prefab;
    public int id;
    public Sprite UI;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    public Buff[] mybuffs;
    public Item CreateItem()
    {
        Item newitem = new Item(this);
        return newitem;
    }
}

[System.Serializable]
public class Item
{
    public string name;
    public int id;
    public Buff[] buffs;
    public Item(ItemObject item)
    {
        name = item.name;
        id = item.id;
        buffs = new Buff[item.mybuffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new Buff(item.mybuffs[i].min, item.mybuffs[i].max);
        }
    }
}
[System.Serializable]
public class Buff
{
    public attribute myattribute;
    public int value;
    public int min;
    public int max;
    public Buff (int _min, int _max)
    {
        min = _min;
        max = _max;
        generatevalue();
    }
    public void generatevalue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}
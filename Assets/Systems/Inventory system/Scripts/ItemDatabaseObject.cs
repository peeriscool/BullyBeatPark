using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="ItemDatabase",menuName ="Inventory system/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    public Dictionary<int, ItemObject> Itemdict = new Dictionary<int, ItemObject>();
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].id = i;
            Itemdict.Add(i,Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        Itemdict = new Dictionary<int, ItemObject>();
    }
}

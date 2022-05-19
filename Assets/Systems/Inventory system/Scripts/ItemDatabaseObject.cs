using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="ItemDatabase",menuName ="Inventory system/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].id = i; 
            GetItem.Add(i,Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemObject>();
    }
}

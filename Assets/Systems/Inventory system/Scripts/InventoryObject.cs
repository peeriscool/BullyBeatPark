using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

[CreateAssetMenu(fileName ="new inventory",menuName = "Inventory system/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savepath;
    public ItemDatabaseObject database;
    public Inventory Container;

    public void Additem(Item _item, int _amount)
    {

        for (int i = 0; i < Container.items.Count; i++)
        {
            if (Container.items[i].item.id == _item.id)
            {
                Container.items[i].AddAmount(_amount);
                return;
            }
        }
        Container.items.Add(new InventorySlot(_item.id, _item, _amount));
    }

    #region Saving and clearing
    //public void Save() //serializ using json scriptable object instances
    //{
    //    string savedata =  JsonUtility.ToJson(this,true);
    //    BinaryFormatter bf = new BinaryFormatter();
    //    Debug.Log("Saving on: " + string.Concat(Application.persistentDataPath, savepath));
    //    FileStream file = File.Create(string.Concat(Application.persistentDataPath,savepath));
    //    bf.Serialize(file, savedata);
    //    file.Close();
    //}
    //public void Load()
    //{
    //    if (File.Exists(string.Concat(Application.persistentDataPath, savepath)))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        Debug.Log("Loading from: " + string.Concat(Application.persistentDataPath, savepath));
    //        FileStream file = File.Open(string.Concat(Application.persistentDataPath, savepath), FileMode.Open);
    //        JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
    //        file.Close();
    //    }
    //}
    [ContextMenu("Save inventory")] //save in editor using cog wheel
    public void SaveIformat()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream((string.Concat(Application.persistentDataPath, savepath)), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();

    }
    [ContextMenu("Load inventory")]//save in editor using cog wheel
    public void LoadIformat()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savepath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream((string.Concat(Application.persistentDataPath, savepath)), FileMode.Open, FileAccess.Read);
            Container = (Inventory)formatter.Deserialize(stream);
            stream.Close();
        }
    }
    [ContextMenu("Clear inventory")]
    public void Clear()
    {
        Container = new Inventory();
    }
    #endregion
   
}
[System.Serializable]
public class Inventory
{
    public List<InventorySlot> items = new List<InventorySlot>();
}
[System.Serializable]
public class InventorySlot
{
    public int id;
    public Item item;
    public int amount;
    public InventorySlot(int _id, Item _item, int _amount)
    {
        id = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}

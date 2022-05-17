using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[CreateAssetMenu(fileName ="new inventory",menuName = "Inventory system/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savepath;
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();


    private void OnEnable() //unityeditor load items
    {
        #if UNITY_EDITOR
          database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/ItemDatabase1.asset", typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ItemDatabaseObject>("ItemDatabase1"); //needs to be in the resources folder
#endif
    }
    
    public void Save() //serializ using json scriptable object instances
    {
        string savedata =  JsonUtility.ToJson(this,true);
        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log("Saving on: " + string.Concat(Application.persistentDataPath, savepath));
        FileStream file = File.Create(string.Concat(Application.persistentDataPath,savepath));
        bf.Serialize(file, savedata);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savepath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            Debug.Log("Loading from: "+string.Concat(Application.persistentDataPath, savepath));
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savepath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }
    public void Additem(ItemObject _item, int _amount)
    {

        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }
        Container.Add(new InventorySlot(database.GetId[_item], _item, _amount));
    }
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++) Container[i].item = database.GetItem[Container[i].id];
        
    }

    public void OnBeforeSerialize()
    {
    
    }
}


[System.Serializable]
public class InventorySlot
{
    public int id;
    public ItemObject item;
    public int amount;
    public InventorySlot(int _id, ItemObject _item, int _amount)
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

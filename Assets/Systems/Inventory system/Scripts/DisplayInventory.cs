using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory; //displayed inventory
    //public ItemInteraction activeinventory;
    public int X_start; //horizontal offset
    public int Y_start; //vertical offset
    public int X_Spacer;
    public int Column;
    public int Y_Spacer;
    public GameObject inventoryprefab; //item that holds all sprites

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    private void Start()
    {
        //if(activeinventory != null & inventory == null) //use item interaction inventory when we have no inventory
        //{
        //    inventory = activeinventory.inventory;
        //}

        CreateDisplay();
    }
    private void Update()
    {
        UpdateDisplay();
    }

    public Vector3 GetPosition(int index)//assign inventory location
    {
        return new Vector3(X_start + (X_Spacer * (index % Column)), Y_start + (-Y_Spacer * (index / Column)), 0f); //use start locations
                                                                                                                   //  return new Vector3(X_start + (X_Spacer * (index % Column)), (-Y_Spacer * (index / Column)), 0f); //no pre defined positions
    }
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.items.Count; i++)
        {
            InventorySlot slot = inventory.Container.items[i];
            var obj = Instantiate(inventoryprefab, Vector3.zero, Quaternion.identity, transform);
            Debug.Log("getting items: " + slot.item.id);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.Itemdict[slot.item.id].UI;  //inventory.Container[i].item.UI
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);           
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0"); //n0 = format with commas           
            itemsDisplayed.Add(slot, obj);
        }
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.items.Count; i++) //item already exist update count
        {
            InventorySlot slot = inventory.Container.items[i];
            if (itemsDisplayed.ContainsKey(slot))
            {
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            }
            else //new item 
            {
                //to do fix GetItem[slot.item.id].UI.name  make sure there is a name
                    var obj = Instantiate(inventoryprefab, Vector3.zero, Quaternion.identity, transform);
                    obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.Itemdict[slot.item.id].UI ;
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0"); //n0 format with commas
                    itemsDisplayed.Add(inventory.Container.items[i], obj);
               

            }
        }
    }
}


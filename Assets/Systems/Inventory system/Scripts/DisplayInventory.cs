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
    public int X_start; //horizontal offset
    public int Y_start; //vertical offset
    public int X_Spacer;
    public int Column;
    public int Y_Spacer;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    private void Start()
    {
        CreateDisplay();
    }
    private void Update()
    {
        UpdateDisplay();
    }
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.UI, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i); 
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0"); //n0 = format with commas
            itemsDisplayed.Add(inventory.Container[i], obj);
        }
    }
    public Vector3 GetPosition(int index)//assign inventory location
    {
        return new Vector3(X_start + (X_Spacer * (index % Column)),Y_start + (-Y_Spacer * (index / Column)), 0f); //use start locations
      //  return new Vector3(X_start + (X_Spacer * (index % Column)), (-Y_Spacer * (index / Column)), 0f); //no pre defined positions
    }
    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++) //item already exist update count
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else //new item
            {
                var obj = Instantiate(inventory.Container[i].item.UI, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0"); //n0 format with commas
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }
}


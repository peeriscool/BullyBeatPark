using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///based on: https://www.youtube.com/watch?v=232EqU1k9yQ&list=PLHgPehvRzcEhbbpAGjSxnCxm_tlPcH3kO&index=3&t=243s
/// </summary>
public class ItemInteraction : MonoBehaviour
{
    public InventoryObject inventory; //reffered to as player in tutorial
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<ItemIngame>();
        if(item)
        {
            inventory.Additem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }
    private void Update() //used for saving file 
    {
      if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
          //  inventory.SaveIformat();
        }
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
        //    inventory.LoadIformat();
        }
    }
    public void OnApplicationQuit()
    {
        inventory.Container.items.Clear();
    }

}

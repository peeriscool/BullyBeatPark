using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new default obj", menuName = "Inventory system/Items/toy")]
public class ToyItem : ItemObject
{
    //set to toy by default
    public void Awake()
    {
        type = ItemType.toy;
    }

}

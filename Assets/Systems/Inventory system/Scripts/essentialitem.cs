using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new default obj", menuName = "Inventory system/Items/essentails")]
public class Essentailitem : ItemObject
{
    //set to toy by default
    public void Awake()
    {
        type = ItemType.toy;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new default obj", menuName = "Inventory system/Items/buffs")]
public class Buffitem : ItemObject
{
    public float effect;
    //ToDO add blackboard refrence

    //set to toy by default
    public void Awake()
    {
        type = ItemType.buff;
    }

}

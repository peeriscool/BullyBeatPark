using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public int stepvalue;
    Vector3 startpoint;
    Vector3 pos;
   public RoomDungeonGenerator myenvoirment;
    // Start is called before the first frame update
    void Start()
    {
        pos = this.gameObject.transform.position;
        startpoint = myenvoirment.RoomList[0].mypositions[0];
        startpoint.y += 5; //add some height
        this.gameObject.transform.position = startpoint;
    }

    // Update is called once per frame
    void Update()
    {
        pos = this.gameObject.transform.position;
        Orientation();

        if (this.gameObject.transform.position.y <= -50) //set player to zero when bellow -50
        {
            this.gameObject.transform.position = startpoint;

        }
    }


    void Orientation()//sets this gameobject's rotation
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            //   animationsystem.ChangeAnimationState("walkingInPlace");
            this.gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward); ////new Quaternion(0, 0, 0, 0);
            pos += new Vector3(stepvalue, 0, 0);
            this.gameObject.transform.position = pos;
            return;
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            //  animationsystem.ChangeAnimationState("walkingInPlace");
            this.gameObject.transform.rotation = Quaternion.AngleAxis(180, Vector3.down);
            pos += new Vector3(-stepvalue, 0, 0);
            this.gameObject.transform.position = pos;
            return;
        }
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            // animationsystem.ChangeAnimationState("walkingInPlace");
            this.gameObject.transform.rotation = new Quaternion(0, 90, 0, -90);
            pos += new Vector3(0, 0,stepvalue);
            this.gameObject.transform.position = pos;
            return;
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            // animationsystem.ChangeAnimationState("walkingInPlace");
            this.gameObject.transform.rotation = new Quaternion(0, 90, 0, 90);
            pos += new Vector3(0, 0, -stepvalue);
            this.gameObject.transform.position = pos;
            return;
        }
        if (Keyboard.current.wKey.isPressed && Keyboard.current.aKey.isPressed)
        {
            this.gameObject.transform.rotation = new Quaternion(0, -45, 0, 90);

            // animationsystem.ChangeAnimationState("diagonal forward");
            return;
        }
        //multipresses
        if (Keyboard.current.wKey.isPressed && Keyboard.current.dKey.isPressed)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 45, 0, 90);
            //    animationsystem.ChangeAnimationState("diagonal forward");
            return;
        }
        if (Keyboard.current.sKey.isPressed && Keyboard.current.dKey.isPressed)
        {
            //  animationsystem.ChangeAnimationState("diagonal forward");
            this.gameObject.transform.rotation = new Quaternion(0, -135, 0, -45);
            return;
        }
        if (Keyboard.current.sKey.isPressed && Keyboard.current.aKey.isPressed)
        {
            //  animationsystem.ChangeAnimationState("diagonal forward");
            this.gameObject.transform.rotation = new Quaternion(0, -135, 0, 45);
            return;
        }

    }
}

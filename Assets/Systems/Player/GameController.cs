using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public float allowedjumpheight;
    public int stepvalue;
    Vector3 startpoint;
    Vector3 pos;
    public RoomDungeonGenerator myenvoirment;
    public MazeGeneration mymaze;
    Agent activEenemy;
    bool Inrange = false;
   public bool grounded = true;
    // Start is called before the first frame update
    void Start()
    {
        Blackboard.player = this.gameObject;
        pos = this.gameObject.transform.position;
        if(mymaze != null)
        {
            startpoint = new Vector3(0, 1, 0);
         //   startpoint.y += 5; //add some height
        }
        else if(myenvoirment != null)
        {
            startpoint = myenvoirment.RoomList[0].mypositions[0];
            startpoint.y += 5; //add some height
        }
        this.gameObject.transform.position = startpoint;
    }

    // Update is called once per frame
    void Update()
    {
       
        pos = this.gameObject.transform.position;
        Orientation();
        actionhandler();

        if (this.gameObject.transform.position.y <= -50) //set player to zero when bellow -50
        {
            this.gameObject.transform.position = startpoint;

        }
        if (this.gameObject.transform.position.y <= allowedjumpheight)//set player to zero when bellow -50
        {
            grounded = true;
        }
    }

    public void actionhandler() //handles punch
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && Inrange)
        {
            Blackboard.playerpunch = true;
            // if(activEenemy.actionindex == 3)//ded
            if (activEenemy.Hp != 0)
            {
                activEenemy.TakeDamage();
                //  manager.WalkEnemyCommand(activEenemy);
                // activEenemy.WalkTo(new Vector3(Random.Range(0, Blackboard.Mazewidth), 0, Random.Range(0, Blackboard.Mazeheight)), activEenemy.location,mazegenerator.grid); //go somewhere els after getting hit
            }
            Inrange = false;
            //deal damage to enemy and consume an action point if in range
        }
        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            Blackboard.playerpunch = false;
        }

        if(Keyboard.current.spaceKey.isPressed && grounded == true)
        {
            grounded = false;
            this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);  
        }
    }
    void Orientation()//sets this gameobject's rotation
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            //   animationsystem.ChangeAnimationState("walkingInPlace");
            this.gameObject.transform.rotation = Quaternion.identity;
            pos += new Vector3(stepvalue, 0, 0);
         //   this.gameObject.transform.position = pos;
            return;
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            //  animationsystem.ChangeAnimationState("walkingInPlace");
            this.gameObject.transform.rotation = Quaternion.AngleAxis(180, Vector3.down);
            pos += new Vector3(-stepvalue, 0, 0);
         //   this.gameObject.transform.position = pos;
            return;
        }
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            // animationsystem.ChangeAnimationState("walkingInPlace");
            this.gameObject.transform.rotation = new Quaternion(0, 90, 0, -90);
            pos += new Vector3(0, 0,stepvalue);
          //  this.gameObject.transform.position = pos;
            return;
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            // animationsystem.ChangeAnimationState("walkingInPlace");
            this.gameObject.transform.rotation = new Quaternion(0, 90, 0, 90);
           // pos += new Vector3(0, 0, -stepvalue);

           // Vector3.MoveTowards(this.gameObject.transform.position, Vector3.negativeInfinity, Time.deltaTime / 4);
           
            return;
        }
       
        //if (Keyboard.current.wKey.wasPressedThisFrame && Keyboard.current.aKey.wasPressedThisFrame)
        //{
           

        //    // animationsystem.ChangeAnimationState("diagonal forward");
        //    return;
        //}


        if (Keyboard.current.dKey.isPressed)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, pos += new Vector3(0, 0, -stepvalue ), Time.deltaTime);
        }
        else if (Keyboard.current.wKey.isPressed)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, pos += new Vector3(stepvalue, 0, 0), Time.deltaTime);
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, pos += new Vector3(-stepvalue, 0, 0), Time.deltaTime);
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, pos += new Vector3(0, 0, stepvalue), Time.deltaTime);
        }
        //multipresses

        if (Keyboard.current.wKey.isPressed && Keyboard.current.aKey.isPressed)
        {
            this.gameObject.transform.rotation = new Quaternion(0, -45, 0, 90);
            
        }
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
    void OnCollisionEnter(Collision dataFromCollision)
    {
        //if (dataFromCollision.gameObject.tag == "Enemy")
        //{
        //    //if enemy is in range allow punch
        //    Debug.Log("kid in range");
        //    activEenemy = dataFromCollision.gameObject.GetComponent<Agent>();
        //    Inrange = true;
       //}
        if (dataFromCollision.gameObject.layer == 6 && Blackboard.getlevelstatus(SceneManagerScript.returnactivesceneint()))// 6 = "walls and floors"
        {
            this.GetComponent<worldToGrid>().floorcheck(dataFromCollision);
        }
    }
    private void OnCollisionStay(Collision dataFromCollision)
    {
        if (dataFromCollision.gameObject.tag == "Enemy")
        {
            //if enemy is in range allow punch
            Debug.Log("kid in range");
            activEenemy = dataFromCollision.gameObject.GetComponent<Agent>();
            Inrange = true;
        }
    }
}

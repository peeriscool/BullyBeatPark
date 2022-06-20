using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameController : MonoBehaviour
{
    //set the hight of the jump
    public float maxheight; 
    //size of the steps you take
    public int stepvalue;
    Vector3 startpoint;
    Vector3 pos;
    //both type of generation to different start points
    public RoomDungeonGenerator myenvoirment;
    public MazeGeneration mymaze;
    // interact with childeren
    Agent activEenemy;
    bool Inrange = false;
    public bool grounded = true;
    // player animation
    public Animator myanim;
     AnimationStates mystates;
    // Start is called before the first frame update
    void Start()
    {
        mystates = new AnimationStates(myanim);
        Blackboard.player = this.gameObject;
        pos = this.gameObject.transform.position;
        if(mymaze != null) //lvl 1
        {
            startpoint = new Vector3(0, 1, 0);
        }
        else if(myenvoirment != null) //lvl 2
        {
            startpoint = myenvoirment.RoomList[0].mypositions[0];
            startpoint.y += 5; //add some height
        }
        else //menu
        {
            startpoint = this.gameObject.transform.position;
        }
        this.gameObject.transform.position = startpoint;
    }

    // Update is called once per frame
    void Update()
    {
        pos = this.gameObject.transform.position;
        Orientation();
        actionhandler();

        mystates.Tick(); //update animations

        if (this.gameObject.transform.position.y <= -50) //set player to zero when bellow -50
        {
            this.gameObject.transform.position = startpoint;
        }
        if (this.gameObject.transform.position.y <= maxheight)//set player to zero when bellow -50
        {
            grounded = true;
        }
    }

    public void actionhandler() //handles punch
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && Inrange)
        {
            Blackboard.playerpunch = true;
            //deal damage to enemy and consume an action point if in range
            if (activEenemy.Hp != 0)
            {
                activEenemy.TakeDamage();
                //  manager.WalkEnemyCommand(activEenemy);
                // activEenemy.WalkTo(new Vector3(Random.Range(0, Blackboard.Mazewidth), 0, Random.Range(0, Blackboard.Mazeheight)), activEenemy.location,mazegenerator.grid); //go somewhere els after getting hit
            }
            Inrange = false;
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
            this.gameObject.transform.rotation = Quaternion.identity;
            pos += new Vector3(stepvalue, 0, 0);
            return;
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            this.gameObject.transform.rotation = Quaternion.AngleAxis(180, Vector3.down);
            pos += new Vector3(-stepvalue, 0, 0);
            return;
        }
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 90, 0, -90);
            pos += new Vector3(0, 0,stepvalue);
            return;
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 90, 0, 90);
            return;
        }
     
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
            return;
        }
        if (Keyboard.current.sKey.isPressed && Keyboard.current.dKey.isPressed)
        {
            this.gameObject.transform.rotation = new Quaternion(0, -135, 0, -45);
            return;
        }
        if (Keyboard.current.sKey.isPressed && Keyboard.current.aKey.isPressed)
        {
            this.gameObject.transform.rotation = new Quaternion(0, -135, 0, 45);
            return;
        }

    }
    void OnCollisionEnter(Collision dataFromCollision)
    {
        if (dataFromCollision.gameObject.layer == 6 && Blackboard.getlevelstatus(SceneManagerScript.returnactivesceneint()))// 6 = "walls and floors"
        {
            this.GetComponent<worldToGrid>().floorcheck(dataFromCollision); //world to grid needs to be replaced with correct position
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

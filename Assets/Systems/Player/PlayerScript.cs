using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(worldToGrid))]
public class PlayerScript : MonoBehaviour
{
    //public GameManager manager;
    public BoxCollider playerange;
    bool Inrange = false;
    Agent activEenemy;
    public Animator playerrig;
    public static bool isgrounded;
    AnimationStates animationsystem;
    public float Speed;
    public float JumpForce;
    private float MoveX;
    private float MoveZ;
    private float SavedSpeed;
    Rigidbody rb;
    new Transform transform;
    controllerInputs ControllerIndex = controllerInputs.walking;
    
    enum controllerInputs //TODO: presets for animtion files
    {
        walking = 0, Running = 1, Crouch = 2
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        Blackboard.player = this.gameObject;
        animationsystem = new AnimationStates(playerrig);
        rb = GetComponent<Rigidbody>();
        SavedSpeed = Speed;
    }

    void Update()
    {
        InputHandler();
        punchhandler();
//        MouseHandler();
        
        animationsystem.Tick();

        if(this.gameObject.transform.position.y <= -50) //set player to zero when bellow -50
        {
            this.gameObject.transform.position = Vector3.zero;
            
        }
    }
   
    public void InputHandler() //mixing diffrent control sets allowing for crouch,running,walking
    {
        if (ControllerIndex == controllerInputs.walking) //link controller to movement 
        {
            Speed = SavedSpeed;
            Orientation(ControllerIndex);
            Movement(ControllerIndex);
        }
        if (ControllerIndex == controllerInputs.Running)
        {
            Speed = SavedSpeed * 2;
            Movement(ControllerIndex);
            Orientation(ControllerIndex);
            //if (Input.GetKey(KeyCode.LeftShift)) //hold shift for going faster //ToDo run animations, enum control... //UIScript.changevalue(null,Cooldown);
            //{
            //    //  Speed = Mathf.Min(5, Mathf.Lerp(0.03f, Speed, Time.deltaTime) + Speed);
            //}
        }
        if (ControllerIndex == controllerInputs.Crouch)
        {
            Speed = SavedSpeed / 2;
            Movement(ControllerIndex);
            Orientation(ControllerIndex);
        }
        
    }
    public void punchhandler() //handles punch
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
    }
    public void MouseHandler()
    {
        //if (Mouse.current.leftButton.wasPressedThisFrame)
        //{
        //    if (playerrig != null)
        //    {
        //        playerrig.Play("Stunned", 0, 0.25f);
        //    }
        //}


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
    void OnCollisionEnter(Collision dataFromCollision)
    {
        
        //if (dataFromCollision.gameObject.tag == "Enemy")
        //{
        //    //if enemy is in range allow punch
        //    Debug.Log("kid in range");
        //    activEenemy = dataFromCollision.gameObject.GetComponent<Agent>();
        //    Inrange = true;

        //}
        //if (dataFromCollision.gameObject.layer == 6 && Blackboard.getlevelstatus(SceneManagerScript.returnactivesceneint()))// 6 = "walls and floors"
        //{
        //    this.GetComponent<worldToGrid>().floorcheck(dataFromCollision);
        //}
    }
    void Orientation(controllerInputs input)//sets this gameobject's rotation
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            animationsystem.ChangeAnimationState("walkingInPlace");
            this.gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward); ////new Quaternion(0, 0, 0, 0);
            return;
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            animationsystem.ChangeAnimationState("walkingInPlace");
            this.gameObject.transform.rotation = Quaternion.AngleAxis(180, Vector3.down);
            return;
        }
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            animationsystem.ChangeAnimationState("walkingInPlace");
            this.gameObject.transform.rotation = new Quaternion(0, 90, 0, -90);
            return;
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            animationsystem.ChangeAnimationState("walkingInPlace");
            this.gameObject.transform.rotation = new Quaternion(0, 90, 0, 90);
            return;
        }
        if (Keyboard.current.wKey.isPressed && Keyboard.current.aKey.isPressed)
        {
            this.gameObject.transform.rotation = new Quaternion(0, -45, 0, 90);
            animationsystem.ChangeAnimationState("diagonal forward");
            return;
        }
        //multipresses
        if (Keyboard.current.wKey.isPressed && Keyboard.current.dKey.isPressed)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 45, 0, 90);
            animationsystem.ChangeAnimationState("diagonal forward");
            return;
        }
        if (Keyboard.current.sKey.isPressed && Keyboard.current.dKey.isPressed)
        {
            animationsystem.ChangeAnimationState("diagonal forward");
            this.gameObject.transform.rotation = new Quaternion(0, -135, 0, -45);
            return;
        }
        if (Keyboard.current.sKey.isPressed && Keyboard.current.aKey.isPressed)
        {
            animationsystem.ChangeAnimationState("diagonal forward");
            this.gameObject.transform.rotation = new Quaternion(0, -135, 0, 45);
            return;
        }

    }
    void Movement(controllerInputs input)//sets this gameobject's position
    {
        MoveX = this.gameObject.transform.position.x;
        MoveZ = this.gameObject.transform.position.z;
        transform = this.gameObject.transform;
        if (Keyboard.current.wKey.isPressed)
        {        
            MoveZ += Speed;
            this.gameObject.transform.position = new Vector3(MoveX, transform.position.y, Mathf.Lerp(MoveZ, MoveZ, Time.deltaTime));
        }
        if (Keyboard.current.sKey.isPressed)
        {
            MoveZ -= Speed;
            this.gameObject.transform.position = new Vector3(MoveX, transform.position.y, Mathf.Lerp(MoveZ, MoveZ, Time.deltaTime));
        }
        if (Keyboard.current.aKey.isPressed)
        {
            MoveX -= Speed;
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(MoveX, MoveX, Time.deltaTime), transform.position.y, MoveZ);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            MoveX += Speed;
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(MoveX, MoveX, Time.deltaTime), transform.position.y, MoveZ);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isgrounded == true)
        {
            isgrounded = false;
            rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
            //  this.gameObject.transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, JumpForce, Time.deltaTime), transform.position.z);
        }
        if (!Keyboard.current.spaceKey.isPressed)
        {
            rb.AddForce(new Vector3(0, -JumpForce * 4, 0), ForceMode.Acceleration);
            if (this.transform.position.y <= 0.35)
            {
                isgrounded = true;
            }
        }
       
    }

}

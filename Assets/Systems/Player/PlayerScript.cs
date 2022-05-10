using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    public Animator playerrig;
    public float Speed;
    public float JumpForce;

    private float MoveX;
    private float MoveZ;
    private float SavedSpeed;
    Rigidbody rb;
    new Vector3 transform;

    controllerInputs ControllerIndex = controllerInputs.walking;
    enum controllerInputs
    {
        walking = 0, Running = 1, Crouch = 2
    }

    void Start()
    {
        Blackboard.player = this.gameObject;
        rb = GetComponent<Rigidbody>();
        SavedSpeed = Speed;
    }

    void FixedUpdate()
    {
        InputHandler();
    }

    public void InputHandler() //mixing diffrent control sets allowing for crouch,running,walking
    {
        MouseHandler();
        #region ControllerIndex switch for multiple enum based controls (Z button)
        //if (Keyboard.current.zKey.wasPressedThisFrame)
        //{
        //    if (ControllerIndex == controllerInputs.Crouch) //loop enum with last and first
        //    {
        //        ControllerIndex = controllerInputs.walking;
        //    }
        //    else
        //    {
        //        ControllerIndex += 1;
        //    }
        //}

        if (ControllerIndex == controllerInputs.walking) //link controller to different ways of movement 
        {
            Orientation(ControllerIndex);
            Movement(ControllerIndex);
          //  TpPlayerMotion(ControllerIndex);

            if (Keyboard.current.shiftKey.wasPressedThisFrame) //hold shift for going faster //ToDo run animations, enum control... //UIScript.changevalue(null,Cooldown);
            {
                if (Speed < SavedSpeed * 4)
                {
                    Speed = Speed * 1.1f;
                    Speed = Mathf.Lerp(Speed, 0f, Time.deltaTime);
                }
            }
            if (Keyboard.current.wKey.wasReleasedThisFrame) //release key to reset
            {
                Speed = SavedSpeed;
            }
            //add animations
        }
        if (ControllerIndex == controllerInputs.Running)
        {
               Movement(ControllerIndex);
           // TpPlayerMotion(ControllerIndex);
            Orientation(ControllerIndex);
            if (Input.GetKey(KeyCode.LeftShift)) //hold shift for going faster //ToDo run animations, enum control... //UIScript.changevalue(null,Cooldown);
            {
                Speed = Mathf.Min(5, Mathf.Lerp(0.03f, Speed, Time.deltaTime) + Speed);
            }
        }
        if (ControllerIndex == controllerInputs.Crouch)
        {
            Movement(ControllerIndex);
            Orientation(ControllerIndex);
            //toDo after animation controller
        }
        #endregion
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
    void Orientation(controllerInputs input)//tp controlls
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 1, 0, 0);

        }
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 90, 0, -90);

        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 90, 0, 90);

        }
        if (Keyboard.current.wKey.wasPressedThisFrame && Keyboard.current.aKey.wasPressedThisFrame)
        {
            this.gameObject.transform.rotation = new Quaternion(0, -45, 0, 90);


        }
        if (Keyboard.current.wKey.wasPressedThisFrame && Keyboard.current.dKey.wasPressedThisFrame)
        {
            this.gameObject.transform.rotation = new Quaternion(0, 45, 0, 90);

        }
        if (Keyboard.current.sKey.wasPressedThisFrame && Keyboard.current.dKey.wasPressedThisFrame)
        {
            this.gameObject.transform.rotation = new Quaternion(0, -135, 0, -45);

        }
        if (Keyboard.current.sKey.wasPressedThisFrame && Keyboard.current.aKey.wasPressedThisFrame)
        {
            this.gameObject.transform.rotation = new Quaternion(0, -135, 0, 45);

        }

    }
    void Movement(controllerInputs input)//tp controlls
    {
        MoveX = this.gameObject.transform.position.x;
        MoveZ = this.gameObject.transform.position.z;
        if (Keyboard.current.wKey.isPressed)
        {
            transform = this.gameObject.transform.position;
            MoveZ += Speed;
            this.gameObject.transform.position = new Vector3(transform.x, transform.y, Mathf.Lerp(this.gameObject.transform.position.z, MoveZ, Time.deltaTime));

        }
        if (Keyboard.current.sKey.isPressed)
        {
            transform = this.gameObject.transform.position;
            MoveZ -= Speed;
            this.gameObject.transform.position = new Vector3(transform.x, transform.y, Mathf.Lerp(this.gameObject.transform.position.z, MoveZ, Time.deltaTime));

        }
        if (Keyboard.current.aKey.isPressed)
        {
            MoveX -= Speed;
            transform = this.gameObject.transform.position;
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(this.gameObject.transform.position.x, MoveX, Time.deltaTime), transform.y, transform.z);

        }
        if (Keyboard.current.dKey.isPressed)
        {
            MoveX += Speed;
            transform = this.gameObject.transform.position;
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(this.gameObject.transform.position.x, MoveX, Time.deltaTime), transform.y, transform.z);


        }
        if (Keyboard.current.spaceKey.isPressed)
        {
            this.gameObject.transform.position = new Vector3(transform.x, Mathf.Lerp(this.gameObject.transform.position.y, Speed, Time.deltaTime), transform.z);
        }
    }
}

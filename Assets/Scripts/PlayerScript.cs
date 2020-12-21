
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    float moveX;
    float moveZ;
    new Vector3 transform;
    public float speed;
    public float JumpForce;
    bool switchcontrols = true;
    Rigidbody rb;
    int enumindex = 0;

    enum controls
    {
        tplerp=0,tp=1,ball=2
    }

    controls a = controls.ball;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switchcontrols = !switchcontrols;
        }
        if (switchcontrols)
        {
            TpPlayerlerp();

        }
        if (!switchcontrols)
        {
            MovePlayer();

        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *=2;
        }

    }


    void MovePlayer() //exponentially increases with time
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("W");
            rb.AddForce(Vector3.forward * (Time.deltaTime *1000f));
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * (Time.deltaTime * 1000f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * (Time.deltaTime * 1000f));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * (Time.deltaTime * 1000f));

        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * (Time.deltaTime * 1000f));
        }
        rb.AddForce(Vector3.down * 10f);
    }
    void TpPlayerMotion()//tp controlls
    {
        moveX = this.gameObject.transform.position.x;
        moveZ = this.gameObject.transform.position.z;
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform = this.gameObject.transform.position;
            moveZ += speed;
            transform.z = moveZ;
            this.gameObject.transform.position = transform;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform = this.gameObject.transform.position;
            moveZ -= speed;
            transform.z = moveZ;
            this.gameObject.transform.position = transform;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform = this.gameObject.transform.position;
            moveX -= speed;
            transform.x = moveX;
            this.gameObject.transform.position = transform;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform = this.gameObject.transform.position;
            moveX += speed;
            transform.x = moveX;
            this.gameObject.transform.position = transform;

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform = this.gameObject.transform.position;
            JumpForce += speed;
            transform.y = moveX;
            this.gameObject.transform.position = transform;
        }
    }
    //to do lerp
    void TpPlayerlerp()//tp controlls
    {
        moveX = this.gameObject.transform.position.x;
        moveZ = this.gameObject.transform.position.z;
        if (Input.GetKey(KeyCode.W))
        {
            transform = this.gameObject.transform.position;
            moveZ += speed;
            transform.z = moveZ;
            this.gameObject.transform.position = new Vector3 (transform.x,transform.y, Mathf.Lerp(this.gameObject.transform.position.z, moveZ,Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform = this.gameObject.transform.position;
            moveZ -= speed;
            transform.z = moveZ;
            this.gameObject.transform.position = new Vector3(transform.x, transform.y, Mathf.Lerp(this.gameObject.transform.position.z, moveZ, Time.deltaTime)); ;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform = this.gameObject.transform.position;
            moveX -= speed;
            transform.x = moveX;
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(this.gameObject.transform.position.x, moveX, Time.deltaTime), transform.y,transform.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform = this.gameObject.transform.position;
            moveX += speed;
            transform.x = moveX;
            this.gameObject.transform.position = new Vector3(Mathf.Lerp(this.gameObject.transform.position.x, moveX, Time.deltaTime), transform.y, transform.z);

        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform = this.gameObject.transform.position;
            JumpForce += speed;
            transform.y = moveX;
            this.gameObject.transform.position = new Vector3(transform.x,Mathf.Lerp(this.gameObject.transform.position.y, JumpForce, Time.deltaTime), transform.z);
        }
    }
}

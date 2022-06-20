using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(LineRenderer))]
public class LineRendererMovement : MonoBehaviour
{
    //Set speed
    public float speed;
    //index for positions
    public int posNum;
    //Increasing value for lerp
    float moveSpeed = new float();
    //Linerenderer's position index
    int indexNum;
    //size to give to the linerender
    int size = 100;
   
    //line rendere positions
    Vector3[] positions;
    //saves the input of the player
    List<Vector3> input;
    //saves the possible directions
    Vector3[] directions; 

    LineRenderer myrenderer;

    // Start is called before the first frame update
    void Start()
    {
        input = new List<Vector3>();
        myrenderer = this.GetComponent<LineRenderer>();
        posNum = 0;
        directions = new Vector3[4]; //4 directions WASD
        directions[0] = Vector3.forward;
        directions[1] = Vector3.back;
        directions[2] = Vector3.left;
        directions[3] = Vector3.right;

        positions = new Vector3[size];
        indexNum = myrenderer.GetPositions(positions);
    }

    void FixedUpdate()
    {
        if(Keyboard.current.wKey.wasPressedThisFrame)
        {
            input.Add(this.transform.position + directions[0]); //register input
            myrenderer.SetPosition(indexNum, input[indexNum]);

            // positions[posNum] = this.transform.position + directions[0];
            // positions[posNum] = this.transform.position + directions[0];//new Vector3(this.transform.position.x + offset,0,this.transform.position.z);//new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));

            // myrenderer.SetPositions(positions);
            // posNum++;


            // posNum = 0;
            //  positions = new Vector3[size];


        }
        movement();
    }
    void movement()
    {
        //round lerp value down to int
        indexNum = Mathf.FloorToInt(moveSpeed);
        //increase lerp value relative to the distance between points to keep the speed consistent.
        moveSpeed += speed / Vector3.Distance(positions[indexNum], positions[indexNum + 1]);
        //and lerp
        this.transform.position = Vector3.Lerp(positions[indexNum], positions[indexNum + 1], moveSpeed - indexNum);
    }
  

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}

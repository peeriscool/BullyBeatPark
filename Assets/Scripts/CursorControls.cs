using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//bronnen
//https://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html
//https://stackoverflow.com/questions/67280366/unity-raycast-not-detecting-parent-objects-tag
//https://docs.unity3d.com/Manual/CameraRays.html
public class CursorControls : MonoBehaviour
{
    private Camera cam;//top down camera
    public GameObject cursor;
    bool clicktrigger = false;
    bool camtrigger = false;
    // origin starts from the camera
    Vector3 origin;
    // direction of the camera
    Vector3 direction;
    // The distance for the raycast
    private readonly float distance = 40f;
    // Used to store info about the object that the raycast hits
    RaycastHit hit;

    static Vector3 savedlocation;
    void Start()
    {
        cam = Camera.main;

    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) //move cursor
        {
            clicktrigger = true;
        }
        if(Mouse.current.middleButton.wasReleasedThisFrame) //move cam
        {
            camtrigger = true;
        }
        if (Mouse.current.rightButton.wasPressedThisFrame) //reset hight
        {
            cam.transform.position = new Vector3(cam.transform.position.x, 30f, cam.transform.position.z);
        }
    }
    void OnGUI()
    {
        if(clicktrigger) //assign cursor location with left clicking
        {
            origin = cam.transform.position;
            direction = cam.transform.forward;
            Event currentEvent = Event.current;
            Vector2 mousePos = Vector2.zero;
            mousePos.x = currentEvent.mousePosition.x;
            mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;
            Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, CameraFloorDetection(mousePos)));
            savedlocation = point;
            cursor.transform.position = point;
        }
        if (camtrigger) //assign camera location with middle clicking
        {
            Event currentEvent = Event.current;
            Vector2 mousePos = Vector2.zero;
            mousePos.x = currentEvent.mousePosition.x;
            mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;
            Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.z / 2));
            camtrigger = false;
            cam.transform.position = point;
        }
    }

    float CameraFloorDetection(Vector3 _mouspos) //uses the users perspective for depth
    {
        Ray ray = cam.ScreenPointToRay(_mouspos);

        if (Physics.Raycast(ray, out hit)) //cursor to floor detection
        {
            if (hit.transform.tag == "Floor")
            {
                clicktrigger = false;
                if (Physics.Raycast(origin, direction, out hit, distance)) //camera to floor distance
                {
                    return hit.distance - 1;
                }
            }
        }
        return -10f; //out of screen location
    }
    public static Vector3 GetCursorPosition()
    {
        return savedlocation;
    }
}
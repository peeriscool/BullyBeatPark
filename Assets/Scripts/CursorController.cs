using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//https://www.codegrepper.com/code-examples/csharp/use+raycast+unity+new+input+system
public class CursorController : MonoBehaviour
{
    [SerializeField]
    private Camera gameCamera;
    private InputAction click;

    void Awake()
    {
        click = new InputAction(binding: "<Mouse>/leftButton");
        click.performed += ctx => {
            RaycastHit hit;
            Vector3 coor = Mouse.current.position.ReadValue();
            if (Physics.Raycast(gameCamera.ScreenPointToRay(coor), out hit))
            {
                hit.collider.GetComponent<FSMExample.IClickable>()?.OnClick();
                Debug.Log("ICLickable?");
            }
        };
        click.Enable();
    }
}
//You can add an IClickable Interface to all GameObjects that want to respond to clicks:

//public interface IClickable
//{
//    void OnClick();
//}

public class ClickableObject : MonoBehaviour, FSMExample.IClickable
{
    public void OnClick()
    {
        Debug.Log("somebody clicked me");
    }
}

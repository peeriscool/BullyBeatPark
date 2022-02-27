using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class boxCollider_OnTriggerEnter_event : MonoBehaviour
{
    GameManager instance;
    BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
       box = this.gameObject.GetComponent<BoxCollider>();
        instance = GameManager.Instance;
        init();
    }

    private void init()
    {
        box.isTrigger = true;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Blackboard.SelectEnemy(other.gameObject);
            Blackboard.EnemylocationPing();
            Time.timeScale = 0.8f;
            Debug.Log("Epic fight moment");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Time.timeScale = 1f;

            Debug.Log("done with fight moment");
        }
    }
    //void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Enemy")
    //    {
    //        Time.timeScale = Time.timeScale / 2;
    //        Debug.Log("Epic fight moment");
    //    }
    //    //foreach (ContactPoint contact in collision.contacts)
    //    //{
    //    //    Debug.DrawRay(contact.point, contact.normal, Color.white);
    //    //}
    //    //if (collision.relativeVelocity.magnitude > 2)
    //    //    Debug.Log("velocity based u say?");
    //}
}

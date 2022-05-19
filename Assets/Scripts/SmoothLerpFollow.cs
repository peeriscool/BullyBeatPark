using UnityEngine;

public class SmoothLerpFollow : MonoBehaviour
{
    // Start is called before the first frame update\

    Transform follow;
    public bool followofsset;
    public Vector3 offset;
    Transform FollowObject;
    void Start()
    {
        FollowObject = this.gameObject.transform;
       
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            follow = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

        FollowObject = this.gameObject.transform;
        if (followofsset)
        {
            this.transform.position = Vector3.MoveTowards(FollowObject.position,
                     new Vector3
                     (
                     Mathf.Lerp(follow.position.x + offset.x, FollowObject.position.x, Time.deltaTime),
                     Mathf.Lerp(follow.position.y+ offset.y, FollowObject.position.y, Time.deltaTime),
                     Mathf.Lerp(follow.position.z + offset.z, FollowObject.position.z, Time.deltaTime)), 0.9f);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(FollowObject.position,
            new Vector3
            (
            Mathf.Lerp(follow.position.x, FollowObject.position.x, Time.deltaTime),
            Mathf.Lerp(follow.position.y, FollowObject.position.y, Time.deltaTime),
            Mathf.Lerp(follow.position.z, FollowObject.position.z, Time.deltaTime)), 0.9f);
        }
    }
}

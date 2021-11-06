using UnityEngine;

public class SmoothLerpFollow : MonoBehaviour
{
    // Start is called before the first frame update\

    public Transform follow;
    Transform FollowObject;
    Transform local;
    void Start()
    {
        FollowObject = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

        FollowObject = this.gameObject.transform;
        this.transform.position = Vector3.MoveTowards(FollowObject.position,
        new Vector3
        (
        Mathf.Lerp(follow.position.x, FollowObject.position.x, Time.deltaTime),
        Mathf.Lerp(follow.position.y, FollowObject.position.y, Time.deltaTime),
        Mathf.Lerp(follow.position.z, FollowObject.position.z, Time.deltaTime)), 0.9f);

    }
}

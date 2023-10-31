using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCfollowMovement : MonoBehaviour {

    public GameObject ThePlayer;
    public float TargetDistance;
    public float AllowedDistance = 5;
    //public GameObject TheNPC;
    public float FollowSpeed;
    public RaycastHit Shot;

  
    void Update() {
        transform.LookAt(ThePlayer.transform);
        TargetDistance = (transform.position - ThePlayer.transform.position).magnitude;
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
        //{
            //TargetDistance = Shot.distance;
            if (TargetDistance >= AllowedDistance)
            {
            Debug.Log("Pursuing player");
                FollowSpeed = 3f;
                //GetComponent<Animation>().Play("runnig");
                transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, FollowSpeed*Time.deltaTime);
            }
            else
            {
                FollowSpeed = 0;
                //GetComponent<Animation>().Play("idle");
            }
        //}
    }
}

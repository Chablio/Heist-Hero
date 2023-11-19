using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCfollowMovement : MonoBehaviour {

    [SerializeField] private Transform[] points;
    [SerializeField] private float targetRadius = 0.1f;
    public GameObject ThePlayer;
    public float TargetDistance;
    public float AllowedDistance = 5;
    //public GameObject TheNPC;
    public float FollowSpeed;
    public RaycastHit Shot;
    private State state = State.PatrolState;

    private int indexOfTarget;
    private Vector3 targetPoint;
    void Start()
    {
        indexOfTarget = -1;
        NextTarget();
        LookAtTarget();
    }

    void Chase() {
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
    
    void Update() {
        if (state == State.PatrolState)
        {
            Patrol();
        } else if(state == State.ChaseState)
        {
            Chase();
        }
    }
    void NextTarget()
    {
        indexOfTarget = (indexOfTarget + 1) % points.Length;
        targetPoint = points[indexOfTarget].position;
        targetPoint.y = transform.position.y;
    }

    
    void LookAtTarget()
    {
        Vector3 lookAt = targetPoint;
        lookAt.y = transform.position.y;

        Vector3 lookDir = (lookAt - transform.position).normalized;
        transform.LookAt(lookDir);
    }
    void Patrol()
    {
        if ((transform.position - targetPoint).magnitude < targetRadius)
        {
            NextTarget();
            LookAtTarget();
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, FollowSpeed * Time.deltaTime);
    }

    enum State
    {
        PatrolState,
        ChaseState
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowFlying : MonoBehaviour
{
    public float camSpeed, yPos;

    public Transform player;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void FixedUpdate()
    {
        Vector2 targetPos = player.position;
        //Vector2 smoothPos = Vector2.Lerp(transform.position, targetPos, camSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x + camSpeed, yPos, this.transform.position.z);
    }
}

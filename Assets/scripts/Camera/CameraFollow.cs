using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float camSpeed, yPos;

    public Transform player;
    SelectPlayer selectPlayer;
    [SerializeField] GameObject kelliTarget;
    [SerializeField] GameObject shonTarget;
    Vector3 correctPos;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        selectPlayer = FindObjectOfType<SelectPlayer>();
    }

   
    void FixedUpdate()
    {
        //не хватает события
        if (selectPlayer.player.currentCharacter == Characters.KelliCharacter)
        {
            correctPos = new Vector3(0.15f*kelliTarget.transform.localScale.x, 0.25f, 0);
            player = kelliTarget.transform;
        }
        else
        {
            correctPos = new Vector3(-0.15f*shonTarget.transform.localScale.x, -0.25f, 0);
            player = shonTarget.transform;
        }
        Vector2 targetPos = player.position + correctPos;
        Vector2 smoothPos = Vector2.Lerp(transform.position, targetPos, camSpeed * Time.deltaTime);

        transform.position = new Vector3(smoothPos.x, smoothPos.y + yPos, this.transform.position.z);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] LayerMask levelMask;
    public bool isGrounded;
    //public PlayerMovement pm;

    private void Start()
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = collision != null && (((1 << collision.gameObject.layer) & platformLayerMask ) != 0) ;
        //(1 << collision.gameObject.layer) &
    }


    
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Running,
    Jumping,
    Sqading
}
public class KelliModel : MonoBehaviour
{
    #region Inspector
    [Header("Current State")]
    public State state;
    public bool facingLeft;
    [Range(-1f, 1f)]
    public float currentSpeed;

    [Header("Balance")]
    public float attackInterval = 0.2f;
    public float speedX =  3;
    public float speedRun;
    #endregion

    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TryMove(float speed)
    {
        currentSpeed = speed; // show the "speed" in the Inspector.

        if (speed != 0)
        {
            bool speedIsNegative = (speed < 0f);
            facingLeft = speedIsNegative; // Change facing direction whenever speed is not 0.
        }

        if (state != State.Jumping)
        {
            state = (speed == 0) ? State.Idle : State.Running;
        }

       
    }

    public void TrySqade()
    {
        return;
    }

    public void TryAttack()
    {
        return;
    }

    public void TryJump()
    {
        StartCoroutine(JumpRoutine());
        
    }

    IEnumerator JumpRoutine()
    {
        if (state == State.Jumping) yield break;
        state = State.Jumping;

        /*const float jumpTime = 1f;
        const float half = jumpTime / 2;
        const float jumpPower = 20f;
        for (float t = 0; t < half; t += Time.deltaTime)
        {
            float d = jumpPower * (half - t);
            transform.Translate(speedX, d*Time.deltaTime, transform.position.z);
            yield return null;
        }
        for (float t = 0; t < half; t += Time.deltaTime)
        {
            float d = jumpPower * t;
            transform.Translate(speedX, -d * Time.deltaTime, transform.position.z);
            yield return null;
        }*/
        rb.AddForce(transform.up * 2);
        yield return new WaitForSeconds(1f);
        

        state = State.Idle;
    }
}

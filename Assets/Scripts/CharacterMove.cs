using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float pow;
    public Transform Yex;
    public Rigidbody2D rb;
    public Animator animator;
    public bool Wait=true;

    private void Update()
    {
        if (Wait)
        {
            //Vector3 direction = Yex.transform.position - transform.position;
            //rb.AddForceAtPosition(direction.normalized, Yex.transform.position); 
            rb.AddForce(Yex.transform.position * pow);

        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Square")
        {
            pow = 0;
          rb.constraints =RigidbodyConstraints2D.FreezeAll;
            animator.enabled = false;
            Wait = false;
        }
    }
}

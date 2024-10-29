using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using System.Numerics;
using UnityEngine;

public class PhysicsTestController : MonoBehaviour
{
    Rigidbody2D rb2D;
    public float speed = 5.0f;
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = this.GetComponent<Rigidbody2D>();
        mat = this.GetComponent<Renderer>().material; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Basic movement
        Vector2 InputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 dir = rb2D.position + InputVector * speed * Time.fixedDeltaTime;

        rb2D.MovePosition(dir);
    }

    // For testing collisions
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Print "Collision 2D" and paint this object green.
        Debug.Log("Collision2D");
        mat.color = Color.green;
 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Print "Trigger 2D" and paint the other object red.
        Debug.Log("Trigger2D");
        Material otherMat = other.gameObject.GetComponent<Renderer>().material;
        otherMat.color = Color.red;
    }
}

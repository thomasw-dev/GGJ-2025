using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    public  float moveSpeed = 10f;
    public Rigidbody2D rigidBody;

    private float horizontal;
    private float vertical;

    public SoapBubble soapBubble;
    public float soapBubbleSpeed = 5;

    private void Start()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rigidBody.velocity = new Vector2(horizontal, vertical).normalized * moveSpeed;



        if (Input.GetMouseButtonDown(0)) 
        {
            // Shoot Bubble
            SoapBubble bubble = Instantiate(soapBubble, transform.parent);
            bubble.transform.position = transform.position;

            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            Vector3 playerPosition = transform.position;
            playerPosition.z = 0;
            Vector3 shootDirection = (mouseWorldPosition - transform.position).normalized;

            bubble.rigidBody.AddForce(shootDirection * soapBubbleSpeed, ForceMode2D.Impulse);
        }
    }
}

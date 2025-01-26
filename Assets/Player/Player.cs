using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public  float moveSpeed = 10f;
    public Rigidbody2D rigidBody;
    public SpriteRenderer bodySprite;

    private float horizontal;
    private float vertical;

    public SoapBubble soapBubble;
    public float soapBubbleSpeed = 5;

    SFX sfx;

    void Awake()
    {
        sfx = GameObject.Find("SFX").GetComponent<SFX>();
    }

    void Start()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (Global.isGamePaused) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rigidBody.velocity = new Vector2(horizontal, vertical).normalized * moveSpeed;

        // Flip sprite on direction
        bodySprite.flipX = (horizontal < 0) ? true : false;


        if (Input.GetMouseButtonDown(0)) 
        {
            // Shoot Bubble
            SoapBubble bubble = Instantiate(soapBubble, transform.parent);

            sfx.Play(Sound.name.ShootBubbles);

            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            Vector3 playerPosition = transform.position;
            playerPosition.z = 0;
            Vector3 shootDirection = (mouseWorldPosition - transform.position).normalized;

            bubble.transform.position = transform.position + shootDirection;

            Vector3 playerVelocity = rigidBody.velocity;
            bubble.rigidBody.AddForce(shootDirection * soapBubbleSpeed, ForceMode2D.Impulse);
        }
    }
}

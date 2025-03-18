using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rigidBody;
    public SpriteRenderer bodySprite;
    [SerializeField] Transform cursor;

    private float horizontal;
    private float vertical;

    public SoapBubble soapBubble;
    public float soapBubbleSpeed = 5;

    float posBoundMaxX = 7.5f;
    float posBoundMinX = -7.5f;
    float posBoundMaxY = 4.25f;
    float posBoundMinY = -3.5f;

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

        if (Global.useTouchInput)
        {
            horizontal = TouchInput.delta.x;
            vertical = TouchInput.delta.y;
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        rigidBody.velocity = new Vector2(horizontal, vertical).normalized * moveSpeed;

        // Flip sprite on direction
        bodySprite.flipX = (horizontal < 0) ? true : false;

        // Calculate shoot direction
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        Vector3 playerPosition = transform.position;
        playerPosition.z = 0;
        Vector3 shootDirection = (mouseWorldPosition - transform.position).normalized;

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            // Shoot Bubble
            SoapBubble bubble = Instantiate(soapBubble, transform.parent);

            sfx.Play(Sound.name.ShootBubbles);

            bubble.transform.position = transform.position;

            Vector3 playerVelocity = rigidBody.velocity;
            bubble.rigidBody.AddForce(shootDirection * soapBubbleSpeed, ForceMode2D.Impulse);
        }

        // Cursor look at mouse
        cursor.rotation = Quaternion.LookRotation(mouseWorldPosition - cursor.position, Vector3.forward);

        // Player position boundaries
        float boundedPosX = Mathf.Clamp(transform.position.x, posBoundMinX, posBoundMaxX);
        float boundedPosY = Mathf.Clamp(transform.position.y, posBoundMinY, posBoundMaxY);
        transform.position = new Vector3(boundedPosX, boundedPosY, transform.position.z);
    }
}

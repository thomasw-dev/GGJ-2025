using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rigidBody;
    public SpriteRenderer bodySprite;
    [SerializeField] Transform cursor;
    [SerializeField] Joystick joystickMove;
    [SerializeField] Joystick joystickShoot;
    [SerializeField] GameObject shootClickArea;

    private float horizontal;
    private float vertical;

    public SoapBubble soapBubble;
    public float soapBubbleSpeed = 5;
    public float fireRate = 0.25f;
    Vector3 shootDirection;

    bool isShooting = false;
    public bool IsShooting
    {
        get => isShooting;
        set
        {
            // Invoke/cancel shooting on value change
            if (isShooting != value)
            {
                if (value == true)
                {
                    InvokeRepeating("ShootBubble", 0f, fireRate);
                }
                else
                {
                    CancelInvoke("ShootBubble");
                }
            }
            isShooting = value;
        }
    }

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

        // Player movement
        if (Global.useTouchInput)
        {
            horizontal = joystickMove.Direction.x;
            vertical = joystickMove.Direction.y;
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        rigidBody.velocity = new Vector2(horizontal, vertical).normalized * moveSpeed;

        // Player position boundaries
        float boundedPosX = Mathf.Clamp(transform.position.x, posBoundMinX, posBoundMaxX);
        float boundedPosY = Mathf.Clamp(transform.position.y, posBoundMinY, posBoundMaxY);
        transform.position = new Vector3(boundedPosX, boundedPosY, transform.position.z);

        // Flip sprite on direction
        bodySprite.flipX = (horizontal < 0) ? true : false;

        // Calculate shoot direction
        if (Global.useTouchInput)
        {
            shootDirection = joystickShoot.Direction;
        }
        else
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            Vector3 playerPosition = transform.position;
            playerPosition.z = 0;
            shootDirection = (mouseWorldPosition - playerPosition).normalized;
        }

        // Cursor direction
        if (Global.useTouchInput)
        {
            if (shootDirection == Vector3.zero)
            {
                cursor.rotation = Quaternion.identity;
            }
            else
            {
                cursor.rotation = Quaternion.LookRotation(joystickShoot.Direction, Vector3.forward);
            }
        }
        else
        {
            cursor.rotation = Quaternion.LookRotation(shootDirection, Vector3.forward);
        }

        // Shoot bubble
        if (Global.useTouchInput)
        {
            if (shootDirection != Vector3.zero)
            {
                IsShooting = true;
            }
            else
            {
                IsShooting = false;
            }
        }
    }

    public void ShootBubble()
    {
        SoapBubble bubble = Instantiate(soapBubble, transform.parent);
        sfx.Play(Sound.name.ShootBubbles);
        bubble.transform.position = transform.position;
        Vector3 playerVelocity = rigidBody.velocity;
        bubble.rigidBody.AddForce(shootDirection * soapBubbleSpeed, ForceMode2D.Impulse);
    }
}

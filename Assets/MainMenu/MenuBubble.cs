using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBubble : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] mailColors;

    float bubbleSpeed = 1f;

    void Start()
    {
        rigidBody.velocity = transform.up * bubbleSpeed;
        spriteRenderer.sprite = mailColors[Random.Range(0, mailColors.Length)];
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(transform.up * bubbleSpeed);
    }
}

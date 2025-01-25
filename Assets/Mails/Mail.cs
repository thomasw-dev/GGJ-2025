using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : MonoBehaviour
{
    public MailType.Colors Color = MailType.Colors.None;
    public Vector2 InitialForce = Vector2.zero;

    Rigidbody2D rigidBody;
    CircleCollider2D col;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        if (InitialForce != Vector2.zero)
        {
            transform.Translate(InitialForce);
        }
    }

    public void DisableInitialPhysics()
    {
        InitialForce = Vector2.zero;
        rigidBody.bodyType = RigidbodyType2D.Static;
        col.isTrigger = true;
    }
}

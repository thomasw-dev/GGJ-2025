using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : MonoBehaviour
{
    public Sprite spriteRed;
    public Sprite spriteBlue;
    public Sprite spriteYellow;

    public Sprite spriteOrange;
    public Sprite spriteGreen;
    public Sprite spritePurple;

    public SpriteRenderer spriteRenderer;

    public bool IsCaptured = false;
    public MailType.Colors Color = MailType.Colors.None;

    Rigidbody2D rigidBody;
    CircleCollider2D col;

    float outOfBound = 10f;

    public void SetColor(MailType.Colors color)
    {
        Color = color;
        switch (color)
        {
            case MailType.Colors.Yellow:
                spriteRenderer.sprite = spriteYellow;
                break;
            case MailType.Colors.Red:
                spriteRenderer.sprite = spriteRed;
                break;
            case MailType.Colors.Blue:
                spriteRenderer.sprite = spriteBlue;
                break;
            case MailType.Colors.Orange:
                spriteRenderer.sprite = spriteOrange;
                break;
            case MailType.Colors.Green:
                spriteRenderer.sprite = spriteGreen;
                break;
            case MailType.Colors.Purple:
                spriteRenderer.sprite = spritePurple;
                break;
            default:
                break;
        }
    }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x) >= outOfBound || Mathf.Abs(transform.position.y) >= outOfBound)
        {
            Destroy(gameObject);
        }
    }

    public void SetInitialVelocity(Vector2 velocity)
    {
        rigidBody.velocity = velocity;
    }

    public void CapturedByBubble()
    {
        IsCaptured = true;
        Destroy(rigidBody);
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        return; // Disable color merge
        /*
        // If it is not captured, touches a mail, and that mail is not captured either
        if (!IsCaptured && col.transform.TryGetComponent(out Mail mail) && !mail.IsCaptured)
        {
            // If self is to the left of the other mail -> be the merge remainder
            if (transform.position.x < col.transform.position.x)
            {
                // If their colors are able to merge
                MailType.Colors mergedColor = MailType.TryMergeColors(Color, mail.Color);
                if (mergedColor != MailType.Colors.None)
                {
                    // Change mail color and move self
                    SetColor(mergedColor);
                    Method.TransformMergeRemainder(transform, rigidBody, col.transform, col.GetComponent<Rigidbody2D>());

                    // Destroy the other mail
                    Destroy(col.gameObject);
                }
            }
        }
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapBubble : MonoBehaviour
{
    public float lifeTime = 7;
    public Rigidbody2D rigidBody;

    public Mail CapturedMail;

    SFX sfx;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sfx = GameObject.Find("SFX").GetComponent<SFX>();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            // Random bubble pop sound
            int num = Random.Range(0, 3);
            if (num == 0) sfx.Play(Sound.name.BubblesPop1);
            if (num == 1) sfx.Play(Sound.name.BubblesPop2);
            if (num == 2) sfx.Play(Sound.name.BubblesPop3);

            Destroy(this.gameObject);
        }

        // Set the mail position to be in the center of itself
        if (CapturedMail != null)
        {
            CapturedMail.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If the bubble hasn't captured a mail yet
        if (CapturedMail == null)
        {
            // If it touches a mail, and the mail is not captured
            if (col.transform.TryGetComponent(out Mail mail) && !mail.IsCaptured)
            {
                // Set itself as the parent for the mail
                mail.transform.SetParent(transform);
                mail.CapturedByBubble();
                CapturedMail = mail;
            }
        }

        return; // Disable color merge
        /*
        // If the bubble has captured a mail: merge with mail or bubble-mail
        else
        {
            // If it touches a mail of a different primary color, and the mail is not captured
            if (col.transform.TryGetComponent(out Mail mail) && !mail.IsCaptured)
            {
                // If their colors are able to merge
                MailType.Colors mergedColor = MailType.TryMergeColors(CapturedMail.Color, mail.Color);
                if (mergedColor != MailType.Colors.None)
                {
                    // Change captured mail color and move self
                    CapturedMail.SetColor(mergedColor);
                    Method.TransformMergeRemainder(transform, rigidBody, col.transform, col.GetComponent<Rigidbody2D>());

                    // Destroy that mail
                    Destroy(col.gameObject);
                }
            }

            // If it has a captured mail and it touches a bubble
            if (CapturedMail != null && col.transform.TryGetComponent(out SoapBubble soapBubble))
            {
                // If the bubble has a captured mail
                if (soapBubble.CapturedMail != null)
                {
                    // If self is to the left of the other bubble -> be the merge remainder
                    if (transform.position.x < col.transform.position.x)
                    {
                        // If their colors are able to merge
                        MailType.Colors mergedColor = MailType.TryMergeColors(CapturedMail.Color, soapBubble.CapturedMail.Color);
                        if (mergedColor != MailType.Colors.None)
                        {
                            // Change captured mail color and move self
                            CapturedMail.SetColor(mergedColor);
                            Method.TransformMergeRemainder(transform, rigidBody, col.transform, col.GetComponent<Rigidbody2D>());

                            // Destroy the other bubble
                            Destroy(col.gameObject);
                        }
                    }
                }
            }
        }
        */
    }
}

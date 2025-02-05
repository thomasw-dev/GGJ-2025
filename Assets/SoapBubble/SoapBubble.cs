using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapBubble : MonoBehaviour
{
    public float lifeTime = 7;
    public Rigidbody2D rigidBody;

    GameObject _capturedMail;

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
        if (_capturedMail != null)
        {
            _capturedMail.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If the bubble doesn't have a mail yet
        if (_capturedMail == null)
        {
            // If it touches any mail
            if (col.transform.TryGetComponent(out Mail mail))
            {
                // If it is not a captured mail
                if (!mail.IsCaptured)
                {
                    // Set itself as the parent for the mail
                    mail.transform.SetParent(transform);
                    mail.CapturedByBubble();
                    _capturedMail = mail.gameObject;
                }
            }
        }
        // If the bubble has captured a mail: merge with mail or bubble-mail
        else
        {
            Mail capturedMail = _capturedMail.GetComponent<Mail>();

            // If it touches a mail of a different primary color
            if (col.transform.TryGetComponent(out Mail mail))
            {
                // If it is not a captured mail
                if (!mail.IsCaptured)
                {
                    MailType.Colors mergedColor = MailType.TryMergeColors(capturedMail.Color, mail.Color);

                    // If their colors are able to merge
                    if (mergedColor != MailType.Colors.None)
                    {
                        // Change captured mail color and move self
                        capturedMail.SetColor(mergedColor);
                        Method.TransformMergeRemainder(transform, rigidBody, col.transform, col.GetComponent<Rigidbody2D>());

                        // Destroy that mail being merged
                        Destroy(col.gameObject);
                    }
                }
            }
        }
    }
}

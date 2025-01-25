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
            Destroy(this.gameObject);

            // Random bubble pop sound
            int num = Random.Range(0, 3);
            if (num == 0) sfx.Play(Sound.name.BubblesPop1);
            if (num == 1) sfx.Play(Sound.name.BubblesPop2);
            if (num == 2) sfx.Play(Sound.name.BubblesPop3);
        }

        // Set the mail position to be in the center of itself
        if (_capturedMail != null)
        {
            _capturedMail.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_capturedMail == null)
        {
            if (col.transform.TryGetComponent(out Mail mail))
            {
                // If it is not a captured mail
                if (!mail.IsCaptured)
                {
                    Debug.Log("Capture the bubble!");

                    // Set itself as the parent for the mail
                    mail.transform.SetParent(transform);
                    mail.CapturedByBubble();
                    _capturedMail = mail.gameObject;
                }
            }
        }
    }
}

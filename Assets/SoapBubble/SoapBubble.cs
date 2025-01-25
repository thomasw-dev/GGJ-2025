using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapBubble : MonoBehaviour
{
    public float lifeTime = 7;
    public Rigidbody2D rigidBody;

    GameObject _capturedMail;

    public void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
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
        if (col.transform.TryGetComponent(out Mail mail))
        {
            // Set itself as the parent for the mail
            mail.transform.SetParent(transform);
            mail.DisableInitialPhysics();
            _capturedMail = mail.gameObject;
        }
    }
}

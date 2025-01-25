using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : MonoBehaviour
{
    public MailType.Colors Color = MailType.Colors.None;
    public Vector2 InitialForce = Vector2.zero;

    void FixedUpdate()
    {
        transform.Translate(InitialForce);
    }
}

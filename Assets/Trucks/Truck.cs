using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField] MailType.Colors _desiredColor = MailType.Colors.None;
    public int TargetMailCount = 1;
    [SerializeField] int _currentMailCount = 0;
    [SerializeField] bool _isAcceptingMail = true;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_currentMailCount >= TargetMailCount)
        {
            _isAcceptingMail = false;
            // Start truck leave animation
            animator.Play("TruckLeave");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Got sth!");

        if (col.transform.TryGetComponent(out TruckManager truckManager))
        {
            Debug.Log("Got manager!");
        }

        if (_isAcceptingMail)
        {
            if (col.transform.TryGetComponent(out Mail mail))
            {
                Debug.Log("Got mail!");
                if (mail.Color == _desiredColor)
                {
                    _currentMailCount++;
                    Destroy(col.gameObject);
                }
            }
        }
    }
}

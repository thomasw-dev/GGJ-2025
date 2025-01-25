using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField] MailType.Colors _desiredColor = MailType.Colors.None;
    [SerializeField] bool _isAcceptingMail = true;
    public int TargetMailCount = 1;
    [SerializeField] int _currentMailCount = 0;
    [SerializeField] TMP_Text _mailsRemainingTMP;

    bool _isArrived = false;
    float _spawnTime;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        _isArrived = false;
        _isAcceptingMail = false;
        _spawnTime = Time.time;
        animator.Play("TruckArrive");
    }

    void Update()
    {
        if (!_isArrived)
        {
            UpdateMailsRemainingTMP();

            // Initially, the truck is not accepting mails until it has arrived (1 second)
            if (Time.time >= _spawnTime + 1f)
            {
                _isAcceptingMail = true;
                _isArrived = true;
                animator.Play("TruckIdle");
            }
        }

        if (_isAcceptingMail)
        {
            UpdateMailsRemainingTMP();

            if (_currentMailCount >= TargetMailCount)
            {
                // Truck finished event

                _isAcceptingMail = false;
                // Start truck leave animation
                animator.Play("TruckLeave");
            }
        }
        
        // Truck failed event when timeout
    }

    void UpdateMailsRemainingTMP()
    {
        if (_mailsRemainingTMP == null) return;

        int num = TargetMailCount - _currentMailCount;
        string text = num > 0 ? num.ToString() : "";
        _mailsRemainingTMP.text = text;
    }

    void OnTriggerStay2D(Collider2D col)
    {
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

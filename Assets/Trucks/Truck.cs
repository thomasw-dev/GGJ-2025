using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField] MailType.Colors _desiredColor = MailType.Colors.None;
    [SerializeField] bool _isAcceptingMail = true;
    public float TimeAllowed = 10f;
    [SerializeField] float _timeRemaining = 10f;
    [SerializeField] Transform _progressMask;
    public int TargetMailCount = 1;
    [SerializeField] int _currentMailCount = 0;
    [SerializeField] TMP_Text _mailsRemainingTMP;

    bool _isArrived = false;
    float _spawnTime;
    bool _hurryUpSoundPlayed = false;

    public event Action TruckFinishedEvent;
    public event Action TruckFailedEvent;

    Animator animator;
    SFX sfx;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sfx = GameObject.Find("SFX").GetComponent<SFX>();
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
                sfx.Play(Sound.name.TruckEnters);
            }
        }

        if (_isAcceptingMail)
        {
            UpdateMailsRemainingTMP();

            _timeRemaining = _spawnTime + TimeAllowed - Time.time;
            UpdateProgressMask(_timeRemaining / TimeAllowed);

            // All mails delivered, truck finished
            if (_currentMailCount >= TargetMailCount)
            {
                TruckFinishedEvent?.Invoke();
                _isAcceptingMail = false;
                animator.Play("TruckLeave");
                sfx.Play(Sound.name.TruckShutsDoorLeaves);
            }
            else
            {
                // Hurry up sound when only 1/3 of the time bar remaining
                if (_timeRemaining <= TimeAllowed / 3)
                {
                    if (!_hurryUpSoundPlayed)
                    {
                        sfx.Play(Sound.name.TruckHurryUp);
                        _hurryUpSoundPlayed = true;
                    }
                }

                // Timeout, truck failed
                if (_timeRemaining <= 0)
                {
                    TruckFailedEvent?.Invoke();
                    _isAcceptingMail = false;
                    animator.Play("TruckLeave");
                    sfx.Play(Sound.name.TruckShutsDoorLeaves);
                }
            }
        }
    }

    void UpdateMailsRemainingTMP()
    {
        if (_mailsRemainingTMP == null) return;

        int num = TargetMailCount - _currentMailCount;
        string text = num > 0 ? num.ToString() : "";
        _mailsRemainingTMP.text = text;
    }

    void UpdateProgressMask(float amount)
    {
        float x = Method.Map(amount, 0, 1, -2, 0);
        _progressMask.localPosition = new Vector3(x, 0, 0);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (_isAcceptingMail)
        {
            if (col.transform.TryGetComponent(out Mail mail))
            {
                if (mail.Color == _desiredColor)
                {
                    _currentMailCount++;
                    sfx.Play(Sound.name.MailSuccess);
                }
                else
                {
                    sfx.Play(Sound.name.MailError);
                }

                Destroy(col.gameObject);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] GameObject _happyFacePrefab;
    [SerializeField] GameObject _angryFacePrefab;
    [SerializeField] Animator _truckSprite;

    bool _isSpawned = false;
    bool _isArrived = false;
    float _spawnTime;
    bool _hurryUpSoundPlayed = false;

    public UnityEvent TruckFinishedEvent;
    public UnityEvent TruckFailedEvent;
    public UnityEvent MailAdded;

    Animator animator;
    SFX sfx;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sfx = GameObject.Find("SFX").GetComponent<SFX>();
    }

    public void SpawnTruck(MailType.Colors color, int mailNeeded, float timeAllowed)
    {
        _isSpawned = true;
        _spawnTime = Time.time;

        _isArrived = false;
        _isAcceptingMail = false;
        animator.Play("TruckArrive");

        _desiredColor = color;
        // Play the static animation of the correct color
        if (_desiredColor == MailType.Colors.Red) _truckSprite.Play("TruckStatic_Red");
        if (_desiredColor == MailType.Colors.Yellow) _truckSprite.Play("TruckStatic_Yellow");
        if (_desiredColor == MailType.Colors.Blue) _truckSprite.Play("TruckStatic_Blue");

        TargetMailCount = mailNeeded;
        _currentMailCount = 0;
        TimeAllowed = timeAllowed;
        _timeRemaining = timeAllowed;
    }

    void Update()
    {
        if (!_isSpawned) return;

        if (!_isArrived)
        {
            UpdateMailsRemainingTMP();

            // Initially, the truck is not accepting mails until it has arrived (1 second)
            if (Time.time >= _spawnTime + 1f)
            {
                _isAcceptingMail = true;
                _isArrived = true;

                // Play the idle animation of the correct color
                if (_desiredColor == MailType.Colors.Red) _truckSprite.Play("TruckIdle_Red");
                if (_desiredColor == MailType.Colors.Yellow) _truckSprite.Play("TruckIdle_Yellow");
                if (_desiredColor == MailType.Colors.Blue) _truckSprite.Play("TruckIdle_Blue");

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
                InstantiateHappyFace();
                sfx.Play(Sound.name.TruckShutsDoorLeaves);
                _isSpawned = false;
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
                    InstantiateAngryFace();
                    sfx.Play(Sound.name.TruckShutsDoorLeaves);
                    _isSpawned = false;
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

    void InstantiateHappyFace()
    {
        GameObject happyFace = Instantiate(_happyFacePrefab, transform.position + Vector3.left * 1.5f, Quaternion.identity);
        happyFace.transform.SetParent(transform.Find("TruckSprite"));
    }

    void InstantiateAngryFace()
    {
        GameObject angryFace = Instantiate(_angryFacePrefab, transform.position + Vector3.left * 1.5f, Quaternion.identity);
        angryFace.transform.SetParent(transform.Find("TruckSprite"));
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!_isSpawned) return;

        if (_isAcceptingMail)
        {
            if (col.transform.TryGetComponent(out Mail mail))
            {
                // Correct mail
                if (mail.Color == _desiredColor)
                {
                    _currentMailCount++;
                    InstantiateHappyFace();
                    MailAdded.Invoke();
                    sfx.Play(Sound.name.MailSuccess);
                }
                // Wrong mail
                else
                {
                    InstantiateAngryFace();
                    sfx.Play(Sound.name.MailError);
                }

                Destroy(col.gameObject);
            }
        }
    }
}

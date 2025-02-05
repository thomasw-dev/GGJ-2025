using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Truck : MonoBehaviour
{
    [SerializeField] MailType.Colors _desiredColor = MailType.Colors.None;
    [SerializeField] bool _isAcceptingMail = true;
    public float TimeAllowed = 10f;
    [SerializeField] float _timeRemaining = 10f;
    [SerializeField] Transform _progressBar;
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
    Vector3 _progressBarInitialPosition;

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

    void Start()
    {
        _progressBarInitialPosition = _progressBar.position;
    }

    public void SpawnTruck(MailType.Colors color, int mailNeeded, float timeAllowed)
    {
        _isSpawned = true;
        _spawnTime = Time.time;

        _isArrived = false;
        _isAcceptingMail = false;
        animator.Play("TruckArrive");
        _hurryUpSoundPlayed = false;

        _desiredColor = color;
        // Play the static animation of the correct color
        if (_desiredColor == MailType.Colors.Red) _truckSprite.Play("TruckStatic_Red");
        if (_desiredColor == MailType.Colors.Yellow) _truckSprite.Play("TruckStatic_Yellow");
        if (_desiredColor == MailType.Colors.Blue) _truckSprite.Play("TruckStatic_Blue");
        if (_desiredColor == MailType.Colors.Orange) _truckSprite.Play("TruckStatic_Orange");
        if (_desiredColor == MailType.Colors.Green) _truckSprite.Play("TruckStatic_Green");
        if (_desiredColor == MailType.Colors.Purple) _truckSprite.Play("TruckStatic_Purple");

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
                if (_desiredColor == MailType.Colors.Orange) _truckSprite.Play("TruckIdle_Orange");
                if (_desiredColor == MailType.Colors.Green) _truckSprite.Play("TruckIdle_Green");
                if (_desiredColor == MailType.Colors.Purple) _truckSprite.Play("TruckIdle_Purple");

                sfx.Play(Sound.name.TruckEnters);
            }
        }

        if (_isAcceptingMail)
        {
            UpdateMailsRemainingTMP();

            _timeRemaining = _spawnTime + 1f + TimeAllowed - Time.time;
            UpdateProgressMask(_timeRemaining / TimeAllowed);

            // All mails delivered, truck finished
            if (_currentMailCount >= TargetMailCount)
            {
                TruckFinishedEvent?.Invoke();
                animator.Play("TruckLeave");
                InstantiateHappyFace();
                sfx.Play(Sound.name.TruckShutsDoorLeaves);
                ResetTruck();
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

                    // Vibrate the progress bar
                    Vector2 r = Random.insideUnitCircle * Method.Map(_timeRemaining, 0, TimeAllowed / 3, 0.1f, 0);
                    _progressBar.transform.position = _progressBarInitialPosition + new Vector3(r.x, r.y, 0);
                }

                // Timeout, truck failed
                if (_timeRemaining <= 0)
                {
                    TruckFailedEvent?.Invoke();
                    animator.Play("TruckLeave");
                    InstantiateAngryFace();
                    sfx.Play(Sound.name.TruckShutsDoorLeaves);
                    ResetTruck();
                }
            }
        }

        // Only show progress bar when the truck is accepting mail
        _progressBar.gameObject.SetActive(_isAcceptingMail);
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
        GameObject happyFace = Instantiate(_happyFacePrefab, transform.Find("TruckSprite"));
        happyFace.transform.Translate(Vector3.left * 1.25f, Space.Self);
    }

    void InstantiateAngryFace()
    {
        GameObject angryFace = Instantiate(_angryFacePrefab, transform.Find("TruckSprite"));
        angryFace.transform.Translate(Vector3.left * 1.25f, Space.Self);
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
                    MailAdded?.Invoke();
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

    void ResetTruck()
    {
        _isSpawned = false;
        _isAcceptingMail = false;
        _hurryUpSoundPlayed = false;
        _progressBar.transform.position = _progressBarInitialPosition;
        _progressMask.localPosition = Vector3.zero;
    }
}

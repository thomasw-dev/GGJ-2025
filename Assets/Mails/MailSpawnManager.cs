using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _mailPrefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] Transform _mailParent;
    [SerializeField] float _spawnDuration = 30;
    [SerializeField] float _spawnInterval = 1;
    [SerializeField] Vector2 _spawnForce = Vector2.zero;

    private MailType.Colors _mailColor;

    [SerializeField] bool _isSpawning;
    public bool IsSpawning
    {
        get => _isSpawning;
        set
        {
            if (value == true)
            {
                InvokeRepeating("SpawnMail", 0, _spawnInterval);
                Invoke("StopSpawn", _spawnDuration);
            }
            else
            {
                CancelInvoke("SpawnMail");
            }
            _isSpawning = value;
        }
    }

    SFX sfx;

    void Awake()
    {
        sfx = GameObject.Find("SFX").GetComponent<SFX>();
    }

    void Start()
    {
        IsSpawning = true;
    }

    public void HandleSpawningColor(MailType.Colors color)
    {
        if (color == MailType.Colors.None)
        {
            IsSpawning = false;
        }
        else
        {
            _mailColor = color;
            IsSpawning = true;
        }
    }

    void SpawnMail()
    {
        GameObject mailObj = Instantiate(_mailPrefab, _spawnPoint.position, Quaternion.identity);
        mailObj.transform.SetParent(_mailParent);
        Mail mail = mailObj.GetComponent<Mail>();
        mail.SetColor(_mailColor);
        mail.SetInitialVelocity(_spawnForce);
        sfx.Play(Sound.name.MailShootsOut);
    }

    void StopSpawn()
    {
        IsSpawning = false;
    }
}

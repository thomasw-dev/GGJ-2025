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

    [SerializeField] bool _isSpawning;
    public bool IsSpawning
    {
        get => _isSpawning;
        set
        {
            if (value == true)
                InvokeRepeating("SpawnMail", 0, _spawnInterval);
            else CancelInvoke("SpawnMail");
            _isSpawning = value;
        }
    }

    List<Coroutine> _spawns;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsSpawning = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            IsSpawning = false;
        }
    }

    void SpawnMail()
    {
        GameObject mail = Instantiate(_mailPrefab, _spawnPoint.position, Quaternion.identity);
        mail.transform.SetParent(_mailParent);
        mail.GetComponent<Mail>().InitialForce = _spawnForce;
    }
}

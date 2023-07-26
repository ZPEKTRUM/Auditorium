using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{

    [SerializeField] UnityEvent _onParticuleDetected;
    [SerializeField] GameObject _prefab;
    [SerializeField] float _fireRate;

    private void Start()
    {


        InvokeRepeating("SpawnParticle", 0f, _fireRate);
    }

    void SpawnParticle()
    {
        _onParticuleDetected.Invoke();
        Vector3 randomDirection = Random.insideUnitCircle;
        Vector3 destination = transform.position + randomDirection;
        Instantiate(_prefab, destination, transform.rotation);

    }

}
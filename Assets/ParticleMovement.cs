using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMovement : MonoBehaviour
{

    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;


    void Start()
    {
        _rb.velocity = transform.right * _speed;

        Destroy(gameObject, 3f);
    }

}
  


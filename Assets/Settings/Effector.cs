using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] CircleCollider2D _circleCollider;

    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnMouseDrag()
    {
        rend.material.color -= Color.white * Time.deltaTime;
        GetComponent<Collider>().attachedRigidbody.AddForce(0, 1, 0);
    }

    public class Effector : MonoBehaviour
    {
        public float horizontalSpeed = 2.0f;

        void OnMouseDrag()
        {
            float h = horizontalSpeed * Input.GetAxis("Effector ");
            transform.Translate(h, 0, 0);
        }
    }
}
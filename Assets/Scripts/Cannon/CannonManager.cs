using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    [SerializeField] private float Vangle;
    [SerializeField] private float Hangle;
    [SerializeField] private float initial_velocity;
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private Vector3 firingDirection;

    private void Awake()
    {
        GameManager.instance.cannon = this;
    }

    private void Start()
    {
        GameManager.instance.cam.setTarget(this.gameObject.transform, new Vector3(0, 2, -2));
        firingPoint = transform.GetChild(0).transform;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(-Vangle, Hangle, 0));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            firingDirection = (transform.position - firingPoint.position).normalized;
            GameObject g = Instantiate(ball, transform);
            Rigidbody rb = g.GetComponent<Rigidbody>();
            rb.velocity = firingDirection * -initial_velocity;
            //Ball b = g.GetComponent<Ball>();
        }
    }

}

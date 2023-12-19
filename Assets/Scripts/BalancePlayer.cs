using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancePlayer : MonoBehaviour
{
    [SerializeField]
    private float targetRotation;

    private Rigidbody2D rb;

    [SerializeField]
    public float rotationForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, 
            targetRotation, 
            rotationForce * Time.deltaTime));
    }
}

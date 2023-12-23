using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancePlayer : MonoBehaviour
{
    private bool rotationLocked;
    private float rotationBeforeLocking;
    private float forceBeforeLocking;

    [SerializeField]
    public float targetRotation;

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

        if (rotationLocked)
            targetRotation = rotationBeforeLocking;
    }


    public void LockRotation()
    {
        forceBeforeLocking = rotationForce;
        rotationBeforeLocking = targetRotation;
        rotationLocked = true;
        rotationForce = 1000.0f;
    }

    public void UnlockRotation()
    {
        targetRotation = rotationBeforeLocking;
        rotationLocked = false;
        rotationForce = forceBeforeLocking;
    }
}

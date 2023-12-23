using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowingScript : MonoBehaviour
{
    [SerializeField]
    private BalancePlayer leftArm;

    [SerializeField]
    private GameObject spear;

    [SerializeField]
    private Animator animator;

    private float throwingForce;
    private bool shouldThrow;

    private void Start()
    {
    }

    void Update()
    {
        if (!Input.GetKey(KeyCode.Space) && shouldThrow)
        {
            animator.SetTrigger("Throw");
            shouldThrow = false;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            shouldThrow = true;
            throwingForce += 10.0f * Time.deltaTime;
        }
    }

    public void SaveAngle()
    {
        spear.GetComponent<BalancePlayer>().enabled = false;
        leftArm.LockRotation();
    }

    private void ThrowSpear()
    {
        MakeSpearProjectile();
        spear.GetComponent<Renderer>().enabled = false;
        spear.GetComponent<Rigidbody2D>().mass = 0.0f;
        StartCoroutine(DrawSpear(1.0f));
        spear.GetComponent<BalancePlayer>().enabled = false;
        leftArm.UnlockRotation();
        throwingForce = 0.0f;
    }

    private void MakeSpearProjectile()
    {
        GameObject spearProjectile = Instantiate(spear);
        spearProjectile.AddComponent<BoxCollider2D>();
        spearProjectile.transform.position = spear.transform.position;

        Destroy(spearProjectile.GetComponent<HingeJoint2D>());
        Destroy(spearProjectile.GetComponent<FixedJoint2D>());
        Destroy(spearProjectile.GetComponent<BalancePlayer>());

        Rigidbody2D spearProjectileRb = spearProjectile.GetComponent<Rigidbody2D>();

        Collider2D[] playerColliders = GetComponentsInChildren<Collider2D>();
        Collider2D spearProjectileCollider = spearProjectile.GetComponent<Collider2D>();

        foreach (Collider2D col2 in playerColliders)
            Physics2D.IgnoreCollision(spearProjectileCollider, col2);

        float angle = spearProjectile.transform.rotation.z + Mathf.PI / 2.0f;
        Vector2 forceVector = new(throwingForce * Mathf.Cos(angle), 
            throwingForce * Mathf.Sin(angle));

        Debug.Log(angle);
        Debug.Log(forceVector.x);
        Debug.Log(forceVector.y);
        spearProjectileRb.AddForce(forceVector);

        
    }

    private IEnumerator DrawSpear(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        spear.GetComponent<Renderer>().enabled = true;
        spear.GetComponent<Rigidbody2D>().mass = 0.1f;
    }

}

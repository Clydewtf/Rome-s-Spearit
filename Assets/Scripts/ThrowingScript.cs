using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ThrowingScript : MonoBehaviour
{
    [SerializeField]
    private KeyCode throwingKey;

    [SerializeField]
    private MyButton throwingButton;

    [SerializeField]
    private BalancePlayer leftArm;

    [SerializeField]
    private GameObject spear;

    [SerializeField]
    private GameObject spearProjectile;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float maxForce;
    [SerializeField]
    private float deltaForce;

    [SerializeField]
    private float throwingForce;
    private bool shouldThrow;

    void Update()
    {
        //ThrowingByKeyboard();
        ThrowingByButton();
    }

    private void ThrowingByKeyboard()
    {
        if (!Input.GetKey(throwingKey) && shouldThrow)
        {
            animator.SetTrigger("Throw");
            shouldThrow = false;
        }
        else if (Input.GetKey(throwingKey))
        {
            shouldThrow = true;
            if (throwingForce <= maxForce)
                throwingForce += deltaForce * Time.deltaTime;
        }
    }

    private void ThrowingByButton()
    {
        if (!throwingButton.isPressed && shouldThrow)
        {
            animator.SetTrigger("Throw");
            shouldThrow = false;
        }
        else if (throwingButton.isPressed)
        {
            shouldThrow = true;
            if (throwingForce <= maxForce)
                throwingForce += deltaForce * Time.deltaTime;
        }
    }

    public void SaveAngle()
    {
        leftArm.LockRotation();
    }

    private void ThrowSpear()
    {
        MakeSpearProjectile();

        spear.GetComponent<Renderer>().enabled = false;
        spear.GetComponent<Rigidbody2D>().mass = 0.0f;

        StartCoroutine(DrawSpear(1.0f));

        leftArm.UnlockRotation();
        throwingForce = 0.0f;
    }

    private void MakeSpearProjectile()
    {
        GameObject spawnedProjectile = Instantiate(spearProjectile);
        spawnedProjectile.transform.SetPositionAndRotation(spear.transform.position, spear.transform.rotation);

        spawnedProjectile.GetComponent<SpriteRenderer>().color = spear.GetComponent<SpriteRenderer>().color;

        Collider2D[] playerColliders = GetComponentsInChildren<Collider2D>();
        Collider2D[] spearProjectileColliders = spawnedProjectile.GetComponents<Collider2D>();

        foreach (Collider2D col1 in playerColliders)
            foreach (Collider2D col2 in spearProjectileColliders)
                Physics2D.IgnoreCollision(col1, col2);

        Rigidbody2D spearProjectileRb = spawnedProjectile.GetComponent<Rigidbody2D>();


        float angle = -spawnedProjectile.transform.eulerAngles.z / 180 * Mathf.PI;

        Vector2 forceVector = new(throwingForce * Mathf.Sin(angle), 
            throwingForce * Mathf.Cos(angle));
        spearProjectileRb.AddForce(forceVector);

        Destroy(spawnedProjectile, 5.0f);
    }

    private IEnumerator DrawSpear(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        spear.GetComponent<Renderer>().enabled = true;
        spear.GetComponent<Rigidbody2D>().mass = 0.1f;
    }
}

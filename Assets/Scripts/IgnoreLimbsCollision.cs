using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreLimbsCollision : MonoBehaviour
{
    void Start()
    {
        var colliders = GetComponentsInChildren<Collider2D>();
        foreach (var firstCollider in colliders)
            foreach (var secondCollider in colliders)
                Physics2D.IgnoreCollision(firstCollider, secondCollider);
    }
}

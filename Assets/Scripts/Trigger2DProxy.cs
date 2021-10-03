using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trigger2DProxy : MonoBehaviour
{
    public Action<Collider2D> OnCollisionTrigger2D_Action;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollisionTrigger2D_Action?.Invoke(collision);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PresenceSensor: MonoBehaviour
{
    public event Action CrookBeenFound;
    public event Action CrookBeenLost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Crook crook) == true)
        {
            CrookBeenFound.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Crook crook) == true)
        {
            CrookBeenLost.Invoke();
        }
    }
}

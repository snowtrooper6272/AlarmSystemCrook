using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DistanceDetector : MonoBehaviour
{
    public event UnityAction<bool> CrookBeenFound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Crook crook) == true)
        {
            CrookBeenFound.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Crook crook) == true)
        {
            CrookBeenFound.Invoke(false);
        }
    }
}

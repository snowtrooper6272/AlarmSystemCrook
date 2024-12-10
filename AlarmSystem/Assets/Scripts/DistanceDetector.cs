using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DistanceDetector : MonoBehaviour
{
    public event UnityAction<bool> CrookBeenFound;

    [SerializeField] private float _distance;


    private void Update()
    {
        Collider2D[] zoneHits = Physics2D.OverlapCircleAll(transform.position, _distance);
        bool isCrookFind = false;

        foreach (Collider2D hit in zoneHits)
        {
            if (hit.TryGetComponent(out Crook crook) == true)
            {
                isCrookFind = true;
            }
        }

        CrookBeenFound.Invoke(isCrookFind);
    }
}

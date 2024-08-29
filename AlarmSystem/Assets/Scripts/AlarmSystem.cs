using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _timeOfTransition;

    private int targetValue = 0;
    private bool _wasPreviousCheckSuccessful = false;

    private void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down);
        bool Iscrook = false;
        
        foreach (RaycastHit2D hit in hits) 
        {
            if (hit.collider.gameObject.TryGetComponent(out Crook crook)) 
            {
                Iscrook = true;

                if (_wasPreviousCheckSuccessful == false)
                {
                    targetValue--;
                    targetValue = Mathf.Abs(targetValue);

                    StartCoroutine(nameof(SmoothVolumeIncrease));
                    _wasPreviousCheckSuccessful = true;
                }
            }
        }

        if (Iscrook == false) 
        {
            _wasPreviousCheckSuccessful = false;
        }
    }

    private IEnumerator SmoothVolumeIncrease()
    {
        _audio.Play();

        float elapsedTime = 0f;
        float startVolume = _audio.volume;

        while (_audio.volume != targetValue)
        {
            float normalizedPosition = elapsedTime / _timeOfTransition;

            elapsedTime += Time.deltaTime;
            _audio.volume = Mathf.Lerp(startVolume, targetValue, normalizedPosition);

            yield return null;
        }
    }
}

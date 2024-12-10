using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _timeOfTransition;
    [SerializeField] private DistanceDetector _distanceDetector;

    private int _targetVolume = 0;
    private bool _wasPreviousCheckSuccessful = false;
    private bool _isCloseCrook = false;

    private void Start()
    {
        _audio.volume = 0;
    }

    private void OnEnable()
    {
        _distanceDetector.CrookBeenFound += Player;
    }
    private void OnDisable()
    {
        _distanceDetector.CrookBeenFound -= Player;
    }

    private void Update()
    {
        if (_isCloseCrook == true) 
        {
            RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, Vector2.down);
            bool Iscrook = false;

            foreach (RaycastHit2D hit in raycastHits)
            {
                if (hit.collider.gameObject.TryGetComponent(out Crook crook))
                {
                    Iscrook = true;

                    if (_wasPreviousCheckSuccessful == false)
                    {
                        _targetVolume--;
                        _targetVolume = Mathf.Abs(_targetVolume);

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
    }

    private IEnumerator SmoothVolumeIncrease()
    {
        float elapsedTime = 0f;
        float startVolume = _audio.volume;

        while (_audio.volume != _targetVolume)
        {
            float normalizedPosition = elapsedTime / _timeOfTransition;

            elapsedTime += Time.deltaTime;
            _audio.volume = Mathf.Lerp(startVolume, _targetVolume, normalizedPosition);

            yield return null;
        }
    }

    private void Player(bool isThereCrook)
    {
        if (isThereCrook == true)
        {
            _isCloseCrook = true;

            if (_audio.isPlaying == false) 
            {
                _audio.Play();
            }
        }
        else 
        {
            _isCloseCrook = false;
            _audio.Stop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crook : MonoBehaviour
{
    [SerializeField] private Transform[] _controlPoints;
    [SerializeField] private float _speed;

    private Transform _currentcontrolPoint;
    private int _currentIndexPoint;

    private void Start()
    {
        _currentcontrolPoint = _controlPoints[_currentIndexPoint];
    }

    private void Update()
    {
        if (transform.position.x != _currentcontrolPoint.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentcontrolPoint.position, _speed * Time.deltaTime);
        }
        else 
        {
            UpdatingDirection();
        }
    }

    private void UpdatingDirection() 
    {
        if (_currentIndexPoint + 1 == _controlPoints.Length)
        {
            return;
        }
        else 
        {
            _currentIndexPoint++;
            _currentcontrolPoint = _controlPoints[_currentIndexPoint];
        }
    }
}

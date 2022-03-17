using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldInputSwipe : MonoBehaviour
{
    [SerializeField]
    private float tapMaxDelta = 0.25f;

    [SerializeField]
    private float maxSwipeDuration = 0.5f;

    [SerializeField]
    private float maxSwipeYdelta = 0.5f;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private float _startTime;
    private float _endTime;

    private void Update()
    {
        if (Input.touchCount == 0) { return; }

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
		{
            _startTime = Time.time;
            _startPosition = GetWorldPosition(touch.position);
		}
        else if (touch.phase == TouchPhase.Ended)
		{
            _endTime = Time.time;
            _endPosition = GetWorldPosition(touch.position);
            ProcessSwipe();
		}
    }

    private void ProcessSwipe()
    {        
        float duration = _endTime - _startTime;

        if (duration > maxSwipeDuration)
        {
            Debug.Log("Twas a Long Touch!");
            return;
        }

        float distance = Vector3.Distance(_startPosition, _endPosition);

        if (distance < tapMaxDelta) 
        { 
            Debug.Log("Twas a Tap!");
            return;
        }       

        if (Mathf.Abs((_endPosition - _startPosition).y) > maxSwipeYdelta)
		{
            Debug.Log("Swiped too vertically");
            return;
		}
        
        if (_endPosition.x < _startPosition.x)
		{
            Debug.Log("Swiped left");
		}
        if (_endPosition.x > _startPosition.x)
		{
            Debug.Log("Swiped right");
		}
    }

	private static Vector3 GetWorldPosition(Vector2 screenPosition)
	{
        return Camera.main.ScreenToWorldPoint(screenPosition);
	}
}

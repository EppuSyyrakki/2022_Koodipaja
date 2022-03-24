using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Koodipaja
{
    public class AnimCurveMover : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve xCurve;

        [SerializeField]
        private AnimationCurve yCurve;

        [SerializeField]
        private float moveTime = 5f;

        [SerializeField]
        private float xMagnitude = 8f;

        [SerializeField]
        private float yMagnitude = 12f;

        private float timer;
        private Vector2 origin;

        private void Start()
        {
            origin = transform.position;
        }

		private void Update()
        {
            timer += Time.deltaTime; 

            if (timer > moveTime) { timer = 0; }

            float x = xCurve.Evaluate(timer / moveTime) * xMagnitude;
            float y = yCurve.Evaluate(timer / moveTime) * yMagnitude;
            Vector2 newPos = new Vector2(x, y) + origin;
            transform.position = newPos;
        }
    }
}


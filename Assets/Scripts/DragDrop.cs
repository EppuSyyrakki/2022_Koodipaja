using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Koodipaja
{
    public class DragDrop : MonoBehaviour
    {
        public Vector2 TouchPosition { get; private set; }

        private bool dragging = false;
        private Transform draggedObject = null;

        private void Update()
        {
            if (dragging)
            {
                draggedObject.position = GetWorldPosition(TouchPosition);
            }
        }


        private void OnTouchPosition(InputAction.CallbackContext ctx)
        {
            TouchPosition = ctx.ReadValue<Vector2>();
        }

        private void OnTouchContact(InputAction.CallbackContext ctx)
        {
        }

        private void OnDrag(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Performed) 
            {
                draggedObject = FindTransformAtTouch();
                
                if (draggedObject == null) { return; }

                Debug.Log("Dragging object :" + draggedObject.gameObject.name);
                dragging = true;
            }
            
            if (ctx.phase == InputActionPhase.Canceled)
            {
                dragging = false;
                draggedObject = null;
            }
        }

        private Transform FindTransformAtTouch()
        {
            Vector3 worldPos = GetWorldPosition(TouchPosition);
            var colliders = Physics2D.OverlapCircleAll(worldPos, 0.5f);

            Debug.Log("Found " + colliders.Length + " colliders at touch");

            foreach(var col in colliders)
            {
                if (col.gameObject.CompareTag("Enemy"))
                {
                    return col.transform;
                }
            }

            return null;
        }

        private static Vector3 GetWorldPosition(Vector2 screenPosition)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
            worldPos.z = 0;
            return worldPos;
        }
    }
}

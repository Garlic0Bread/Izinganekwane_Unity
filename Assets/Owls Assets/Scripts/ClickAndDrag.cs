using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
    public Vector3 resetPos;
    private Vector3 mousePos;
    private Rigidbody rb;
    private Vector3 offset;
    private bool isMoving = true; // Flag to control movement
    bool isTrigger = false;

    private void Start()
    {
        // Ensure there is a Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // Set to kinematic if not affected by other physics
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void OnMouseDown()
    {
        if (isMoving)
        {
            mousePos = GetMouseWorldPosition();
            offset = mousePos - transform.position;
        }
    }
    private void OnMouseUp()
    {
        isTrigger = false;
        rb.MovePosition(resetPos);
    }
    private void OnMouseDrag()
    {
        if (isMoving)
        {
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            Vector3 newPosition = mouseWorldPos - offset;
            rb.MovePosition(newPosition);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CombatCollider_Top"))
        {
            
        }
    }
}

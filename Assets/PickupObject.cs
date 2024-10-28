using System.Collections;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public Transform holdPoint;          // Position to hold the picked-up object
    public float pickUpRange = 1.5f;     // Range within which the player can pick up objects
    public float dropOffset = 1f;        // Offset to move object away from player on drop
    private GameObject pickedUpObject;   // Reference to the currently held object

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pickedUpObject == null)
            {
                AttemptPickUp();
            }
            else
            {
                DropObject();
            }
        }

        if (pickedUpObject != null)
        {
            pickedUpObject.transform.position = holdPoint.position;
        }
    }

    void AttemptPickUp()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickUpRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("PickUp"))
            {
                pickedUpObject = collider.gameObject;
                pickedUpObject.transform.position = holdPoint.position;
                pickedUpObject.transform.SetParent(holdPoint);
                pickedUpObject.GetComponent<Rigidbody2D>().isKinematic = true;
                pickedUpObject.GetComponent<Collider2D>().enabled = false; // Temporarily disable collider
                break;
            }
        }
    }

    void DropObject()
    {
        if (pickedUpObject != null)
        {
            // Drop object slightly away from the player
            Vector3 dropPosition = transform.position + transform.right * dropOffset;
            pickedUpObject.transform.position = dropPosition;

            pickedUpObject.GetComponent<Rigidbody2D>().isKinematic = false;
            pickedUpObject.transform.SetParent(null);
            StartCoroutine(EnableColliderAfterDelay(pickedUpObject));

            pickedUpObject = null; // Clear reference
        }
    }

    // Re-enable collider after a brief delay to prevent collision with player
    private IEnumerator EnableColliderAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(0.2f); // Short delay to prevent collision
        obj.GetComponent<Collider2D>().enabled = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickUpRange);
    }
}

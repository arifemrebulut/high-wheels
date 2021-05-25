using UnityEngine;

public class SuspensionRayCheck : MonoBehaviour
{
    [SerializeField] float rayLength = 0.2f;
    [SerializeField] float raycastZOffset = 0.85f;

    Vector3 rayStartPosition;

    RaycastHit hit;

    private void Start()
    {
        rayStartPosition = transform.position + new Vector3(0f, 0f, raycastZOffset);
    }

    void Update()
    {
        rayStartPosition = transform.position + new Vector3(0f, 0f, raycastZOffset);

        CheckBlocks();
    }

    private void CheckBlocks()
    {
        if (Physics.Raycast(rayStartPosition, Vector3.forward, out hit, rayLength, LayerMask.GetMask("Block")))
        {
            ICollideable collideable = hit.collider.gameObject.GetComponent<ICollideable>();

            if (collideable != null)
            {
                collideable.ExicuteCollisionActions();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(rayStartPosition, rayStartPosition + new Vector3(0f, 0f, rayLength));
    }
}

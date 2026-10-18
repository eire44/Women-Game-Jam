using UnityEngine;

public class MovementClamp : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public Vector3 KeepInsideScreen(Vector3 position)
    {
        float screenZ = -mainCamera.transform.position.z;

        Vector3 minBounds = mainCamera.ViewportToWorldPoint(
            new Vector3(0, 0, screenZ));

        Vector3 maxBounds = mainCamera.ViewportToWorldPoint(
            new Vector3(1, 1, screenZ));

        Collider2D col = GetComponent<Collider2D>();

        float halfWidth = 0f;
        float halfHeight = 0f;

        if (col != null)
        {
            halfWidth = col.bounds.extents.x;
            halfHeight = col.bounds.extents.y;
        }

        position.x = Mathf.Clamp(
            position.x,
            minBounds.x + halfWidth,
            maxBounds.x - halfWidth);

        position.y = Mathf.Clamp(
            position.y,
            minBounds.y + halfHeight,
            maxBounds.y - halfHeight);

        return position;
        //float screenZ = -mainCamera.transform.position.z;

        //Vector3 minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, screenZ));

        //Vector3 maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, screenZ));

        //SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        //float halfWidth = 0f;
        //float halfHeight = 0f;

        //if (sprite != null)
        //{
        //    halfWidth = sprite.bounds.extents.x;
        //    halfHeight = sprite.bounds.extents.y;
        //}

        //position.x = Mathf.Clamp(position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);

        //position.y = Mathf.Clamp(position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        //return position;
    }
}

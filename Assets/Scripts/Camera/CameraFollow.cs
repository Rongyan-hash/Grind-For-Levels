using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private const float CameraZ = -10f;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, CameraZ);

    private void LateUpdate()
    {
        if (target == null) return;

       transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, CameraZ);
    }
}

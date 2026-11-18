using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

    private void LateUpdate()
    {
        if (target == null) return;

        float t = 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime * 10f);
    Vector3 p = transform.position;

    float x = Mathf.Lerp(p.x, target.position.x + offset.x, t);
    float y = Mathf.Lerp(p.y, target.position.y + offset.y, t);

    transform.position = new Vector3(x, y, -10f);   // z 恒定
    }
}

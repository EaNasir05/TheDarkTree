using UnityEngine;

public class FireballDirection : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Vector3 mousePosition;

    public void Update()
    {
        mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        UpdateSpawnPointPosition();
    }

    private void UpdateSpawnPointPosition()
    {
        Vector3 rotation = transform.position - mousePosition;
        float z = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
}

using TMPro.Examples;
using UnityEngine;

public class BattlegroundEntrance : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = new Vector2((float)-12.25, (float)-1.66);
            _camera.transform.position = new Vector3(0, 1, -15);
            _camera.orthographicSize = 11;
        }
    }
}

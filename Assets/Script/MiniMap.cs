using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = playerTransform.position;
        newPosition.y = transform.position.y; // Maintenir la hauteur de la mini-map
        transform.position = newPosition;
    }
}

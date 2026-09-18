
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform playerposition;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] float smoothtime;



    private void Update()
    {

    }

    private void LateUpdate()
    {

        Vector3 newPosition = Vector3.Lerp(transform.position, playerposition.position + offset, smoothtime * Time.deltaTime);
        transform.position = newPosition;
    }
}



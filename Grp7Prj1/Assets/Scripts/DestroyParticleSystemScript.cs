using UnityEngine;

public class DestroyParticleSystemScript : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 0.5f;
    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}

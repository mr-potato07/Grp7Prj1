using UnityEngine;

public class KillZoneScript : MonoBehaviour
{
    [SerializeField] private Transform spawnposition;

    public int damagegiven = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = spawnposition.position;
            other.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;



            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(damagegiven);

               
            }
        }
    }
}

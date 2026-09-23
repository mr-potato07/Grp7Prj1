using UnityEngine;

public class HealthPickupScript : MonoBehaviour
{
    [SerializeField] private int healthAmountRestore;
    [SerializeField] private AudioClip healthPickupSFX;
    [SerializeField] private GameObject BananaParticlesys;

    private AudioSource audiosrc;
    private void Start()
    {
        audiosrc = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool hasRestoredHealth = other.gameObject.GetComponent<PlayerHealthScript>().RestoreHealth(healthAmountRestore);

            if (hasRestoredHealth)
            {
                audiosrc.PlayOneShot(healthPickupSFX);
                Instantiate(BananaParticlesys, transform.position, Quaternion.identity);
            
                Destroy(gameObject, 0.20f);
               
            }

            

           

        }
    }
}

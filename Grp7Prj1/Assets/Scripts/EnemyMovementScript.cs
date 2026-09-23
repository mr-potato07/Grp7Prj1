using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float bounce = 100f;

    [SerializeField] private int damagegiven = 1;

    [SerializeField] private float knockbackForce = 100f;

    [SerializeField] private float upwardsForce = 5f;

    [SerializeField] private AudioClip DeathSFX;

    private AudioSource audiosrc;
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        audiosrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(moveSpeed < 0)
        {
            rend.flipX = true;
        } if (moveSpeed > 0)
        {
            rend.flipX = false;
        }
    }
    void FixedUpdate()
    {
        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock") || (other.gameObject.CompareTag("Enemy")) || (other.gameObject.CompareTag("Player")))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Player"))
            {
            other.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(damagegiven);

            if(other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovementScript>().TakeKnockBack(knockbackForce, upwardsForce);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovementScript>().TakeKnockBack(-knockbackForce, upwardsForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            

            Rigidbody2D rb = other.attachedRigidbody;

            if(rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.AddForce(new Vector2( 0, bounce ));
            }
            audiosrc.PlayOneShot(DeathSFX);
            Destroy(gameObject, 0.1f);
        }
    }

}

using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private int startinghealth = 5;
    [SerializeField] private Transform spawnposition;
    [SerializeField] private Slider healthbar;

    [SerializeField] private Image healthfill;


    [SerializeField] private AudioClip takeDamageSFX;
    private int currenthealth;

    private AudioSource audiosrc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthealth = startinghealth;
        Healthbar();
        audiosrc = GetComponent<AudioSource>();
    }


    void Update()
    {

    }

    public void TakeDamage(int damage)
    {

        currenthealth -= damage;
        print(currenthealth);
        Healthbar();
        audiosrc.PlayOneShot(takeDamageSFX);
        if (currenthealth <= 0)
        {
            Respawn();
        }


    }


    private void Respawn()
    {
        currenthealth = startinghealth;
        transform.position = spawnposition.position;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        Healthbar();
    }

    private void Healthbar()
    {
        healthbar.value = currenthealth;

        if (currenthealth <= 2)
        {
            healthfill.color = Color.crimson;
        }
        else
        {
            healthfill.color = Color.limeGreen;
        }
    }

    public bool RestoreHealth(int healthRestoreAmount)
    {
        if (currenthealth >= startinghealth)
        {
            return false;
        }
        else currenthealth += healthRestoreAmount;
        Healthbar();


        if (currenthealth > startinghealth)
        {
            currenthealth = startinghealth;
        }
        return true;
    }
}

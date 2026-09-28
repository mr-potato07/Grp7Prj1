using UnityEngine;

public class TutorialTriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject tutorialImage;

    private void Start()
    {

        tutorialImage.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialImage.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialImage.SetActive(false);
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishScript : MonoBehaviour
{
    [SerializeField] private GameObject panel, finishtext;
    [SerializeField] private int levelIndex;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          
              /*  panel.SetActive(true);*/
              /*  finishtext.SetActive(true);*/
                anim.enabled = true;
                Invoke(nameof(LoadNextLevel), 3f);
            
        }
    }
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          /*  panel.SetActive(false);*/
            /*finishtext.SetActive(false);*/

        }
    }
}

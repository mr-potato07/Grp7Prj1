using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretRoomTrigger : MonoBehaviour
{
    [SerializeField] private TilemapRenderer foregroundRenderer;
    [SerializeField] private AudioSource audioSource;

    private bool opened = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (opened)
            return;

        if (other.CompareTag("Player"))
        {
            opened = true;

            // Spela ljudet
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Dölj foreground-tilemapen
            if (foregroundRenderer != null)
            {
                foregroundRenderer.enabled = false;
            }
        }
    }
}
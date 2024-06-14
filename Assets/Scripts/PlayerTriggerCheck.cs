using UnityEngine;

public class PlayerTriggerCheck : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingPlatform"))
        {
            player.transform.parent = collision.gameObject.transform;
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            SoundManager.instance?.PlayDeathSound();
            player.TakeDamage(100f);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            SoundManager.instance?.PlayCollectSound();
            GameManager.Instance?.AddToCoins();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Key"))
        {
            player.hasKey = true;
            SoundManager.instance?.PlayCollectSound();
            GameManager.Instance?.EnableKeyUI();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Finish") && player.hasKey)
        {
            GameManager.Instance?.ShowVictory();
        }

        if (collision.gameObject.CompareTag("Boundary"))
        {
            GameManager.Instance?.RestartGame();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingPlatform"))
        {
            player.transform.parent = null;
        }
    }
}

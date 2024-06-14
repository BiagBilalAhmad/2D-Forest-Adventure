using UnityEngine;

public class TriggerTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var block = GetComponentInParent<BreakableBlock>();
            StartCoroutine(block.BreakBlock());

            if (block.canCollide)
                Destroy(gameObject, block.delay + 0.5f);
        }
    }
}

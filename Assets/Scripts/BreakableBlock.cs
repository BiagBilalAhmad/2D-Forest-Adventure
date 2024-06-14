using System.Collections;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    public float delay = 0;
    public bool canCollide = false;
    public bool canDestroy = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //GetComponent<Animator>().enabled = true;
            StartCoroutine(BreakBlock());
        }
    }

    public IEnumerator BreakBlock()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.1f);
        if (!canCollide)
            GetComponent<Collider2D>().enabled = false;
        if(canDestroy)
            Destroy(gameObject, 5f);
    }
}

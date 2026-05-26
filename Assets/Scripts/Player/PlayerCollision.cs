using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            HealthManager.health--;

            if (HealthManager.health <= 0)
            {
                PlayerManager.isGameOver = true;
                AudioManager.instance.Play("GameOver");
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(GetHurt());
            }
        }
    }

    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 8, true);

        if (animator != null)
            animator.SetLayerWeight(1, 1);

        yield return new WaitForSeconds(3);

        if (animator != null)
            animator.SetLayerWeight(1, 0);

        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
}
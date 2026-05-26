using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Animator animator;
    public GameObject bullet;
    public float force = 450;

    private Transform bulletHole;
    private PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (animator == null) animator = GetComponent<Animator>();

        bulletHole = transform.Find("BulletHole");

        if (bulletHole == null)
            Debug.LogError("BulletHole child not found on " + gameObject.name);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Fire();
    }

    void Fire()
    {
        if (bulletHole == null || bullet == null) return;

        animator.SetTrigger("shoot");
        AudioManager.instance.Play("Shoot");

        // Original rotation kept
        GameObject go = Instantiate(bullet, bulletHole.position, bullet.transform.rotation);
        Rigidbody2D bulletRB = go.GetComponent<Rigidbody2D>();
        bulletRB.gravityScale = 0;

        bulletRB.AddForce(playerMovement.isFacingRight
            ? Vector2.right * force
            : Vector2.left * force);

        Destroy(go, 1.2f);
    }
}
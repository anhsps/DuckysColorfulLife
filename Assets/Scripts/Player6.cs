using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player6 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private bool moveRight = true;

    [SerializeField] private float speed = 2.5f, rayDis = 0.35f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckWall();

        bool isMoving = Mathf.Abs(rb.velocity.x) > 0.1f;
        animator.SetBool("Run", isMoving);
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveRight ? speed : -speed, rb.velocity.y);
        transform.rotation = Quaternion.Euler(0, moveRight ? 0 : 180, 0);
    }

    private void CheckWall()
    {
        Vector2 dir = moveRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, rayDis, LayerMask.GetMask("Ground"));

        if (hit.collider != null) moveRight = !moveRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 dir = moveRight ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)dir * rayDis);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
            GameManager6.instance.GameWin();
        if (collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            SoundManager6.instance.PlaySound(4);
            GameManager6.instance.GameLose();
        }
    }
}

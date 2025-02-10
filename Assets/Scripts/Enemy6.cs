using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moveRight = true;

    [SerializeField] private float rayDis = 0.55f;
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckWall();
    }

    private void Move()
    {
        if (rb.bodyType == RigidbodyType2D.Dynamic)
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring6 : MonoBehaviour
{
    [SerializeField] private float bounceForce = 8.5f;
    private Vector3 localScale;
    private Collider2D springCol;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        springCol = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Trap"))
        {
            SoundManager6.instance.PlaySound(6);
            springCol.enabled = false;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb) rb.velocity = new Vector2(rb.velocity.x, bounceForce);
            StartCoroutine(ScaleSpring());
        }
    }

    private IEnumerator ScaleSpring()
    {
        transform.localScale = new Vector3(localScale.x, localScale.y * 3f, localScale.z);
        yield return new WaitForSeconds(0.2f);
        transform.localScale = localScale;
        springCol.enabled = true;
    }
}

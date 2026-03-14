using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D rb;
    SpriteRenderer spr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        if (!isLive)
            return;
        float dir = target.position.x - transform.position.x;
        dir = Mathf.Sign(dir);

        rb.linearVelocity = new Vector2( dir * speed, rb.linearVelocity.y );
        
    }

    void LateUpdate()
    {
        if (!isLive)
            return;
        spr.flipX = target.position.x < rb.position.x;
    }
}

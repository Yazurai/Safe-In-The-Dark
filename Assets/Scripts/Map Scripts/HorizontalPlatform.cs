using UnityEngine;

public class HorizontalPlatform : Photon.MonoBehaviour
{
    public float MaxX;
    public float MinX;

    public float speed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    [PunRPC]
    public void SetDirection(bool direction)
    {
        if (direction)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }

    void Update() {
        if(rb.position.x > MaxX)
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        else if (rb.position.x < MinX)
        {
            rb.velocity = new Vector2(speed, 0);
        }
    }
}

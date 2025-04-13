using NUnit.Framework.Constraints;
using UnityEngine;

public class FB_GroundMoveLeft : FB_MoveLeft
{
    private SpriteRenderer spriteRenderer;
    private float width;
    private float startPosX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        width = spriteRenderer.bounds.size.x;
        startPosX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.position.x <= startPosX - width / 2)
        {
            transform.position = new Vector2(startPosX, transform.position.y);
        }
    }
}

using UnityEngine;

public class FB_ParallaxBackground : FB_GroundMoveLeft
{
    public float parallaxFactor;

    protected override void Move()
    {
        transform.Translate(Vector2.left * FB_GameManager.Instance.gameSpeed * parallaxFactor * Time.deltaTime);
    }
}

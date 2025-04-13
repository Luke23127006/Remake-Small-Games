using UnityEngine;

public class FB_MoveLeft : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.left * FB_GameManager.Instance.gameSpeed * Time.deltaTime);
    }
}

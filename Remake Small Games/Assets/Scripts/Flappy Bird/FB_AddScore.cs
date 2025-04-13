using UnityEngine;

public class FB_AddScore : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FB_GameManager.Instance.AddScore();
            boxCollider.enabled = false;
        }
    }
}

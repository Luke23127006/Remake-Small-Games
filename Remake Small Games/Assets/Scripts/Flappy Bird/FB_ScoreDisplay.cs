using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class FB_ScoreDisplay : MonoBehaviour
{
    public Image[] digits;
    private HorizontalLayoutGroup layout;
    private RectTransform rectTransform;
    private int layoutWidth = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layout = GetComponent<HorizontalLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();

        FB_GameManager.Instance.OnScoreUpdated += UpdateScore;
    }

    void OnDestroy()
    {
        if (FB_GameManager.Instance != null)
        {
            FB_GameManager.Instance.OnScoreUpdated -= UpdateScore;
        }
    }

    void UpdateScore()
    {
        // clear layout
        foreach (Transform child in layout.transform)
        {
            Destroy(child.gameObject);
        }
        layoutWidth = 0;

        string score = FB_GameManager.Instance.GetScore.ToString();
        for (int i = 0; i < score.Length; i++)
        {
            int digit = score[i] - '0';

            Image digitImage = Instantiate(digits[digit], layout.transform);
            layoutWidth += (int)digitImage.rectTransform.rect.width * (int)digitImage.rectTransform.localScale.x;
            rectTransform.sizeDelta = new Vector2(layoutWidth, rectTransform.sizeDelta.y);
        }
    }
}

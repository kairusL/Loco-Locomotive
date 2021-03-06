using UnityEngine;

public class UIFollow : MonoBehaviour
{
    public RectTransform rectTransform;
    public Vector3 offset;

    void Update()
    {
        Vector2 vec2 = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + offset);
        rectTransform.anchoredPosition = new Vector2(vec2.x - Screen.width / 2 + 0, vec2.y - Screen.height / 2 + 60);
    }

}

using UnityEngine;
using UnityEngine.UI;

public class ItemIndicator : MonoBehaviour
{
    public float speed;
    public Transform image;
    public Transform text;
    public int targetProcess = 5;
    private float currentAmout = 0;
    Items.Item item;

    void Update()
    {
        if (currentAmout < targetProcess)
        {
            currentAmout += speed * Time.deltaTime;
            if (currentAmout > targetProcess)
                currentAmout = targetProcess;
            text.GetComponent<Text>().text = ((int)currentAmout).ToString() + "s";
            image.GetComponent<Image>().fillAmount = currentAmout / 5.0f;
        }
    }
}

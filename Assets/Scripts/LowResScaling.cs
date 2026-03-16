using UnityEngine;

public class LowResScaling : MonoBehaviour
{
    private int lastWidth;
    private int lastHeight;
    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        UpdateSize();
    }

    void Update()
    {
        if (Screen.width != lastWidth || Screen.height != lastHeight)
        {
            UpdateSize();
        }
    }

    void UpdateSize()
    {
        lastWidth = Screen.width;
        lastHeight = Screen.height;

        rt.sizeDelta = new Vector2(Screen.width, Screen.height) * 1.01f;
    }
}
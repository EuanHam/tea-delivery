using UnityEngine;

public class LowResScaling : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(Screen.width, Screen.height) * 1.01f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

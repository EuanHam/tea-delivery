using UnityEngine;
using TMPro;
public class BobaDriver : MonoBehaviour
{

    [SerializeField] private TMP_Text hudText;
    public NewOrder load;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (load != null)
        {
            load.ttl -= Time.deltaTime;
            hudText.text = string.Format("{0:N2}", load.ttl);
            if (load.ttl <= 0) load = null;
        }
    }
}

using UnityEngine;
using TMPro;
public class BobaDriver : MonoBehaviour
{
    public NewOrder load;

    [SerializeField] private DyanmicMinimap minimap;

    [SerializeField] private GameObject ui;
    [SerializeField] private TMP_Text time, name;

    void Start()
    {
        ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (load != null)
        {
            ui.SetActive(true);
            minimap.dest = load.customer.transform;
            load.ttl -= Time.deltaTime;
            time.text = string.Format("{0:N2}", load.ttl);
            if (load.ttl <= 0) {
                minimap.dest = null;
                load = null;
            }
        } else
        {
            ui.SetActive(false);
            // TODO set minimap.dest to bobashop
        }
    }
}

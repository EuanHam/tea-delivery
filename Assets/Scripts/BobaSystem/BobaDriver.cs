using UnityEngine;
using TMPro;
public class BobaDriver : MonoBehaviour
{
    public NewOrder load;
    public int balance;

    [SerializeField] private DyanmicMinimap minimap;

    [SerializeField] private GameObject ui;
    [SerializeField] private TMP_Text time_text, name_text, balance_text;

    void Start()
    {
        ui.SetActive(false);
        balance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (load != null)
        {
            ui.SetActive(true);
            minimap.dest = load.customer;
            load.ttl -= Time.deltaTime;
            time_text.text = string.Format("{0:N2}", load.ttl);
            if (load.ttl <= 0) {
                minimap.dest = null;
                load = null;
            }
        } else
        {
            ui.SetActive(false);
            
            // TODO set minimap.dest to bobashop
        }
        balance_text.text = "$" + balance.ToString();
    }
}

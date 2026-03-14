using UnityEngine;

public class IvenUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool activeInventory = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryPanel.SetActive(activeInventory);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }
}

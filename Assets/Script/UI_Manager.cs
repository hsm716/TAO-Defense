using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject bottom_background;
    [SerializeField]
    private GameObject scroll_view;
    [SerializeField]
    private GameObject item_panel;

    public GameObject Trap_1;
    public GameObject Trap1_value;
    public GameObject Trap_2;
    public GameObject Trap2_value;
    public GameObject Trap_block;
    public GameObject TrapBlock_value;
    public GameObject Trap_snow;
    public GameObject TrapSnow_value;
    public GameObject Trap_roket;
    public GameObject TrapRoket_value;

    void Start()
    {
        int width = Screen.width;
        int height = Screen.height;
        bottom_background.GetComponent<RectTransform>().sizeDelta = new Vector2(width*0.85f, height * 0.25f);
        bottom_background.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, height*0.1f);

        item_panel.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 0.85f, height * 0.25f);
        item_panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        Trap_1.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 0.2f, height * 0.08f);
        Trap_1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-width*0.23f, -height*0.01f);

        Trap1_value.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 0.13f, height * 0.022f);
        Trap1_value.GetComponent<RectTransform>().anchoredPosition = new Vector2(-width * 0.22f, -height * 0.035f);
        

        Trap_2.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 0.2f, height * 0.08f);
        Trap_2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-width * 0.08f, -height * 0.01f);

        Trap2_value.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 0.13f, height * 0.022f);
        Trap2_value.GetComponent<RectTransform>().anchoredPosition = new Vector2(-width * 0.08f, -height * 0.035f);


        Trap_block.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 0.2f, height * 0.08f);
        Trap_block.GetComponent<RectTransform>().anchoredPosition = new Vector2(width * 0.15f, -height * 0.01f);

        TrapBlock_value.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 0.13f, height * 0.022f);
        TrapBlock_value.GetComponent<RectTransform>().anchoredPosition = new Vector2(width * 0.15f, -height * 0.035f);


        Trap_snow.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 0.2f, height * 0.08f);
        Trap_snow.GetComponent<RectTransform>().anchoredPosition = new Vector2(-width * 0.23f, -height * 0.01f);

        TrapSnow_value.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 0.13f, height * 0.022f);
        TrapSnow_value.GetComponent<RectTransform>().anchoredPosition = new Vector2(-width * 0.22f, -height * 0.035f);


        Trap_roket.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 0.2f, height * 0.08f);
        Trap_roket.GetComponent<RectTransform>().anchoredPosition = new Vector2(-width * 0.08f, -height * 0.01f);

        TrapRoket_value.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 0.13f, height * 0.022f);
        TrapRoket_value.GetComponent<RectTransform>().anchoredPosition = new Vector2(-width * 0.08f, -height * 0.035f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

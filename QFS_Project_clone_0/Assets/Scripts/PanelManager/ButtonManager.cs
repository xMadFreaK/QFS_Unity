using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonManager : MonoBehaviour
{
    public PanelType desiredPanelType;

    PanelManager panelManager;
    Button menueButton;


    // Start is called before the first frame update
    void Start()
    {
        menueButton = GetComponent<Button>();
        menueButton.onClick.AddListener(OnButtonClicked);
        panelManager = PanelManager.GetInstance();
    }

    void OnButtonClicked() {
        panelManager.SwitchCanvas(desiredPanelType);
    }
}

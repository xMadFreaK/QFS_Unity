using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PanelType {
    Background,
    AccountCreationScreen,
    LogInScreen,
    MainScreen,
    MatchmakingScreen,
    StatisticScreen,
    GameScreen,
    SettingScreen,
    ResultScreen,
    LOGINSCREENTEST,
    GAME
}



public class PanelManager : Singleton<PanelManager> {                   //derives from abstract class Singleton, want to make sure, that only one PanelManager exists.

    List<PanelController> panelControllerList;
    PanelController lastActivePanel;

    protected override void Awake() {
        panelControllerList = GetComponentsInChildren<PanelController>().ToList();      //ToList() puts the array from GetComp-method into the list

        for(int i = 0; i<panelControllerList.Count; i++) {                              //Deactivate all panels
            panelControllerList[i].gameObject.SetActive(false);
        }
        
        SwitchCanvas(PanelType.LogInScreen);                                  //starts with this screen

    }

    public void SwitchCanvas(PanelType _type) {
        if(lastActivePanel != null) {
            lastActivePanel.gameObject.SetActive(false);                    //deactivate the last active panel
            Debug.Log("lastActivePanel succesfully deacitvated");
        }
        else
        {
            Debug.Log("No lastActivePanel");
        }

        PanelController desiredPanel = panelControllerList.Find(x => x.panelType == _type);
        PanelController backgroundPanel = panelControllerList.Find(y => y.panelType == PanelType.Background);

        if (desiredPanel != null) {

            backgroundPanel.gameObject.SetActive(true);
            desiredPanel.gameObject.SetActive(true);                 //activates the start-panel
            lastActivePanel = desiredPanel;
        } else {
            Debug.Log("The desired panel was not found");
        }

    }


}    
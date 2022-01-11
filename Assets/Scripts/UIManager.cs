using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    GameObject Menu;

    [SerializeField]
    FirebaseManagerMain MainManager;

    [SerializeField]
    PopupUIController MenuController;

    UIList UIList;
    //Screen object variables

    public void Start()
    {
        //UIList.GetUI("QuestListUI").SetActive(false);
        //UIList.GetUI("TeamsUI").SetActive(false);
        //UIList.GetUI("HelpUI").SetActive(false);
        UIList = FindObjectOfType<UIList>();
        UIList.GetUI("MainUI").SetActive(true);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void ToMainUI()
    {
        ToScreen("MainUI");
    }

    public void ToScreen(string name)
    {
        Debug.Log(name);
        if (MenuController)
            MenuController.HideMenu();
        Debug.Log(UIList.GetActivUI().name);
        UIList.GetActivUI().SetActive(false);
        Debug.Log(UIList.GetActivUI().name);
        UIList.GetUI(name.ToString()).SetActive(true);
        Debug.Log(UIList.GetActivUI().name);
    }

    public void ToMenu()
    {
        Debug.Log("Start Menu");
        MenuController.ShowMenu();
    }

    public void Exit()
    {
        Info.Uid = null;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("AuthScene");
    }

    public void HideMenu()
        => MenuController.HideMenu();

    public void OpenQuest(GameObject gameObject)
    {
        Debug.Log(gameObject.name);
        MainManager.OpenQuest(gameObject);
        ToScreen("QuestUI");
    }

    public void CreateRoom()
    {
        MainManager.CreateRoom();
        ToScreen("RoomUI");
        MainManager.ManagerRealtime.Room.LoadStageQuest();
    }

    public void JoinToRoom()
    {
        MainManager.JoinRoom();
        Debug.Log("Start Load Room");
        ToScreen("RoomUI");
        MainManager.ManagerRealtime.Room.LoadStageQuest();
    }

    public void ExitRoom()
    {
        MainManager.ExitRoom();
        ToScreen("MainUI");
    }

    public void StartQuest()
    {
        Info.CorrectStage = 0;
        Debug.Log("Correct Stage:" + Info.CorrectStage);
        MainManager.LoadLevel();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private static MenuController instance;
    public GameObject statsUI;
    private SaveDataHandler saveDataHandler;
    [SerializeField]
    private List<GameObject> menuItems=new List<GameObject>();
    private string menuTag="MenuWindow";
    public bool menuOpen{get;private set;}
    // Start is called before the first frame update
    void Start()
    {
        instance=this;
        foreach(Transform child in transform)
        {
            if(child.tag==menuTag)
                menuItems.Add(child.gameObject);
        }
        saveDataHandler=SaveDataHandler.GetInstance();
        OpenStartMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static MenuController GetInstance()
    {
        //if(MenuController.instance==null)
          //  MenuController.instance=new MenuController();
        return MenuController.instance;
    }
    private void CloseAllWindows()
    {
        for(int i=0;i<menuItems.Count;i++)
            menuItems[i].SetActive(false);
        menuOpen=false;
    }
    public void OpenStartMenu()
    {
        Time.timeScale=0;
        CloseAllWindows();
        menuItems[0].SetActive(true);
        menuOpen=true;
    }
    public void PauseGame()
    {
        Time.timeScale=0;
        CloseAllWindows();
       // Debug.Log(menuItems.Count);
        menuItems[1].SetActive(true);
        
        menuOpen=true;
    }

    public void ResumeGame()
    {
        CloseAllWindows();
        Time.timeScale=1;
    }

    public void OpenSaveMenu()
    {
        Time.timeScale=0;
        CloseAllWindows();
        menuItems[2].SetActive(true);
        menuOpen=true;
    }
    public void OpenLoadMenu()
    {
        Time.timeScale=0;
        CloseAllWindows();
        menuItems[3].SetActive(true);
        menuOpen=true;
    }
    public void SaveProgress(int entry)
    {
        saveDataHandler.SaveProgress(entry);
        
    }
    public void LoadProgress(int entry)
    {
        if(entry<0)
        {
            saveDataHandler.LoadProgress(entry);
            Time.timeScale=1;
            CloseAllWindows();
           // statsUI.SetActive(true);
        }
        else if(!saveDataHandler.SlotEmpty(entry))
        {
            saveDataHandler.LoadProgress(entry);
            Time.timeScale=1;
            CloseAllWindows();
        }
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

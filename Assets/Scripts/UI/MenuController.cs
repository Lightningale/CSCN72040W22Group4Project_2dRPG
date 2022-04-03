using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private static MenuController instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static MenuController GetInstance()
    {
        if(MenuController.instance==null)
            MenuController.instance=new MenuController();
        return MenuController.instance;
    }
    public void PauseGame()
    {
        Time.timeScale=0;
    }

    public void ResumeGame()
    {
        Time.timeScale=1;
    }

    public void OpenSaveMenu()
    {

    }
    public void OpenLoadMenu()
    {

    }
    public void LoadProgress(int slot)
    {

    }
    public void SaveProgress(int slot)
    {

    }
    public void QuitGame()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerControlSet
{
    public float horizontalInput,verticalInput;
    public int AlphaKeyDown;
    public bool leftClick;
    public bool clickJump;
}
public class InputManager
{
    private static InputManager instance;
    // Start is called before the first frame update
    [SerializeField]
    private MenuController menuController;
    private PlayerControlSet playerControlSet;


    private InputManager()
    {

    }
    public static InputManager GetInstance()
    {
        if(InputManager.instance==null)
            InputManager.instance=new InputManager();
        return InputManager.instance;
    }
    public PlayerControlSet GetControlInput()
    {
        playerControlSet.horizontalInput=Input.GetAxisRaw("Horizontal");//Only -1 or 1
        playerControlSet.verticalInput=Input.GetAxisRaw("Vertical");
        playerControlSet.clickJump=Input.GetButtonDown("Jump");
        playerControlSet.leftClick=Input.GetMouseButtonDown(0);
        if(Input.GetKeyDown(KeyCode.Alpha0))
            playerControlSet.AlphaKeyDown=0;
        else if(Input.GetKeyDown(KeyCode.Alpha1))
            playerControlSet.AlphaKeyDown=1;
        else if(Input.GetKeyDown(KeyCode.Alpha2))
            playerControlSet.AlphaKeyDown=2;
        else if(Input.GetKeyDown(KeyCode.Alpha3))
            playerControlSet.AlphaKeyDown=3;
        else if(Input.GetKeyDown(KeyCode.Alpha4))
            playerControlSet.AlphaKeyDown=4;
        else if(Input.GetKeyDown(KeyCode.Alpha5))
            playerControlSet.AlphaKeyDown=5;
        else if(Input.GetKeyDown(KeyCode.Alpha6))
            playerControlSet.AlphaKeyDown=6;
        else if(Input.GetKeyDown(KeyCode.Alpha7))
            playerControlSet.AlphaKeyDown=7;
        else if(Input.GetKeyDown(KeyCode.Alpha8))
            playerControlSet.AlphaKeyDown=8;
        else if(Input.GetKeyDown(KeyCode.Alpha9))
            playerControlSet.AlphaKeyDown=9;
        else
            playerControlSet.AlphaKeyDown=-1;
        if(playerControlSet.AlphaKeyDown!=-1)
        {
            //Debug.Log(playerControlSet.AlphaKeyDown+" pressed");
        }
        if(playerControlSet.leftClick)
          Debug.Log(playerControlSet.leftClick);
        return playerControlSet;
    }
    public bool OpenMenuInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}

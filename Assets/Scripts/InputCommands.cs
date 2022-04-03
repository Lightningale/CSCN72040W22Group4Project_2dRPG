using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputCommands
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class OpenPauseMenu:InputCommands
{

}
public class OpenSaveMenu:InputCommands
{

}
public class OpenLoadMenu:InputCommands
{

}
public class SaveProgress:InputCommands
{
    public SaveProgress(int entry)
    {
        
    }
}
public class LoadProgress:InputCommands
{
    public LoadProgress(int entry)
    {
        
    }
}
public class CloseAllWindows:InputCommands
{

}
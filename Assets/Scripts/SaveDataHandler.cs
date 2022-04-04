using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataHandler
{
    private static SaveDataHandler instance;
    private const int maxSaves=3;
    private Player.SaveData[] saveDataList;
    private Player player;

    private SaveDataHandler()
    {
        saveDataList=new Player.SaveData[maxSaves];
        player=MonoBehaviour.FindObjectOfType<Player>() as Player;
    }
    public static SaveDataHandler GetInstance()
    {
        if(SaveDataHandler.instance==null)
            SaveDataHandler.instance=new SaveDataHandler();
        return instance;
    }
    public void SaveProgress(int entry)
    {
        saveDataList[entry]=player.GenerateSave();
    }
    public void LoadProgress(int entry)
    {
        if(entry<0)
            player.ResetState();
        else
            player.LoadSave(saveDataList[entry]);
    }
    public bool SlotEmpty(int entry)
    {
        if(saveDataList[entry]==null)
            return true;
        else
            return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public int health;
    public int coins;
    public int corruption;
    public int honour;

    public int level;
    public bool saveExists;
    public string lastSceneName;
    public float xC;
    public float yC;
    public int powerPoints;
    

    public Data (PlayerProperties playerProps)
    {
        health = playerProps.health;
        saveExists = playerProps.saveExisted;
        coins = PlayerProperties.gold;
        corruption = PlayerProperties.corruption;
        honour = PlayerProperties.honour;
        lastSceneName = playerProps.playerScene;
        xC = playerProps.currentPos.x;
        yC = playerProps.currentPos.y;
        powerPoints = playerProps.power;

    }

    
}

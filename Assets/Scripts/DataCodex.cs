using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCodex : MonoBehaviour
{
    public static DataCodex instance;
    public DataCodex GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //COOLDOWN DATA

    public float TelekineticSlashCooldown = 2f;
    public float TelekineticErasureCooldown = 6f;

}

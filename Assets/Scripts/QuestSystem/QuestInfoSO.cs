using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject 
{ 
    [field : SerializeField] public string id { get; private set; }

    [Header("General")]

    public string displayName;

    [Header("Requirements")]

    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]

    public GameObject[] questStepPrefabs;

    [Header("Rewards")]

    public int MadnessReward;

    public int MemoryPiece;

    public GameObject WeaponReward;







    //Make the id the name of the SO 
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
using UnityEngine;
using System.Collections;

public class AssignmentAlgorithm : MonoBehaviour {


    public float m_TimeExecution = 5.0f;
    protected float m_CurrentTimeExecution = 0.0f;

    // only for debug
    public float m_InitialFitness = 0.0f;
    public float m_CurrentFitness = 0.0f;
      
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void assignWeaponToTarget (Team other)
    {

    }

    public virtual void assignWeaponToTarget(GameObject[] Weapons, GameObject[] Targets)
    {

    }
}

using UnityEngine;
using System.Collections;

public class TeamInfo : MonoBehaviour {

    private int m_IDTeam;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int IDTeam
    {
        set { m_IDTeam = value; }
        get { return m_IDTeam; }
    }
}

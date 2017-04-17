using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team : MonoBehaviour {

    // 2nd list entity
    [HideInInspector]
    private List<GameObject> m_Members = new List<GameObject>();

    public int m_ID;

    // team color
    public Color m_TeamColor;  

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        //print("bonjour");
	}



    // search for target
    public void searchforTarget(Team other)
    {
        //print("bonjour");
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3( 200 ,transform.position.y , transform.position.z), 5.0f * Time.deltaTime);
        if( other == null || other == this)
        {
            return;
        }

        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(20, transform.position.y, transform.position.z), 5.0f * Time.deltaTime);

        //transform.position = new Vector3(20, transform.position.y, transform.position.z);
        
        // get all element on team
        foreach (GameObject ally in m_Members)
        {
            //transform.position = new Vector3(20, transform.position.y, transform.position.z);
       
            TargetSystem targetSystem = ally.GetComponent<TargetSystem>();
            Vector3 position = new Vector3(200, ally.transform.position.y,300);
            //ally.transform.position = Vector3.MoveTowards(ally.transform.position, position, 5.0f * Time.deltaTime);
            foreach (GameObject ennemy in other.m_Members)
            {
                //transform.position = new Vector3(20, transform.position.y, transform.position.z);

                //ally.transform.position = Vector3.MoveTowards(ally.transform.position, new Vector3(20, ally.transform.position.y, ally.transform.position.z),5.0f * Time.deltaTime );
                
                if (ennemy.activeSelf == true)
                   targetSystem.Target = ennemy;
                //Vector3 position = new Vector3(200, ally.transform.position.y,300);
                //ally.transform.position = Vector3.MoveTowards(ally.transform.position, position, 5.0f * Time.deltaTime);
               
            }
        }
    }

    public void assignTarget(Team other)
    {
        if (other == null || other == this)
        {
            return;
        }

        // v
        GameObject[] tabEnnemy = new GameObject[other.m_Members.Capacity]; 
        float[] H = new float[other.m_Members.Capacity];
        float[] hl = new float[other.m_Members.Capacity];
        int i = 0;
        foreach (GameObject ennemy in other.m_Members)
        {
            tabEnnemy[i] = ennemy;
            H[i] = ennemy.GetComponent<TankHealth>().Health;
            hl[i] = H[i];
            i++;
        }

        foreach (GameObject ally in m_Members)
        {
            TargetSystem targetSystem = ally.GetComponent<TargetSystem>();
            float minValue = 100000;
            int allocated = -1;
            float dmg = ally.GetComponent<TankShooting>().damage;
            for (int t = 0; t < tabEnnemy.Length; t++ )
            {

               // if (tabEnnemy[t].activeSelf == false)
                 //   continue;


                float reduct =  ( hl[t] - dmg ) / H[t];

                if (hl[t] <= 0)
                    continue;

                if (minValue > reduct)
                {
                    minValue = reduct;
                    allocated = t;
                }
            }
            if (allocated != -1)
            {
                targetSystem.Target = tabEnnemy[allocated];
                hl[allocated] -= dmg;
            }

            /*foreach (GameObject ennemy in other.m_Members)
            {
                targetSystem.Target = ennemy;
                //float dj
                float value = ally.GetComponent<TankShooting>().damage
            }*/
        }
    }

    public void addMember(GameObject member)
    {
        m_Members.Add(member);
    }

    public List<GameObject> Members
    {
        get { return m_Members; }
    }

    public List<GameObject> getActiveMembers()
    {
        List<GameObject> members = new List<GameObject>();
        foreach (GameObject ally in m_Members)
        {
            if (ally.activeSelf)
            {
                members.Add(ally);
            }
        }
        return members;
        
    }
}

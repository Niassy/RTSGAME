using UnityEngine;
using System.Collections;

public class TargetSystem : MonoBehaviour {

    // gameobjecy target
    private GameObject m_Target;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public virtual bool isTargetInRangeShooting()
    {
        if (m_Target == null || m_Target.activeSelf == false)
            return false;

        float dist = Vector3.Distance(transform.position, m_Target.transform.position);
        float range = gameObject.GetComponent<TankShooting>().rangeShooting;

        if (dist <= range )
           return true;
     
        return false;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// 


    ////////////////////// Properties //////////////////


    public GameObject Target
    {
        get {return m_Target;}
        set { m_Target = value; }
    }

}

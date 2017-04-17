using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    private Vector3 m_TargetPosition;
    public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void FixedUpdate()
    {
        if (m_TargetPosition == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position,m_TargetPosition, speed * Time.deltaTime);
    
    }

    ///////////////// Properties ///////////////

    public Vector3 TargetPosition
    {
        set { m_TargetPosition = value; }
        get { return m_TargetPosition; }
    }


}

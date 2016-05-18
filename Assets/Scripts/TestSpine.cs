using UnityEngine;
using System.Collections;

public class TestSpine : MonoBehaviour {

    private SkeletonAnimation skeanim;

	// Use this for initialization
	void Start ()
    {
        skeanim = GetComponent<SkeletonAnimation>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            skeanim.loop = false;
            skeanim.AnimationName = "trip";
            
        }
    }
}

using PedometerU.Tests;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    public Transform p1, p2, p3, p4, p5, p6, p7;
    public Transform pp1, pp2, pp3, pp4, pp5, pp6, pp7;
    public bool move;

    private StepCounter SC;

    private void Awake()
    {
        move = false;
        SC = FindObjectOfType<StepCounter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (move == true)
        {
            #region Moving Background

            p6.localPosition += new Vector3(-0.005f, 0, 0);
            pp6.localPosition += new Vector3(-0.005f, 0, 0);

            p7.localPosition += new Vector3(-0.01f, 0, 0);
            pp7.localPosition += new Vector3(-0.01f, 0, 0);
            #endregion

            #region B-Left

            if (p6.localPosition.x <= -7.53f)
            {
                p6.localPosition = new Vector3(25f, -0.13f, 0);
            }

            if (p7.localPosition.x <= -7.53f)
            {
                p7.localPosition = new Vector3(25f, 0.22f, 0);
            }
            #endregion

            #region B-Right


            if (pp6.localPosition.x <= -25.415f)
            {
                pp6.localPosition = new Vector3(7.1f, -0.19f, 0);
            }

            if (pp7.localPosition.x <= -25.415f)
            {
                pp7.localPosition = new Vector3(7.1f, 0.16f, 0);
            }
            #endregion

        }



    }
    
}

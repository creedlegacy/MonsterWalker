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
            p1.localPosition += new Vector3(-0.005f, 0, 0);
            pp1.localPosition += new Vector3(-0.005f, 0, 0);

            p2.localPosition += new Vector3(-0.003f, 0, 0);
            pp2.localPosition += new Vector3(-0.003f, 0, 0);

            p3.localPosition += new Vector3(-0.002f, 0, 0);
            pp3.localPosition += new Vector3(-0.002f, 0, 0);

            p4.localPosition += new Vector3(-0.001f, 0, 0);
            pp4.localPosition += new Vector3(-0.001f, 0, 0);

            p5.localPosition += new Vector3(-0.0015f, 0, 0);
            pp5.localPosition += new Vector3(-0.0015f, 0, 0);

            p6.localPosition += new Vector3(-0.001f, 0, 0);
            pp6.localPosition += new Vector3(-0.001f, 0, 0);

            p7.localPosition += new Vector3(-0.003f, 0, 0);
            pp7.localPosition += new Vector3(-0.003f, 0, 0);
            #endregion

            #region B-Left
            if (p1.localPosition.x <= -8.09f)
            {
                p1.localPosition = new Vector3(27.4f, -0.19f, 0);
            }

            if (p2.localPosition.x <= -8.09f)
            {
                p2.localPosition = new Vector3(27.4f, -0.19f, 0);
            }

            if (p3.localPosition.x <= -8.09f)
            {
                p3.localPosition = new Vector3(27.4f, -0.19f, 0);
            }

            if (p4.localPosition.x <= -8.09f)
            {
                p4.localPosition = new Vector3(27.4f, -0.19f, 0);
            }

            if (p5.localPosition.x <= -8.09f)
            {
                p5.localPosition = new Vector3(27.4f, -0.19f, 0);
            }

            if (p6.localPosition.x <= -8.09f)
            {
                p6.localPosition = new Vector3(27.4f, -0.19f, 0);
            }

            if (p7.localPosition.x <= -8.09f)
            {
                p7.localPosition = new Vector3(27.4f, -0.19f, 0);
            }
            #endregion

            #region B-Right
            if (pp1.localPosition.x <= -26.1f)
            {
                pp1.localPosition = new Vector3(9.4f, -0.19f, 0);
            }

            if (pp2.localPosition.x <= -26.1f)
            {
                pp2.localPosition = new Vector3(9.4f, -0.19f, 0);
            }

            if (pp3.localPosition.x <= -26.1f)
            {
                pp3.localPosition = new Vector3(9.4f, -0.19f, 0);
            }

            if (pp4.localPosition.x <= -26.1f)
            {
                pp4.localPosition = new Vector3(9.4f, -0.19f, 0);
            }

            if (pp5.localPosition.x <= -26.1f)
            {
                pp5.localPosition = new Vector3(9.4f, -0.19f, 0);
            }

            if (pp6.localPosition.x <= -26.1f)
            {
                pp6.localPosition = new Vector3(9.4f, -0.19f, 0);
            }

            if (pp7.localPosition.x <= -26.1f)
            {
                p7.localPosition = new Vector3(9.4f, -0.19f, 0);
            }
            #endregion

        }


    }



}

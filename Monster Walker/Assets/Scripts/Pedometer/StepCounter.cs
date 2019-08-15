namespace PedometerU.Tests {

    using UnityEngine;
    using UnityEngine.UI;

    public class StepCounter : MonoBehaviour {

        public Text stepText, distanceText, indict, stepGoal, lon, lat, mileTitle, mileStep;
        public Pedometer pedometer;
        public double feet;
        public float multiplier;
        public int step1, milestone, mileNum;
        private bool notwalking;
        private Monster M;
        private ParallaxBackground pb;

        public void Awake()
        {
            notwalking = false;
            mileNum = 1;
            milestone = 500;
            multiplier = 1.0f;
            M = FindObjectOfType<Monster>();
            pb = FindObjectOfType<ParallaxBackground>();
        }

        private void Start () {
            // Create a new pedometer
            // Reset UI
            mileStep.text = milestone.ToString();
            pedometer = new Pedometer(OnStep);
            OnStep(0, 0);
        }

        private void OnStep (int steps, double distance) {
            // Display the values // Distance in feet
            if (steps > 0)
            {
                M.An.SetBool("IsWalking", true);
                pb.move = true;
            }
            step1 = steps;

            #region multiplier

            
            if (step1 >= milestone)
            {
                if (step1 <= 4999)
                {
                    mileNum += 1;
                    mileTitle.text = "Milestone #" + mileNum.ToString();
                    milestone += 500;
                    multiplier += 0.1f;
                    mileStep.text = milestone.ToString();
                }
                else if (step1 >= 5000)
                {
                    multiplier = 2.0f;
                    mileTitle.text = "Final Milestone";
                    mileStep.text = "Reach!";
                }
            }

           
            #endregion

            stepText.text = steps.ToString();
            M.idletime = 0;
            feet = distance/ 3280.8f;
            distanceText.text = feet.ToString("#.##") + " km";
        }

        public void OnDisable () {
            // Release the pedometer
            pedometer.Dispose();
            pedometer = null;
        }

        public void Update()
        {
           
        }

    }
}
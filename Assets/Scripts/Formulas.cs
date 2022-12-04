using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Formulas
{
    class Formulas : MonoBehaviour
    {
        static private double bulletBeginVelocity;
        static private double bulletBeginVelocityX;
        static private double bulletBeginVelocityY;
        static private double bulletBeginVelocityZ;
        static private double carVelocity;
        static private double betha;
        static private double alpha;
        static private double g;
        static private double bulletAngle;
        static private double bulletMass;

        public Slider bulletInitialVelocity;
        public Slider vehicleVelocity;
        public Slider beth;
        //public Slider aplh;
        //public float aplhaAngle;
        //public float Bulletang;
        public Transform cameraToShoot;
        private UnityAction<object> onShoot;
        private UnityAction<object> onSimulationStateChange;

        public static double BulletBeginVelocityX { get => bulletBeginVelocityX; set {
                bulletBeginVelocityX = value;
                Debug.Log("New initial bullet X speed value = " + bulletBeginVelocityX);
            } }
        public static double BulletBeginVelocityY { get => bulletBeginVelocityY; set {
                bulletBeginVelocityY = value;
                Debug.Log("New initial bullet Y speed value = " + bulletBeginVelocityY);
            } }
        public static double CarVelocity { get => carVelocity; set {
                carVelocity = value;
                Debug.Log("New car velocity value = " + carVelocity);
            } }
        public static double Betha { get => betha; set {
                betha = value;
                Debug.Log("New betha value = " + betha);
            } }

        public static double Alpha { get => alpha; 
            set { 
                alpha = value;
                Debug.Log("New alpha value = " + alpha);
            }
        }

        public static double G { get => g;
            set { 
                g = value;
                Debug.Log("New g value = " + g);
            } 
        }

        public static double BulletBeginVelocity { get => bulletBeginVelocity; 
            set { 
                bulletBeginVelocity = value;
                Debug.Log("New bullet initial velocity value = " + bulletBeginVelocity);
            } 
        }

        private void Awake()
        {
            onShoot = new UnityAction<object>(OnShoot);
            onSimulationStateChange = new UnityAction<object>(OnSimulationStateChange);
        }

        private void OnEnable()
        {
            EventManager.StartListening("Shoot", onShoot);
            EventManager.StartListening("SimulationState", onSimulationStateChange);
        }

        private void OnDisable()
        {
            EventManager.StopListening("Shoot", onShoot);
            EventManager.StopListening("SimulationState", onSimulationStateChange);
        }

        public static void Init()
        {
            //BulletBeginVelocity = Math.Sqrt( ( ( BulletBeginVelocityX * BulletBeginVelocityX ) + ( BulletBeginVelocityY * BulletBeginVelocityY ) + ( CarVelocity * CarVelocity ) ) );
            //Betha = 1;
            //Alpha = 45;

            BulletBeginVelocityY = BulletBeginVelocity * Mathf.Sin((float)ConvertToRadians(Alpha));
            BulletBeginVelocityX = BulletBeginVelocity * Mathf.Cos((float)ConvertToRadians(Alpha));

            bulletBeginVelocityZ = BulletBeginVelocityX * Mathf.Sin((float)ConvertToRadians(bulletAngle));// + carVelocity;
            BulletBeginVelocityX = BulletBeginVelocityX * Mathf.Cos((float)ConvertToRadians(bulletAngle));

            bulletMass = 1;

            G = 9.81;
        }

        public static Vector3 GetPosition(double time)
        {
            Vector3 position = new Vector3();

            position.x = (float)GetPositionX(time);
            position.y = (float)GetPositionY(time);
            position.z = (float)GetPositionZ(time);

            return position;
        }

        public static double GetPositionX(double time)
        {
            double result = 0;

            //https://matematyka.pl/viewtopic.php?t=157528
            result = (BulletBeginVelocityX / Betha ) * ( 1 - Math.Exp( ( -Betha * time ) ) );

            return result;
        }

        public static double GetPositionY(double time)
        {
            double result = 0;

            result = ( ( BulletBeginVelocityY / Betha ) + ( G / ( Betha * Betha ) ) ) * ( 1 - Math.Exp( ( -Betha * time ) ) ) - ( ( G * time ) / Betha );

            return result;
        }

        public static double GetPositionZ(double time)
        {
            double result = 0;

            result = (bulletBeginVelocityZ / Betha ) * ( 1 - Math.Exp( ( -Betha * time ) ) );
            return result;
        }

        public static Vector3 GetVelocity(double time)
        {
            Vector3 velocity = new Vector3();

            velocity.x = (float)GetVelocityX(time);
            velocity.y = (float)GetVelocityY(time);
            velocity.z = (float)GetVelocityZ(time);

            return velocity;
        }

        public static double ConvertToRadians(double angle)
        {
            double result = 0;

            result = ( Math.PI / 180 ) * angle;

            return result;
        }

        public static double GetVelocityZ(double time)
        {
            double result = 0;

            result = BulletBeginVelocity * Math.Exp( ( -Betha * time ) ) * Math.Cos( ConvertToRadians( Alpha ) );

            return result;
        }

        public static double GetVelocityY(double time)
        {
            double result = 0;

            result = ( ( ( BulletBeginVelocity * Math.Sin( ConvertToRadians( Alpha ) ) ) + ( G / Betha ) ) * Math.Exp( ( -Betha * time ) ) ) -  ( G / Betha );

            return result;
        }

        public static double GetVelocityX(double time)
        {
            double result = 0;

            result = CarVelocity * Math.Exp( ( -Betha * time ) );

            return result;
        }

        public static Vector3 GetAcceleration(double time)
        {
            Vector3 velocity = new Vector3();

            velocity.x = (float)GetAccelerationX(time);
            velocity.y = (float)GetAccelerationY(time);
            velocity.z = (float)GetAccelerationZ(time);

            return velocity;
        }

        public static double GetAccelerationX(double time)
        {
            double result = 0;

            result = -Betha * GetVelocityX(time);

            return result;
        }

        public static double GetAccelerationY(double time)
        {
            double result = 0;

            result = (-G) - ( Betha * GetVelocityY(time) );

            return result;
        }

        public static double GetAccelerationZ(double time)
        {
            double result = 0;

            result = -Betha * GetVelocityZ(time);

            return result;
        }

        private void OnSimulationStateChange(object data)
        {
            if ((bool) data)
            {

                BulletBeginVelocity = bulletInitialVelocity.value;
                CarVelocity = vehicleVelocity.value;
                //Alpha = aplh.value;
                
                Betha = beth.value;
                //bulletAngle = Bulletang;

            }

        }

        private void OnShoot(object data)
        {
            Alpha = -cameraToShoot.rotation.eulerAngles.x;
            bulletAngle = cameraToShoot.rotation.eulerAngles.y;

            Init();
        }

    }
}

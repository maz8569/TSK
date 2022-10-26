using System;
using System.Numerics;

namespace Formulas
{
    class Formule
    {
        static private double bulletBeginVelocity;
        static private double bulletBeginVelocityX;
        static private double bulletBeginVelocityY;
        static private double carVelocity;
        static private double betha;
        static private double alpha;
        static private double g;
        static private double bulletMass;

        public static void Init()
        {
            bulletBeginVelocityX = 1;
            bulletBeginVelocityY = 1;
            carVelocity = 1;
            bulletBeginVelocity = Math.Sqrt( ( ( bulletBeginVelocityX * bulletBeginVelocityX ) + ( bulletBeginVelocityY * bulletBeginVelocityY ) + ( carVelocity * carVelocity ) ) );

            betha = 1;
            alpha = 45;
            bulletMass = 1;

            g = 9.81;
        }

        public static Vector3 GetPosition(double time)
        {
            Vector3 position = new Vector3();

            position.X = (float)GetPositionX(time);
            position.Y = (float)GetPositionY(time);
            position.Z = (float)GetPositionZ(time);

            return position;
        }

        public static double GetPositionX(double time)
        {
            double result = 0;

            //https://matematyka.pl/viewtopic.php?t=157528
            result = (bulletBeginVelocityX / betha ) * ( 1 - Math.Exp( ( -betha * time ) ) );

            return result;
        }

        public static double GetPositionY(double time)
        {
            double result = 0;

            result = ( ( bulletBeginVelocityY / betha ) + ( g / ( betha * betha ) ) ) * ( 1 - Math.Exp( ( -betha * time ) ) ) - ( ( g * time ) / betha );

            return result;
        }

        public static double GetPositionZ(double time)
        {
            double result = 0;

            result = (carVelocity / betha ) * ( 1 - Math.Exp( ( -betha * time ) ) );
            return result;
        }

        public static Vector3 GetVelocity(double time)
        {
            Vector3 velocity = new Vector3();

            velocity.X = (float)GetVelocityX(time);
            velocity.Y = (float)GetVelocityY(time);
            velocity.Z = (float)GetVelocityZ(time);

            return velocity;
        }

        public static double ConvertToRadians(double angle)
        {
            double result = 0;

            result = ( Math.PI / 180 ) * angle;

            return result;
        }

        public static double GetVelocityX(double time)
        {
            double result = 0;

            result = bulletBeginVelocity * Math.Exp( ( -betha * time ) ) * Math.Cos( ConvertToRadians( alpha ) );

            return result;
        }

        public static double GetVelocityY(double time)
        {
            double result = 0;

            result = ( ( ( bulletBeginVelocity * Math.Cos( ConvertToRadians( alpha ) ) ) + ( g / betha ) ) * Math.Exp( ( -betha * time ) ) ) -  ( g / betha );

            return result;
        }

        public static double GetVelocityZ(double time)
        {
            double result = 0;

            result = carVelocity * Math.Exp( ( -betha * time ) );

            return result;
        }

        public static Vector3 GetAcceleration(double time)
        {
            Vector3 velocity = new Vector3();

            velocity.X = (float)GetAccelerationX(time);
            velocity.Y = (float)GetAccelerationY(time);
            velocity.Z = (float)GetAccelerationZ(time);

            return velocity;
        }

        public static double GetAccelerationX(double time)
        {
            double result = 0;

            result = -betha * GetVelocityX(time);

            return result;
        }

        public static double GetAccelerationY(double time)
        {
            double result = 0;

            result = (-g) - ( betha * GetVelocityY(time) );

            return result;
        }

        public static double GetAccelerationZ(double time)
        {
            double result = 0;

            result = -betha * GetVelocityZ(time);

            return result;
        }
        
    }
}

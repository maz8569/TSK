using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Statistics
{
    private class Stat
    {
        public float mTime;
        public Vector3 mPosition;
        public Vector3 mVelocity;
        public Vector3 mAcceleration;

        public Stat(float time, Vector3 position, Vector3 velocity, Vector3 acceleration)
        {
            mTime = time;
            mPosition = position;
            mVelocity = velocity;
            mAcceleration = acceleration;
        }
    }

    private List<Stat> stats;

    public Statistics()
    {
        stats = new List<Stat>();
        stats.Clear();
    }

    public void Save()
    {
        string text = "Time;Px;Py;Pz;Vx;Vy;Vz;V;Ax;Ay;Az;A\n";
        foreach(Stat stat in stats)
        {
            double velocity = Math.Sqrt((
                            (stat.mVelocity.x * stat.mVelocity.x) + 
                            (stat.mVelocity.y * stat.mVelocity.y) +
                            (stat.mVelocity.z * stat.mVelocity.z)
                            ));
            double acceleration = Math.Sqrt((
                            (stat.mAcceleration.x * stat.mAcceleration.x) + 
                            (stat.mAcceleration.y * stat.mAcceleration.y) +
                            (stat.mAcceleration.z * stat.mAcceleration.z)
                            ));

            text += stat.mTime + ";"
                    + stat.mPosition.x + ";"
                    + stat.mPosition.y + ";"
                    + stat.mPosition.z + ";"
                    + stat.mVelocity.x + ";"
                    + stat.mVelocity.y + ";"
                    + stat.mVelocity.z + ";"
                    + velocity + ";"
                    + stat.mAcceleration.x + ";"
                    + stat.mAcceleration.y + ";"
                    + stat.mAcceleration.z + ";"
                    + acceleration + ""
                    + "\n";
        }
        SaveToFile("WriteText.csv", text);
    }

    public void SaveToFile(string file, string text)
    {
        File.WriteAllTextAsync(file, text);
    }

    public void Add(float time, Vector3 position, Vector3 velocity, Vector3 acceleration)
    {
        stats.Add(new Stat(time, position, velocity, acceleration));
    }
}

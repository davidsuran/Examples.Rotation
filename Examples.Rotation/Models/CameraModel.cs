using Microsoft.Xna.Framework;
using System;

namespace CameraControllerDemo.Models
{
    internal class CameraModel : ActorModel
    {
        public PositionSettings PositionSettings { get; private set; }

        public Matrix CameraRotationMatrix { get; set; }

        public CameraModel(Vector3 position, Vector3 target) : base(position, target)
        {
            CameraRotationMatrix = Matrix.Identity;
            PositionSettings = new PositionSettings()
            {
                TargetPositionOffset = new Vector3(0, 25, -350)
            };
        }

    }

    [System.Serializable]
    internal class PositionSettings
    {
        public Vector3 TargetPositionOffset = new Vector3(0, 0, 0);
        public float looksSmooth = 100;
        public float distanceFromTarget = -8;
        public float zoomSmooth = 5f;
        public float zoomStep = 2;
        public float zoomAcceleration = -0.5f;
        public float maxZoom = -2;
        public float minZoom = -15;
        public bool smoothFollow = true;
        public float smooth = 0.5f;

        //[HideInInspector]
        public float newDistance = -8; //set by zoom input
                                       //[HideInInspector]
        public float adjustmentDistance = -8;
    }
}

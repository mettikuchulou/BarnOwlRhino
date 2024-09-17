using BarnOwlRhino.Models;
using Rhino;
using Rhino.Display;
using Rhino.Geometry;
using System;
using System.Windows.Input;

namespace BarnOwlRhino.ViewModels
{
    public class CameraNavigation : BaseViewModel
    {
        public RhinoView View;
        public RhinoViewport Viewport;

        private Point3d cameraLocation;
        private Vector3d cameraDirection;
        private Point3d newCameraLocation;

        private bool isViewportPerpective;

        public double truckSensitivity = 0.5;
        public double rotateSensitivity = 0.01;
        public double dollySenitivity = 0.5;

        public CameraNavigation()
        {
            RhinoApp.Idle += RhinoApp_Idle;
        }

        private void RhinoApp_Idle(object sender, EventArgs e)
        {
            XInput.UpdateState();
            UpdateCamera();
        }

        private void UpdateCamera()
        {
            if (!IsConnected) return;

            View = RhinoDoc.ActiveDoc.Views.ActiveView;
            Viewport = View.ActiveViewport;

            cameraDirection = Viewport.CameraZ;
            cameraLocation = Viewport.CameraLocation;

            isViewportPerpective = Viewport.IsPerspectiveProjection;

            Truck();
            Dolly();
            Rotate();

            View.Redraw();
        }

        public void Truck()
        {
            if (isViewportPerpective)
            {
                // Calculate new camera location for trucking
                Vector3d cameraTruckY = new Vector3d(cameraDirection.X, cameraDirection.Y, 0);
                Vector3d cameraTruckX = Viewport.CameraX;
                Vector3d translationYVector = cameraTruckY * LeftThumbY * truckSensitivity;
                Vector3d translationXVector = cameraTruckX * LeftThumbX * truckSensitivity;
                newCameraLocation = cameraLocation + translationXVector - translationYVector;

                Viewport.SetCameraLocation(newCameraLocation, true);
            }
            else
            {
                Pan2D();
            }
        }

        public void Dolly()
        {
            if (isViewportPerpective)
            {
                // Calculate new camera location for dolling
                if (LeftTrigger > 0)
                {
                    Vector3d translationNVector = cameraDirection * LeftTrigger * dollySenitivity;
                    newCameraLocation = cameraLocation + translationNVector;
                }
                if (RightTrigger > 0)
                {
                    Vector3d translationNVector = cameraDirection * RightTrigger * dollySenitivity;
                    newCameraLocation = cameraLocation - translationNVector;
                }

                Viewport.SetCameraLocation(newCameraLocation, true);
            }
            else
            {
                Pan2D();
            }
        }

        public void Rotate()
        {
            if (isViewportPerpective)
            {
                // Rotate camera around origin point
                Point3d rotateCenter = Point3d.Origin;
                double rotateAngleX = rotateSensitivity * RightThumbX;
                double rotateAngleY = rotateSensitivity * RightThumbY;
                Viewport.Rotate(rotateAngleX, Vector3d.ZAxis, rotateCenter);
                Viewport.Rotate(rotateAngleY, Viewport.CameraX, rotateCenter);
            }
            else
            {
                Pan2D();
            }
        }

        private void Pan2D()
        {
            // Calculate camera position in parallel projection views
            Vector3d cameraPanX = Viewport.CameraX;
            Vector3d cameraPanY = Viewport.CameraY;
            Vector3d transform2dX = cameraPanX * LeftThumbX * truckSensitivity;
            Vector3d transform2dY = cameraPanY * LeftThumbY * truckSensitivity;
            Point3d newCamera2dLocation = cameraLocation + transform2dX + transform2dY;

            Viewport.SetCameraLocation(newCamera2dLocation, true);
        }
    }
}

using BarnOwlRhino.Models;
using BarnOwlRhino.ViewModels;
using Rhino;
using System;

namespace BarnOwlRhino
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class BarnOwlRhinoPlugin : Rhino.PlugIns.DigitizerPlugIn
    {
        CameraNavigation CameraNavigation = new CameraNavigation();
        PopUpMenuViewModel PopUpMenuViewModel = new PopUpMenuViewModel();


        public BarnOwlRhinoPlugin()
        {
            Instance = this;
        }

        ///<summary>Gets the only instance of the BarnOwlRhinoPlugin plug-in.</summary>
        public static BarnOwlRhinoPlugin Instance { get; private set; }

        ///<summary>
        /// Defines the behavior in response to a request of a user to either enable or disable
        /// input from the digitizer.
        /// It is called by Rhino. If enable is true and EnableDigitizer() returns false,
        /// then Rhino will not calibrate the digitizer.
        ///</summary>
        ///<param name="enable">If true, enable the digitizer. If false, disable the digitizer.</param>
        ///<returns>You should return true if the digitizer should be calibrated. Otherwise, false.</returns>
        protected override bool EnableDigitizer(bool enable)
        {
            throw new NotImplementedException("EnableDigitizer has no defined behavior in the BarnOwlRhino.BarnOwlRhinoPlugin class.");
        }

        ///<summary>
        /// Gets the unit system in which the digitizer works.
        /// passes points to SendPoint().  Rhino uses this value when it calibrates a digitizer.
        /// This unit system must not change.
        /// </summary>
        protected override UnitSystem DigitizerUnitSystem
        {
            get { throw new NotImplementedException("DigitizerUnitSystem is not implemented in the BarnOwlRhino.BarnOwlRhinoPlugin class."); }
        }

        /// <summary>
        /// Gets the point tolerance, or the distance the digitizer must move (in digitizer
        /// coordinates) for a new point to be considered real rather than noise. Small
        /// desktop digitizer arms have values like 0.001 inches and 0.01 millimeters.
        /// This value should never be smaller than the accuracy of the digitizing device.
        /// </summary>
        protected override double PointTolerance
        {
            get { throw new NotImplementedException("PointTolerance is unknown in the BarnOwlRhino.BarnOwlRhinoPlugin class."); }
        }
        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.
    }
}
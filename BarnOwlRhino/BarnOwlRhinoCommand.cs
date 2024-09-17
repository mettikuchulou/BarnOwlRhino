using Rhino;
using Rhino.Commands;
using RhinoWindows;


namespace BarnOwlRhino
{
    public class BarnOwlRhinoCommand : Command
    {
        public BarnOwlRhinoCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static BarnOwlRhinoCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "BarnOwlRhinoCommand";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            var menu = new Views.PopupMenu();
            menu.ShowSemiModal(RhinoApp.MainWindowHandle());
            return Result.Success;
        }

    }
}

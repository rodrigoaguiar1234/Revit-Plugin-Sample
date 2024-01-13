using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Windows.Forms;
using Autodesk.Revit.Creation;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Linq.Expressions;



namespace myFirstPlugin
{
    /// <summary>
    /// Implements revit add-in IExternalCommand interface
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Command : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            Autodesk.Revit.DB.Document doc = uiApp.ActiveUIDocument.Document;

            MessageBox.Show("Plugin Created");

            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("Jacobian Plugin GRID");
                // Create the geometry line which the grid locates
                XYZ start = new XYZ(0, 0, 0);
                XYZ end = new XYZ(30, 30, 0);
                Line geomLine = Line.CreateBound(start, end);
                string Nome = "RAA";
                // Create a grid using the geometry line
                Autodesk.Revit.DB.Grid lineGrid = Autodesk.Revit.DB.Grid.Create(doc, geomLine);


                if (null == lineGrid || lineGrid.Name == Nome)
                {
                    throw new Exception("Create a new straight grid failed.");
                }

                try
                {
                    // Modify the name of the created grid
                    lineGrid.Name = Nome;
                }
                catch
                {
                    MessageBox.Show("Grid Already Created");
                }


                transaction.Commit();
            }
            return Result.Succeeded;
        }



    }


}

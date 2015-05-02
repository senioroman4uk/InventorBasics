using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inventor;

namespace InventorBasics
{
    public partial class Form1 : Form
    {
        Inventor.Application inventor = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    inventor = System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application") as Inventor.Application; ;
                }
                catch (Exception)
                {
                    Type inventorType = System.Type.GetTypeFromProgID("Inventor.Application");
                    inventor = System.Activator.CreateInstance(inventorType) as Inventor.Application;
                    inventor.Visible = true;
                }                
            }
            catch (Exception)
            {
                MessageBox.Show("Error connecting to inventor", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (inventor == null || string.IsNullOrWhiteSpace(CaptionTB.Text))
            {
                if (inventor == null)
                    MessageBox.Show("No inventor instance detected", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (inventor.ActiveView != null)
                inventor.ActiveView.Caption = CaptionTB.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int width, height;
            if (!int.TryParse(widthTB.Text, out width) || !int.TryParse(HeightTB.Text, out height) || inventor == null)
            {
                if (inventor == null)
                    MessageBox.Show("No inventor instance detected", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (inventor.ActiveView != null)
            {
                inventor.ActiveView.Width = width;
                inventor.ActiveView.Height = height;
            }
        }

        private void CreatePart_Click(object sender, EventArgs e)
        {
            if (inventor == null)
            {
                MessageBox.Show("No inventor instance detected", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PartDocument doc = inventor.Documents.Add(DocumentTypeEnum.kPartDocumentObject, null , true) as PartDocument;
            doc.PropertySets["{F29F85E0-4FF9-1068-AB91-08002B27B3D9}"]["Author"].Value = "Vladyslav Romanchuk";
           
            //User-defined property
            doc.PropertySets["{D5CDD505-2E9C-101B-9397-08002B2CF9AE}"].Add("Parts R Us", "Supplier");
            PartComponentDefinition partDefinition = (PartComponentDefinition)doc.ComponentDefinition;
            
            // Create a 2D sketch on the X-Y plane.
            PlanarSketch sketch1 = (PlanarSketch)partDefinition.Sketches.Add(partDefinition.WorkPlanes[3]);
            TransientGeometry tg = inventor.TransientGeometry;
            Point2d[] points = new Point2d[5] { tg.CreatePoint2d(0, 0), tg.CreatePoint2d(0, 20), tg.CreatePoint2d(20, 20), tg.CreatePoint2d(20, -10), tg.CreatePoint2d(10, -10) };
            SketchLine[] lines = new SketchLine[5];
            lines[0] = sketch1.SketchLines.AddByTwoPoints(points[0], points[1]);
            for (int i = 1; i < lines.Length - 1; i++)
                lines[i] = sketch1.SketchLines.AddByTwoPoints(lines[i-1].EndSketchPoint, points[i + 1]);
            sketch1.SketchArcs.AddByCenterStartEndPoint(tg.CreatePoint2d(10, 0), lines[3].EndSketchPoint, lines[0].StartSketchPoint, false);
            
            //Extrude
            Profile profile = sketch1.Profiles.AddForSolid();
            ExtrudeDefinition extrudeDefinition = partDefinition.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kNewBodyOperation);
            extrudeDefinition.SetDistanceExtent(6, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
            ExtrudeFeature extrude = (ExtrudeFeature)partDefinition.Features.ExtrudeFeatures.Add(extrudeDefinition);
            
            //second scatch
            Face topCap = extrude.EndFaces[1];
            sketch1 = partDefinition.Sketches.Add(topCap, false);

            Point2d center = sketch1.ModelToSketchSpace(tg.CreatePoint(2.5, 1.5, 1.5));
            SketchCircle Circle = sketch1.SketchCircles.AddByCenterRadius(center, 1);
            profile = sketch1.Profiles.AddForSolid(true, null, null);
            extrudeDefinition =  partDefinition.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kJoinOperation);
            extrudeDefinition.SetDistanceExtent(4, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
            extrude = (ExtrudeFeature)partDefinition.Features.ExtrudeFeatures.Add(extrudeDefinition);
            Edges cylinderEdges = extrude.SideFaces[1].Edges;
            EdgeCollection filletEdges = inventor.TransientObjects.CreateEdgeCollection(null);
            //foreach (var el in cylinderEdges)
            //    filletEdges.Add(el);
            filletEdges.Add(cylinderEdges[2]);
            //adding fillet
            partDefinition.Features.FilletFeatures.AddSimple(filletEdges, 0.25, false, false, false, false, false, true);
            //doc.SaveAs("D:\\SaveTest2.ipt", false);
        }

        private void CalculateHoleB_Click(object sender, EventArgs e)
        {
            if (inventor == null)
            {
                MessageBox.Show("No inventor instance detected", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AssemblyDocument doc = inventor.ActiveDocument as AssemblyDocument;
            if(doc == null || doc.ComponentDefinition == null)
                return;
            
            AssemblyComponentDefinition partComponentDefinition = doc.ComponentDefinition;
            var wall = partComponentDefinition.Occurrences.get_ItemByName("wall:1");
            var weight = partComponentDefinition.Occurrences.get_ItemByName("weight:1");
            PartComponentDefinition weightDefinition = weight.Definition as PartComponentDefinition;
            PartComponentDefinition wallDefinition = wall.Definition as PartComponentDefinition;
            SurfaceBody surfaceBody2 = inventor.TransientBRep.Copy(weight.SurfaceBodies[1]);
            Vector rotationVector = inventor.TransientGeometry.CreateVector(0, 1, 0);
            Inventor.Point point = inventor.TransientGeometry.CreatePoint(0, 0, 0);

            
            
            var collection = inventor.TransientObjects.CreateObjectCollection();
            for (int i = 1; i <= 120; i++)
            {
                Matrix wallMatrix = wall.Transformation, weigthMatrix = weight.Transformation;
                weigthMatrix.Invert();
                wallMatrix.PreMultiplyBy(weigthMatrix);
                SurfaceBody surfaceBody = inventor.TransientBRep.Copy(weight.SurfaceBodies[1]);
                wallMatrix.SetToRotation(i * Math.PI / 180, rotationVector, point);
                inventor.TransientBRep.Transform(surfaceBody, wallMatrix);    
                inventor.TransientBRep.DoBoolean(surfaceBody2, surfaceBody, BooleanTypeEnum.kBooleanTypeUnion);
            }

            NonParametricBaseFeatureDefinition featureDefinition = weightDefinition.Features.NonParametricBaseFeatures.CreateDefinition();
            featureDefinition.OutputType = BaseFeatureOutputTypeEnum.kSolidOutputType;

            collection.Add(surfaceBody2);
            featureDefinition.BRepEntities = collection;
            NonParametricBaseFeature baseFeature = weightDefinition.Features.NonParametricBaseFeatures.AddByDefinition(featureDefinition);
            
            //CombineFeature combineFeature = doc.Features.CombineFeatures.Add(wall.SurfaceBodies[1], collection, PartFeatureOperationEnum.kCutOperation);
        }
    }
}

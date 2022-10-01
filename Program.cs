#region Copyright

//      .NET Sample
//
//      Copyright (c) 2012 by Autodesk, Inc.
//
//      Permission to use, copy, modify, and distribute this software
//      for any purpose and without fee is hereby granted, provided
//      that the above copyright notice appears in all copies and
//      that both that copyright notice and the limited warranty and
//      restricted rights notice below appear in all supporting
//      documentation.
//
//      AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
//      AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
//      MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
//      DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
//      UNINTERRUPTED OR ERROR FREE.
//
//      Use, duplication, or disclosure by the U.S. Government is subject to
//      restrictions set forth in FAR 52.227-19 (Commercial Computer
//      Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
//      (Rights in Technical Data and Computer Software), as applicable.
//

#endregion

#region imports

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Media.Effects;
using UiViewModels.Actions;
using Autodesk.Max;
using Autodesk.Max.Wrappers;
using ManagedServices;
using Microsoft.Win32;

#endregion

namespace AppendXRefsToAss {
    public class AdnMenuSampleStrings {
        // just convienence for globals strings. Normally strings would probably be loaded from resources
        public static string actionText01 = "Append XRefs to ASS";
        public static string actionCategory = "Export";
    }

    /// <summary>
    /// CuiActionCommandAdapter setup
    /// </summary>
    public class AppendXRefsToAssCommand : CuiActionCommandAdapter {
        //Obj Number
        string _objectData = ";OBJECT {0}\r\n" +
                             "\"MESH\"\r\n" +
                             // Absolute path to folder
                             "\"{1}\"\r\n" +
                             //Object name
                             "\"{2}\"\r\n36\r\n-1.0000000000	-1.0000000000	1.0000000000\r\n-0.5773502588	-0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.0000000000	0.0000000000\r\n\r\n-1.0000000000	1.0000000000	-1.0000000000\r\n-0.5773502588	0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.2500000000	0.0000000000\r\n\r\n-1.0000000000	-1.0000000000	-1.0000000000\r\n-0.5773502588	-0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.0000000000	0.0000000000\r\n\r\n-1.0000000000	1.0000000000	1.0000000000\r\n-0.5773502588	0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.2500000000	0.0000000000\r\n\r\n1.0000000000	1.0000000000	-1.0000000000\r\n0.5773502588	0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.5000000000	0.0000000000\r\n\r\n-1.0000000000	1.0000000000	-1.0000000000\r\n-0.5773502588	0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.2500000000	0.0000000000\r\n\r\n1.0000000000	1.0000000000	1.0000000000\r\n0.5773502588	0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.5000000000	0.0000000000\r\n\r\n1.0000000000	-1.0000000000	-1.0000000000\r\n0.5773502588	-0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.7500000000	0.0000000000\r\n\r\n1.0000000000	1.0000000000	-1.0000000000\r\n0.5773502588	0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.5000000000	0.0000000000\r\n\r\n1.0000000000	-1.0000000000	1.0000000000\r\n0.5773502588	-0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.7500000000	0.0000000000\r\n\r\n-1.0000000000	-1.0000000000	-1.0000000000\r\n-0.5773502588	-0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	1.0000000000	0.0000000000\r\n\r\n1.0000000000	-1.0000000000	-1.0000000000\r\n0.5773502588	-0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.7500000000	0.0000000000\r\n\r\n1.0000000000	1.0000000000	-1.0000000000\r\n0.5773502588	0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.5000000000	0.0000000000\r\n\r\n-1.0000000000	-1.0000000000	-1.0000000000\r\n-0.5773502588	-0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.1250000000	0.7500000000	0.0000000000\r\n\r\n-1.0000000000	1.0000000000	-1.0000000000\r\n-0.5773502588	0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.1250000000	0.5000000000	0.0000000000\r\n\r\n-1.0000000000	1.0000000000	1.0000000000\r\n-0.5773502588	0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.8750000000	0.5000000000	0.0000000000\r\n\r\n1.0000000000	-1.0000000000	1.0000000000\r\n0.5773502588	-0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.7500000000	0.0000000000\r\n\r\n1.0000000000	1.0000000000	1.0000000000\r\n0.5773502588	0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.5000000000	0.0000000000\r\n\r\n-1.0000000000	-1.0000000000	1.0000000000\r\n-0.5773502588	-0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.0000000000	0.0000000000\r\n\r\n-1.0000000000	1.0000000000	1.0000000000\r\n-0.5773502588	0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.2500000000	0.0000000000\r\n\r\n-1.0000000000	1.0000000000	-1.0000000000\r\n-0.5773502588	0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.2500000000	0.0000000000\r\n\r\n-1.0000000000	1.0000000000	1.0000000000\r\n-0.5773502588	0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.2500000000	0.0000000000\r\n\r\n1.0000000000	1.0000000000	1.0000000000\r\n0.5773502588	0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.5000000000	0.0000000000\r\n\r\n1.0000000000	1.0000000000	-1.0000000000\r\n0.5773502588	0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.5000000000	0.0000000000\r\n\r\n1.0000000000	1.0000000000	1.0000000000\r\n0.5773502588	0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.5000000000	0.0000000000\r\n\r\n1.0000000000	-1.0000000000	1.0000000000\r\n0.5773502588	-0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.7500000000	0.0000000000\r\n\r\n1.0000000000	-1.0000000000	-1.0000000000\r\n0.5773502588	-0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.7500000000	0.0000000000\r\n\r\n1.0000000000	-1.0000000000	1.0000000000\r\n0.5773502588	-0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.7500000000	0.0000000000\r\n\r\n-1.0000000000	-1.0000000000	1.0000000000\r\n-0.5773502588	-0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	1.0000000000	0.0000000000\r\n\r\n-1.0000000000	-1.0000000000	-1.0000000000\r\n-0.5773502588	-0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	1.0000000000	0.0000000000\r\n\r\n1.0000000000	1.0000000000	-1.0000000000\r\n0.5773502588	0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.5000000000	0.0000000000\r\n\r\n1.0000000000	-1.0000000000	-1.0000000000\r\n0.5773502588	-0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.3750000000	0.7500000000	0.0000000000\r\n\r\n-1.0000000000	-1.0000000000	-1.0000000000\r\n-0.5773502588	-0.5773502588	-0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.1250000000	0.7500000000	0.0000000000\r\n\r\n-1.0000000000	1.0000000000	1.0000000000\r\n-0.5773502588	0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.8750000000	0.5000000000	0.0000000000\r\n\r\n-1.0000000000	-1.0000000000	1.0000000000\r\n-0.5773502588	-0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.8750000000	0.7500000000	0.0000000000\r\n\r\n1.0000000000	-1.0000000000	1.0000000000\r\n0.5773502588	-0.5773502588	0.5773502588\r\n0.0000000000	0.0000000000	0.0000000000\r\n0\r\n1\r\n0.6250000000	0.7500000000	0.0000000000\r\n\r\n12\r\n-1		0	1	2\r\n-1		3	4	5\r\n-1		6	7	8\r\n-1		9	10	11\r\n-1		12	13	14\r\n-1		15	16	17\r\n-1		18	19	20\r\n-1		21	22	23\r\n-1		24	25	26\r\n-1		27	28	29\r\n-1		30	31	32\r\n-1		33	34	35\n";

        //Instance #  
        string _instanceData = ";INSTANCE {0}\r\n" +
                               // Object #
                               "{1}\r\n" +
                               // Object name
                               "\"{2}\"\r\n" +
                               // # (Again?)
                               "{3}\r\n" +
                               // Parent # (I think?)
                               "{4}\r\n0\r\n" +
                               // X Y Z W - Rotation?
                               "{5}\t{6}\t{7}\t{8}\r\n" +
                               // X Y Z
                               "{9}\t{10}\t{11}\r\n1.0000000000\r\n" +
                               // X Y Z W - Orientation ?
                               "0.0000000000\t0.0000000000\t0.0000000000\t1.0000000000\r\n0.0000000000	0.0000000000	0.0000000000\r\n1.0000000000\n";

        public class Quaternion {
            public Quaternion() { }
            public Quaternion(double x, double y, double z, double w) {
                this.w = w;
                this.x = x;
                this.y = y;
                this.z = z;
            }
            
            public double w;
            public double x;
            public double y;
            public double z;
        }

        public static Quaternion ToQuaternion(double roll, double pitch, double yaw) {
            double cy = Math.Cos(yaw * 0.5);
            double sy = Math.Sin(yaw * 0.5);
            double cp = Math.Cos(pitch * 0.5);
            double sp = Math.Sin(pitch * 0.5);
            double cr = Math.Cos(roll * 0.5);
            double sr = Math.Sin(roll * 0.5);

            Quaternion q = new Quaternion();
            q.w = cr * cp * cy + sr * sp * sy;
            q.x = sr * cp * cy - cr * sp * sy;
            q.y = cr * sp * cy + sr * cp * sy;
            q.z = cr * cp * sy - sr * sp * cy;

            return q;
        }

        public override void Execute(object parameter) {
            OpenFileDialog ofd = new OpenFileDialog {
                Title = "Select .ass file",
                Filter = "ASS Files (*.ass)|*.ass|All files (*.*)|*.*",
                AddExtension = true,
            };

            if (!ofd.ShowDialog().Value) return;

            DateTime start = DateTime.Now;
            ;
            IGlobal global = Autodesk.Max.GlobalInterface.Instance;

            IInterface14 ip = global.COREInterface14;

            string assPath = ofd.FileName;
            string dataPath = Path.GetFullPath(Path.Combine(assPath, @"..\..\..\..\..\"));

            // if (!Path.GetFileName(assPath).EndsWith(".ass"))
            // {
            //     ip.PushPrompt("File is not an .ASS file");
            //     return;
            // }

            List<string> assLines = File.ReadAllLines(assPath).ToList();
            int objectC = 0;
            int instanceC = 0;
            int objectIn = 0;
            int instanceIn = 0;

            for (int i = 0; i < assLines.Count; i++)
            {
                string line = assLines[i];

                if (line.StartsWith(";OBJECT")) objectC++;
                if (line.StartsWith(";INSTANCE")) instanceC++;
                if (line.StartsWith(";### OBJECTS ###")) objectIn = i;
                if (line.StartsWith(";### INSTANCES ###")) instanceIn = i;
            }

            IINode rootNode = ip.RootNode;
            List<IINode> xRefs = new List<IINode>();

            for (int i = 0; i < rootNode.NumberOfChildren; i++)
            {
                IINode childNode = rootNode.GetChildNode(i);
                if (childNode.Name[0] == '+') xRefs.Add(childNode);
            }

            List<string> genObjects = new List<string>();
            List<string> genInstances = new List<string>();

            foreach (IINode node in xRefs)
            {
                string[] spl = node.Name.Split(':');
                string name = spl[0].Substring(1, spl[0].Length - 1);
                string path = dataPath + "\\" + spl[1];

                string objEntry = string.Format(_objectData, objectC, path, name);
                genObjects.Add(objEntry);

                IInterval interval = global.Interval.Create(int.MinValue,
                    int.MaxValue);

                int time = ip.Time;

                Quaternion quat = GetRotationQuaternion(node.TMController.RotationController, time, interval);

                object posx = 0f;
                object posy = 0f;
                object posz = 0f;

                IControl posController = node.TMController.PositionController;
                posController.XController.GetValue(time, ref posx, interval, GetSetMethod.Absolute);
                posController.YController.GetValue(time, ref posy, interval, GetSetMethod.Absolute);
                posController.ZController.GetValue(time, ref posz, interval, GetSetMethod.Absolute);

                string insEntry = string.Format(_instanceData, instanceC, objectC, name, instanceC - 1, 1,
                    $"{quat.x:F10}", $"{quat.y:F10}", $"{quat.z:F10}", $"{quat.w:F10}", 
                    $"{posx:F10}", $"{posy:F10}", $"{posz:F10}");
                
                genInstances.Add(insEntry);

                objectC++;
                instanceC++;
            }

            assLines[objectIn + 1] = objectC.ToString();
            assLines[instanceIn + 1] = instanceC.ToString();
            assLines.InsertRange(instanceIn, genObjects);
            assLines.AddRange(genInstances);

            File.WriteAllLines(assPath, assLines);

            ip.PushPrompt(
                $"{objectC} objects and {instanceC} instances written to {Path.GetFileName(assPath)} in {start - DateTime.Now:mm\\:ss} seconds");
        }

        public override string InternalActionText { get { return AdnMenuSampleStrings.actionText01; } }

        public override string InternalCategory { get { return AdnMenuSampleStrings.actionCategory; } }

        public override string ActionText { get { return InternalActionText; } }

        public override string Category { get { return InternalCategory; } }
    }
}
using System;
using System.Collections.Generic;

using Rhino;
using Rhino.Geometry;

namespace UFG
{
    public class SiteObj
    {
        Line RAYLINE;
        protected Curve SITE;
        protected Point3d INTXPT;
        protected double SETBACKDIST;
        protected double INTXDIST;
        protected Point3d CENTROID;
        public string COMMENT;
        public Extrusion SOLID;
        double FSR;

        public SiteObj() { }
        public SiteObj(
            Line ray,
            Curve site,
            Point3d intxpt,
            double setbackdist,
            Point3d p,
            double fsr)
        {
            RAYLINE = ray;
            SITE = site;
            INTXPT = intxpt;
            SETBACKDIST = setbackdist;
            CENTROID = p;
            FSR = fsr;
        }

        public Extrusion GetOffsetExtrusion()
        {
            var offsetCrv = SITE.Offset(
                CENTROID,
                Vector3d.ZAxis,
                SETBACKDIST,
                Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance,
                CurveOffsetCornerStyle.Sharp
                );
            SOLID = Extrusion.Create(offsetCrv[0], 10, true);
            return SOLID;
        }
        public Curve GetSite() { return SITE; }
        public Point3d GetIntxPt() { return INTXPT; }
        public double GetSetbackDist() { return SETBACKDIST; }
        public double GetIntxDist() { return INTXDIST; }
        public string GetComment() { return COMMENT; }
        public Point3d GetCentroid() { return CENTROID; }
        public Line GetRayLine() { return RAYLINE; }
    }
}

using System;
using System.Collections.Generic;

using Rhino;
using Rhino.Geometry;

namespace UFG
{
    public class IntxObj
    {
        Line RAYLINE;
        protected Curve SITE;
        protected Point3d INTXPT;
        protected double SETBACKDIST;
        protected double INTXDIST;
        protected Point3d CENTROID;
        public string COMMENT;

        public IntxObj() { }
        public IntxObj(
            Line ray,
            Curve site,
            Point3d intxpt,
            double setbackdist,
            Point3d p)
        {
            RAYLINE = ray;
            SITE = site;
            INTXPT = intxpt;
            SETBACKDIST = setbackdist;
            CENTROID = p;
        }
        public IntxObj(Curve site, Point3d c) {
            SITE = site;
            CENTROID = c;
        }
        public IntxObj(Line ray, 
            Curve site, 
            Point3d intxpt, 
            double setbackdist, 
            double intxdist, 
            string s, 
            Point3d cen)
        {
            RAYLINE = ray;
            SITE = site;
            INTXPT = intxpt;
            SETBACKDIST = setbackdist;
            INTXDIST = intxdist;
            COMMENT = s;
            CENTROID = cen;
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

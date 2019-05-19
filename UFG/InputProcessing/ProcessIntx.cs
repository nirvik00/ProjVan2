using System;
using System.Collections.Generic;

using Rhino;
using Rhino.Geometry;

namespace UFG
{

    public class ProcessIntx
    {
        protected List<IntxObj> INTXOBJLI;
        protected List<ProcObj> PROCOBJLI;
        protected List<Curve> SITECRVLI;
        protected List<LineCurve> FULLSTREETLI;
        protected List<double> SETBACKDISTLI;
        protected List<string> LAYERNAMES;
        protected double FSR;
        protected double MINHT;
        protected double MAXHT;
        protected int NUMRAYS;
        protected double MAGRAYS;
        protected List<Line> RAYLI;

        public ProcessIntx() { }

        public ProcessIntx(
            List<ProcObj>procobjli, 
            List<Curve>sitecrvs,
            double fsr,
            double minht,
            double maxht,
            int numrays,
            double magrays
           )
        {
            RAYLI = new List<Line>();
            INTXOBJLI = new List<IntxObj>();

            SITECRVLI = new List<Curve>();
            SITECRVLI = sitecrvs;

            PROCOBJLI = new List<ProcObj>();
            PROCOBJLI = procobjli;
            FSR = fsr;
            MAXHT= maxht;
            MINHT= minht;
            NUMRAYS = numrays;
            MAGRAYS = magrays;

            SETBACKDISTLI = new List<double>();
            FULLSTREETLI = new List<LineCurve>();

            for (int i=0;i<PROCOBJLI.Count; i++)
            {
                List<LineCurve> li = PROCOBJLI[i].GetStreetLineCurves();
                for(int j=0; j<li.Count; j++) { FULLSTREETLI.Add(li[j]); }
                SETBACKDISTLI.Add(PROCOBJLI[i].GetSetbackDist());
            }
        }

        public void GenRays()
        {
            INTXOBJLI = new List<IntxObj>();
            for (int i = 0; i < SITECRVLI.Count; i++)
            {
                Curve sitecrv = SITECRVLI[i];
                Point3d p = Rhino.Geometry.AreaMassProperties.Compute(sitecrv).Centroid;

                Line ray = new Line();
                Point3d fIntxPt = Point3d.Unset;
                double fsetbackdist = double.NaN;
                double minD = 1000000000000.00;

                //int n = NUMRAYS;
                // double angle = (360 / n) * Math.PI / 180;
                for (double j = 0; j < 2 * Math.PI; j += Math.PI/4)
                {
                    double x = p.X + (MAGRAYS * Math.Cos(j) - MAGRAYS * Math.Sin(j));
                    double y = p.Y + (MAGRAYS * Math.Sin(j) + MAGRAYS * Math.Cos(j));
                    Point3d r = new Point3d(x, y, 0);
                    ray = new Line(p, r);
                    RAYLI.Add(ray);

                    for(int k=0; k< PROCOBJLI.Count; k++)
                    {
                        List<LineCurve> streets = PROCOBJLI[k].GetStreetLineCurves();
                        double setbackdist = PROCOBJLI[k].GetSetbackDist();
                        Point3d intxPt=GetIntx(ray, streets, sitecrv, setbackdist);
                        double d = p.DistanceTo(intxPt);
                        if (d < minD)
                        {
                            minD = d;
                            fIntxPt = intxPt;
                            fsetbackdist = setbackdist;
                        }
                    }

                }
                IntxObj obj = new IntxObj(ray, sitecrv, fIntxPt, fsetbackdist, p);
                INTXOBJLI.Add(obj);
            }
        }

        public Point3d GetIntx(Line lineA, List<LineCurve> streets, Curve sitecrv, double setbackdist)
        {
            Point3d intxPt = new Point3d();
            double minD = 10000000000.00;

            Point3d p = lineA.PointAt(0.0);
            Point3d q = lineA.PointAt(1.0);

            for (int i=0; i<streets.Count; i++)
            {
                Line lineB = streets[i].Line;
                Point3d r = lineB.PointAt(0.0);
                Point3d s = lineB.PointAt(1.0);
                double a = 0.0;
                double b = 0.0;
                var t = Rhino.Geometry.Intersect.Intersection.LineLine(lineA, lineB, out a, out b);
                if (t == true)
                {
                    Point3d p2 = lineA.PointAt(a);
                    Point3d q2 = lineB.PointAt(b);
                    double pp2 = p.DistanceTo(p2);
                    double qp2 = q.DistanceTo(p2);
                    double pq = p.DistanceTo(q);
                    double rq2 = r.DistanceTo(q2);
                    double sq2 = s.DistanceTo(q2);
                    double rs = r.DistanceTo(s);


                    if ((Math.Abs(pp2 + qp2 - pq) < 1.1) && (Math.Abs(rq2 + sq2 - rs) < 1.1))
                    {
                        if (pp2 < minD)
                        {
                            minD = pp2;
                            intxPt = p2;
                        }
                    }
                }
            }

            return intxPt;
        }

        public List<IntxObj> GetIntxObjList() { return INTXOBJLI; }
        public List<Curve> GetSites() { return SITECRVLI; }
        public List<LineCurve> GetStreets() { return FULLSTREETLI; }
        public List<Line> GetRays() { return RAYLI; }
    } // end class
} // end namespace 



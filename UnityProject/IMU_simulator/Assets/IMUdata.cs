using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Point3D
{
	public double X { get; set; }
	public double Y { get; set; }
	public double Z { get; set; }
}


public class IMUdata
{
	public string source;
	public double timeStamp = 0;
	public Point3D magneticOrientation = new Point3D();

	public Point3D gpsPosition = new Point3D();
	#region "Constructors"
	public IMUdata(string source)
	{
		this.source = source;
	}

	public IMUdata(string source, string csv_line)
	{
		this.source = source;
		try {
			string[] stringSeparators = {","};
			string[] aux = csv_line.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
			if (aux.Length > 6) {
				this.timeStamp = Convert.ToDouble(aux[0]);
				this.magneticOrientation.X = Convert.ToDouble(aux[1].Replace(".", ","));
				this.magneticOrientation.Y = Convert.ToDouble(aux[2].Replace(".", ","));
				this.magneticOrientation.Z = Convert.ToDouble(aux[3].Replace(".", ","));
				this.gpsPosition.X = Convert.ToDouble(aux[4].Replace(".", ","));
				this.gpsPosition.Y = Convert.ToDouble(aux[5].Replace(".", ","));
				this.gpsPosition.Z = Convert.ToDouble(aux[6].Replace(".", ","));
			} else {
				this.timeStamp = 0;
			}
		} catch (Exception ex) {
			Console.WriteLine (ex.ToString ());
		}
	}

	public string Get_csvLine()
	{
		string res = "";

		res = res + this.timeStamp + ",";
		res = res + "" + this.magneticOrientation.X.ToString().Replace(",", ".") + ",";
		res = res + "" + this.magneticOrientation.Y.ToString().Replace(",", ".") + ",";
		res = res + "" + this.magneticOrientation.Z.ToString().Replace(",", ".") + ",";
		res = res + "" + this.gpsPosition.X.ToString().Replace(",", ".") + ",";
		res = res + "" + this.gpsPosition.Y.ToString().Replace(",", ".") + ",";
		res = res + "" + this.gpsPosition.Z.ToString().Replace(",", ".") + ",#";

		return res;
	}
	#endregion

	public Point3D gps_XYZ {
		get {
			Point3D res = new Point3D();
			double cosLat = Math.Cos(this.gpsPosition.X * Math.PI / 180.0);
			double sinLat = Math.Sin(this.gpsPosition.X * Math.PI / 180.0);
			double cosLon = Math.Cos(this.gpsPosition.Y * Math.PI / 180.0);
			double sinLon = Math.Sin(this.gpsPosition.Y * Math.PI / 180.0);
			double rad = 6378137.0;
			double f = 1.0 / 298.257224;
			double C = 1.0 / Math.Sqrt(cosLat * cosLat + (1 - f) * (1 - f) * sinLat * sinLat);
			double S = (1.0 - f) * (1.0 - f) * C;
			double h = 0.0;
			res.X = (rad * C + h) * cosLat * cosLon;
			res.Y = (rad * C + h) * cosLat * sinLon;
			res.Z = (rad * S + h) * sinLat;
			return res;
		}
	}


	public override string ToString()
	{
		return this.Get_csvLine();
	}


}
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

public class Program
{
	public static void Main()
	{
		Traversal t = new Traversal(0, 5);
		t.Traverse(1);
		t.Traverse(4);
		Console.WriteLine(t.CartesianPoint());

		Traversal tt = null;
		List<Vector2> points = new List<Vector2>();

		for (int i = 0; i < 9; i++)
		{
			Iteration(tt, i, points);
			Console.WriteLine(points.Distinct().ToList().Count);
		}


		foreach (Vector2 p in points.Distinct())
		{
			Console.WriteLine(p.X + ", " + p.Y);
		}
	}

	public static void Iteration(Traversal t, int depth, List<Vector2> points)
	{
		if (depth == 0)
		{
			points.Add(new Traversal(0, 5).CartesianPoint());
			return;
		}

		Iterate(t, depth, depth, points);
	}

	public static void Iterate(Traversal t, int startDepth, int depth, List<Vector2> points)
	{
		if (depth <= 0)
		{
			points.Add(t.CartesianPoint());
			return;
		}

		for (int i = 1; i <= 5; i++)
		{
			if (startDepth == depth)
			{
				t = new Traversal(0, 5);
			}

			t.Traverse(i);
			Iterate(t, startDepth, depth - 1, points);
			t.RemoveLast();
		}
	}
}

public class Traversal
{
	public float sideLength = 1;
	public float apothem;
	public float theta;

	public int startRotation;
	public int polySides;
	public List<int> sideNumbers = new List<int>();

	public Traversal(int startRotation, int polySides)
	{
		this.startRotation = startRotation;
		this.polySides = polySides;

		apothem = (float)(sideLength / (2 * Math.Tan(DegToRad(180.0f / polySides))));
		theta = 360.0f / polySides;
	}

	public void Traverse(int side)
	{
		if (side <= 5 && side > 0)
		{
			sideNumbers.Add(side);
		}
	}

	public void RemoveLast()
	{
		sideNumbers.RemoveAt(sideNumbers.Count - 1);
	}

	public Vector2 CartesianPoint()
	{
		int rot = startRotation;
		float x = 0;
		float y = 0;

		for (int i = 0; i < sideNumbers.Count; i++)
		{
			x += (1 - 2 * rot) * (2 * apothem * (float)Math.Sin(DegToRad(theta * sideNumbers[i] - theta / 2.0f)));
			y += (1 - 2 * rot) * (2 * apothem * (float)Math.Cos(DegToRad(theta * sideNumbers[i] - theta / 2.0f)));

			rot = 1 - rot;
		}

		x = (float)Math.Round(x, 3);
		y = (float)Math.Round(y, 3);

		return new Vector2(x, y);
	}

	public static float DegToRad(float degrees)
	{
		return degrees * (float)(Math.PI / 180.0f);
	}
}
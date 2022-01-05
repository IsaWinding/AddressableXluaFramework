using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathType
{
    Once = 1,
    Loop = 2,
    PingPong = 3,
}
[System.Serializable]
public class PathPoint
{
    public Vector3 pos;
    public PathPoint NextPoint;
    public PathPoint PrePoint;
}

public class AIPath
{
	public List<PathPoint> Paths = new List<PathPoint>();
	public PathType pathType = PathType.Loop;
	private PathPoint nextPoint;

	public static List<PathPoint> GetPathsByVector3s(List<Vector3> pPoints) {

		var paths = new List<PathPoint>();
		foreach (var temp in pPoints)
		{
			var pathPoint = new PathPoint() { pos = temp};
			paths.Add(pathPoint);
		}
		return paths;
	}
	public static void InitPath(List<PathPoint> pPaths, PathType pPathType, out PathPoint oFirstPoint)
	{
		for (var i = 0; i < pPaths.Count; i++)
		{
			var path = pPaths[i];
			if (i == 0)
			{
				path.NextPoint = pPaths[i + 1];
				if (pPathType == PathType.Loop)
					path.PrePoint = pPaths[pPaths.Count - 1];
			}
			else if (i == pPaths.Count - 1)
			{
				if (pPathType == PathType.Loop)
					path.NextPoint = pPaths[0];
				path.PrePoint = pPaths[i - 1];
			}
			else
			{
				path.NextPoint = pPaths[i + 1];
				path.PrePoint = pPaths[i - 1];
			}
		}
		oFirstPoint = pPaths[0];
	}
	public void Init()
	{
		for (var i = 0; i < Paths.Count; i++)
		{
			var path = Paths[i];
			if (i == 0)
			{
				path.NextPoint = Paths[i + 1];
				if (pathType == PathType.Loop)
					path.PrePoint = Paths[Paths.Count - 1];
			}
			else if (i == Paths.Count - 1)
			{
				if (pathType == PathType.Loop)
					path.NextPoint = Paths[0];
				path.PrePoint = Paths[i - 1];
			}
			else
			{
				path.NextPoint = Paths[i + 1];
				path.PrePoint = Paths[i - 1];
			}
		}
		nextPoint = Paths[0];
	}
	public PathPoint GetNextPoint()
	{
		return nextPoint;
	}
	public static bool IsReachNextPoint(PathPoint pCurPoint,Vector3 pos)
	{
		if (Vector3.Distance(pCurPoint.pos, pos) <= 1)
			return true;
		return false;
	}
	private bool isForward = true;
	public static void GetNextPointEx(PathPoint pCurPoint, PathType pPathType,bool pIsForward,out PathPoint oNextPoint,out bool oIsForward)
	{
		oNextPoint = pCurPoint.NextPoint;
		oIsForward = pIsForward;

		if (pPathType == PathType.PingPong)
		{
			if (pIsForward)
			{
				if (pCurPoint.NextPoint == null)
				{
					oNextPoint = pCurPoint.PrePoint;
					oIsForward = false;
				}
			}
			else
			{
				if (pCurPoint.PrePoint != null)
					oNextPoint = pCurPoint.PrePoint;
				else
				{
					oIsForward = true;
				}
			}
		}
	}
	public void OnReachNextPoint()
	{
		if (pathType == PathType.Once)
		{
			nextPoint = nextPoint.NextPoint;
		}
		else if (pathType == PathType.Loop)
		{
			nextPoint = nextPoint.NextPoint;
		}
		else if (pathType == PathType.PingPong)
		{
			if (isForward)
			{
				if (nextPoint.NextPoint != null)
					nextPoint = nextPoint.NextPoint;

				else
				{
					nextPoint = nextPoint.PrePoint;
					isForward = false;
				}
			}
			else
			{
				if (nextPoint.PrePoint != null)
					nextPoint = nextPoint.PrePoint;
				else
				{
					nextPoint = nextPoint.NextPoint;
					isForward = true;
				}
			}
		}
	}
}

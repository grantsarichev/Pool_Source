using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class PoolControllerParameters<T> where T:MonoBehaviour,IPoolObject
{
	private readonly string name;
	private readonly T objectTemplate;
	private readonly int poolSize;

	public PoolControllerParameters(string name,T objectTemplate,int poolSize)
	{
		this.name = name;
		this.objectTemplate = objectTemplate;
		this.poolSize = poolSize;
	}

	public string Name
	{
		get
		{
			return name;
		}
	}
	public T ObjectTemplate
	{
		get
		{
			return objectTemplate;
		}
	}
	public int PoolSize
	{
		get
		{
			return poolSize;
		}
	}
}

class PoolExceptionon:Exception
{
	public PoolExceptionon(string message):base(message)
	{

	}
}

public class ObjectsPoolController<T> where T:MonoBehaviour,IPoolObject
{

	private UnityObjectsPool<T> []pooles;

	public ObjectsPoolController(params PoolControllerParameters<T>[] poolesParameters)
	{
		pooles = new UnityObjectsPool<T>[poolesParameters.Length];
		for (int i=0; i<poolesParameters.Length; i++)
		{
			pooles[i]=new UnityObjectsPool<T>(poolesParameters[i].Name,poolesParameters[i].ObjectTemplate,poolesParameters[i].PoolSize);
		}
	}

	public T GetObjectByName(string name)
	{
		T foundObject=null;

		for(int i=0;i<pooles.Length;i++)
		{
			if(pooles[i].Name==name)
			{
				foundObject=pooles[i].GetObject();
				break;
			}
		}

		if (foundObject == null) 
		{
			throw new PoolExceptionon(String.Format("Pool with the '{0}' name is not founded",name));
		}

		return foundObject;
	}

	public T GetRandomObject()
	{
		int randomPoolNumber =(int) UnityEngine.Random.Range (0, pooles.Length);
		T foundObject = pooles [randomPoolNumber].GetObject ();

		return foundObject;



	}
}

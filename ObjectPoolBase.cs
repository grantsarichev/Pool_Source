using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class ObjectPoolBase<T> where T:IPoolObject
{
	protected readonly string name;
	protected readonly T objectTemplate;
	protected int poolSize;
	protected List<T> poolObjects;

	public ObjectPoolBase(string name,T objectTemplate,int poolSize)
	{
		this.name = name;
		this.objectTemplate = objectTemplate;
		this.poolSize = poolSize;
		poolObjects = new List<T> ();
		AddObjects (poolSize);
	}

	public string Name 
	{
		get 
		{
			return name;
		}
	}

	public int PoolSize 
	{
		get 
		{
			return poolSize;
		}
		set 
		{
			poolSize = value;
		}
	}

	public T GetObject()
	{
		if (poolObjects.Count == 0) 
		{
			AddObjects(1);
		}

		T targetObject = poolObjects [poolObjects.Count - 1];
		targetObject.Pop ();
		poolObjects.Remove (targetObject);

	    return targetObject;

	}

	protected abstract void AddObjects (int count);
	
}

public class UnityObjectsPool<T>:ObjectPoolBase<T> where T:MonoBehaviour,IPoolObject
{

	public UnityObjectsPool(string name,T objectTemplate,int poolSize):base(name,objectTemplate,poolSize)
	{
	}

	#region implemented abstract members of ObjectPoolBase
	protected override void AddObjects (int count)
	{
		for (int i=0; i<count; i++) 
		{
			T createdObj = Object.Instantiate (objectTemplate)as T;
			createdObj.Push();

			/*createdObj.PushEvent+=()=>
			                        {
										createdObj.Push();
										poolObjects.Add(createdObj);
									};
*/
			poolObjects.Add (createdObj);
		}

	}
	#endregion

}

public class CustomObjectsPool<T>:ObjectPoolBase<T> where T:IPoolObject,new()
{

	public CustomObjectsPool(string name,T objectTemplate,int poolSize):base(name,objectTemplate,poolSize)
	{
	}

	#region implemented abstract members of ObjectPoolBase
	protected override void AddObjects (int count)
	{
		for (int i=0; i<count; i++) 
		{
			T createdObj =new T();
			createdObj.Push();
			poolObjects.Add (createdObj);
		}
	}
	#endregion
}







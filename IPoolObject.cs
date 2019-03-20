using UnityEngine;
using System;
using System.Collections;

public interface IPoolObject
{
	event Action PushEvent;
	event Action PopEvent;

	void Push();
	void Pop();
}

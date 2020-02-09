using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
	// Start is called before the first frame update
	public static void main(String[] args)
	{
		GameManager gm = new GameManager();
		gm.Start();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
	/**
	 * 成功確率のパーセンテージを渡して成功したかどうかを判定する
	 * @param probability
	 * @return
	 */
	public static bool RandomJudge(int probability)
	{
		int judgementValue = UnityEngine.Random.Range(0, 100);

		if (probability >= judgementValue)
		{
			return true;
		}
		return false;

	}
}

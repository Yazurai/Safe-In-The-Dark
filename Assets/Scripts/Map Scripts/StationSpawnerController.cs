using UnityEngine;
using System.Collections;

public class StationSpawnerController : MonoBehaviour 
{
	public int SpawnNumber;
	public int [] chosen;
	public GameObject [] chosenGameobjects;
	bool temp = false;
	int newIndex;
	void Start()
	{
		chosen = new int [SpawnNumber];
		chosenGameobjects = new GameObject[SpawnNumber];
	}

	public GameObject [] GetSpawns()
	{
		for (int i = 0; i < SpawnNumber; i++) 
		{
			do 
			{
				newIndex = Random.Range(0,transform.childCount - 1);
				temp = true;
				foreach (var item in chosen) 
				{
					if(item == newIndex)
						temp = false;
				}
			} while (temp == false);
			chosenGameobjects[i] = transform.GetChild(newIndex).gameObject;
			chosen [i] = newIndex;
		}
		return chosenGameobjects;
	}

	public GameObject GetSpawn()
	{
		return transform.GetChild(Random.Range(0,transform.childCount - 1)).gameObject;
	}
}

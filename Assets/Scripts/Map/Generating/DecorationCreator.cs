using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationCreator : MonoBehaviour
{
	private TileGrid tileGrid;
	private DecorationSettings[] staticDecorSets;
	private DecorationSettings[] dynamicDecorSets;
	private GameObject decorParent;

	public void SetTileGrid(ref TileGrid tileGrid)
	{
		this.tileGrid = tileGrid;
	}

	public void SetDecorationSettings(DecorationSettings[] staticDecorSets, DecorationSettings[] dynamicDecorSets)
	{
		this.staticDecorSets = staticDecorSets;
		this.dynamicDecorSets = dynamicDecorSets;
	}

	public void CreateStaticDecorations()
	{
		decorParent = new GameObject();
		decorParent.name = "StaticDecorations";

		RandomGenerator.SetTileMapSize(tileGrid.CountX, tileGrid.CountZ);
		int[,] decorMap;

		foreach (var decorSets in staticDecorSets)
		{
			decorMap = RandomGenerator.Generate(decorSets.GetGeneratorSettings());
			CreateDecorations(decorMap, decorSets);
		}
	}

	private void CreateDecorations(int[,] decorMap, DecorationSettings decorSets)
	{
		Random.InitState(decorSets.GetSeed().GetHashCode());

		int decorCount = decorSets.GetDecorations().Length;
		float minScale = decorSets.minScale;
		float maxScale = decorSets.maxScale;


		int i = 0;
		for (int x = 0; x < tileGrid.CountX; x++)
		{
			for (int z = 0; z < tileGrid.CountZ; z++)
			{
				if (decorMap[x, z] == 1 && tileGrid[x, z] == decorSets.GetTileHolder())
				{
					i++;
					Transform tr = Instantiate(decorSets.GetDecorations()[i % decorCount]).transform;

					tr.position = new Vector3(x, 0, z) * tileGrid.TileSize;
					tr.localScale *= Random.Range(minScale, maxScale);
					tr.localRotation = Quaternion.Euler(0, Random.Range(0, 180), 0);

					tr.parent = decorParent.transform;
				}
			}
		}
	}
}

[System.Serializable]
public class Allows
{
	public AllowDecorateType allowDecorate;
	public AllowBuildType allowBuild;
	public AllowExtractType allowExtract;

	public Allows(int decorateCode, int buildCode = 0, int extractCode = 0)
	{
		allowDecorate = (AllowDecorateType)decorateCode;
		allowBuild = (AllowBuildType)buildCode;
		allowExtract = (AllowExtractType)extractCode;
	}

	/// <summary>
	/// decorateCode, buildCode = 0, extractCode = 0
	/// </summary>
	/// <param name="mas[0] = decorateCode"></param>
	/// <param name="mas[1] = buildCode = 0"></param>
	/// <param name="mas[2] = extractCode = 0"></param>
	public Allows(params int[] mas)
	{
		allowDecorate = (AllowDecorateType)mas[0];

		if (mas.Length > 1)
		{
			allowBuild = (AllowBuildType)mas[1];
		}
		else
		{
			allowBuild = AllowBuildType.None;
		}

		if (mas.Length > 2)
		{
			allowExtract = (AllowExtractType)mas[2];
		}
		else
		{
			allowExtract = AllowExtractType.None;
		}
	}
}

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
}

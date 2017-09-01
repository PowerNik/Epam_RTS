[System.Flags]
public enum AllowDecorateType
{
	None = 0,
	/// <summary>
	/// Можно ли поменять текстуру
	/// </summary>
	Texturing = 0x01,

	/// <summary>
	/// Можно ли добавить неизменный объект
	/// </summary>
	Scenery = 0x02,

	/// <summary>
	/// Можно ли добавить динамический объект (препятствие)
	/// </summary>
	Dynamic = 0x04,
}

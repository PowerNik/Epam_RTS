﻿namespace MapGenerate
{
	public class Square
	{
		// Вершины квадрата
		public ControlNode topLeft, topRight, botRight, botLeft;

		// Середины сторон фигур
		public Node centreTop, centreBot, centreRight, centreLeft;

		// Середины сторон видимых стен
		public Node wallCentreTop, wallCentreBot, wallCentreRight, wallCentreLeft;

		public SquareConfiguration configration;

		public Square(ControlNode topLeft, ControlNode topRight, ControlNode botRight, ControlNode botLeft)
		{
			// Обход вершин по часовой стрелке
			this.topLeft = topLeft;
			this.topRight = topRight;
			this.botRight = botRight;
			this.botLeft = botLeft;

			// TL--CT--TR
			// |        |
			// CL      CR
			// |        |
			// BL--CB--BR

			CalculateCenterNodes();
			CalculateWallCenterNodes();

			CalculateConfiguration();
		}

		private void CalculateCenterNodes()
		{
			centreTop = topLeft.right;
			centreBot = botLeft.right;
			centreRight = botRight.above;
			centreLeft = botLeft.above;
		}

		private void CalculateWallCenterNodes()
		{
			wallCentreTop = topLeft.wallRight;
			wallCentreBot = botLeft.wallRight;
			wallCentreRight = botRight.wallAbove;
			wallCentreLeft = botLeft.wallAbove;
		}

		private void CalculateConfiguration()
		{
			//   TL--CT--TR
			//   |        |
			// 8 CL      CR 4
			//   |        |
			//   BL--CB--BR
			//   1    2

			int sum = 0;

			if (botLeft.active)
			{
				sum += 1;
			}

			if (botRight.active)
			{
				sum += 2;
			}

			if (topRight.active)
			{
				sum += 4;
			}

			if (topLeft.active)
			{
				sum += 8;
			}

			configration = (SquareConfiguration)sum;
		}
	}
}
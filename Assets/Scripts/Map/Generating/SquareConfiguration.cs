namespace MapGenerate
{
	public enum SquareConfiguration
	{
		NullPoint = 0,          // 1 2 3
								// 8 * 4
								// 7 6 5

		// Базовые фигуры (треугольники)
		// Номер равен номеру верхней левой вершины
		OnePoint_1 = 1,         //    1       2       4       8
		OnePoint_2 = 2,         //  O O *   * O O   * * *   * * * 
		OnePoint_4 = 4,         //  O * *   * * O   * * O   O * * 
		OnePoint_8 = 8,         //  * * *   * * *   * O O   O O *

		// Составные фигуры
		// Номер равен сумме номеров используемых базовых треугольников
		TwoPoint_3 = 3,         //	  3       6       9      12
		TwoPoint_6 = 6,         //  O O O   * O O   * * *   O O *
		TwoPoint_9 = 9,         //  O * O   * * O   O * O   O * *
		TwoPoint_12 = 12,       //  * * *   * O O   O O O   O O *

		TwoPoint_5 = 5,         //    5      10
		TwoPoint_10 = 10,       //  O O *   * O O
								//  O * O   O * O
								//  * O O   O O *

		ThreePoint_7 = 7,       //    7      11		 13		 14
		ThreePoint_11 = 11,     //  O O O   O O O   O O *   * O O
		ThreePoint_13 = 13,     //  O * O   O * O	O * O	O * O
		ThreePoint_14 = 14,     //  * O O   O O *	O O O	O O O

		FourPoint_15 = 15       //   15  
								//  O O O
								//  O * O
								//  O O O
	};
}
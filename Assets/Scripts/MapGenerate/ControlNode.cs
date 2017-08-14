using UnityEngine;

namespace MapGenerate
{
	public class ControlNode : Node
	{
		public bool active;
		public Node above, right;

		public ControlNode(Vector3 position, bool active, float squareSize) : base(position)
		{
			this.active = active;
			above = new Node(position + Vector3.forward * squareSize / 2f);
			right = new Node(position + Vector3.right * squareSize / 2f);
		}
	}
}
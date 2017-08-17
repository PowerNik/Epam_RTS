using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ActionBehaviour : MonoBehaviour {

    public Sprite ButtonIcon;
    public abstract UnityAction GetClickAction();
}

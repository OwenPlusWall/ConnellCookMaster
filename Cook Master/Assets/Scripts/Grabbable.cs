using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    GameObject gameObject { get;}
}

public class Grabbable : MonoBehaviour, IGrabbable
{

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If the object inherit of this interface, it must be a struct or a class with just the information on what to do but no logic behind.
/// ZhuLi (the classes behind the static instance) will know how to do the thing.
/// </summary>
public interface IZhuLiCommand { }

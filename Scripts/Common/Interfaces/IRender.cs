using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this interface is used to render or delete objects in the scene
public interface IRender
{
    void Render();
    void Clear();
    
}

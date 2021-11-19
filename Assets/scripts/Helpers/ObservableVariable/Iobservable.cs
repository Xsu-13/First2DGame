using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface Iobservable 
{
    event Action<object> OnChanged;
}

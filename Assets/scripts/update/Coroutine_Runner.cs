using UnityEngine;
using XLua;
using System.Collections.Generic;
using System.Collections;
using System;

namespace XLuaTest
{
    public class Coroutine_Runner : MonoBehaviour
    {
    }


    public static class CoroutineConfig
    {
        [LuaCallCSharp]
        public static List<Type> LuaCallCSharp => new List<Type>()
        {
            typeof(WaitForSeconds)
        };

        [CSharpCallLua]
        public static List<Type> CSharpCallLua = new List<Type>() {
                typeof(Action),
                typeof(Func<double, double, double>),
                typeof(Action<string>),
                typeof(Action<double>),
                typeof(UnityEngine.Events.UnityAction),
                typeof(System.Collections.IEnumerator)
            };
    }
}

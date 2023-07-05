using UnityEngine;
using XLua;

namespace XLuaTest
{
    public class RawObjectTest : MonoBehaviour
    {
        public static void PrintType(object o)
        {
            Debug.Log("type:" + o.GetType() + ", value:" + o);
        }

        public static void SayHi()
        {
            Debug.Log("hi");
        }

        // Use this for initialization
        void Start()
        {
            LuaEnv luaenv = new LuaEnv();
            // Directly pass 1234 to an object parameter, xLua will select the long that can retain the maximum precision to pass
            luaenv.DoString("CS.XLuaTest.RawObjectTest.PrintType(1234)");
            // Through a class that inherits RawObject, it can be specified to pass an int
            luaenv.DoString("CS.XLuaTest.RawObjectTest.PrintType(CS.XLua.Cast.Int32(1234))");

            luaenv.DoString("CS.XLuaTest.RawObjectTest.SayHi()");
            luaenv.Dispose();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

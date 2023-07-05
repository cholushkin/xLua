using NaughtyAttributes;
using UnityEngine;
using XLua;

namespace Qonsole
{
    [LuaCallCSharp()]
    public static class MyDomainObjectConsoleCommands
    {
        // todo: attrib 
        // [RegisterConsoleAlias("scene.object.SpawnZombie")]
        public static void SpawnZombie()
        {
            new GameObject("Zombie").AddComponent<Zombie>();
        }

        public static GameObject SpawnZombie(Vector3 pos)
        {
            var z = new GameObject("Zombie").AddComponent<Zombie>();
            z.transform.position = pos;
            return z.gameObject;
        }

        public static void SetColor(Color color)
        {
            Debug.Log(color);
        }









    }
}

public class ExampleRegisterMethods : MonoBehaviour
{
    LuaEnv luaenv = null;

    void OnEnable()
    {
        luaenv = new LuaEnv();
    }

    [Button()]
    void SpawnZombie()
    {
        luaenv.DoString("CS.Qonsole.MyDomainObjectConsoleCommands.SpawnZombie()");
    }

    [Button()]
    void SpawnZombieAt111()
    {
        luaenv.DoString("CS.Qonsole.MyDomainObjectConsoleCommands.SpawnZombie(CS.UnityEngine.Vector3.one)");
    }

    [Button()]
    void SpawnZombieAt666()
    {
        luaenv.DoString(@"
                            local z = CS.Qonsole.MyDomainObjectConsoleCommands.SpawnZombie(CS.UnityEngine.Vector3(6,6,6))
                            local t = z:GetComponent('Transform')
                            print(t.Position)
        ");
    }

    [Button()]
    void SetColor()
    {
        luaenv.DoString(@"CS.Qonsole.MyDomainObjectConsoleCommands.SetColor(CS.UnityEngine.Color.red);");
    }

    public string TableName = "_G";
    [Button()]
    void CallPrintTable()
    {
        luaenv.DoString($"for n in pairs({TableName}) do print(n) end");
    }

    void Update()
    {
        if (luaenv != null)
        {
            luaenv.Tick();
        }
    }

    void OnDisable()
    {
        luaenv.Dispose();
    }
}

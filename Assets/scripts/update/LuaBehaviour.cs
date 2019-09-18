using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaBehaviour : MonoBehaviour
{
    private string m_LuaPath;

    //TODO Use lua table to optimize function call.
    protected LuaTable m_LuaTable;
    public LuaTable LuaTable
    {
        get
        {
            return m_LuaTable;
        }
    }

    protected Action<LuaTable> m_LuaAwake;
    protected Action<LuaTable> m_LuaStart;
    protected Action<LuaTable> m_LuaOnDestroy;
    //TODO move update call to lua
    protected Action<LuaTable> m_LuaUpdate;

    public virtual void InitLua(string luaPath,params object[] args)
    {
        m_LuaPath = luaPath;
    }

    protected virtual void OnTableRequired()
    {
        //TODO inject data

        m_LuaAwake = m_LuaTable.Get<Action<LuaTable>>("Awake");
        m_LuaStart = m_LuaTable.Get<Action<LuaTable>>("Start");
        m_LuaOnDestroy = m_LuaTable.Get<Action<LuaTable>>("OnDestroy");
        m_LuaUpdate = m_LuaTable.Get<Action<LuaTable>>("Update");
        //TODO add listeners

    }

    // Start is called before the first frame update
    void Start()
    {
        if (!string.IsNullOrEmpty(m_LuaPath))
        {
            m_LuaTable = LuaMain.Instance.Require(m_LuaPath);
            if (m_LuaTable != null)
            {
                OnTableRequired();
                m_LuaAwake?.Invoke(m_LuaTable);
            }
            else
            {
                Debug.LogError("LuaTable not returned: " + m_LuaPath);
                Dispose();
            }
        }

        m_LuaStart?.Invoke(m_LuaTable);
    }

    // Update is called once per frame
    void Update()
    {
        m_LuaUpdate?.Invoke(m_LuaTable);
    }

    protected virtual void OnDestroy()
    {
        m_LuaOnDestroy?.Invoke(m_LuaTable);
        Dispose();
    }

    protected virtual void Dispose()
    {
        m_LuaAwake = null;
        m_LuaStart = null;
        m_LuaOnDestroy = null;
        m_LuaUpdate = null;
        m_LuaPath = null;
        //release lua listeners when them exists;

        //release lua table
        if (m_LuaTable != null)
        {
            m_LuaTable.Dispose();
            m_LuaTable = null;
        }
    }
}

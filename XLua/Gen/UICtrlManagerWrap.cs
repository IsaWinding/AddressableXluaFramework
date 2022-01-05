#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class UICtrlManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UICtrlManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 10, 6, 6);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "OpenLoginPanel", _m_OpenLoginPanel_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ShowMessage", _m_ShowMessage_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CloseMessage", _m_CloseMessage_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "QueueOpen", _m_QueueOpen_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CloseTopQueueUI", _m_CloseTopQueueUI_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OpenBaseUI", _m_OpenBaseUI_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CloseTopBaseUI", _m_CloseTopBaseUI_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RevokeAll", _m_RevokeAll_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RevokeToHomeBaseUI", _m_RevokeToHomeBaseUI_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "BasePolicy", _g_get_BasePolicy);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "QueuePolicy", _g_get_QueuePolicy);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "MessageQueuePolicy", _g_get_MessageQueuePolicy);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DefaultOpen", _g_get_DefaultOpen);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DiableOpen", _g_get_DiableOpen);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DestoryOpen", _g_get_DestoryOpen);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "BasePolicy", _s_set_BasePolicy);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "QueuePolicy", _s_set_QueuePolicy);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "MessageQueuePolicy", _s_set_MessageQueuePolicy);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DefaultOpen", _s_set_DefaultOpen);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DiableOpen", _s_set_DiableOpen);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DestoryOpen", _s_set_DestoryOpen);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new UICtrlManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UICtrlManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenLoginPanel_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    UICtrlManager.OpenLoginPanel(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ShowMessage_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _pMessage = LuaAPI.lua_tostring(L, 1);
                    
                    UICtrlManager.ShowMessage( _pMessage );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseMessage_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    UICtrlManager.CloseMessage(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_QueueOpen_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UICtrlBase>(L, 1)&& translator.Assignable<System.Action>(L, 2)&& translator.Assignable<OpenData>(L, 3)) 
                {
                    UICtrlBase _ctrl = (UICtrlBase)translator.GetObject(L, 1, typeof(UICtrlBase));
                    System.Action _pCB = translator.GetDelegate<System.Action>(L, 2);
                    OpenData _pOpenData = (OpenData)translator.GetObject(L, 3, typeof(OpenData));
                    
                    UICtrlManager.QueueOpen( _ctrl, _pCB, _pOpenData );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UICtrlBase>(L, 1)&& translator.Assignable<System.Action>(L, 2)) 
                {
                    UICtrlBase _ctrl = (UICtrlBase)translator.GetObject(L, 1, typeof(UICtrlBase));
                    System.Action _pCB = translator.GetDelegate<System.Action>(L, 2);
                    
                    UICtrlManager.QueueOpen( _ctrl, _pCB );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UICtrlManager.QueueOpen!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseTopQueueUI_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    UICtrlManager.CloseTopQueueUI(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenBaseUI_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UICtrlBase>(L, 1)&& translator.Assignable<System.Action>(L, 2)&& translator.Assignable<OpenData>(L, 3)) 
                {
                    UICtrlBase _ctrl = (UICtrlBase)translator.GetObject(L, 1, typeof(UICtrlBase));
                    System.Action _pCB = translator.GetDelegate<System.Action>(L, 2);
                    OpenData _pOpenData = (OpenData)translator.GetObject(L, 3, typeof(OpenData));
                    
                    UICtrlManager.OpenBaseUI( _ctrl, _pCB, _pOpenData );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<UICtrlBase>(L, 1)&& translator.Assignable<System.Action>(L, 2)) 
                {
                    UICtrlBase _ctrl = (UICtrlBase)translator.GetObject(L, 1, typeof(UICtrlBase));
                    System.Action _pCB = translator.GetDelegate<System.Action>(L, 2);
                    
                    UICtrlManager.OpenBaseUI( _ctrl, _pCB );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UICtrlManager.OpenBaseUI!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseTopBaseUI_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    UICtrlManager.CloseTopBaseUI(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RevokeAll_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    UICtrlManager.RevokeAll(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RevokeToHomeBaseUI_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    UICtrlManager.RevokeToHomeBaseUI(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BasePolicy(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, UICtrlManager.BasePolicy);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_QueuePolicy(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, UICtrlManager.QueuePolicy);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MessageQueuePolicy(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, UICtrlManager.MessageQueuePolicy);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DefaultOpen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, UICtrlManager.DefaultOpen);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DiableOpen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, UICtrlManager.DiableOpen);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DestoryOpen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, UICtrlManager.DestoryOpen);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BasePolicy(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    UICtrlManager.BasePolicy = (UICtrlPolicy)translator.GetObject(L, 1, typeof(UICtrlPolicy));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_QueuePolicy(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    UICtrlManager.QueuePolicy = (UICtrlPolicyQueue)translator.GetObject(L, 1, typeof(UICtrlPolicyQueue));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MessageQueuePolicy(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    UICtrlManager.MessageQueuePolicy = (UICtrlPolicyQueue)translator.GetObject(L, 1, typeof(UICtrlPolicyQueue));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DefaultOpen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    UICtrlManager.DefaultOpen = (OpenData)translator.GetObject(L, 1, typeof(OpenData));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DiableOpen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    UICtrlManager.DiableOpen = (OpenData)translator.GetObject(L, 1, typeof(OpenData));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DestoryOpen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    UICtrlManager.DestoryOpen = (OpenData)translator.GetObject(L, 1, typeof(OpenData));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}

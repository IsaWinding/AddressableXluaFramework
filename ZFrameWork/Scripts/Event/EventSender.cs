using System;
using System.Collections.Generic;

namespace EventG
{
	public class EventSender<TKey, TValue>
	{

		private Dictionary<TKey, Action<TValue>> dic = new Dictionary<TKey, Action<TValue>>();

		public void AddListener(TKey pKey, Action<TValue> pAction)
		{
			Action<TValue> _cbs;
			if (dic.TryGetValue(pKey, out _cbs))
			{
				dic[pKey] = _cbs + pAction;
			}
			else
			{
				dic.Add(pKey, pAction);
			}
		}
		public void RemoveListener(TKey pKey, Action<TValue> pAction)
		{
			Action<TValue> _cbs;
			if (dic.TryGetValue(pKey, out _cbs))
			{
				_cbs = (Action<TValue>)Delegate.Remove(_cbs, pAction);
				if (_cbs == null)
				{
					dic.Remove(pKey);
				}
				else
				{
					dic[pKey] = _cbs;
				}
			}
		}
		public bool HasListener(TKey pKey)
		{
			return dic.ContainsKey(pKey);
		}

		public void SendMessage(TKey pKey, TValue pValue)
		{
			Action<TValue> _cbs;
			if (dic.TryGetValue(pKey, out _cbs))
			{
				_cbs.Invoke(pValue);
			}
		}
		public void Clear()
		{
			dic.Clear();
		}

	}
}
	

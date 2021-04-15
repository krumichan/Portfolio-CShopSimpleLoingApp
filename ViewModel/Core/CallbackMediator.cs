using System;
using System.Collections.Generic;

namespace LoginApp.ViewModel.Core
{
    /// <summary>
    /// 화면 전환을 위한 Callback을 가진 중개자 클래스
    /// </summary>
    public static class CallbackMediator
    {
        // 임의의 topic에 대한 Callback들을 가지고 있는 Dictionary
        private static IDictionary<string, List<Action<object>>> callbackDictionary =
            new Dictionary<string, List<Action<object>>>();

        // 해당 topic에 Callback 추가
        public static void Subscribe(string topic, Action<object> callback)
        {
            if (!callbackDictionary.ContainsKey(topic))
            {
                var callbacks = new List<Action<object>>();
                callbacks.Add(callback);
                callbackDictionary.Add(topic, callbacks);
            }
            else
            {
                bool isExists = false;
                foreach (var callable in callbackDictionary[topic])
                {
                    if (callable.Method.ToString() == callback.Method.ToString())
                    {
                        break;
                    }
                }

                if (!isExists)
                {
                    callbackDictionary[topic].Add(callback);
                }
            }
        }

        // 해당 topic에 Callback 삭제
        public static void Unsubscribe(string topic, Action<object> callback)
        {
            if (callbackDictionary.ContainsKey(topic))
            {
                callbackDictionary[topic].Remove(callback);
            }
        }

        // 해당 topic의 모든 Callback 호출
        public static void Notify(string topic, object args = null)
        {
            if (callbackDictionary.ContainsKey(topic))
            {
                foreach (var callable in callbackDictionary[topic])
                {
                    callable(args);
                }
            }
        }
    }
}

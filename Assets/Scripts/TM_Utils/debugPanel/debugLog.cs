/*
    debugLog.cs

    Tony Monckton (c) 2019
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TM.Utils {
 #if UNITY_EDITOR
        public class MessageLog {
            public string name;
            public string category;
            public string key;
            public string text;

            public MessageLog() {
                name = "";
                category = "";
                key = "";
                text = "";
            }
        }

        public class CategoryLog {
            public string category;
            public bool expanded;

            public CategoryLog() {
                category = "";
                expanded = false;
            }
        }

        public class debugLog
        {
                static debugLog _instance = null;
                public static debugLog instance { 
                        get { 
                                if (_instance == null) {
                                    _instance = new debugLog();
                                }
                                return _instance; 
                        } 
                }
                
                private debugLog() {}

                // my vars
                private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                public Dictionary<string, MessageLog> messageLog = new Dictionary<string, MessageLog>();
                public Dictionary<string, CategoryLog> categoryLog = new Dictionary<string, CategoryLog>();

                public void Awake()
                {
                    if (_instance != null) {
                            //Destroy(this.gameObject);
                            return;
                    }

                    _instance = this;
                }

                public CategoryLog getCategory( string _key)    {  return categoryLog[_key];  }
                public void setCategory( CategoryLog _cat ) {
                    if (categoryLog[_cat.category] != null ) {
                        categoryLog[_cat.category].category = _cat.category;
                        categoryLog[_cat.category].expanded = _cat.expanded;
                    }
                }
                public void addCategory( CategoryLog _cat ) {
                    if ( categoryLog.ContainsKey(_cat.category) == false) {
                        categoryLog.Add( _cat.category, _cat );
                    }
                }

                public MessageLog getMessage ( string _key ) {  return messageLog[_key];    }
                public void setMessage( MessageLog _msg ) { }

                public void  beginTimer() {
                    stopwatch.Start();
                }

                public void endTimer( string _category, string _name ) {
                    object _message = stopwatch.ElapsedMilliseconds;
                    Log( _category, _name, _message);
                }

                public void Log( string _category, string _name, object _message) {
                        MessageLog message;
                        CategoryLog cat;
                        string key = _category + "_" + _name;

                        if (!categoryLog.ContainsKey(_category)) {
                            cat = new CategoryLog();
                            cat.category = _category;
                            cat.expanded = false;
                            categoryLog.Add(_category, cat);
                        }

                        if (!messageLog.ContainsKey(key)) {
                                message             = new MessageLog();
                                message.name        = _name;
                                message.category    = _category;
                                message.key         = key;
                                message.text        = _message.ToString();
                                messageLog.Add(key, message);
                        } else {
                                message = messageLog[key];
                                message.text = _message.ToString();
                        } 
                }

                void OnDestroy() { 
                    messageLog.Clear();  
                }
        }
#endif
}
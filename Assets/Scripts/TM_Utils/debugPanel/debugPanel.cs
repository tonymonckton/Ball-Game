/*
    debugPanel.cs

    Tony Monckton (c) 2019
 */

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TM.Utils {

#if UNITY_EDITOR
    public class debugPanel : EditorWindow
    { 
        private GUIStyle guiStyle = new GUIStyle();
        private GUIStyle guiStyleFoldout = new GUIStyle();

        Dictionary<string, bool> catToggle = new Dictionary<string, bool>();
        List<bool> catExpaned = new List<bool>();

        [MenuItem("TM Utils/Debug Panel")]
        public static void ShowWindow()
        {
            GetWindow<debugPanel>(false, "DebugPanel", true);
        }

        void OnGUI()
        {
            guiStyle.fontSize = 15;
            guiStyle.margin = new RectOffset(15,5,2,2);

            guiStyleFoldout.fontSize = 15;
            guiStyleFoldout.fontStyle = FontStyle.Bold;
            guiStyleFoldout = "Foldout";

            // dropdown
            EditorGUILayout.BeginVertical();
                Dictionary<string, MessageLog>msgs =  debugLog.instance.messageLog;
                var ordered = msgs.OrderBy(x => x.Value.category);
                string cat = "";
                bool e = false;
                foreach(KeyValuePair<string, MessageLog> entry in ordered)
                {
                    if ( cat != entry.Value.category) {
                        cat = entry.Value.category;
                        if ( catToggle.ContainsKey(cat) == false) {
                            catToggle.Add(cat, true);
                        }
                        e = GUILayout.Toggle(catToggle[cat], cat, guiStyleFoldout, GUILayout.ExpandWidth(false));
                        catToggle[cat] = e;
                    }

                    if (e) {
                            string msg = entry.Value.name + ": " + entry.Value.text;
                            GUILayout.Label(msg, guiStyle);
                    }
                }
            EditorGUILayout.EndVertical();

            GUILayout.FlexibleSpace();
                EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUI.backgroundColor = Color.grey;
            GUI.color = Color.white;

            // reset things...
            if (GUILayout.Button("Reset", GUILayout.Width(100), GUILayout.Height(30)))
            {

            }
            EditorGUILayout.EndHorizontal();
        }

        public void OnInspectorUpdate()
        {
            this.Repaint();
        }
    }
#endif
}
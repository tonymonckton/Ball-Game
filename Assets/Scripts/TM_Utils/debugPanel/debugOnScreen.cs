using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TM.Utils;

public class debugOnScreen : MonoBehaviour
{
    private string newline = "\r\n"; 
    public  Text textPanel;

    void Update()
    {
#if UNITY_EDITOR
        if (textPanel != null) {
            string message = "";
            textPanel.text = "";

            Dictionary<string, MessageLog>msgs =  debugLog.instance.messageLog;
            var ordered = msgs.OrderBy(x => x.Value.category);
            string cat = "";
            foreach(KeyValuePair<string, MessageLog> entry in ordered)
            {
                if ( cat != entry.Value.category) {
                    cat = entry.Value.category;
                    message += ("<b>"+cat+"</b>"+newline);
                }

                string msg = "    " + entry.Value.name + ": " + entry.Value.text;
                message += (msg+newline);
            }

            textPanel.text = message;
        }
#endif
    }
}

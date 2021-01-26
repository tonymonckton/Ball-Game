using UnityEngine;
using System.Collections;

public interface IListener {
	void OnEvent(string name, object param = null);
}

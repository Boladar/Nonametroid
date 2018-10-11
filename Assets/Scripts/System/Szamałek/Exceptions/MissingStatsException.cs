using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingStatsException : System.Exception {
	public MissingStatsException (string message)
		: base (message) {}
}

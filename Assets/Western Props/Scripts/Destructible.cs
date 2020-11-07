// --------------------------------------
// This script is totally optional. It is an example of how you can use the
// destructible versions of the objects as demonstrated in my tutorial.
// Watch the tutorial over at http://youtube.com/brackeys/.
// --------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public GameObject destroyedVersion;	// Reference to the shattered version of the object

	// If the player clicks on the object
	public void DestroyGM()
	{
		// Spawn a shattered object
		Destroy(Instantiate(destroyedVersion, transform.position, transform.rotation),8f);
		// Remove the current object
		Destroy(gameObject);
	}

}

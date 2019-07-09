using UnityEngine;
using System.Collections;
using clsurgutils = U_r_g_utils.clsurgutils;

/// <summary>
/// 2015-04-01
/// ULTIMATE RAGDOLL GENERATOR V4.6
/// © THE ARC GAMES STUDIO 2015
/// 
/// <para>Basic class to test a gameobject's rigidbodies against the dismemberator utility, to determine if compilation and separation complete successfully for all parts.</para>
/// <para>Apply this class to a 'spectator' gameobject (for example the camera), since it will be used to destroy and respawn the target gameobject if so desired.</para>
/// </summary>
public class clsdismemberatortester : MonoBehaviour {
	/// <summary>
	/// The model to test. Can be used from the scene, but needs to be in the project for reinstantiation to work.
	/// </summary>
	public GameObject vargammodel;
	/// <summary>
	/// Spawn position for the reinstantiation
	/// </summary>
	public Vector3 vargamspawnposition = new Vector3(0,0,0);
	
	/// <summary>
	/// The rigidbody array that's used to detach ragdolled parts
	/// </summary>
	private Rigidbody[] varDbodies = new Rigidbody[0];
	/// <summary>
	/// The instanced model
	/// </summary>
	private GameObject varmodelinstance;

	void Start() {
		if (vargammodel != null) {
			if(vargammodel.transform.root == transform.root) {
				Debug.LogError("Can't host the tester on the target.\nPlease host the tester in a persistent scene object (for example the main camera).", gameObject);
				return;
			}
			metinstantiatemodel();
			varDbodies = varmodelinstance.GetComponentsInChildren<Rigidbody>();
			if (varDbodies.Length == 0) {
				Debug.LogError("There's no rigidbodies to test in the chosen target: make sure it's ragdolled and prefabbed.");
			}
		}
		else {
			Debug.LogError("Please assign a model to be able to test its separation.", transform);
		}
	}

	void OnGUI() {
		if (vargammodel != null) {
			for (int varbodycounter = 0; varbodycounter < varDbodies.Length; varbodycounter++) {
				if (varDbodies[varbodycounter] != null) {
					if (GUILayout.Button("Separate " + varDbodies[varbodycounter].name)) {
						clsdismemberator varD = varmodelinstance.GetComponentInChildren<clsdismemberator>();
						if (varD != null) {
							clsurgutils.metdismember(varDbodies[varbodycounter].transform, varD.vargamstumpmaterial, varD, varD.vargamparticleparent, varD.vargamparticlechild, true, true);
						}
						else {
							Debug.LogError("There's no dismemberator in the specified target.");
						}
					}
				}
			}
			if (GUILayout.Button("Reistantiate [" + vargammodel.name + "]")) {
				Destroy(varmodelinstance);
				metinstantiatemodel();
				varDbodies = varmodelinstance.GetComponentsInChildren<Rigidbody>();
				Debug.Log("Reinstantiation complete.\n(please make sure vargammodel is a PREFAB)", varmodelinstance);
			}
		}
		else {
			GUILayout.Label("A dismemberator tester is on-scene, but it has no model to test.");
		}
	}
	
	/// <summary>
	/// Instantiate the model
	/// </summary>
	private void metinstantiatemodel() {
		varmodelinstance = Instantiate(vargammodel, transform.position, Quaternion.identity) as GameObject;
		Transform varinstancetransform = varmodelinstance.transform;
		varinstancetransform.position = vargamspawnposition;
		varinstancetransform.rotation = Quaternion.identity;
		varinstancetransform.parent = null;
	}
}

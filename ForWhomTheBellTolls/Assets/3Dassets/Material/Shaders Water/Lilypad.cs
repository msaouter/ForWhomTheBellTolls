using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Lilypad : MonoBehaviour
{
    //sert pour le verrou pour inc nb_id
    static Object obj_lock = new Object();

    static int nb_id = 0;
    int id;
    Material mat;

    public void setId(int id)
    {
        this.id = id;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //pour id 
        lock (obj_lock)
        {
            id = nb_id;
            Interlocked.Increment(ref nb_id);
        }
        mat = GetComponent<MeshRenderer>().material;
        mat.SetFloat("_Id", id); 
    }
}

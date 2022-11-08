using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomPoint : MonoBehaviour
{
    public MeshCollider lookupCollider;

    public bool bangGetPoint;
    private Vector3 randomPoint;

    private GameObject particleSystemPrefab;
    private Transform particlesParent;

    private ParticleSystem literacyParticleSystem, energyConsumptionParticleSystem, averageWageParticleSystem;

    private float particleSize = 0.5f;
    private float particleLife = 360f;

    private Color literacyColor = Color.green;
    private Color energyConsumptionColor = Color.yellow;
    private Color averageWageColor = Color.blue;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        lookupCollider = gameObject.AddComponent<MeshCollider>();
        initDone = true;
    }
    public int currentValue = 0;
    public string currentYear = "";
    public string dataType = "";
    public void SetValues(GameObject _particleSystemPrefab, Transform _particlesParent)
    {
        particleSystemPrefab = _particleSystemPrefab;
        particlesParent = _particlesParent;
        InstantiateParticleSystem();
    }

    public void ClearLiteracyParticles()
    {
        if(literacyParticleSystem.particleCount>0)
            literacyParticleSystem.Clear();
    }

    public void ClearEnergyParticles()
    {
        if (energyConsumptionParticleSystem.particleCount > 0)
            energyConsumptionParticleSystem.Clear();
    }

    public void ClearWageParticles()
    {
        if (averageWageParticleSystem.particleCount > 0)
            averageWageParticleSystem.Clear();
    }

    private void InstantiateParticleSystem()
    {
        GameObject scannerParticleInst1 = (GameObject)UnityEngine.Object.Instantiate(particleSystemPrefab, transform.position, transform.rotation);
        scannerParticleInst1.transform.parent = particlesParent.transform;
        scannerParticleInst1.gameObject.name = "literacy";
        this.literacyParticleSystem = scannerParticleInst1.GetComponent<ParticleSystem>();

        GameObject scannerParticleInst2 = (GameObject)UnityEngine.Object.Instantiate(particleSystemPrefab, transform.position, transform.rotation);
        scannerParticleInst2.transform.parent = particlesParent.transform;
        scannerParticleInst2.gameObject.name = "energy";
        this.energyConsumptionParticleSystem = scannerParticleInst2.GetComponent<ParticleSystem>();

        GameObject scannerParticleInst3 = (GameObject)UnityEngine.Object.Instantiate(particleSystemPrefab, transform.position, transform.rotation);
        scannerParticleInst3.transform.parent = particlesParent.transform;
        scannerParticleInst3.gameObject.name = "wage";
        this.averageWageParticleSystem = scannerParticleInst3.GetComponent<ParticleSystem>();

        
        //var random = new System.Random();
        //var color = String.Format("#{0:X6}", random.Next(0x1000000));
        //ColorUtility.TryParseHtmlString(color + "ff", out literacyColor);
        //Debug.Log(col);


        literacyColor = Color.blue;
        energyConsumptionColor = Color.yellow; //Color.magenta;
        averageWageColor = Color.yellow;// Color.blue;
    }
    private bool initDone = false;
    ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();

    private void InvokePoints(Color col,string year,int count, string type, bool randomColor = false)
    {
        currentValue = count;
        currentYear = year;
        dataType = type;

        if (randomColor)
        {
            literacyColor = energyConsumptionColor = averageWageColor = col;
        }
        //if (type == "energy" && count > 100)
        //{
        //    count = 100;
        //}
        if(count > 100)
        {
            count = 100;
            particleSize = 0.1f;
        }
        //Debug.Log(count);
        for (int i = 0; i < count; i++)
        {
            if (!initDone)
            {
                lookupCollider = gameObject.AddComponent<MeshCollider>();
                initDone = true;
            }
            Vector3 randomPoint = GetRandomPointOnMesh(lookupCollider.sharedMesh);
            randomPoint += lookupCollider.transform.position;

            emitParams.position = randomPoint;
            emitParams.velocity = Vector3.zero;
            emitParams.startSize = particleSize;
            //emitParams.startLifetime = UnityEngine.Random.Range(-particleLife * 0.1f, particleLife * 0.1f);

            switch (type)
            {
                case "literacy":
                    //Debug.Log("literacy");
                    emitParams.startColor = literacyColor;
                    //ClearLiteracyParticles();
                    literacyParticleSystem.Emit(emitParams, 1);
                    break;
                case "energy":
                    //Debug.Log("energyConsumptionColor");
                    emitParams.startColor = energyConsumptionColor;
                    //ClearEnergyParticles();
                    energyConsumptionParticleSystem.Emit(emitParams, 1);
                    break;
                case "wage":
                    emitParams.startColor = averageWageColor;
                    //ClearWageParticles();
                    averageWageParticleSystem.Emit(emitParams, 1);
                    break;
                default:
                    emitParams.startColor = literacyColor;
                    literacyParticleSystem.Emit(emitParams, 1);
                    break;
            }

        }
        //gameObject.SetActive(false);
    }

    public void GeneratePoints(Color col, string year, int count,string type, bool randomColor = false)
    {
        InvokePoints(col,year, count, type,randomColor);
    }

    float[] sizes;
    float[] cumulativeSizes;
    Vector3 GetRandomPointOnMesh(Mesh mesh)
    {
         sizes = GetTriSizes(mesh.triangles, mesh.vertices);
        cumulativeSizes = new float[sizes.Length];
        float total = 0;

        for (int i = 0; i < sizes.Length; i++)
        {
            total += sizes[i];
            cumulativeSizes[i] = total;
        }

        //so everything above this point wants to be factored out

        float randomsample = UnityEngine.Random.value * total;

        int triIndex = -1;

        for (int i = 0; i < sizes.Length; i++)
        {
            if (randomsample <= cumulativeSizes[i])
            {
                triIndex = i;
                break;
            }
        }

        if (triIndex == -1) Debug.LogError("triIndex should never be -1");

        Vector3 a = mesh.vertices[mesh.triangles[triIndex * 3]];
        Vector3 b = mesh.vertices[mesh.triangles[triIndex * 3 + 1]];
        Vector3 c = mesh.vertices[mesh.triangles[triIndex * 3 + 2]];

        //generate random barycentric coordinates

        float r = UnityEngine.Random.value;
        float s = UnityEngine.Random.value;

        if (r + s >= 1)
        {
            r = 1 - r;
            s = 1 - s;
        }
        //and then turn them back to a Vector3
        Vector3 pointOnMesh = a + r * (b - a) + s * (c - a);
        return pointOnMesh;

    }

    float[] GetTriSizes(int[] tris, Vector3[] verts)
    {
        int triCount = tris.Length / 3;
        float[] sizes = new float[triCount];
        for (int i = 0; i < triCount; i++)
        {
            sizes[i] = .5f * Vector3.Cross(verts[tris[i * 3 + 1]] - verts[tris[i * 3]], verts[tris[i * 3 + 2]] - verts[tris[i * 3]]).magnitude;
        }
        return sizes;

        /*
         * 
         * more readably:
         * 
for(int ii = 0 ; ii < indices.Length; ii+=3)
{
    Vector3 A = Points[indices[ii]];
    Vector3 B = Points[indices[ii+1]];
    Vector3 C = Points[indices[ii+2]];
    Vector3 V = Vector3.Cross(A-B, A-C);
    Area += V.magnitude * 0.5f;
}
         * 
         * 
         * */
    }

    
    
}

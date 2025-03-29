using UnityEngine;

public class CorpseManager : MonoBehaviour
{
    public static CorpseManager instance;
    [SerializeField] private Transform _corpsePoint;
    private GameObject corpse;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject GetCorpse() { return corpse; }

    public void SetCorpse(GameObject corpse)
    {
        if (this.corpse == null)
        {
            this.corpse = corpse;
            corpse.transform.parent = _corpsePoint;
            corpse.transform.position = _corpsePoint.position;
        }
    }

    public void DeleteCorpse()
    {
        if (corpse != null)
        {
            Destroy(corpse);
        }
        corpse = null;
    }
}

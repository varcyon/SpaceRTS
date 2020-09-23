using TMPro;
using UnityEngine;

public class PlayerResourcesUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI ironAmount;
    [SerializeField] TextMeshProUGUI nickelAmount;
    [SerializeField] TextMeshProUGUI platniumAmount;
    [SerializeField] TextMeshProUGUI goldAmount;
    [SerializeField] TextMeshProUGUI rhodiumAmount;

    private void Update()
    {
        ironAmount.text = PlayerResources.r.resources.Find(x => x.eName == "Iron").amount.ToString();
        nickelAmount.text = PlayerResources.r.resources.Find(x => x.eName == "Nickel").amount.ToString();
        platniumAmount.text = PlayerResources.r.resources.Find(x => x.eName == "Platnium").amount.ToString();
        goldAmount.text = PlayerResources.r.resources.Find(x => x.eName == "Gold").amount.ToString();
        rhodiumAmount.text = PlayerResources.r.resources.Find(x => x.eName == "Rhodium").amount.ToString();

    }

}

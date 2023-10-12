using UnityEngine;
public class AbilityHandler : MonoBehaviour{
    [SerializeField] private BaseAbility[] AvailableAbilities = new BaseAbility[4];
    [SerializeField] public PlayerAbilityInputHandler InputHandler;
    public BaseAbility ActiveAbility {get;private set;}
    public void Awake(){
        //AvailableAbilities = GetComponentsInChildren<BaseAbility>();
        
        if(InputHandler == null) {
            Debug.Log("Ability Input Handler is missing");
            return;
        }
        InputHandler.OnAbilityChanged += (inputIndex) => ChangeAbility(inputIndex);

    }
    public void Start(){
        if(AvailableAbilities == null || AvailableAbilities[0] == null) return;
        ActiveAbility = AvailableAbilities[0];
    }
    public void ChangeAbility(int input){
        if(AvailableAbilities == null 
        || input >= AvailableAbilities.Length 
        || AvailableAbilities[input] == null) return;

        // ActiveAbility.gameObject.SetActive(false);
        ActiveAbility = AvailableAbilities[input];
        // ActiveAbility.gameObject.SetActive(true);
    }
}
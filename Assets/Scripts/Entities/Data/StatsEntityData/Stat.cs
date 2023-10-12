[System.Serializable]
public class Stat{
    public float HitPoint;
    public float Energy;
    public float Defense;
    public float Damage;
    public float AttackSpeed;
    public Stat(){
        HitPoint = 1000;
        Energy = 200;
        Defense = 10;
        Damage = 200;
        AttackSpeed = 2;
    }
}

[System.Serializable]
public class PlayerStat : Stat{
    public float CurrentExp;
    public int CurrentLevel; 
    public PlayerStat() : base(){
        CurrentLevel = 1;
    }
}

[System.Serializable]
public class EnemyStat: Stat{
    public float Exp;
    public EnemyStat() : base(){
        Exp = 0; 
    }
}
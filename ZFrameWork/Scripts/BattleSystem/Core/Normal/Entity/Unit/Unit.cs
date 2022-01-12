using System.Collections.Generic;
using UnityEngine;
using Battle.Skill;
using UnityEngine.AI;
public class Unit:IDriver
{
    public int Id;
    public bool PlayerOwer;
    public float followDistance { get { return AttributeInfo.GetAttributeItem(AttributeType.FlowRange).Value.Value; } }
    public float atkDistance { get { return AttributeInfo.GetAttributeItem(AttributeType.AtkRange).Value.Value; } }
    public float mp { get { return AttributeInfo.GetAttributeItem(AttributeType.Mp).Value.Value; } }
    public CampType campType { get { return StateInfo.campType; } }
    private float atkCd { get { return AttributeInfo.GetAttributeItem(AttributeType.AtkCd).Value.Value; } }
    private float atk { get { return AttributeInfo.GetAttributeItem(AttributeType.Atk).Value.Value; } }
    private float warnDistance { get { return AttributeInfo.GetAttributeItem(AttributeType.WarnRange).Value.Value; } }
    
    private bool IsNeedBackToHome = false;
    private Vector3 findTargetPos;
    private Vector3 targetPos;
    private float nextAtkTime;
    private float nextTime = 0;
    private Unit Target;
    private AttributeComponent AttributeInfo;
    private StateInfo StateInfo;
    private SkillInfo skillInfo;
    private AIPath path;
    private SkillReleaser skillReleaser;
    private GameObject model;
    private HpBar hpBar;
    private GameObject unit;
    private UnitAgent unitAgent;
    private UnitAnimation unitAni;
    private AIPolicy aiPolicy;
    private bool isDead = false;
    private System.Action<int> onDead;
    public void OnCostMp(float pCost) {
        var mpValue = AttributeInfo.GetAttributeItem(AttributeType.Mp);
        mpValue.Value.AddValue(-pCost);
    }
    public bool IsInAreaRange(AreaType pAreaType, float pRange,Vector3 pAtkPos) {
        if (pAreaType == AreaType.Sphere)
        {
            return Vector3.Distance(this.unit.transform.position, pAtkPos) <= pRange;
        }
        return false;
    }
    public bool IsCanSelect(CampType pAtkCamp,int pAtkUId,TargetSelectType pSelectType) {
        if (!IsDead()) {
            if (pSelectType == TargetSelectType.Self)
            {
                return pAtkUId == this.Id;
            }
            else if (pSelectType == TargetSelectType.Friend) {
                return pAtkCamp == this.campType;
            }
            else if (pSelectType == TargetSelectType.Enemy) {
                return pAtkCamp != this.campType;
            }
        }
        return false;
    }
    public Unit(int pId, AttributeComponent pAttributeInfo, StateInfo pStateInfo) {
        Id = pId;
        AttributeInfo = pAttributeInfo;
        StateInfo = pStateInfo;
    }
    public void SetOnDeadCB(System.Action<int> pOnDead) {onDead = pOnDead;}
    public void SetPlayerOwer(bool pPlayerOwer) {
        this.PlayerOwer = pPlayerOwer;}
    public void LoadAiPath(AIPath pPath) {path = pPath;}
    public void SetSkillInfo(SkillInfo pSkillInfo) {
        skillInfo = pSkillInfo;
    }
    public void ReleaseSkill(int pSkillId,System.Action pFinish) {
        var skill = skillInfo.GetSkill(pSkillId);
        if (skillReleaser.IsCanRelease(skill)){
            unitAgent.SetNavMeshAgentEnable(false);
            skillReleaser.ReleaseSkill(skill, () => {
                unitAgent.SetNavMeshAgentEnable(true);
                pFinish?.Invoke();
            });
        }
    }
    public void LoadAIPolicy(AIPolicyType pAiType) {
        if (pAiType == AIPolicyType.Soldier){
            aiPolicy = new SoldierPolicy(this);
        }
        else if (pAiType == AIPolicyType.Tower){
            aiPolicy = new TowerPolicy(this);
        }
        else if (pAiType == AIPolicyType.Monster){
            aiPolicy = new MonsterPolicy(this);
        }
    }
    public bool IsDead(){return isDead;}
    public void OnAiAction(float pTime, float pDeltaTime,List<Unit> pAllUnits) {
        if (aiPolicy != null && !IsDead()){
            aiPolicy.OnRun(pAllUnits);
        }
    }
    public void OnRun(float pTime, float pDeltaTime){
        if (pTime >= nextTime){
            nextTime += 1;
            OnSecondRepeat();
        }
    }
    public void AutoAttackTarget(Unit pTarget)
    {
        var targetUnit = pTarget;
        SetCurTarget(pTarget);
        if (targetUnit != null && !targetUnit.IsDead())
        {
            if (targetUnit.campType != campType)
            {
                if (CurTargetInRange(atkDistance))
                {
                    SelfAtk(()=> {
                        AutoAttackTarget(Target);
                    });
                }
                else
                {
                    MoveToTargetPos(targetUnit.GetPos(), () => {
                        SelfAtk(()=> {
                            AutoAttackTarget(Target);
                        });
                    });
                }
            }
            else
            {
                MoveToTargetPos(targetUnit.GetPos(), () => {
                    Idle();
                });
            }
        }
    }
    public void OnInit() {
        if(unit == null){
            unit = new GameObject("Unit"+Id);
            unit.transform.SetParent(BattleField.Instance.GlobalRoot.transform);
            unit.transform.SetPositionAndRotation(StateInfo.GetPos(), Quaternion.identity);
            unitAgent = unit.AddComponent<UnitAgent>();
            var unitTag = unit.AddComponent<UnitTag>();
            unitTag.Id = this.Id;
            unitTag.PlayerOwer = this.PlayerOwer;
            unit.AddComponent<UnityEngine.Playables.PlayableDirector>();
            skillReleaser = unit.AddComponent<SkillReleaser>();
            skillReleaser.BindUnit(this);

        }
        if (model == null){
            model = AssetManager.Instance.Instantiate(StateInfo.Model);
            model.transform.SetParent(unit.transform,false);
            model.transform.localPosition = Vector3.zero;
            model.transform.rotation = Quaternion.identity;
            model.name = "Model";
        }
        if(hpBar == null){
            var barGo = AssetManager.Instance.Instantiate(StateInfo.HpBar);
            barGo.transform.SetParent(unit.transform);
            barGo.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            hpBar = barGo.GetComponent<HpBar>();
            var hp = AttributeInfo.GetAttributeItem(AttributeType.Hp);
            hpBar.SetHp(hp.Value.Value,hp.Value.Max);
            hpBar.SetHeight(StateInfo.modelHeight);
            hpBar.BindValueItem(AttributeInfo.GetAttributeItem(AttributeType.Hp).Value);
        }
       
        unitAni = this.model.gameObject.GetComponent<UnitAnimation>();
        isDead = false;
    }
    public void OnLoopMove(){
        if (path.IsReachCurPoint(this.model.transform.position)){
            path.OnReachNextPoint();
        }
        var nextPos = path.GetNextPoint();
        if (nextPos != null)
            MoveToTargetPos(nextPos.pos);
        else
            Idle();
    }
    public void Idle(){
        SetCurTarget(null);
        StopMove();
        unitAni.PlayAniByType(AniNameType.Idle, 1);
    }
    public bool IsHaveTarget() { return Target != null; }
    public bool CurTargetInRange(float pRange){
        float oDistance;
        if (Target != null && !Target.IsDead()){
            return IsInRange(Target, pRange, out oDistance);
        }
        return false;
    }
    public bool NeedSelectTarget(List<Unit> pAllUnits)//是否需要重新选择追踪目标
    {
        float oDistance = 0;
        if (Target != null && !Target.IsDead() && IsInRange(Target, followDistance,out oDistance))
            return false;
        var unit = GetOneUnitInRange(pAllUnits, warnDistance);
        return unit != null;
    }
    public void OnDestory() {
        if (model != null){
            AssetManager.Instance.FreeGameObject(model);
            model = null;}
        if (hpBar != null){
            AssetManager.Instance.FreeGameObject(hpBar.gameObject);
            hpBar = null;}
        if (unit != null){
            GameObject.Destroy(unit);
            unit = null;}
    }
    public bool NeedBackToHome(){
        if (IsNeedBackToHome)
            return true;
        if (Target == null)
            return false;
        if (Target.IsDead())
            return true;
        var distance = Vector3.Distance(this.unit.transform.position, findTargetPos);
        return distance >= followDistance;
    }
    public void SetCurTarget(Unit pTarget){
        Target = pTarget;
        if(pTarget != null)
            findTargetPos = this.unit.transform.position;
    }
    private void SelfAtk(System.Action pOnFinish = null) {
        var curTime = Time.realtimeSinceStartup;
        FaceToCurTarget();
        //Debug.LogError("curTime"+ curTime);
       // Debug.LogError("nextAtkTime" + nextAtkTime);
        if (curTime >= nextAtkTime){
            nextAtkTime = curTime + atkCd;
            unitAni.PlayAniByType(AniNameType.Attack, 4, true, () =>{
                DoNormalAttack(null, null);
            });
            BattleField.Instance.AddDelayAction(atkCd+0.01f, pOnFinish);
        }
    }
    public void Attack(System.Action pOnFinish = null) {
        var curTime = Time.realtimeSinceStartup;
        if (curTime >= nextAtkTime){
            nextAtkTime = curTime + atkCd;
            unitAni.PlayAniByType(AniNameType.Attack, 4, false, () =>{
                DoNormalAttack(null, null);
                if (pOnFinish != null){
                    pOnFinish.Invoke();
                }
            });
        }
    }
    public void DoNormalAttack(System.Action pAtkCB, System.Action pKillCB){
        if (Target != null){
            Target.OnDamage(atk, pAtkCB, pKillCB);
        }
    }
    public bool IsCanCampSelect(CampType pTargetCampType){
        if (campType == CampType.PlayerA){
            return pTargetCampType == CampType.PlayerB || pTargetCampType == CampType.Monster;
        }
        else if (campType == CampType.PlayerB){
            return pTargetCampType == CampType.PlayerA || pTargetCampType == CampType.Monster;
        }
        else if (campType == CampType.Monster){
            return pTargetCampType == CampType.PlayerA || pTargetCampType == CampType.PlayerB;
        }
        return false;
    }
    private bool IsInRange(Unit pTarget, float pRange,out float oDistance)
    {
        var targetPos = pTarget.model.transform.position;
        var selfPos = this.model.transform.position;
        oDistance = Vector3.Distance(targetPos, selfPos);
        if (oDistance <= pRange)
        {
            return true;
        }
        return false;
    }
    public Unit GetOneUnitInRange(List<Unit> pAllUnits, float pRange)
    {
        Unit result = null;
        float curDistance = 9999;
        for (var i = 0; i < pAllUnits.Count; i++)
        {
            var unit_ = pAllUnits[i];
            float oDistance;
            if (!unit_.IsDead() && IsCanCampSelect(unit_.campType) && IsInRange(unit_, pRange,out oDistance))
            {
                if (oDistance < curDistance)
                {
                    curDistance = oDistance;
                    result = pAllUnits[i];
                }
            }
        }
        return result;
    }
    public void SelectAdjustTarget(List<Unit> pAllUnits)
    {
        var unit = GetOneUnitInRange(pAllUnits, warnDistance);
        SetCurTarget(unit);
    }
    //护甲减免公式
    public static float GetPhyDef(float pDefValue)
    {
        return 0.052f * pDefValue / (0.9f + 0.048f * pDefValue);
    }
    public void OnDmgEvent(Unit pAtkUnit,DamgeEvent pDmgEvent) {
        var dmgValue = pDmgEvent.value;
        if (pDmgEvent.damgeValueType == DamgeValueType.AddPhyAtk)
        {
            dmgValue += pAtkUnit.atk * pDmgEvent.effect;
        }
        OnDamage(dmgValue, null, null);
    }
    public void OnDamage(float pDamage, System.Action pAtkCB, System.Action pKillCB)
    {
        var hpValue = AttributeInfo.GetAttributeItem(AttributeType.Hp);
        var damageRe = GetPhyDef(AttributeInfo.GetAttributeItem(AttributeType.Def).Value.Value);
        var realDamage = pDamage * (1 - damageRe);
        hpValue.Value.AddValue(-realDamage);
        if (hpValue.Value.Value <= 0){
            pKillCB?.Invoke();
            OnDead();
        }
        pAtkCB?.Invoke();
    }
    private void OnDead()
    {
        Target = null;
        isDead = true;
        unitAni.PlayAniByType(AniNameType.Dead, 6,false,()=> {
            if (onDead != null)
            {
                this.unit.SetActive(false);
                onDead.Invoke(Id);
            }
        });
    }
    public void FaceToCurTarget(){
        if (Target != null)
            this.model.transform.LookAt(Target.model.transform.position);
    }
    public void MoveToTargetPos(Vector3 pTargetPos,System.Action pOnReach = null){
        targetPos = pTargetPos;
        if (unitAgent.MoveToTaraget(targetPos, pOnReach)) {
            unitAni.PlayAniByType(AniNameType.Move, 3);
        }
    }
    public Vector3 GetPos() {
        return this.unit.transform.position;
    }
    public void MoToCurTarget(){
        MoveToTargetPos(Target.unit.transform.position);
    }
    public void StopMove() { unitAgent.Stop();}
    public void BackToHome()
    {
        var distance = Vector3.Distance(this.unit.transform.position, findTargetPos);
        if (distance >= 1){
            IsNeedBackToHome = true;
            SetCurTarget(null);
            MoveToTargetPos(findTargetPos);
        }
        else{
            IsNeedBackToHome = false;
        }
    }
    public void OnSecondRepeat()
    {
        AttributeInfo.OnSecondRepeat();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemUI : InteractionUI {
    private void Start() {
        AddDic();
    }
    public void ShowQuestion(DiffucultEnum type) {
        QuestionUI question = InteractionManager.instance.InteractionUIDic[UIType.Problem] as QuestionUI;
        question.Set(type);
        question.IncreaseCnt();
    }
    public override void AddDic() {
        InteractionManager.instance.InteractionUIDic.Add(MyType, this);
    }
}

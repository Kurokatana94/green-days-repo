using UnityEngine;

public class InGameSkillTutorial : MonoBehaviour
{
    private GameMaster gameMaster;
    private GameOverSystem gameOver;
    private bool hasAbility = false;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        gameOver = GameObject.FindGameObjectWithTag("GO").GetComponent<GameOverSystem>();

        foreach (bool isActive in gameMaster.skillActive)
        {
            if (isActive == true)
            {
                hasAbility = true;
                return;
            }
        }

        if(!hasAbility)
        {
            gameMaster.haveSkill[1] = true;
            gameMaster.skillActive[1] = true;
        }
    }

    private void Update()
    {
        if (gameOver.gameOver && !hasAbility)
        {
            gameMaster.haveSkill[1] = false;
            gameMaster.skillActive[1] = false;
        }
    }
}

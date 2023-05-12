using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AfterLevelMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject skillPanel;
    [SerializeField]
    private GameObject levelPanel;

    [SerializeField]
    private Button SkillPage;

    [SerializeField]
    private Button LevelPage;

    private SkillTreeManager tree;


    public void SetTree(SkillTreeManager tree) {

        this.tree = tree;
    }

    public void ShowSkillTree() {

        levelPanel.SetActive(false);
        skillPanel.SetActive(true);
        SkillPage.Select();
        skillPanel.GetComponent<SkillTreeMenu>().Activate(tree);
    }

    public void ShowLevelsTree()
    {
        skillPanel.SetActive(false);
        levelPanel.GetComponent<LevelMenu>().Activate();
        levelPanel.SetActive(true);
    }


}

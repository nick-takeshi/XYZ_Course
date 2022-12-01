using System;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogComponent : MonoBehaviour
{
    [SerializeField] private Mode _mode;
    [SerializeField] private DialogData _bound;
    [SerializeField] private DialogDef _external;

    private DialogueBoxController _dialogBox;
    public void Show()
    {
        if (_dialogBox == null)
            _dialogBox = FindObjectOfType<DialogueBoxController>();

        _dialogBox.ShowDialog(Data);
    }

    public void Show(DialogDef def)
    {
        _external = def;
        Show();
    }

    public DialogData Data
    {
        get
        {
            switch (_mode)
            {
                case Mode.Bound:
                    return _bound;
                case Mode.External:
                    return _external.Data;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum Mode
    {
        Bound,
        External
    }
}

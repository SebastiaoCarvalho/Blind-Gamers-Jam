class Action {

    protected bool isDone = false;
    public bool IsDone {
        get { return isDone; }
    }

    public virtual void execute() {
        // do something
        isDone = true;
    }

}
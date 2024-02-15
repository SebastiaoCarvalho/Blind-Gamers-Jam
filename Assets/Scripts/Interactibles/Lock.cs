public class Lock {
    private string key;
    private Door door;

    public Lock(string key, Door door) {
        this.key = key;
        this.door = door;
        door.AddLock(this);
    }

    public bool Unlock(string key) {
        if (key == this.key) {
            door.OpenLock(this);
            return true;
        }
        return false;
    }
}
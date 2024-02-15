public class Key {
    private string key;
    public string KeyName => key;
    private Lock _lock;

    public Key(string key, Lock lockk) {
        this.key = key;
        this._lock = lockk;
    }

    public void Use() {
        _lock.Unlock(key);
    }

}
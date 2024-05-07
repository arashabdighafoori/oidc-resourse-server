export class CacheService {
  expiration = 5;
  constructor() {}

  async get<T>(group: string, key: string, fn: () => Promise<T>) {
    // if anything is cached already ...
    const cache_reponse = this.search(key);
    if (cache_reponse) {
      // validate if it hasn't been `this.exiration` minutes
      const cached = JSON.parse(cache_reponse);
      if (new Date(cached.timestamp) > new Date()) {
        return cached.value;
      }
    }

    // ... else get and cache it
    const value = await fn();
    this.set(group, key, value);
    return value;
  }

  search(key: string) {
    return localStorage.getItem(key);
  }

  clear(groupname: string) {
    const str = this.search(groupname);
    if (str) {
      const keys = JSON.parse(str);
      keys.forEach((key: string) => {
        localStorage.removeItem(key);
      });
    }
  }

  set<T>(group: string, key: string, value: T) {
    const timestamp = new Date();
    timestamp.setMinutes(timestamp.getMinutes() + this.expiration);
    localStorage.setItem(
      key,
      JSON.stringify({
        value,
        timestamp,
      })
    );
    {
      let str = this.search(group);
      if (!str) str = '[]';
      let group_obj = JSON.parse(str);
      group_obj = [...group_obj, key];
      localStorage.setItem(group, JSON.stringify(group_obj));
    }
  }
}

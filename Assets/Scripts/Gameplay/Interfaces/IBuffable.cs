using System.Collections.Generic;
using TriggerSystem;

public interface IBuffable
{
    void AddBuff(Buff buff);
    void RemoveBuff(Buff buff);
    List<Buff> GetBuffs();
}

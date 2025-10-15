using System;

namespace Entities
{

    public interface IExtraSkill
    {
        public void SleepStanding();
    }

    public interface IMakeJSON
    {
        public string[] MakeAsJSONFormat();
    }
}

using SmartTech.Marketing.Core.Auth.User;

namespace SmartTech.Marketing.Core.Auth.Contract
{
    public interface ICurrentUserService
    {
        void Load();
        ActiveContext activeContext { get; set; }



    }
}

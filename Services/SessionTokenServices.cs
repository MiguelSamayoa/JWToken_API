using API_BasicStore.Models;

namespace API_BasicStore.Services
{
    public interface ISessionTokenServices
    {
        public Task<SessionToken> Savetoken(SessionToken Token);
    }

    public class SessionTokenServices : ISessionTokenServices
    {
        private readonly DBContext DbContext;

        public SessionTokenServices(DBContext dBContext)
        {
            this.DbContext = dBContext;
        }

        public async Task<SessionToken> Savetoken(SessionToken Token)
        {
            if (Token == null) throw new ArgumentNullException("token");

            await DbContext.AddAsync(Token);

            await DbContext.SaveChangesAsync();

            return Token;
        }
    }
}

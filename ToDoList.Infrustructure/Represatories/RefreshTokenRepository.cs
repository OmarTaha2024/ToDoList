using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Entities.Identity;
using ToDoList.Infrustructure.Abstract;
using ToDoList.Infrustructure.Context;
using ToDoList.Infrustructure.InfrustructureBases;

namespace ToDoList.Infrustructure.Represatories
{
    public class RefreshTokenRepository : GenericRepository<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Fields
        protected readonly DbSet<UserRefreshToken> _userrefreshToken;

        #endregion
        #region Ctor

        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _userrefreshToken = dbContext.Set<UserRefreshToken>();

        }
        #endregion
    }
}

using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Dto;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ReCapProjectContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new ReCapProjectContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }

        public List<UserDetailDto> GetUserByEmail(string email)
        {
            using (var context = new ReCapProjectContext())
            {
                var result = from user in context.Users
                             where user.Email == email
                             select new UserDetailDto
                             {
                                 Id = user.Id,
                                 Email = user.Email,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName
                             };
                return result.ToList();
            }
        }
    }
}

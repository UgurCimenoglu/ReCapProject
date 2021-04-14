using Business.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Dto;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User entity)
        {
            _userDal.Add(entity);
            return new SuccessResult("Kullanici Eklendi.");
        }

        public IResult Delete(User entity)
        {
            _userDal.Delete(entity);
            return new SuccessResult("Kullanici Silindi.");
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), "Kullanicilar Listelendi.");
        }

        public IDataResult<List<UserDetailDto>> GetUserByEmail(string email)
        {
            var result = _userDal.GetUserByEmail(email);
            return new SuccessDataResult<List<UserDetailDto>>(result);
        }

        public User GetByMail(string email)
        {
            var result = _userDal.Get(user => user.Email == email);
            return result;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);

        }

        public IResult Update(User entity)
        {
            var user = _userDal.Get(u => u.Id == entity.Id);
            user.Email = entity.Email;
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            _userDal.Update(user);
            return new SuccessResult("Kullanici Güncellendi.");
        }
    }
}

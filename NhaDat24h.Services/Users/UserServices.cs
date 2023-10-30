﻿using Microsoft.Extensions.Options;
using NhaDat24h.Data.Base;
using NhaDat24h.DataAccess.Entities;
using NhaDat24h.DataAccess.Interface;
using NhaDat24h.DataDto.Users;
using NhaDat24h.Repository.Repositories;

namespace NhaDat24h.Services.Users
{
    public class UserServices : IUserServices
    {
        private readonly ICommonRepository<TbUser> _userRepository;
        private readonly ICommonUoW _commonUoW;
        private readonly AppSettings _appSettings;

        public UserServices(ICommonRepository<TbUser> userRepository, ICommonUoW commonUoW, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _commonUoW = commonUoW;
            _appSettings = appSettings.Value;
        }

        public ResponseBase InserUser(UserInsertDataDto param)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                param.IdU = System.Guid.NewGuid().ToString();
                param.IdTt = 5;
                param.Fullname = "Vũ Minh Đức";
                param.Phone = "0964531148";
                param.Email = "duahau2209@";
                param.Mobile = "123456789";
                param.Password = "111111";
                param.Address = "dongky";
                param.Doituong = 0;
                param.Weburl = "111";
                param.NickYahoo = "222";
                param.Ip = "333";
                param.NickSkype = "444";
                param.GioiTinh = 0;
                param.Avatar = "555";
                param.Kh = true;
                param.Dateupdateavatar = DateTime.Now;
                var data = UserRepository.InsertUser(param);
                response.Code = ErrorCodeMessage.Success.Key;
                response.Message = ErrorCodeMessage.Success.Value;
                response.Data = data;
                return response;
            }
            catch (Exception e)
            {
                response.Code = ErrorCodeMessage.Exception.Key;
                response.Message = e.Message;
                return response;
            }
        }
        //public ResponseBase GetAllUser()
        //{
        //    ResponseBase response = new ResponseBase();
        //    try
        //    {
        //        var users = _userRepository.FindAll(x => x.UserType != 0);
        //        response.Code = ErrorCodeMessage.Success.Key;
        //        response.Message = ErrorCodeMessage.Success.Value;
        //        response.Data = users.ToList();
        //        return response;
        //    }
        //    catch (Exception e)
        //    {
        //        response.Code = ErrorCodeMessage.Exception.Key;
        //        response.Message = e.Message;
        //        return response;
        //    }
        //}
        //public ResponseBase SetAuthor(int UserId, int UserType)
        //{
        //    ResponseBase response = new ResponseBase();
        //    try
        //    {
        //        _commonUoW.BeginTransaction();
        //        var user = _userRepository.FindAll(x => x.UserId == UserId).SingleOrDefault();
        //        if (user == null)
        //        {
        //            response.Code = ErrorCodeMessage.Exception.Key;
        //            response.Message = "User not exist";
        //            return response;
        //        }
        //        user.UserType = UserType;
        //        _userRepository.Update(user);
        //        _commonUoW.Commit();

        //        response.Code = ErrorCodeMessage.Success.Key;
        //        response.Message = ErrorCodeMessage.Success.Value;
        //        response.Data = user;
        //        return response;
        //    }
        //    catch (Exception e)
        //    {
        //        response.Code = ErrorCodeMessage.Exception.Key;
        //        response.Message = e.Message;
        //        return response;
        //    }
        //}
        //public ResponseBase CheckUserByPhone(string phoneNumber)
        //{
        //    ResponseBase response = new ResponseBase();
        //    var user = _userRepository.FindAll(x => x.MobileNo == phoneNumber).FirstOrDefault();
        //    response.Data = user;
        //    return response;

        //}
        //public ResponseBase CheckUserByEmail(string email)
        //{
        //    ResponseBase response = new ResponseBase();

        //    var user = _userRepository.FindAll(x => x.Email == email).FirstOrDefault();
        //    response.Data = user;

        //    return response;
        //}

        //public ResponseBase CheckUserByID(int ID)
        //{
        //    ResponseBase response = new ResponseBase();
        //    var user = _userRepository.FindAll(x => x.UserId == ID).FirstOrDefault();
        //    response.Data = user;
        //    return response;
        //}


        //public ResponseBase RegisterUserPhone(User user)
        //{
        //    ResponseBase response = new ResponseBase();
        //    try
        //    {
        //        var userExited = _userRepository.FindAll(x => x.MobileNo == user.MobileNo).FirstOrDefault();
        //        _commonUoW.BeginTransaction();
        //        if (userExited == null)
        //        {
        //            _userRepository.Insert(user);
        //        }
        //        _commonUoW.Commit();
        //        var data = _userRepository.FindAll(x => x.MobileNo == user.MobileNo).FirstOrDefault();
        //        response.Data = data;
        //        return response;

        //    }
        //    catch (Exception ex)
        //    {
        //        _commonUoW.RollBack();
        //        return response;
        //    }
        //}

        //public ResponseBase UpdateInfo(User user)
        //{
        //    ResponseBase response = new ResponseBase();

        //    try
        //    {
        //        _commonUoW.BeginTransaction();
        //        if (user != null)
        //        {
        //            _userRepository.Update(user);

        //        }
        //        _commonUoW.Commit();

        //    }
        //    catch (Exception ex)
        //    {
        //        _commonUoW.RollBack();
        //    }
        //    response.Data = user;
        //    return response;
        //}
        //public ResponseBase RegisterUserEmail(User user)
        //{
        //    ResponseBase response = new ResponseBase();

        //    try
        //    {
        //        var userExited = _userRepository.FindAll(x => x.Email == user.Email).FirstOrDefault();
        //        _commonUoW.BeginTransaction();
        //        if (userExited == null)
        //        {
        //            _userRepository.Insert(user);
        //        }
        //        else
        //        {
        //            _userRepository.Update(user);
        //        }
        //        _commonUoW.Commit();
        //        var data = _userRepository.FindAll(x => x.Email == user.Email).FirstOrDefault();
        //        response.Data = data;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _commonUoW.RollBack();
        //        return response;
        //    }
        //}

        //public ResponseBase UserLogin(string email, string? password, Common.Enums.UserType enumType)
        //{
        //    ResponseBase response = new ResponseBase();
        //    User result = default;
        //    if (enumType == Common.Enums.UserType.User)
        //    {
        //        result = _userRepository.FindSingle(x => x.Email == email && x.IsActive == 1 && x.Password == password);
        //    }
        //    else
        //    {
        //        result = _userRepository.FindSingle(x => x.Email == email && x.IsActive == 1
        //                                            && x.Password == password && x.UserType == (int)enumType);
        //    }
        //    response.Data = result;
        //    return response;
        //}

        //public ResponseBase CheckUserByAccount(string email, string password)
        //{
        //    ResponseBase response = new ResponseBase();

        //    var user = _userRepository.FindAll(x => x.Email == email && x.Password == password).FirstOrDefault();
        //    response.Data = user;

        //    return response;
        //}
        //public ResponseBase CheckUserByAccountAndGetToken(string email, string password)
        //{
        //    ResponseBase response = new ResponseBase();

        //    var user = _userRepository.FindAll(x => x.Email == email && x.Password == password).FirstOrDefault();
        //    var data = new AuthenticateResponse()
        //    {
        //        User = user,
        //        Token = GenerateJwtToken(user),
        //        Status = "active"
        //    };
        //    if (user == null)
        //        data.Status = "not found";

        //    response.Data = data;

        //    return response;
        //}
        //public string GenerateJwtToken(User user)
        //{
        //    // generate token that is valid for 7 days
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim("id", user.UserId.ToString()),
        //            new Claim("Name", user.UserName)
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        //public User GetById(int Id)
        //{
        //    try
        //    {
        //        var user = _userRepository.GetById(Id);
        //        return user;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using userapi.Controllers;
using Microsoft.EntityFrameworkCore;
using userapi.Models;
using Xunit;
using Xunit.Priority;
using Microsoft.AspNetCore.Mvc;

namespace userapi_tests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class UsersControllerTest
    {
        UsersContext _context;

        public UsersControllerTest()
        {
            var options = new DbContextOptionsBuilder<UsersContext>().UseInMemoryDatabase("UsersControllerTest").Options;
            _context = new UsersContext(options);
        }

        [Fact, Priority(10)]
        public void PostUserSuccess()
        {
            UsersController _usersController = new UsersController(_context);

            Users _user = new Users
            {
                UserId = 1,
                UserName = "test_name",
                Email = "test_email",
                Alias = "test_alias",
                FirstName = "test_firstname",
                LastName = "test_lastname"
            };

            var res = _usersController.PostUsers(_user);

            Assert.IsType<CreatedAtActionResult>(res.Result);
            CreatedAtActionResult caRes = (CreatedAtActionResult)res.Result;
            Assert.Equal(_user, caRes.Value);
        }

        [Fact, Priority(20)]
        public void PostUserInvalid()
        {
            UsersController _usersController = new UsersController(_context);

            Users _user = new Users
            {
                UserId = 1
            };

            var res = _usersController.PostUsers(_user);
            //res = _usersController.PostUsers(_user);

            Assert.IsType<BadRequestObjectResult>(res.Result);
        }

        [Fact, Priority(30)]
        public void GetUserSuccess()
        {
            UsersController _usersController = new UsersController(_context);

            var res = _usersController.GetUsers();

            Assert.Equal<Users>(_context.Users, res);
        }

        [Fact, Priority(40)]
        public void GetUserByIdSuccess()
        {
            UsersController _usersController = new UsersController(_context);

            var res = _usersController.GetUsers(1);

            Assert.IsType<OkObjectResult>(res.Result);
        }

        [Fact, Priority(50)]
        public void GetUserByIdBadRequest()
        {
            UsersController _usersController = new UsersController(_context);

            _usersController.ModelState.AddModelError("test_model_error", "err");
            var res = _usersController.GetUsers(1);

            Assert.IsType<BadRequestObjectResult>(res.Result);
        }

        [Fact, Priority(60)]
        public void GetUserByIdNotFound()
        {
            UsersController _usersController = new UsersController(_context);

            var res = _usersController.GetUsers(-1);

            Assert.IsType<NotFoundResult>(res.Result);
        }

        [Fact, Priority(70)]
        public void PutUserSuccess()
        {
            UsersController _usersController = new UsersController(_context);

            Users _user = new Users
            {
                UserId = 1,
                UserName = "new_name",
                Email = "new_email",
                Alias = "new_alias",
                FirstName = "new_firstname",
                LastName = "new_lastname"
            };

            var res = _usersController.PutUsers(_user.UserId, _user);

            Assert.IsType<NoContentResult>(res.Result);
            Assert.Equal(_user, _context.Users.Find(_user.UserId));
        }

        [Fact, Priority(80)]
        public void PutUserBadRequest()
        {
            UsersController _usersController = new UsersController(_context);

            Users _user = new Users
            {
                UserId = 1,
                UserName = "new_name",
                Email = "new_email",
                Alias = "new_alias",
                FirstName = "new_firstname",
                LastName = "new_lastname"
            };
            _usersController.ModelState.AddModelError("test_model_error", "err");
            var res = _usersController.PutUsers(_user.UserId, _user);

            Assert.IsType<BadRequestObjectResult>(res.Result);
        }

        [Fact, Priority(90)]
        public void PutUserInputIdDoesntMatchInputUserId()
        {
            UsersController _usersController = new UsersController(_context);

            Users _user = new Users
            {
                UserId = 1,
                UserName = "new_name",
                Email = "new_email",
                Alias = "new_alias",
                FirstName = "new_firstname",
                LastName = "new_lastname"
            };

            var res = _usersController.PutUsers(-1, _user);

            Assert.IsType<BadRequestResult>(res.Result);
        }

        [Fact, Priority(100)]
        public void PutUserInputNotFound()
        {
            UsersController _usersController = new UsersController(_context);

            Users _user = new Users
            {
                UserId = -1,
                UserName = "new_name",
                Email = "new_email",
                Alias = "new_alias",
                FirstName = "new_firstname",
                LastName = "new_lastname"
            };

            var res = _usersController.PutUsers(_user.UserId, _user);

            Assert.IsType<NotFoundResult>(res.Result);
        }
    }
}

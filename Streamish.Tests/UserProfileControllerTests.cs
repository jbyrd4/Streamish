﻿using Streamish.Controllers;
using Streamish.Models;
using Streamish.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Streamish.Tests.Mocks
{
    public class UserProfileControllerTests
    {
        [Fact]
        public void Get_Returns_All_UserProfiles()
        {
            // Arrange 
            var userProfileCount = 20;
            var userProfiles = CreateTestUserProfiles(userProfileCount);

            var repo = new InMemoryUserProfileRepository(userProfiles);
            var controller = new UserProfileController(repo);

            // Act 
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUserProfiles = Assert.IsType<List<UserProfile>>(okResult.Value);

            Assert.Equal(userProfileCount, actualUserProfiles.Count);
            Assert.Equal(userProfiles, actualUserProfiles);
        }

        [Fact]
        public void Get_By_Id_Returns_NotFound_When_Given_Unknown_id()
        {
            // Arrange 
            var userProfiles = new List<UserProfile>(); // no videos

            var repo = new InMemoryUserProfileRepository(userProfiles);
            var controller = new UserProfileController(repo);

            // Act
            var result = controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_By_Id_Returns_Video_With_Given_Id()
        {
            // Arrange
            var testUserProfileId = 99;
            var userProfiles = CreateTestUserProfiles(5);
            userProfiles[0].Id = testUserProfileId; // Make sure we know the Id of one of the videos

            var repo = new InMemoryUserProfileRepository(userProfiles);
            var controller = new UserProfileController(repo);

            // Act
            var result = controller.Get(testUserProfileId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUserProfiles = Assert.IsType<UserProfile>(okResult.Value);

            Assert.Equal(testUserProfileId, actualUserProfiles.Id);
        }

        [Fact]
        public void Post_Method_Adds_A_New_UserProfile()
        {
            // Arrange 
            var userProfileCount = 20;
            var userProfiles = CreateTestUserProfiles(userProfileCount);

            var repo = new InMemoryUserProfileRepository(userProfiles);
            var controller = new UserProfileController(repo);

            // Act
            var newUserProfile = new UserProfile()
            {
                Name = "Name",
                Email = "Email@Email.com",
                ImageUrl = "http://youtube.url?v=1234",
                DateCreated = DateTime.Today,
            };

            controller.Post(newUserProfile);

            // Assert
            Assert.Equal(userProfileCount + 1, repo.InternalData.Count);
        }

        [Fact]
        public void Put_Method_Returns_BadRequest_When_Ids_Do_Not_Match()
        {
            // Arrange
            var testUserProfileId = 99;
            var userProfiles = CreateTestUserProfiles(5);
            userProfiles[0].Id = testUserProfileId; // Make sure we know the Id of one of the videos

            var repo = new InMemoryUserProfileRepository(userProfiles);
            var controller = new UserProfileController(repo);

            var userProfileToUpdate = new UserProfile()
            {
                Id = testUserProfileId,
                Name = "Updated!",
                Email = "Updated@updated.com",
                ImageUrl = "http://some.url",
                DateCreated = DateTime.Today,
            };
            var someOtherUserProfileId = testUserProfileId + 1; // make sure they aren't the same

            // Act
            var result = controller.Put(someOtherUserProfileId, userProfileToUpdate);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Put_Method_Updates_A_UserProfile()
        {
            // Arrange
            var testUserProfileId = 99;
            var userProfiles = CreateTestUserProfiles(5);
            userProfiles[0].Id = testUserProfileId; // Make sure we know the Id of one of the videos

            var repo = new InMemoryUserProfileRepository(userProfiles);
            var controller = new UserProfileController(repo);

            var userProfileToUpdate = new UserProfile()
            {
                Id = testUserProfileId,
                Name = "Updated!",
                Email = "Updated@updated.com",
                ImageUrl = "http://some.url",
                DateCreated = DateTime.Today,
            };

            // Act
            controller.Put(testUserProfileId, userProfileToUpdate);

            // Assert
            var userProfileFromDb = repo.InternalData.FirstOrDefault(p => p.Id == testUserProfileId);
            Assert.NotNull(userProfileFromDb);

            Assert.Equal(userProfileToUpdate.Name, userProfileFromDb.Name);
            Assert.Equal(userProfileToUpdate.Email, userProfileFromDb.Email);
            Assert.Equal(userProfileToUpdate.ImageUrl, userProfileFromDb.ImageUrl);
            Assert.Equal(userProfileToUpdate.DateCreated, userProfileFromDb.DateCreated);
        }

        [Fact]
        public void Delete_Method_Removes_A_UserProfile()
        {
            // Arrange
            var testUserProfileId = 99;
            var userProfiles = CreateTestUserProfiles(5);
            userProfiles[0].Id = testUserProfileId; // Make sure we know the Id of one of the videos

            var repo = new InMemoryUserProfileRepository(userProfiles);
            var controller = new UserProfileController(repo);

            // Act
            controller.Delete(testUserProfileId);

            // Assert
            var userProfileFromDb = repo.InternalData.FirstOrDefault(p => p.Id == testUserProfileId);
            Assert.Null(userProfileFromDb);
        }

        private List<UserProfile> CreateTestUserProfiles(int count)
        {
            var userProfiles = new List<UserProfile>();
            for (var i = 1; i <= count; i++)
            {
                userProfiles.Add(new UserProfile()
                {
                    Id = i,
                    Name = $"User {i}",
                    Email = $"user{i}@example.com",
                    DateCreated = DateTime.Today.AddDays(-i),
                    ImageUrl = $"http://user.url/{i}",
                });
            }
            return userProfiles;
        }

        private UserProfile CreateTestUserProfile(int id)
        {
            return new UserProfile()
            {
                Id = id,
                Name = $"User {id}",
                Email = $"user{id}@example.com",
                DateCreated = DateTime.Today.AddDays(-id),
                ImageUrl = $"http://user.url/{id}",
            };
        }
    }
}

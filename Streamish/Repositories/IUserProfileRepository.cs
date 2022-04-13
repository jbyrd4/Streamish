﻿using Streamish.Models;
using System.Collections.Generic;

namespace Streamish.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAll();
        UserProfile GetById(int id);
        UserProfile GetVideosByUserProfile(int id);
        void Add(UserProfile userProfile);
        void Update(UserProfile userProfile);
        void Delete(int id);
    }
}
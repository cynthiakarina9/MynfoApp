namespace Mynfo.Helpers
{
    using System;
    using Domain;
    using Models;

    public static class Converter
    {
        public static UserLocal ToUserLocal(User user)
        {
            return new UserLocal
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImagePath = user.ImagePath,
                LastName = user.LastName,
                UserId = user.UserId,
                UserTypeId = user.UserTypeId,
            };
        }

        public static User ToUserDomain(UserLocal user, byte[] imageArray)
        {
            return new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImagePath = user.ImagePath,
                LastName = user.LastName,
                UserId = user.UserId,
                UserTypeId = user.UserTypeId,
                ImageArray = imageArray,
            };
        }

        public static ProfileEmail ToProfileLocal(ProfileLocal profile)
        {
            return new ProfileEmail
            {
                Name = profile.ProfileName,
                Email = profile.value,
            };
        }
    }
}
